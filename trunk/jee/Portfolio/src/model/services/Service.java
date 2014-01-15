package model.services;

import javax.ejb.Stateful;
import javax.persistence.EntityManager;
import javax.persistence.Persistence;

/**
 * 
 * Class DAO which implements InterfaceDAO
 * @author ABALINE & AHOUNOU
 * 26 déc. 2013
 */
@Stateful
public class Service implements IService {
	
	private static final String JPA_UNIT_NAME = "PORTFOLIO-EJB";
	
	private EntityManager entityManager;
	
	public Service(){
		entityManager = Persistence.createEntityManagerFactory(JPA_UNIT_NAME).createEntityManager();
	}

	@Override
	public Object insert(Object entity) {
		entityManager.getTransaction().begin();
		entityManager.persist(entity);
		entityManager.getTransaction().commit();
		return entity;
	}

	@Override
	public void delete(Object entity) {
		entityManager.getTransaction().begin();
		entity = entityManager.merge(entity);
		entityManager.remove(entity);
		entityManager.getTransaction().commit();
	}

	@Override
	public Object update(Object entity) {
		entityManager.getTransaction().begin();
		entity = entityManager.merge(entity);
		entityManager.getTransaction().commit();
		return entity;
	}

	@Override
	public EntityManager getEntityManager(){
		return entityManager;
	}
	
	
	
}
