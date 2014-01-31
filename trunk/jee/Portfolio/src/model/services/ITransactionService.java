package model.services;

import java.util.List;

import model.jpa.Transaction;
import model.jpa.User;

/**
 * Class ITransactionService, helps to manage transaction
 * @author ABALINE & AHOUNOU
 * 31 janv. 2014
 */
public interface ITransactionService extends IService{
	
	/**
	 * Return list of transaction where user appear
	 * @param user
	 * @return
	 */
	public List<Transaction> getTransactionOfUser(User user);
}
