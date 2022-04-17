#include "c_ping.h"

#define PACKET_SIZE (sizeof(struct iphdr) + sizeof(struct icmphdr))

void c_ping_to(struct c_ping_in in) {
    int sockfd;
    if ((sockfd = socket(AF_INET, SOCK_RAW, IPPROTO_ICMP)) < 0) {
        return perror("Socket creation"), (void)1;
    }

    int optval = 1;
    if (setsockopt(sockfd, IPPROTO_IP, IP_HDRINCL, &optval, sizeof(int)) < 0) {
        return perror("Cannot set socket option"), (void)1;
    }

    char* packet = xmalloc(PACKET_SIZE);
    fill_iphdr(_iphdr(packet));
    _iphdr(packet)->daddr = in.ip;
    _iphdr(packet)->saddr = in.hostip;
    _iphdr(packet)->tot_len = PACKET_SIZE;
    _iphdr(packet)->protocol = IPPROTO_ICMP;
    _set_ipcheck(packet);

    uint8_t received = 0;
    fill_icmphdr(_icmphdr(packet));
    _icmphdr(packet)->type = 0x8;
    _icmphdr(packet)->code = 0x0;
    _icmphdr(packet)->un.echo.id = random();

    struct sockaddr_in dest_addr_in = {
        .sin_family = AF_INET,
        .sin_addr.s_addr = _iphdr(packet)->daddr
    };

    struct sockaddr* dest_addr = 
        ((struct sockaddr*)&dest_addr_in);

    for (int attempt = 1; attempt <= in.attempts; attempt++) {
        _icmphdr(packet)->checksum = 0;
        _icmphdr(packet)->un.echo.sequence = attempt;
        _set_icmpcheck(packet);
        sleep(1);
        clock_t set_at = clock();
        if (sendto(sockfd, packet, PACKET_SIZE, MSG_DONTWAIT,
                dest_addr, sizeof dest_addr_in) < 0) {
            //return perror("Cannot send icmp request"), (void)1;
        }
        CREATE_BUF(recvbuf, PACKET_SIZE*2);
        if (recv(sockfd, recvbuf, sizeof recvbuf, 0) < 0) {
            //return perror("Cannot receive icmp response"), (void)1;
        }
        clock_t stop_at = clock();
        if (_icmphdr(recvbuf)->type == 0) {
            received++;
        }
        c_ping_print(_iphdr(recvbuf), _icmphdr(recvbuf),
            attempt, (double)(stop_at - set_at));
    }
    c_ping_final_print(in.attempts, received);
    free(packet), close(sockfd);
}

void c_ping_print(struct iphdr* iphdr, struct icmphdr* icmphdr,
        int attempt, double elapsed) {
    uint8_t type = icmphdr->type;
    uint8_t code = icmphdr->code;

    if (type == 0) {
        char* ipaddrbuf = inet_ntoa(
            *((struct in_addr*)&iphdr->saddr));
        printf("%d bytes from %s", 
            ntohs(iphdr->tot_len)-(iphdr->ihl*4), ipaddrbuf);
    } else {
        printf("%s (type=%d code=%d)", icmp_type_tostr(type), type, code); 
    }
    printf(" icmp_seq=%d ttl=%d time=%fs\n", attempt,
        iphdr->ttl, elapsed / CLOCKS_PER_SEC);
}

void c_ping_final_print(int sent, int received) {
    printf("\nsent=%d, received=%d (packet loss=%d%%)\n", 
        sent, received, (int)(((float)(sent-received))/sent*100));
}