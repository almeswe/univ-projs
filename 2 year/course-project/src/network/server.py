import time
import json
import socket

from typing import List
from typing import Dict
from typing import Tuple
from datetime import datetime, timedelta
import threading

def sender() -> None:
    sockfd: socket.socket = socket.socket(
        socket.AF_INET, socket.SOCK_DGRAM)
    while True:
        sockfd.sendto(input('enter a message: ').encode(), ('localhost', 5555))

def main() -> None:
    s = PongServer(('localhost', 5555))
    #t = threading.Thread(target=sender)
    #t.start()
    s.start()

class PongPendingPlayer(object):
    def __init__(self, addr: Tuple[str, int]) -> None:
        self.addr: Tuple[str, int] = addr
        self.pended: datetime = datetime.now()

    def expired(self) -> bool:
        return self.pended+timedelta(seconds=15) <= datetime.now()

class PongServer(object):
    def __init__(self, host: Tuple[str, int]) -> None:
        self.host: Tuple[str, int] = host
        self.__init_socket()
        self.__init_user_queue()
        self.__init_check_thread()

    def __init_user_queue(self) -> None:
        self.pending_players: List[PongPendingPlayer] = []

    def __init_socket(self) -> None:
        self.sockfd: socket.socket = socket.socket(
            socket.AF_INET, socket.SOCK_DGRAM)
        self.sockfd.bind(self.host)
        print(f'Server running on: {self.host}')

    def __init_check_thread(self) -> None:
        self.check_thread: threading.Thread = threading.Thread(
            target=self.__check_for_expired)

    def __check_for_expired(self) -> None:
        while True:
            for pending_player in self.pending_players:
                if pending_player.expired():
                    self.pending_players.remove(pending_player)
                    print(f'Pop from pending queue, too long pending. ({pending_player.addr})')
                    self.sockfd.sendto(json.dumps(self.make_response(
                        None, 0, 0, 'Too long pending.')).encode(), pending_player.addr)
            time.sleep(0.5)

    def make_response(self, addr: Tuple[str, int], orientation: int, 
            status: int = 1, message: str = "") -> Dict[str, str]:
        return {
            "pong_server_method": "FIND",
            "pong_server_response": {
                "ipv4": addr[0] if status == 1 else "",
                "port": str(addr[1]) if status == 1 else "",
                "orientation": str(orientation) if status == 1 else ""
            },
            "pong_server_response_status": str(status),
            "pong_server_response_status_message": message,
        }

    def start(self) -> None:
        self.check_thread.start()
        while True:
            data: Tuple[bytes, Tuple[str, int]] = \
                self.sockfd.recvfrom(64)
            if data[0].decode() == 'FIND':
                if len(self.pending_players) > 0:
                    pending_player: PongPendingPlayer = self.pending_players.pop()
                    print(f'Pop from pending queue for exchange... ({pending_player.addr})')
                    self.sockfd.sendto(json.dumps(self.make_response(
                        pending_player.addr, 0)).encode(), data[1])
                    self.sockfd.sendto(json.dumps(self.make_response(
                        data[1], 1)).encode(), pending_player.addr)
                    print(f'{data[1]} <-> {pending_player.addr}')
                else:
                    self.pending_players.append(PongPendingPlayer(data[1]))
                    print(f'Received FIND method, appending to pending queue... ({data[1]})')
main()