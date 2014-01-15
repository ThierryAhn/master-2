package model.services;

import model.jpa.Company;

/**
 * Class CompanyService extends Service and implements ICompanyService
 * @author ABALINE & AHOUNOU
 * 27 déc. 2013
 */
public class CompanyService extends Service implements ICompanyService {

	@Override
	public Company getCompany(String name) {
		return getEntityManager().
				createQuery("SELECT c FROM Company c WHERE c.name = :name", 
						Company.class).
					setParameter("name", name).getSingleResult();
	}

}
