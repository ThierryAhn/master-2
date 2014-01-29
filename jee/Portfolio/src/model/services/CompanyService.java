package model.services;

import java.util.List;

import model.jpa.Company;
import model.jpa.Exchange;

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

	@Override
	public List<Company> getAllCompany() {
		return getEntityManager().
				createQuery("SELECT c FROM Company c", Company.class).
					getResultList();
	}

	@Override
	public List<Company> getAllCompanyByExchange(Exchange exchange, int offset,
			int noOfRecords) {
		return getEntityManager().
				createQuery("SELECT c FROM Company c WHERE c.exchange = :exchange", Company.class).
						setMaxResults(noOfRecords).setFirstResult(offset).
					setParameter("exchange", exchange).getResultList();
	}

	@Override
	public int count(Exchange exchange) {
		
		return getEntityManager().createQuery("SELECT c.companyId FROM Company c WHERE "
						+ "c.exchange = :exchange", Company.class).setParameter("exchange", exchange).
							getResultList().size();
	}

	@Override
	public Company getCompanyBySymbol(String symbol) {
		return getEntityManager().
				createQuery("SELECT c FROM Company c WHERE c.symbol = :symbol", 
						Company.class).
					setParameter("symbol", symbol).getSingleResult();
	}

}
