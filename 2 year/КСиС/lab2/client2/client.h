#ifndef CLIENT2_H
#define CLIENT2_H

#include <stdlib.h>
#include <unistd.h>
#include <arpa/inet.h>
#include <sys/socket.h>
#include <netinet/in.h>

#include <error.h>
#include <errno.h>
#include <stdbool.h>
#include <string.h>
#include <stdio.h>
#include <pthread.h>
#include "../protocol.h"

#define client_error(msg, ...) \
    printf(msg, __VA_ARGS__)

#define client_panic(msg, code, ...) \
    client_error(msg, __VA_ARGS__), exit(code)

#define create_buf(buf, size) \
    char buf[size]; memset(buf, 0, size)

typedef struct {
    int sockfd;
    const char* name;
    socklen_t dest_size;
    struct sockaddr_in dest;
} client_data;

void client_start(client_data data);
client_data client_init(const char* ip, uint16_t port, const char* name);

#endif