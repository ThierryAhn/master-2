/**
 * 
 */
package model.jpa;


import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.OneToOne;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

/**
 * Class Transaction
 * @author ABALINE & AHOUNOU
 * 23 déc. 2013
 */
@Entity
public class Transaction {
	@Id
	@GeneratedValue
	private int transactionId;
	/**
	 * Transaction type(BUY/SELL)
	 */
	@Enumerated(EnumType.STRING)
	private TransactionType type;
	/**
	 * The date of the transaction
	 */
	@Column(name = "DATE_FIELD")
	@Temporal(TemporalType.DATE)
	private Date date;
	/**
	 * Action cost
	 */
	private double price;
	/**
	 * Number of action buyed or selled
	 */
	private int number;
	/**
	 * Fee on sell or buy (0.5 of the value)
	 */
	private double fee;
	
	/**
	 * Action concerned by the transaction
	 */
	@OneToOne(fetch = FetchType.LAZY)
	private Action action;
	/**
	 * Client concerned by the transaction
	 */
	@OneToOne(fetch = FetchType.LAZY)
	private User user;
	
	/**
	 * Default constructor
	 */
	public Transaction(){
	}

	/**
	 * Constructor with parameters
	 * @param type
	 * @param date
	 * @param price
	 * @param number
	 * @param fee
	 * @param action
	 * @param client
	 */
	public Transaction(TransactionType type, Date date, double price,
			int number, double fee, Action action, User client) {
		this.type = type;
		this.date = date;
		this.price = price;
		this.number = number;
		this.fee = fee;
		this.action = action;
		this.user = client;
	}
	
	/**
	 * @return the transactionId
	 */
	public int getTransactionId() {
		return transactionId;
	}

	/**
	 * @param transactionId the transactionId to set
	 */
	public void setTransactionId(int transactionId) {
		this.transactionId = transactionId;
	}

	/**
	 * @return the type
	 */
	public TransactionType getType() {
		return type;
	}

	/**
	 * @param type the type to set
	 */
	public void setType(TransactionType type) {
		this.type = type;
	}

	/**
	 * @return the date
	 */
	public Date getDate() {
		return date;
	}

	/**
	 * @param date the date to set
	 */
	public void setDate(Date date) {
		this.date = date;
	}

	/**
	 * @return the price
	 */
	public double getPrice() {
		return price;
	}

	/**
	 * @param price the price to set
	 */
	public void setPrice(double price) {
		this.price = price;
	}

	/**
	 * @return the number
	 */
	public int getNumber() {
		return number;
	}

	/**
	 * @param number the number to set
	 */
	public void setNumber(int number) {
		this.number = number;
	}
	
	/**
	 * @return the fee
	 */
	public double getFee() {
		return fee;
	}

	/**
	 * @param fee the fee to set
	 */
	public void setFee(double fee) {
		this.fee = fee;
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

	/* (non-Javadoc)
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString() {
		return "Transaction [transactionId=" + transactionId + ", type=" + type
				+ ", date=" + date + ", price=" + price + ", number=" + number
				+ ", fee=" + fee + ", action=" + action + ", user=" + user
				+ "]";
	}
	
	
	
}
