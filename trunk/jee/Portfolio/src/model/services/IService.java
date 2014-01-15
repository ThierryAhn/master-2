package model.services;

import javax.ejb.Remote;
import javax.persistence.EntityManager;

/**
 * Interface InterfaceDAO, helps to persist data in the database
 * @author ABALINE & AHOUNOU
 * 26 déc. 2013
 */
@Remote
public interface IService {
	
	/**
	 * Insert entity
	 * @param entity
	 * @return the object inserted
	 */
	public Object insert(Object entity);
	/**
	 * Remove entity
	 * @param entity
	 */
	public void delete(Object entity);
	/**
	 * Update entity
	 * @param entity
	 * @return the object updated
	 */
	public Object update(Object entity);
	/**
	 * Return entity manager variable
	 * @return
	 */
	public EntityManager getEntityManager();
}
