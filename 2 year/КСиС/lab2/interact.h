#ifndef INTERACT_H
#define INTERACT_H

#include <error.h>
#include <errno.h>
#include <stdio.h>
#include <stdint.h>
#include <stdlib.h>
#include <string.h>
#include <stdbool.h>

#define DEFAULT_INPUT_SIZE 128

#define create_buf(buf, size) \
    char buf[size]; memset(buf, 0, size)

uint16_t ask_for_port();
uint32_t ask_for_ip4();

#endif