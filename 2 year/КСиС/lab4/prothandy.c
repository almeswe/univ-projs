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

uint16_t calculate_checksum(void* hdr, uint32_t hdrsize) {
    /*
        Calculates checksum for header.
    */

    uint32_t nleft;
    uint32_t sum = 0;
    uint16_t answer = 0;
    uint16_t* word = hdr;

    for (nleft = hdrsize; nleft > 1; nleft -= 2) {
        sum += *word++;
    }
    if (nleft == 1) {
        *(u_char*)(&answer) = *(u_char*)word;
        sum += answer;
    }
    sum = (sum >> 16) + (sum & 0xffff);
    sum += (sum >> 16);
    return ~sum;
}

uint32_t get_hostip() {
    /*
        Retrieves the host ip address.
        NOTE: returns ip in network byte order.
    */

    CREATE_BUF(buf, 256);
    struct hostent* host;
    gethostname(buf, 256);
    host = gethostbyname(buf);
    return *(uint32_t*)(struct in_addr *)host->h_addr;
}

const char* icmp_type_tostr(uint8_t type) {
    /*
        Converts (maps) the ICMP type field with
        string equivalent.
    */

    return (type < 0 || type > 255) ? NULL :
        icmp_type_strs[type]; 
}

void _make_default_iphdr(struct iphdr* header) {
    /*
        Sets default ip header options.
    */

    memset(header, 0, sizeof(struct iphdr));
    header->id = 0;
    header->tos = 0;
    header->ihl = 5;
    header->ttl = 64;
    header->version = 4;
    header->frag_off = 0;
}

void _make_default_icmphdr(struct icmphdr* header) {
    /*
        Sets default icmp header options.
    */

    memset(header, 0, sizeof(struct icmphdr));
    header->type = ICMP_ECHO;
    header->code = 0x0;
}