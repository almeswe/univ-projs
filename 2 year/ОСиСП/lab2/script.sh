#! /bin/bash

count=0

[ $# -ne 3 ] && echo ">$0: Incorrect argument count passed!" && exit 1

for item in $(find $(realpath $1) -type f 2> /dev/null); do
    size=$(stat -c %s $item 2> errlog)

    if [ $? -eq 0 ] && [ $size -lt $3 ] && [ $size -gt $2 ]; then
        echo "$item : $size"
        count=$((count+1))
        [ $count -eq 20 ] && break
    fi

    # error printing
    while read error; do
        echo "> $0: $error" 
    done < errlog
done

exit 0