package model.services;

import java.util.List;

import model.jpa.Transaction;
import model.jpa.User;

/**
 * Class TransactionService
 * @author ABALINE & AHOUNOU
 * 31 janv. 2014
 */
public class TransactionService extends Service implements ITransactionService{

	@Override
	public List<Transaction> getTransactionOfUser(User user) {
		return getEntityManager().
				createQuery("SELECT t FROM Transaction t WHERE t.user = :user", Transaction.class).
					setParameter("user", user).getResultList();
	}
	
	
}
