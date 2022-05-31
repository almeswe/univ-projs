import socket

def main() -> None:
    connection: socket.socket = socket.socket(
        socket.AF_INET, socket.SOCK_STREAM)
    connection.connect(('localhost', 5555))
    connection.send(input('Enter message: ').encode())
    input()

main()