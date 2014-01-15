/**
 * 
 */
package model.jpa;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;

/**
 * Class Exchange
 * @author ABALINE & AHOUNOU
 * 23 déc. 2013
 */
@Entity
/* @NamedQueries({
    @NamedQuery(name = "Exchange.findAll", query = "SELECT e FROM Exchange e"),
    @NamedQuery(name = "Exchange.findByName", query = "SELECT e FROM Exchange e "
    		+ "WHERE e.name = :name")
    })*/
public class Exchange {
	@Id
	@GeneratedValue
	private int exchangeId;
	/**
	 * Name of the exchange
	 */
	@Column(unique=true, nullable=false)
	private String name;
	
	/**
	 * Default constructor
	 */
	public Exchange() {
	}
	
	/**
	 * Constructor with name
	 * @param name
	 */
	public Exchange(String name) {
		this.name = name;
	}

	/**
	 * @return the name
	 */
	public String getName() {
		return name;
	}
	/**
	 * @param name the name to set
	 */
	public void setName(String name) {
		this.name = name;
	}

	/* (non-Javadoc)
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString() {
		return "Exchange [exchangeId=" + exchangeId + ", name=" + name + "]";
	}
	
}
