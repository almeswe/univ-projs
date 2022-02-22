#include "subnet.c"

#include <stdio.h>
#include <stdlib.h>

#include <ifaddrs.h>
#include <arpa/inet.h>
#include <sys/socket.h>

#define def_in_addr(ip) \
	struct in_addr addr = { .s_addr = htonl(ip) }

void main(int argc, char* argv[]) {
	if (argc != 2) {
		printf("argumets:\n   1. net interface\n"), exit(1);
	}
	
	char* interface = argv[1];
    uint32_t mask, hostip;
    uint32_t nodes[MAX_NODES_STORED]; 
    memset(nodes, 0, MAX_NODES_STORED);
    if (get_interface_info(interface, &mask, &hostip)) {
	    if (get_subnet_nodes(mask, hostip, nodes)) {
			printf("\n");
			for (int i = 0; nodes[i]; i++) {
				char mac[MAC_ADDR_LEN];
				def_in_addr(nodes[i]);
				if (htonl(hostip) == nodes[i] ? get_host_mac_address(interface, mac) : get_mac_address(nodes[i], mac)) 
					printf("%d)%s%3s\t: %s\n", i+1, inet_ntoa(addr), "", mac);
			}
		}
	}
}