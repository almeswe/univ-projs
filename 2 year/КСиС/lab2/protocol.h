#ifndef PROTOCOL_H
#define PROTOCOL_H

#include <stdint.h>

#define sz_bytes  4
#define id_bytes  8
#define hdr_bytes sizeof(struct prot_hdr)

#define get_sz(buf)     ((unsigned)ntohl(((int*)buf)[0]))
#define put_sz(buf, sz) (((int*)buf)[0] = htonl((unsigned)(sz)))

#define get_id(buf)     ((char*)((long long*)(buf+sz_bytes)[0]))
#define put_id(buf, id) (((long long*)(buf+sz_bytes))[0] = (long long)id)
 
struct prot_hdr {
    uint32_t sz; 
    char     id[8];
};

#endif