/**
 * 
 */
package model.jpa;

import java.io.Serializable;

import javax.persistence.Basic;
import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.EnumType;
import javax.persistence.Enumerated;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.OneToOne;

/**
 * Class Client
 * @author ABALINE & AHOUNOU
 * 23 déc. 2013
 */
@Entity
public class User implements Serializable{
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	@Id
	@GeneratedValue
	private int userId;
	/**
	 * Client confidence level which determines if it possible to allow a client
	 * 	sell actions which he doesn't own.
	 */
	@Enumerated(EnumType.STRING)
	private ConfidenceLevel confidenceLevel;
	/**
	 * The last name of the client
	 */
	private String lastName;
	/**
	 * The first name of the client
	 */
	@Basic(fetch = FetchType.LAZY, optional = false)
	private String firstName;
	/**
	 * Client address
	 */
	private Address address;
	/**
	 * The login of the client
	 */
	private String login;
	/**
	 * The password of the client
	 */
	private String password;
	/**
	 * The account of the client
	 * By default client don't have account
	 */
	@OneToOne(fetch = FetchType.LAZY, orphanRemoval=true, cascade={CascadeType.PERSIST})
	@JoinColumn(name = "accountId")
	private Account account;
	
	/**
	 * Default Constructor
	 */
	public User(){
	}

	/**
	 * Constructor with parameters
	 * @param lastName
	 * @param firstName
	 * @param address
	 * @param login
	 * @param password
	 */
	public User(String lastName, String firstName, Address address, 
			String login, String password) {
		this.lastName = lastName;
		this.firstName = firstName;
		this.address = address;
		this.login = login;
		this.password = password;
		confidenceLevel = ConfidenceLevel.NORMAL;
	}
	
	/**
	 * @return the clientId
	 */
	public int getUserId() {
		return userId;
	}

	/**
	 * @param clientId the clientId to set
	 */
	public void setUserId(int clientId) {
		this.userId = clientId;
	}

	/**
	 * @return the account
	 */
	public Account getAccount() {
		return account;
	}

	/**
	 * @param account the account to set
	 */
	public void setAccount(Account account) {
		this.account = account;
	}

	/**
	 * @param address the address to set
	 */
	public void setAddress(Address address) {
		this.address = address;
	}

	/**
	 * @return the confidenceLevel
	 */
	public ConfidenceLevel getConfidenceLevel() {
		return confidenceLevel;
	}

	/**
	 * @param confidenceLevel the confidenceLevel to set
	 */
	public void setConfidenceLevel(ConfidenceLevel confidenceLevel) {
		this.confidenceLevel = confidenceLevel;
	}

	/**
	 * @return the lastName
	 */
	public String getLastName() {
		return lastName;
	}

	/**
	 * @param lastName the lastName to set
	 */
	public void setLastName(String lastName) {
		this.lastName = lastName;
	}

	/**
	 * @return the firstName
	 */
	public String getFirstName() {
		return firstName;
	}

	/**
	 * @param firstName the firstName to set
	 */
	public void setFirstName(String firstName) {
		this.firstName = firstName;
	}
	
	/**
	 * @return the address
	 */
	public Address getAddress() {
		return address;
	}

	/**
	 * @param address the address to set
	 */
	public void setFirstName(Address address) {
		this.address = address;
	}

	/**
	 * @return the login
	 */
	public String getLogin() {
		return login;
	}

	/**
	 * @param login the login to set
	 */
	public void setLogin(String login) {
		this.login = login;
	}

	/**
	 * @return the password
	 */
	public String getPassword() {
		return password;
	}

	/**
	 * @param password the password to set
	 */
	public void setPassword(String password) {
		this.password = password;
	}
	
	/**
	 * Add cash to account
	 * @Pre(account != null)
	 * @param amount
	 */
	public void deposit(double amount){
		if(account != null){
			// the new balance of the account
			double balance = account.getAmount() + amount;
			account.setAmount(balance);
		}
	}
	
	/**
	 * Get cash from account
	 * @Pre(account != null)
	 * @param amount
	 */
	public void withdraw(double amount){
		if(account != null){
			// the new balance of the account
			double balance = account.getAmount() - amount;
			account.setAmount(balance);
		}
	}
	
	/**
	 * Open a new account for the client
	 * @Pre(account == null) a client can have just one account
	 * @return true if the account is well opened, false if there is account set 
	 * 	the client
	 */
	public boolean openAccount(){
		if(account == null){
			account = new Account();
			return true;
		}
		return false;
	}
	
	/**
	 * Close the account of the user
	 */
	public void closeAccount(){
		account = null;
	}

	/* (non-Javadoc)
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString() {
		return "Client [clientId=" + userId + ", confidenceLevel="
				+ confidenceLevel + ", lastName=" + lastName + ", firstName="
				+ firstName + ", address=" + address + ", login=" + login
				+ ", password=" + password + ", account=" + account + "]";
	}
	
}
