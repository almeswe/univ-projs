#include "c_ping.h"

int c_ping_to(struct c_ping_in in) {
    /*
        Sending icmp echo requests to destination
        ip address, and processes retrieved replies.
    */

    int sockfd;         // specifying SOCK_RAW to take control of ip header creation
    if ((sockfd = socket(AF_INET, SOCK_RAW, IPPROTO_ICMP)) < 0) {
        return perror("Socket creation"), 1;
    }

    int optval = 1;     // specifying IP_HDRINCL socket option, needed for charging kernel to not help us with building the ip header
                        // we will build everything barebones
    if (setsockopt(sockfd, IPPROTO_IP, IP_HDRINCL, &optval, sizeof(int)) < 0) {
        return perror("Cannot set socket option"), 1;
    }

    char* packet = xmalloc(PACKET_SIZE);                        // allocating memory for packet, which contains ip and icmp headers
    _make_default_iphdr(_iphdr(packet));                        // making default skeleton of ip header
    _iphdr(packet)->daddr = in.ip;
    _iphdr(packet)->saddr = in.hostip;
    _iphdr(packet)->tot_len = PACKET_SIZE;
    _iphdr(packet)->protocol = IPPROTO_ICMP;

    _make_default_icmphdr(_icmphdr(packet));                    // making default skeleton of imcp echo request header
    _icmphdr(packet)->un.echo.id = random();                    // specifying random identifier for each c_ping execution

    int received = 0;                                           // count of received icmp echo replyes
    struct sockaddr_in dest_addr_in = {                         // target c_ping address (needed for sendto and recv functions)
        .sin_family = AF_INET,
        .sin_addr.s_addr = _iphdr(packet)->daddr
    };
    struct sockaddr* dest_addr = 
        ((struct sockaddr*)&dest_addr_in);

    for (int attempt = 1; attempt <= in.attempts; attempt++) {  // iterating through each attempt specified in c_ping_in
        _icmphdr(packet)->checksum = 0;                         // renew the checksum, because we need to calcuate it again each iteration
        _icmphdr(packet)->un.echo.sequence += 1;
        _set_icmpcheck(packet);
        _set_ipcheck(packet);
        clock_t set_at = clock();
        sendto(sockfd, packet, PACKET_SIZE, 0, 
            dest_addr, sizeof dest_addr_in);                    // sending packet to destination. No need for error checking, 
                                                                // because in this case we will receive the errors from this functions (sendto, recv), instead of icmp errors
        CREATE_BUF(recvbuf, PACKET_SIZE*2);
        recv(sockfd, recvbuf, sizeof recvbuf, 0);               // receiveing reply 
        clock_t stop_at = clock();
        received += _icmphdr(recvbuf)->type == 0;               // if type of icmp reply is 0 (means ICMP_ECHO_REPLY), 
                                                                // then it means that we pinged destination successfully
        c_ping_print(_iphdr(recvbuf), _icmphdr(recvbuf),
            attempt, (double)(stop_at - set_at));
    }
    c_ping_final_print(in.attempts, received);
    return FINALIZE(sockfd, packet), 0;
}

void c_ping_print(struct iphdr* iphdr, struct icmphdr* icmphdr,
        int attempt, double elapsed) {
    /*
        Prints main information about current c_ping reply
            (ttl, time, bytes received, icmp_seq, code, type)
    */

    uint8_t type = icmphdr->type;
    uint8_t code = icmphdr->code;

    if (type == ICMP_ECHOREPLY) {
        char* ipaddrbuf = inet_ntoa(
            *((struct in_addr*)&iphdr->saddr));
        printf("%d bytes from %s", 
            ntohs(iphdr->tot_len)-(iphdr->ihl*4), ipaddrbuf);
    } 
    else {
        printf("%s (type=%d code=%d)", 
            icmp_type_tostr(type), type, code); 
    }
    printf(" icmp_seq=%d ttl=%d time=%fs\n", attempt,
        iphdr->ttl, elapsed / CLOCKS_PER_SEC);
}

void c_ping_final_print(int sent, int received) {
    /*
        Prints final information about all c_ping requests/replies,
        and makes the packet loss percent. 
    */
   
    printf("\nsent=%d, received=%d (packet loss=%d%%)\n", 
        sent, received, (int)(((float)(sent-received))/sent*100));
}