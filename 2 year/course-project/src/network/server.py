import socket

def main() -> None:
    connection: socket.socket = socket.socket(
        socket.AF_INET, socket.SOCK_STREAM)
    connection.bind(('localhost', 5555)) 
    connection.listen(1)
    a, b = connection.accept()
    data = a.recv(1024)
    print(f'{data.decode()}')
    input()

main()