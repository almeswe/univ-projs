package com.poit.entity.criteria;

public final class SearchCriteria {
    public static enum Laptop{
        OS, BATTERY_CAPACITY, RAM_MEMORY_CAPACITY
    }

    public static enum Fridge{
        POWER_CONSUMPTION, HEIGHT, WIDTH
    }

    public static enum Kettle{
        POWER_CONSUMPTION, VOLUME
    }

    private SearchCriteria() {}
}
