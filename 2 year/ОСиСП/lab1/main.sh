#!/bin/bash

cc=gcc

if $cc $1 ; then 
    ./a.out $2 $3 $4 $5 $6 $7
else
    echo "compilation failed"
fi