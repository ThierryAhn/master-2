package model.services;

import model.jpa.User;

/**
 * Class IClientService helps to find client
 * @author ABALINE & AHOUNOU
 * 28 déc. 2013
 */
public interface IUserService extends IService{
	
	/**
	 * Get a User identified by his login
	 * @param login of the user
	 * @return
	 */
	public User getUser(String login);
	
	/**
	 * Get a User identified by his login and passwords
	 * @param login of the user
	 * @param password of the user
	 * @return
	 */
	public User getUser(String login, String password);
	
	/**
	 * Get a User identified by his id
	 * @param userId
	 * @return
	 */
	public User getUser(int userId);

}
