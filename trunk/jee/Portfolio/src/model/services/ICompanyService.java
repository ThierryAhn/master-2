package model.services;

import model.jpa.Company;

/**
 * Class ICompanyService helps to find company
 * @author ABALINE & AHOUNOU
 * 27 déc. 2013
 */
public interface ICompanyService extends IService{
	
	/**
	 * Find a Company identified by his name
	 * @param name of the company
	 * @return
	 */
	public Company getCompany(String name);
}
