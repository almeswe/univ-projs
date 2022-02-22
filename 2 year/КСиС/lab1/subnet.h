#include <errno.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <stdbool.h>

#include <ifaddrs.h>
#include <arpa/inet.h>
#include <sys/socket.h>

#include "ping.c"
#include "common.h"

bool get_interface_info(char* interface, 
    __Out__ uint32_t* mask, __Out__ uint32_t* hostip);

bool get_subnet_nodes(uint32_t mask, uint32_t hostip,
    __Out__ uint32_t* nodes);

bool get_mac_address(uint32_t ip,
    __Out__ char* mac);

bool get_host_mac_address(const char* interface,
    __Out__ char* mac);