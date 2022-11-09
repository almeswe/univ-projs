package com.poit.entity;

public class Laptop extends Appliance {
    private String operatingSystem;
    private double batteryCapacity;
    private int ramMemoryCapacity;

    public Laptop(double price, String manufacturer, String color, String operatingSystem, double batteryCapacity, int ramMemoryCapacity) {
        super(price, manufacturer, color);
        this.operatingSystem = operatingSystem;
        this.batteryCapacity = batteryCapacity;
        this.ramMemoryCapacity = ramMemoryCapacity;
    }

    public Laptop() {
    }

    public String getOperatingSystem() {
        return operatingSystem;
    }

    public void setOperatingSystem(String operatingSystem) {
        this.operatingSystem = operatingSystem;
    }

    public double getBatteryCapacity() {
        return batteryCapacity;
    }

    public void setBatteryCapacity(double batteryCapacity) {
        this.batteryCapacity = batteryCapacity;
    }

    public int getRamMemoryCapacity() {
        return ramMemoryCapacity;
    }

    public void setRamMemoryCapacity(int ramMemoryCapacity) {
        this.ramMemoryCapacity = ramMemoryCapacity;
    }

    @Override
    public String toString() {
        return "Laptop{" +
                "operatingSystem='" + operatingSystem + '\'' +
                ", batteryCapacity=" + batteryCapacity +
                ", ramMemoryCapacity=" + ramMemoryCapacity +
                ", price=" + price +
                ", manufacturer='" + manufacturer + '\'' +
                ", color='" + color + '\'' +
                '}';
    }
}
