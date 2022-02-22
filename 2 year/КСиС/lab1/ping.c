#include "ping.h"

#define ICMP_ATTEMPTS 2

#define PAYLOAD_SIZE 512
#define PACKET_SIZE  PAYLOAD_SIZE + sizeof(struct icmphdr)

bool ping_to(uint32_t ip) {
    int desc;
    if ((desc = socket(AF_INET, SOCK_DGRAM, IPPROTO_ICMP)) < 0) {
        return printf("Cannot create socket! Message: %s\n",
            strerror(errno)), false;
    }

    struct timeval timeout = {
        .tv_sec  = 0,
        .tv_usec = 10000
    };

    setsockopt(desc, SOL_SOCKET, SO_SNDTIMEO, 
        (void *)&timeout, sizeof timeout);
    setsockopt(desc, SOL_SOCKET, SO_RCVTIMEO, 
        (void *)&timeout, sizeof timeout);

    int cont = 1;
    setsockopt(desc, SOL_SOCKET, SO_REUSEADDR,
        &cont, sizeof(int));

    struct sockaddr_in destaddr = {
        .sin_addr = htonl(ip),
        .sin_family = AF_INET
    };

    char packet[PACKET_SIZE];

    for (int attempt = 1; attempt <= ICMP_ATTEMPTS; attempt++) {
        struct icmphdr* header = (struct icmphdr*)packet;
        header->un.echo.id = 0x1234;
        header->type = ICMP_ECHO, header->code = 0;
        header->un.echo.sequence = attempt;

        memset(packet + sizeof(struct icmphdr), 0, PAYLOAD_SIZE);
        
        if (sendto(desc, packet, PACKET_SIZE, 0, 
                (struct sockaddr*)&destaddr, sizeof destaddr) >= 0) {

            char buffer[PACKET_SIZE];
            if (recv(desc, buffer, PACKET_SIZE, 0) >= 0) {
                memcpy(header, buffer, sizeof header);
                if (header->code == ICMP_ECHOREPLY) 
                    return close(desc), true;
            }
        }
    }
    return close(desc), false;
}