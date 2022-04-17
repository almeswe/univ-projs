#include "prothandy.h"

static const char* icmp_type_strs[256] = {
    [0] = "Echo Reply",
    [3] = "Destination Unreachable",
    [4] = "Source Quench",
    [5] = "Redirect Message",
    [8] = "Echo Request",
    [9] = "Router Advertisement",
    [10] = "Router Solicitation",
    [11] = "Time Exceeded[6]",
    [12] = "Parameter Problem: Bad IP header",
    [13] = "Timestamp",
    [14] = "Timestamp Reply",
    [15] = "Information Request",
    [16] = "Information Reply",
    [17] = "Address Mask Request",
    [18] = "Address Mask Reply",
    [30] = "Traceroute",
    [42] = "Extended Echo Request",
    [43] = "Extended Echo Reply",
};

uint16_t calculate_checksum(void* addr, size_t size) {
    uint32_t sum = 0;
    uint16_t answer = 0;
    uint16_t* w = addr;
    uint32_t nleft;

    for(nleft = size; nleft > 1; nleft -= 2) {
        sum += *w++;
    }
    if(nleft == 1) {
        *(u_char*) (&answer) = *(u_char*) w;
        sum += answer;
    }
    sum = (sum >> 16) + (sum & 0xffff);
    sum += (sum >> 16);
    return ~sum;
}

char* get_host_address() {
    /*struct ifaddrs* temp;
    struct ifaddrs* interfaces;

    if (getifaddrs(&interfaces) != 0) {
        return perror("Cannot get host ip address.\n"), exit(1);
    }

    for (temp = interfaces; interfaces; interfaces = interfaces->ifa_next){
        if (interfaces->ifa_netmask) {
            ;
        }
    }*/
}

const char* icmp_type_tostr(uint8_t type) {
    return (type < 0 || type > 255) ? NULL :
        icmp_type_strs[type]; 
}

void fill_iphdr(struct iphdr* header) {
    memset(header, 0, sizeof(struct iphdr));
    header->id = 0;
    header->tos = 0;
    header->ihl = 5;
    header->ttl = 64;
    header->version = 4;
    header->frag_off = 0;
}

void fill_icmphdr(struct icmphdr* header) {
    memset(header, 0, sizeof(struct icmphdr));
    header->type = 0;
    header->code = ICMP_ECHO;
}