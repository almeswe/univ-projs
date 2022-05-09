#include "c_traceroute.h"

int c_traceroute_for(struct c_traceroute_in in) {
    /*
        Performs tracing path to specified ip address, and prints the path.
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

    char* packet = xmalloc(PACKET_SIZE);                                        // allocating memory for packet, which contains ip and icmp headers
    _make_default_iphdr(_iphdr(packet));                                        // making default skeleton of ip header
    _iphdr(packet)->daddr = in.ip;
    _iphdr(packet)->saddr = in.hostip;
    _iphdr(packet)->tot_len = PACKET_SIZE;
    _iphdr(packet)->protocol = IPPROTO_ICMP;

    _make_default_icmphdr(_icmphdr(packet));                                    // making default skeleton of imcp echo request header
    _icmphdr(packet)->un.echo.id = random();                                    // specifying random identifier for each c_ping execution

    struct sockaddr_in dest_addr_in = {                                         // target c_ping address (needed for sendto and recv functions)
        .sin_family = AF_INET,
        .sin_addr.s_addr = _iphdr(packet)->daddr
    };
    struct sockaddr* dest_addr = 
        ((struct sockaddr*)&dest_addr_in);
    c_traceroute_welcome_print(in.ip, MAX_TTL);

    for (uint8_t ttl = 1; ttl <= (uint8_t)(MAX_TTL); ttl++) {                   // iterating until max ttl bound is faced
        printf("%02d) ", ttl);      
        _iphdr(packet)->ttl = ttl;                                              // updating current ttl in ip header
        uint8_t code = 0, type = 0;                                             
        CREATE_BUF(recvbuf, PACKET_SIZE*2);
        clock_t set_at = clock();
        for (uint8_t attempt = 1; attempt <= MAX_VERIFY_ATTEMPTS; attempt++) {  // send 3 icmp requests, and use last result as control result
            _icmphdr(packet)->checksum = 0;                                     // updating checksum in icmp header
            _icmphdr(packet)->un.echo.sequence += 1;                            
            _set_icmpcheck(packet);
            _set_ipcheck(packet);
            sendto(sockfd, packet, PACKET_SIZE, 0, 
                dest_addr, sizeof dest_addr_in);
            recv(sockfd, recvbuf, sizeof recvbuf, 0);
            clock_t stop_at = clock();
            type = _icmphdr(recvbuf)->type;
            code = _icmphdr(recvbuf)->code;
            c_traceroute_hop_print((double)(stop_at-set_at));
        }
        c_traceroute_print(_iphdr(recvbuf), _icmphdr(recvbuf));
        if (type != ICMP_TIME_EXCEEDED) {                                       // in case of error or echo reply, we need to exit, 
                                                                                // because potentially there are no route then
            if (type == ICMP_ECHOREPLY) {                                       // if we received reply, it means that we reached destination
                return c_traceroute_final_print(
                    _iphdr(recvbuf), ttl), 0;                                                     
            }
            break;
        }
    }
    printf("\nTrace finished.\n");
    return FINALIZE(sockfd, packet), 1;
}

void c_traceroute_print(struct iphdr* iphdr, struct icmphdr* icmphdr) {
    /*
        Prints the information about current hop between source and
        ttl exceeded point.
    */

    switch (icmphdr->type) {
        case ICMP_ECHOREPLY:
        case ICMP_TIME_EXCEEDED:
            printf("ip=%s\n", inet_ntoa((*((struct in_addr*)&iphdr->saddr))));
            break;
        default:
            printf("err=%s\n", icmp_type_tostr(icmphdr->type));
            break;
    }
}

void c_traceroute_hop_print(double elapsed) {
    /*
        Prints information about current icmp request attempt (delay).
    */

    printf("%.5fs, ", elapsed / CLOCKS_PER_SEC);
}

void c_traceroute_welcome_print(uint32_t ipaddr, uint8_t max_ttl) {
    /*
        Prints start information about tracing destination, and max ttl.
    */

    printf("Trace for %s, max ttl=%d\n\n", inet_ntoa(*(struct in_addr*)&ipaddr), max_ttl);
}

void c_traceroute_final_print(struct iphdr* lastiphdr, int hops) {
    /*
        Prints final informatin about traced path if succeded.
    */

    printf("\nTraced from=%s ", inet_ntoa(*(struct in_addr*)&lastiphdr->daddr));
    printf("to=%s, hops=%d.\n", inet_ntoa(*(struct in_addr*)&lastiphdr->saddr), hops);
}