package com.poit.entity;

public class Kettle extends Appliance {
    private double powerConsumption;
    private double volume;

    public Kettle(double price, String manufacturer, String color, double powerConsumption, double volume) {
        super(price, manufacturer, color);
        this.powerConsumption = powerConsumption;
        this.volume = volume;
    }

    public double getPowerConsumption() {
        return powerConsumption;
    }

    public Kettle() {
    }

    public void setPowerConsumption(double powerConsumption) {
        this.powerConsumption = powerConsumption;
    }

    public double getVolume() {
        return volume;
    }

    public void setVolume(double volume) {
        this.volume = volume;
    }

    @Override
    public String toString() {
        return "Kettle{" +
                "powerConsumption=" + powerConsumption +
                ", volume=" + volume +
                ", price=" + price +
                ", manufacturer='" + manufacturer + '\'' +
                ", color='" + color + '\'' +
                '}';
    }
}
