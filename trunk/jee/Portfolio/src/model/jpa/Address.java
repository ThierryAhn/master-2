/**
 * 
 */
package model.jpa;

import javax.persistence.Embeddable;

/**
 * Class Adresse
 * @author ABALINE & AHOUNOU
 * 24 déc. 2013
 */
@Embeddable
public class Address {
	/**
	 * Street number
	 */
	private int number;
	/**
	 * Street name
	 */
	private String street;
	/**
	 * City
	 */
	private String city;
	/**
	 * Country
	 */
	private String country;
	
	/**
	 * Default constructor
	 */
	Address(){
	}

	/**
	 * Constructor with parameters
	 * @param number
	 * @param street
	 * @param city
	 * @param country
	 */
	public Address(int number, String street, String city, String country) {
		this.number = number;
		this.street = street;
		this.city = city;
		this.country = country;
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
	 * @return the street
	 */
	public String getStreet() {
		return street;
	}

	/**
	 * @param street the street to set
	 */
	public void setStreet(String street) {
		this.street = street;
	}

	/**
	 * @return the city
	 */
	public String getCity() {
		return city;
	}

	/**
	 * @param city the city to set
	 */
	public void setCity(String city) {
		this.city = city;
	}

	/**
	 * @return the country
	 */
	public String getCountry() {
		return country;
	}

	/**
	 * @param country the country to set
	 */
	public void setCountry(String country) {
		this.country = country;
	}
	
}
