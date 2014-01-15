package model.services;

import model.jpa.User;

/**
 * Class UserService extends Service and implements IUserService
 * @author ABALINE & AHOUNOU
 * 28 déc. 2013
 */
public class UserService extends Service implements IUserService{

	@Override
	public User getUser(String login) {
		return getEntityManager().
				createQuery("SELECT u FROM User u WHERE u.login = :login", 
						User.class).setParameter("login", login).
							getSingleResult();
	}

	@Override
	public User getUser(int userId) {
		return getEntityManager().
				createQuery("SELECT u FROM User u WHERE u.userId = :userId", 
						User.class).setParameter("userId", userId).
						getSingleResult();
	}

}
