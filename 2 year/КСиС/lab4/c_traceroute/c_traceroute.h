#ifndef _CUSTOM_TRACEROUTE_H
#define _CUSTOM_TRACEROUTE_H

#include <time.h>
#include <errno.h>
#include <string.h>
#include <unistd.h>

#include <netdb.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <sys/socket.h>

#include "../xmemory.h"
#include "../prothandy.h"
#include "../printerr.h"

#define MAX_TTL             30
#define MAX_VERIFY_ATTEMPTS 3

struct c_traceroute_in {
    uint32_t ip;            // destination ip address for tracing
    uint32_t hostip;        // host ip address from which tracing is performed
};

int c_traceroute_for(struct c_traceroute_in in);
void c_traceroute_print(struct iphdr* iphdr, struct icmphdr* icmphdr);
void c_traceroute_hop_print(double elapsed);
void c_traceroute_welcome_print(uint32_t ipaddr, uint8_t max_ttl);
void c_traceroute_final_print(struct iphdr* lastiphdr, int hops);

#endif