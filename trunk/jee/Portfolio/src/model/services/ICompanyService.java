package model.services;

import java.util.List;

import model.jpa.Company;
import model.jpa.Exchange;

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
	
	/**
	 * Return all company
	 * @return
	 */
	public List<Company> getAllCompany();
	
	/**
	 * Return a limited number of company identified by an exchange
	 * @param exchange
	 * @param offset
	 * @param noOfRecords
	 * @return
	 */
	public List<Company> getAllCompanyByExchange(Exchange exchange, int offset,
			int noOfRecords);
	
	/**
	 * Find a Company identified by his symbol
	 * @param symbol
	 * @return
	 */
	public Company getCompanyBySymbol(String symbol);
	
	/**
	 * Return number of company identified by an exchange
	 * @param exchange
	 * @return
	 */
	public int count(Exchange exchange);
}
