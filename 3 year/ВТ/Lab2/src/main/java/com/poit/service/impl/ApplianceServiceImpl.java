package com.poit.service.impl;

import com.poit.dao.ApplianceDAO;
import com.poit.dao.impl.ApplianceDAOImpl;
import com.poit.entity.Appliance;
import com.poit.entity.criteria.Criteria;
import com.poit.service.ApplianceService;

import java.util.List;

public class ApplianceServiceImpl implements ApplianceService {
    private final ApplianceDAO applianceDAO = new ApplianceDAOImpl();
    @Override
    public List<Appliance> find(Criteria criteria) {
        return applianceDAO.find(criteria);
    }
}
