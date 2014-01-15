package model.jpa;

import javax.persistence.Embeddable;
import javax.persistence.JoinColumn;

@Embeddable
public class TransactionPK {
	
	@JoinColumn(name = "actionId")
	private Action action;
	
	@JoinColumn(name = "userId")
	private User user;
	
	/**
	 * Default constructor
	 */
	public TransactionPK(){
	}

	/**
	 * Constructor
	 * @param action
	 * @param user
	 */
	public TransactionPK(Action action, User user) {
		this.action = action;
		this.user = user;
	}

	/**
	 * @return the action
	 */
	public Action getAction() {
		return action;
	}

	/**
	 * @param action the action to set
	 */
	public void setAction(Action action) {
		this.action = action;
	}

	/**
	 * @return the user
	 */
	public User getUser() {
		return user;
	}

	/**
	 * @param user the user to set
	 */
	public void setUser(User user) {
		this.user = user;
	}
	
}
