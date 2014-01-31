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
	public List<Action> getActionsByCompany(Company company);
	/**
	 * Get all distinct action (the first one with the last price )
	 * @return
	 */
	public List<Action> getDistinctActions();
	
	/**
	 * Return action identified by id
	 * @param actionId
	 * @return
	 */
	public Action getActionById(int actionId);
}
