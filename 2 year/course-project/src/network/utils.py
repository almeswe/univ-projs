from typing import List
from typing import Callable

def sock_syscall(syscall: Callable, syscall_args: List[object],
        handler: Callable = None, args: List[object] = []) -> object:
    try:
        return syscall(*syscall_args)
    except Exception as e:
        print(f'Error occured in sock_syscall: {e}')
        if handler != None:
            handler(*args)
    return None