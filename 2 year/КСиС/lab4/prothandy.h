#ifndef _CHECKSUMS_H
#define _CHECKSUMS_H

#include <stdio.h>
#include <stdlib.h>
#include <netdb.h>
#include <stdint.h>
#include <string.h>
#include <unistd.h>
#include <ifaddrs.h>
#include <sys/types.h>
#include <netinet/ip_icmp.h>

#define _iphdr(packet)   ((struct iphdr*)(packet))
#define _icmphdr(packet) ((struct icmphdr*)(packet + sizeof(struct iphdr)))

#define _set_ipcheck(packet)   (_iphdr(packet)->check = calculate_checksum(_iphdr(packet), sizeof (struct iphdr)))
#define _set_icmpcheck(packet) (_icmphdr(packet)->checksum = calculate_checksum(_icmphdr(packet), sizeof (struct icmphdr)))

#define CREATE_BUF(buf, size) \
    char buf[size]; memset(buf, 0, sizeof buf)

char* get_host_address();
const char* icmp_type_tostr(uint8_t type);
uint16_t calculate_checksum(void* addr, size_t size);

void fill_iphdr(struct iphdr* header);
void fill_icmphdr(struct icmphdr* header);

#endif
