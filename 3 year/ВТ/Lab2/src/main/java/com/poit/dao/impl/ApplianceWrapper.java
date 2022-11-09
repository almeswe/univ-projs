package com.poit.dao.impl;

import com.poit.entity.Appliance;

import java.util.ArrayList;

public class ApplianceWrapper {
    private final ArrayList<Appliance> appliances = new ArrayList<>();

    public ApplianceWrapper() {
    }

    public ArrayList<Appliance> getAppliances() {
        return appliances;
    }

    public void add(Appliance appliance) {
        appliances.add(appliance);
    }

}
