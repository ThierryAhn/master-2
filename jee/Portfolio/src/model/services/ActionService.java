package model.services;

import java.util.List;

import model.jpa.Action;
import model.jpa.Company;

/**
 * Class ActionService
 * @author ABALINE & AHOUNOU
 * 29 janv. 2014
 */
public class ActionService extends Service implements IActionService{

	@Override
	public List<Action> getActionsByCompany(Company company) {
		return getEntityManager().
				createQuery("SELECT a FROM Action a WHERE a.company = :company", Action.class).
					setParameter("company", company).getResultList();
	}

	@Override
	public List<Action> getDistinctActions() {
		return getEntityManager().
				createQuery("SELECT a FROM Action a GROUP BY a.company", 
						Action.class).getResultList();
	}

	@Override
	public Action getActionById(int actionId) {
		return getEntityManager().
				createQuery("SELECT a FROM Action a WHERE a.actionId = :actionId", 
						Action.class).
					setParameter("actionId", actionId).getSingleResult();
	}

}
