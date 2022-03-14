#ifndef SERVER2_H
#define SERVER2_H

#include <unistd.h>
#include <arpa/inet.h>
#include <sys/socket.h>
#include <netinet/in.h>

#include <error.h>
#include <errno.h>
#include <stdbool.h>
#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>

#include "../client2/client.h"

#define serv_error(msg, ...) \
    printf(msg, __VA_ARGS__)

#define serv_panic(msg, code, ...) \
    serv_error(msg, __VA_ARGS__), exit(code)

#define create_buf(buf, size) \
    char buf[size]; memset(buf, 0, size)

#define connected_count \
    (sizeof connected / sizeof(serv_connection_data))

enum {
    REQUEST_BUF_SIZE  = 512,
    RESPONSE_BUF_SIZE = 512
};

typedef struct {
    int sockfd;
    struct sockaddr_in addr;
} serv_data;

typedef struct {
    int csockfd;
    serv_data data;
    struct sockaddr_in connected_addr;
} serv_connection_data;

void serv_start(serv_data data);
void serv_serve(serv_connection_data con_data);

serv_data serv_init(const char* ip, uint16_t port);

#endif