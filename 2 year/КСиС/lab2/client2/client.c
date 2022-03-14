#include "client.h"
 
void init_transfer(char** message, size_t* message_size) {
    *message_size = 4096;
    *message = (char*)calloc(1,
        (*message_size)*sizeof(char));
    memset(*message, 0, *message_size);
    fgets((*message)+hdr_bytes, *(message_size)-hdr_bytes, stdin);
}

void try_init_file_transfer(char** message, size_t* message_size) {
    FILE* fd = NULL;
    (*message)[hdr_bytes+strcspn(
        (*message)+hdr_bytes, "\n")] = 0;

    if (fd = fopen((*message)+hdr_bytes, "r+")) {
        fseek(fd, 0L, SEEK_END);
        *message_size = ftell(fd)+hdr_bytes;
        rewind(fd), free(*message);
        *message = (char*)calloc(1, *message_size);
        fread((*message)+hdr_bytes, sizeof(char), 
            (*message_size)-hdr_bytes, fd);
        fclose(fd);
    }
}

void client_disconnect(client_data data) {
    close(data.sockfd);
}

void* client_recv_handler(void* args) {
    int recvpack_size = -1;
    client_data data = *(client_data*)args;

    while (true) {
        struct prot_hdr header;
        size_t response_ptr = 0;
        size_t response_size = 512;
        size_t response_pack_size = 0;
        create_buf(response, RECV_CHUNK_SIZE);

        if ((response_pack_size = recv(data.sockfd, 
                response, sizeof response, 0)) <= 0) {
            if (response_pack_size < 0) {
                client_error("Cannot receive contents: %s", strerror(errno));
            }
            return client_disconnect(data), NULL;
        }

        header = ((struct prot_hdr*)(response))[0];
        response_size = ntohl(header.sz);
        response_ptr = response_pack_size;
        if (response_size) {
            printf("[%s] %s", header.id, 
                response+hdr_bytes), fflush(stdout);
        }

        while (response_ptr < response_size) {
            memset(response, 0, sizeof response);
            if ((response_pack_size = recv(data.sockfd, 
                    response, sizeof response, 0)) <= 0) {
                if (response_pack_size < 0) {
                    client_error("Cannot receive contents: %s", strerror(errno));
                }
                return client_disconnect(data), NULL;
            }
            response_ptr += response_pack_size;
            printf("%s", response), fflush(stdout);
        }
        if (response_size) {
            printf("\n");
        }
        fflush(stdout);
    }
    return NULL;
} 

void client_start(client_data data) {
    if (connect(data.sockfd, (struct sockaddr*)&data.dest, data.dest_size) < 0) {
        client_panic("Cannot connect to the other side: %s\n", 1, strerror(errno));
    }

    pthread_t thread;
    client_data* arg = (client_data*)
        malloc(sizeof(client_data)); *(arg) = data;
    pthread_create(&thread, NULL, 
        client_recv_handler, (void*)arg);

    while (true) {
        char* message = NULL;
        size_t message_ptr = 0;
        size_t message_size = 0;
        struct prot_hdr header; 
        init_transfer(&message, &message_size);
        try_init_file_transfer(&message, &message_size);

        sprintf(header.id, "%s", data.name);
        header.sz = htonl(strlen(message+hdr_bytes)+hdr_bytes);
        ((struct prot_hdr*)message)[0] = header;

        while (message_ptr < message_size) {
            if ((message_ptr += send(data.sockfd, message + 
                    message_ptr, SEND_CHUNK_SIZE, 0)) < 0) {
                client_error("Cannot send contents: %s\n", strerror(errno));
                break;
            }
        }
        free(message);
    }
    pthread_join(thread, NULL);
}

client_data client_init(const char* ip, uint16_t port, const char* name) {
    int sockfd;
    if ((sockfd = socket(AF_INET, SOCK_STREAM, 0)) < 0) {
        client_panic("Cannot create socket: %s", 1, strerror(errno));
    }

    struct sockaddr_in dest_addr = {
        .sin_family = AF_INET,
        .sin_port = htons(port),
        .sin_addr = inet_addr(ip),
    };
    
    client_data data = {
        .name = name,
        .sockfd = sockfd,
        .dest = dest_addr,
        .dest_size = sizeof dest_addr
    };
    return data;
}