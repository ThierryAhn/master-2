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
	public List<Action> getActionsByCompnay(Company company) {
		return getEntityManager().
				createQuery("SELECT a FROM Action a WHERE a.company = :company", 
						Action.class).
					setParameter("company", company).getResultList();
	}

}
