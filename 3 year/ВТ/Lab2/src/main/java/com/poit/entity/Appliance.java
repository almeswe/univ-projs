package com.poit.entity;

import com.fasterxml.jackson.annotation.JsonTypeInfo;

@JsonTypeInfo(use = JsonTypeInfo.Id.CLASS, property = "className")
public abstract class Appliance {
    protected double price;
    protected String manufacturer;
    protected String color;

    public Appliance(double price, String manufacturer, String color) {
        this.price = price;
        this.manufacturer = manufacturer;
        this.color = color;
    }

    public Appliance() {
    }

    public double getPrice() {
        return price;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public String getManufacturer() {
        return manufacturer;
    }

    public void setManufacturer(String manufacturer) {
        this.manufacturer = manufacturer;
    }

    public String getColor() {
        return color;
    }

    public void setColor(String color) {
        this.color = color;
    }
}
