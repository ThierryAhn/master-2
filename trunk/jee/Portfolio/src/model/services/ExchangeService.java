package model.services;

import javax.ejb.Stateful;

import model.jpa.Exchange;

/**
 * Class ExchangeService extends Service and implements IExchangeService
 * @author ABALINE & AHOUNOU
 * 27 déc. 2013
 */
@Stateful
public class ExchangeService extends Service implements IExchangeService{
	
	@Override
	public Exchange getExchange(String name){
		
		// name query doesn't work
		/* return getEntityManager().
				createQuery("Exchange.findByName", Exchange.class).
					setParameter("name", name).getSingleResult();*/
		
		return getEntityManager().
				createQuery("SELECT e FROM Exchange e WHERE e.name = :name", 
						Exchange.class).
					setParameter("name", name).getSingleResult();
	}
	
}
