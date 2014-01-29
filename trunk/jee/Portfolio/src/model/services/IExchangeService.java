package model.services;

import java.util.List;

import javax.ejb.Remote;

import model.jpa.Exchange;

/**
 * Interface IExchangeService helps to find exchanges
 * @author ABALINE & AHOUNOU
 * 27 déc. 2013
 */
@Remote
public interface IExchangeService extends IService{
	
	/**
	 * Find an Exchange identified by his name
	 * @param name of the exchange
	 * @return
	 */
	public Exchange getExchange(String name);
	
	/**
	 * Get all exchanges
	 * @return
	 */
	public List<Exchange> getAllExchange();
}
