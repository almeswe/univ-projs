package com.poit.dao;

import com.poit.entity.Appliance;
import com.poit.entity.criteria.Criteria;

import java.util.List;

public interface ApplianceDAO {
    List<Appliance> find(Criteria criteria);
}
