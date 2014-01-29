package model.services;

import java.util.List;

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
		return getEntityManager().
				createQuery("SELECT e FROM Exchange e WHERE e.name = :name", 
						Exchange.class).
					setParameter("name", name).getSingleResult();
	}
	
	@Override
	public List<Exchange> getAllExchange() {
		return getEntityManager().
				createQuery("SELECT e FROM Exchange e", Exchange.class).
					getResultList();
	}
	
}
