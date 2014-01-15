/**
 * 
 */
package model.jpa;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

/**
 * Class Account
 * @author ABALINE & AHOUNOU
 * 23 déc. 2013
 */
@Entity
public class Account {
	@Id
	@GeneratedValue
	private int accountId;
	
	/**
	 * The balance of the account
	 */
	private double amount;
	
	/**
	 * Default constructor
	 */
	public Account(){
	}
	
	/**
	 * Constructor with parameter
	 * @param amount
	 */
	public Account(double amount){
		this.amount = amount;
	}

	/**
	 * @return the amount
	 */
	public double getAmount() {
		return amount;
	}

	/**
	 * @param amount the amount to set
	 */
	public void setAmount(double amount) {
		this.amount = amount;
	}
	
	
}
