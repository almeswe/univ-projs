CMAKE_MINIMUM_REQUIRED(VERSION 3.22)
PROJECT(Lab2)
FILE(GLOB COMMON_FILES dft.c fft.c parser.c wav.c gen.c filter.c)
SET(CMAKE_BUILD_TYPE Debug)
SET(
    ENTRIES 
    "lab2_dec"
    "lab2_filter"
    "lab2_restore"
)
FOREACH(ENTRY IN LISTS ENTRIES)
    STRING(CONCAT ENTRY_SOURCE ${ENTRY} ".c")
    ADD_EXECUTABLE(
        ${ENTRY}
        ${COMMON_FILES}
        ${ENTRY_SOURCE}
    )
    TARGET_LINK_LIBRARIES(
        ${ENTRY}
        m
    )
ENDFOREACH()