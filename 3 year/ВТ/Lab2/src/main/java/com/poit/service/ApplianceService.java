package com.poit.service;

import com.poit.entity.Appliance;
import com.poit.entity.criteria.Criteria;

import java.util.List;

public interface ApplianceService {
    List<Appliance> find(Criteria criteria);
}
