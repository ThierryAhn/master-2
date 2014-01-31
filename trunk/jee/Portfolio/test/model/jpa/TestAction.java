package model.jpa;

import java.sql.Date;

import model.services.CompanyService;
import model.services.ICompanyService;
import model.services.IService;
import model.services.Service;


public class TestAction {

	public static void main(String[] args) {
		ICompanyService daoCompany = new CompanyService();
		
		Company company = daoCompany.getCompany("1-800 FLOWERS.COM, Inc.");
		
		Action action = new Action(new Date(2013, 10, 18), 976.58, 
				1015.46, 974.00, 1011.41, 11566400, 1011.41, company);
		
		IService dao = new Service();
		
		dao.insert(action);
	}

}
