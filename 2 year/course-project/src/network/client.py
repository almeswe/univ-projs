import os
import sys
import json
import socket
from threading import Lock
from threading import Thread

from typing import Dict
from typing import Tuple
from typing import Callable

PONG_CLIENT_BUFSIZE = 1024
PONG_CLIENT_CONFIG_FILE = 'config.json'

class PongServerException(Exception):
    def __init__(self, *args: object) -> None:
        super().__init__(*args)

class PongClientException(Exception):
    def __init__(self, *args: object) -> None:
        super().__init__(*args)

class PongClientTerminalException(Exception):
    def __init__(self, *args: object) -> None:
        super().__init__(*args)

class PongClient(object):
    def __init__(self) -> None:
        self.__init_socket()
        self.__init_config()
        self.__init_threads()
        self.on_recv: Callable = None
        self.is_disconnected: bool = False

    def __init_socket(self) -> None:
        self.sockfd: socket.socket = socket.socket(
            socket.AF_INET, socket.SOCK_DGRAM)

    def __init_config(self) -> None:
        self.player_addr: Tuple[str, int] = None
        self.server_addr: Tuple[str, int] = None
        self.orientation: int = -1
        if not os.path.exists(PONG_CLIENT_CONFIG_FILE):
            raise PongClientTerminalException('Cannot load config.json.')
        with open(PONG_CLIENT_CONFIG_FILE, 'r', encoding='utf-8') as raw:
            data: json = json.load(raw)
            try:
                self.server_addr = (data['server']['ipv4'],
                    int(data['server']['port']))
            except Exception as e:
                raise PongClientTerminalException(f'Cannot load config.json, message: {e}')

    def __init_threads(self) -> None:
        self.__mutex: Lock = Lock()
        self.__polling: bool = False
        self.__receiver: Thread = Thread(target=self.recv)

    def recv(self) -> None:
        while self.__polling:
            try:
                response: Tuple[bytes, Tuple[str, int]] = \
                    self.sockfd.recvfrom(PONG_CLIENT_BUFSIZE)
                if self.on_recv != None:
                    self.on_recv(response[0].decode())
            except socket.error:
                if self.on_recv != None:
                    try:
                        self.verify_peer_node()
                    except PongClientTerminalException as e:
                        self.on_recv(json.dumps(self.make_error_response(f'{e}')))
        sys.exit()

    def send(self, response: Dict[str, str]) -> None:
        try:
            self.sockfd.sendto(json.dumps(response).encode(), self.player_addr)
        except Exception as e:
            print(f'send: {e}')
            self.verify_peer_node()

    def make_error_response(self, message: str) -> Dict[str, str]:
        return {
            "pong_client_game_status": "0",
            "pong_client_game_status_message": message,
            "pong_client_sync_data": {}
        }

    def make_sync_response(self, lplayer: int = None, rplayer: int = None, 
            ball: Tuple[int, int] = None, scores: Tuple[int, int] = None) -> Dict[str, str]:
        response: Dict[str, str] = {
            "pong_client_game_status": "1",
            "pong_client_game_status_message": "",
            "pong_client_sync_data": {}
        }
        if lplayer != None:
            response['pong_client_sync_data']['lplayer'] = str(lplayer)
        if rplayer != None:
            response['pong_client_sync_data']['rplayer'] = str(rplayer)
        if ball != None:
            response['pong_client_sync_data']['ball'] = \
                ','.join([str(ball[0]), str(ball[1])])
        if scores != None:
            response['pong_client_sync_data']['scores'] = \
                ','.join([str(scores[0]), str(scores[1])])
        return response

    def __send_ping_to_peer_node(self) -> None:
        max_attempts: int = 3
        print('Sending PING method to opponent.')
        self.sockfd.sendto('PING'.encode(), self.player_addr)
        for attempt in range(max_attempts):
            print(f'\t[{attempt+1}/{max_attempts}] Waiting for PONG response.')
            try:
                response: Tuple[bytes, Tuple[str, int]] = \
                    self.sockfd.recvfrom(PONG_CLIENT_BUFSIZE)
                if response[1] == self.player_addr:
                    if response[0].decode() == 'PONG':
                        print('PONG method received, peer node exists.')
                        return
            except Exception as e:
                pass
        raise PongClientTerminalException(f'Cannot verify peer node' +
            f' with address. {self.player_addr}')

    def __send_pong_to_peer_node(self) -> None:
        try:
            print('Receiving PING method from opponent...')
            response: Tuple[bytes, Tuple[str, int]] = \
                self.sockfd.recvfrom(PONG_CLIENT_BUFSIZE)
            if response[1] == self.player_addr:
                if response[0].decode() == 'PING':
                    print('PING method received, peer node exists.')
                    print('Sending PONG back.')
                    self.sockfd.sendto('PONG'.encode(), self.player_addr)
                    return
        except Exception as e:
            pass
        raise PongClientTerminalException(f'Cannot verify peer node' +
            f' with address. {self.player_addr}')

    def verify_peer_node(self) -> None:
        try:
            if self.orientation == 0:
                self.__send_ping_to_peer_node()
            if self.orientation == 1:
                self.__send_pong_to_peer_node()
        except socket.error as e:
            raise PongClientTerminalException(f'Cannot verify peer node' +
                f' with address. {self.player_addr}')

    def connect(self) -> None:
        print(f'Searching opponent...')
        print(f'Connecting to server [{self.server_addr[0]}:{self.server_addr[1]}]')
        self.sockfd.sendto('FIND'.encode(), self.server_addr)
        self.sockfd.settimeout(20.0)
        try:
            response: bytes = self.sockfd.recvfrom(PONG_CLIENT_BUFSIZE)[0]
            response_text: Dict[str, str] = json.loads(response.decode())
            if int(response_text['pong_server_response_status']) == 0:
                raise PongServerException(response_text['pong_server_response_status_message'])
            self.player_addr = (response_text['pong_server_response']['ipv4'],
                int(response_text['pong_server_response']['port']))
            self.orientation = int(response_text['pong_server_response']['orientation'])
            print(f'Found opponent: ip={self.player_addr[0]}, port={self.player_addr[1]}')
        except socket.error as e:
            raise PongServerException('Cannot connect to the server, try again later.')

    def set_recv_callback(self, callback: Callable) -> None:
        self.on_recv = callback

    def start_receiving(self) -> None:
        self.sockfd.settimeout(3.0)
        self.__polling = True
        self.__receiver.start()

    def disconnect(self) -> None:
        with self.__mutex:
            if not self.is_disconnected:
                self.is_disconnected = True
                self.__polling = False
                self.sockfd.close()

if __name__ == '__main__':
    print('Try to run main.py')