#ifndef _PROTOCOL_HANDY_FUNCTIONS_H
#define _PROTOCOL_HANDY_FUNCTIONS_H

/*
    Handy functions and macroses for working with
    IP and ICMP protocols.
*/

#include <stdio.h>
#include <stdint.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>

#include <ifaddrs.h>
#include <sys/types.h>
#include <sys/socket.h>

#include <netdb.h>
#include <arpa/inet.h>
#include <netinet/in.h>
#include <netinet/ip_icmp.h>

#define _iphdr(packet)   ((struct iphdr*)(packet))                           // retrieves the ip header from raw packet
#define _icmphdr(packet) ((struct icmphdr*)(packet + sizeof(struct iphdr)))  // retrieves the icmp header from raw packet

#define _set_ipcheck(packet)   (_iphdr(packet)->check = \
    calculate_checksum(_iphdr(packet), sizeof(struct iphdr)))
#define _set_icmpcheck(packet) (_icmphdr(packet)->checksum = \
    calculate_checksum(_icmphdr(packet), sizeof(struct icmphdr)))

#define FINALIZE(socket, packet) \
    close(socket), free(packet)

#define CREATE_BUF(buf, size) \
    char buf[size]; memset(buf, 0, sizeof buf)

#define PACKET_SIZE (sizeof(struct iphdr) + sizeof(struct icmphdr))

uint32_t get_hostip();
uint16_t calculate_checksum(void* hdr, uint32_t size);

void _make_default_iphdr(struct iphdr* header);
void _make_default_icmphdr(struct icmphdr* header);
const char* icmp_type_tostr(uint8_t type);

#endif // _PROTOCOL_HANDY_FUNCTIONS_H
