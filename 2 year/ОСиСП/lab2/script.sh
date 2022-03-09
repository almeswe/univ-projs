#!/bin/bash

count=0

[ $# -ne 3 ] && echo ">$0: Incorrect argument count passed: $# (must be 3)." && exit 1

for item in $(find $1 -follow); do
    size=$(stat -c %s "$item")
    
    if [ $? -ne 0 ]; then
        echo ">$0: $size"
        continue
    fi

    if [ $3 -gt $size ] && [ $2 -lt $size ]; then
        count=$((count+1))
        echo "$count) $item: $size" 
        if [ $count = "20" ]; then 
            break
        fi
    fi
done