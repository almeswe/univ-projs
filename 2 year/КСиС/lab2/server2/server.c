#include "server.h"

static serv_connection_data connected[10];

void add_connection(serv_connection_data connection) {
    for (int i = 0; i < connected_count; i++) {
        if (connected[i].csockfd == 0) {
            connected[i] = connection;
            return;
        }
    }
    printf("Cannot add connection!");
    exit(1);
}

void rem_connection(serv_connection_data connection) {
    for (int i = 0; i < connected_count; i++) {
        if (connected[i].csockfd == connection.csockfd) {
            memset(&(connected[i]), 0, sizeof connected[i]);
        }
    }
}

void serv_connect(serv_connection_data con_data) {
    printf("connected:    [%s:%d]\n", 
        inet_ntoa(con_data.connected_addr.sin_addr),
        ntohs(con_data.connected_addr.sin_port)
    );
    add_connection(con_data);
}

void serv_disconnect(serv_connection_data con_data) {
    printf("disconnected: [%s:%d]\n", inet_ntoa(con_data.connected_addr.sin_addr),
        ntohs(con_data.connected_addr.sin_port));
    rem_connection(con_data);
    close(con_data.csockfd);
}

void serv_broadcast(const char* message, size_t message_size, serv_connection_data con_data) {
    if (!message_size) {
        return;
    }
    
    for (int i = 0; i < connected_count; i++) {
        if (connected[i].csockfd && connected[i].csockfd != con_data.csockfd) {
            size_t sent_ptr = 0;
            while (sent_ptr < message_size) {
                if ((sent_ptr += send(connected[i].csockfd, message + 
                        sent_ptr, SEND_CHUNK_SIZE, 0)) < 0) {
                    rem_connection(connected[i]);
                    break;
                }
                //printf("sent: <%lu/%luB>\n", sent_ptr, message_size);
            }
            //printf("\n");
        }
    }
}

void* serv_serve_handler(void* args) {
    serv_serve(*(serv_connection_data*)args);
    return free(args), NULL;
}

void serv_start(serv_data data) {
    struct sockaddr connected_addr;
    socklen_t connected_addr_size = 
        sizeof connected_addr;

    printf("server runs on: [%s:%d]\naccepting connections.\n", 
        inet_ntoa(data.addr.sin_addr), ntohs(data.addr.sin_port));

    while (true) {
        memset(&connected_addr, 0, 
            sizeof connected_addr);

        int csockfd;
        if ((csockfd = accept(data.sockfd, &connected_addr,
                &connected_addr_size)) < 0) {
            serv_error("Cannot accept connection: %s\n", strerror(errno));
            break;
        }

        serv_connection_data connection_data = {
            .data = data,
            .csockfd = csockfd,
            .connected_addr = *((struct sockaddr_in*)
                (&connected_addr))
        };

        pthread_t con_thread;
        serv_connection_data* args = (serv_connection_data*)
            malloc(sizeof(serv_connection_data)); *args = connection_data;
        pthread_create(&con_thread, NULL, serv_serve_handler, (void*)args);
    }
}

void serv_serve(serv_connection_data con_data) {
    serv_connect(con_data);

    while (true) {
        // dynamic buffer for the response we receive
        char* stretchy = NULL;
        // pointer in stretchy buffer, pointing to last added byte
        size_t response_ptr = 0;
        // default message's buffer size
        size_t response_size = 512;
        // size of last received packet from recv
        size_t response_pack_size = 0;

        // creating temp buffer for data received from recv
        create_buf(response, RECV_CHUNK_SIZE);
        struct prot_hdr header;

        if ((response_pack_size = recv(con_data.csockfd, 
                response, sizeof response, 0)) <= 0) {
            // if the code from recv is negative - error
            // if the code is zero - connection closed
            if (response_pack_size < 0) {
                serv_error("Cannot receive contents: %s", strerror(errno));
            }
            // cast to void type clears the 
            // warning from the compiler about the return type

            // in both cases of an exit code of recv, need to close the connection
            // from the server side 
            return serv_disconnect(con_data), (void)1;
        }

        // retrieving the size of whole response
        header = ((struct prot_hdr*)(response))[0];
        response_size = ntohl(header.sz);
        printf("\nrecv: <%s : %luB>\n", header.id, 
            response_pack_size);

        // set start pointer
        response_ptr = response_pack_size;
        // allocating the needed space
        stretchy = (char*)calloc(1, response_size+sizeof response);
        memcpy(stretchy+hdr_bytes, response+hdr_bytes, 
            response_pack_size-hdr_bytes);
        ((struct prot_hdr*)stretchy)[0] = header;
        
        // retrieving the data until the pointer 
        // is less than the actual contents length
        while (response_ptr < response_size) {
            memset(response, 0, sizeof response);
            // perform the same actions as before
            if ((response_pack_size = recv(con_data.csockfd, 
                    response, sizeof response, 0)) <= 0) {
                if (response_pack_size < 0) {
                    serv_error("Cannot receive contents: %s", strerror(errno));
                }
                return serv_disconnect(con_data), (void)1;
            }
            memcpy(stretchy+response_ptr, response, 
                response_pack_size);
            // shift the pointer by the size of retrieved packet
            response_ptr += response_pack_size;
            printf("recv: <%s : %luB>\n", header.id, 
                response_pack_size);
        }
        // send message to all connected clients
        //printf("%s\n", stretchy+12);
        serv_broadcast(stretchy, response_size + 
            sizeof response, con_data);
        free(stretchy);
    }
}

serv_data serv_init(const char* ip, uint16_t port) {
    int sockfd;
    if ((sockfd = socket(AF_INET, SOCK_STREAM, 0)) < 0) {
        serv_panic("Cannot create socket: %s", 1, strerror(errno));
    }

    int enable = 1;
    setsockopt(sockfd, SOL_SOCKET, SO_REUSEADDR, 
        &enable, sizeof(int));

    struct sockaddr_in addr = {
        .sin_port = htons(port), 
        .sin_addr = inet_addr(ip),
        .sin_family = AF_INET,
    };

    if (bind(sockfd, (struct sockaddr*)&addr, sizeof addr) < 0) {
        serv_panic("Cannot bind socket: %s", 2, strerror(errno));
    }

    if (listen(sockfd, 15) < 0) {
        serv_panic("Cannot listen socket: %s", 3, strerror(errno));
    }

    serv_data data = {
        .addr = addr,
        .sockfd = sockfd
    };

    return memset(connected, 0, 
        sizeof connected), data;
}