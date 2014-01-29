package model.services;

import java.util.List;

import javax.ejb.Remote;

import model.jpa.Action;
import model.jpa.Company;

@Remote
public interface IActionService {
	
	/**
	 * Get list of actions  
	 * @param company
	 * @return
	 */
	public List<Action> getActionsByCompnay(Company company);
}
