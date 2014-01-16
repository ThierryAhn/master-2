/**
 * 
 */
package model.jpa;

import javax.persistence.CascadeType;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.OneToOne;

/**
 * Class Company
 * @author ABALINE & AHOUNOU
 * 23 d�c. 2013
 */
@Entity
public class Company {
	@Id
	@GeneratedValue
	private int companyId;
	/**
	 * The symbol of the coompany
	 */
	private String symbol;
	/**
	 * Name of the company
	 */
	private String name;
	/**
	 * Company last sale
	 */
	private double lastSale;
	/**
	 * The market cap
	 */
	private double marketCap;
	/**
	 * 
	 */
	private String adrTso = "n/a";
	/**
	 * The initial public offering year
	 */
	private int ipoYear;
	/**
	 * Sector of activity
	 */
	private String sector;
	/**
	 * The puropse the company
	 */
	private String industry;
	/**
	 * Company url
	 */
	private String summaryQuote;
	/**
	 * The exchange in which the company is quoted
	 */
	@OneToOne(fetch = FetchType.LAZY, cascade={CascadeType.MERGE})
	@JoinColumn(name = "exchangeId")
	private Exchange exchange;
	
	/**
	 * Default constructor
	 */
	public Company(){
	}
	
	/**
	 * Constructor with parameters
	 * @param symbol
	 * @param name
	 * @param lastSale
	 * @param marketCap
	 * @param adrTso
	 * @param ipoYear
	 * @param sector
	 * @param industry
	 * @param summaryQuote
	 * @param exchange
	 */
	public Company(String symbol, String name, double lastSale, 
			double marketCap, String adrTso, int ipoYear, String sector, 
			String industry, String summaryQuote, Exchange exchange) {
		this.symbol = symbol;
		this.name = name;
		this.lastSale = lastSale;
		this.marketCap = marketCap;
		this.adrTso = adrTso;
		this.ipoYear = ipoYear;
		this.sector = sector;
		this.industry = industry;
		this.summaryQuote = summaryQuote;
		this.exchange = exchange;
	}
	
	/**
	 * Constructor with parameters
	 * @param symbol
	 * @param name
	 * @param lastSale
	 * @param marketCap
	 * @param ipoYear
	 * @param sector
	 * @param industry
	 * @param summaryQuote
	 * @param exchange
	 */
	public Company(String symbol, String name, double lastSale, 
			double marketCap, int ipoYear, String sector, 
			String industry, String summaryQuote, Exchange exchange) {
		this.symbol = symbol;
		this.name = name;
		this.lastSale = lastSale;
		this.marketCap = marketCap;
		this.ipoYear = ipoYear;
		this.sector = sector;
		this.industry = industry;
		this.summaryQuote = summaryQuote;
		this.exchange = exchange;
	}
	
	/**
	 * @return the symbol
	 */
	public String getSymbol() {
		return symbol;
	}

	/**
	 * @param symbol the symbol to set
	 */
	public void setSymbol(String symbol) {
		this.symbol = symbol;
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
	
	/**
	 * @return the lastSale
	 */
	public double getLastSale() {
		return lastSale;
	}

	/**
	 * @param lastSale the lastSale to set
	 */
	public void setLastSale(double lastSale) {
		this.lastSale = lastSale;
	}

	/**
	 * @return the marketCap
	 */
	public double getMarketCap() {
		return marketCap;
	}

	/**
	 * @param marketCap the marketCap to set
	 */
	public void setMarketCap(double marketCap) {
		this.marketCap = marketCap;
	}

	/**
	 * @return the adrTso
	 */
	public String getAdrTso() {
		return adrTso;
	}

	/**
	 * @param adrTso the adrTso to set
	 */
	public void setAdrTso(String adrTso) {
		this.adrTso = adrTso;
	}

	/**
	 * @return the sector
	 */
	public String getSector() {
		return sector;
	}

	/**
	 * @param sector the sector to set
	 */
	public void setSector(String sector) {
		this.sector = sector;
	}

	/**
	 * @return the industry
	 */
	public String getIndustry() {
		return industry;
	}

	/**
	 * @param industry the industry to set
	 */
	public void setIndustry(String industry) {
		this.industry = industry;
	}

	/**
	 * @return the summaryQuote
	 */
	public String getSummaryQuote() {
		return summaryQuote;
	}

	/**
	 * @param summaryQuote the summaryQuote to set
	 */
	public void setSummaryQuote(String summaryQuote) {
		this.summaryQuote = summaryQuote;
	}

	/**
	 * @return the ipoYear
	 */
	public int getIpoYear() {
		return ipoYear;
	}

	/* (non-Javadoc)
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString() {
		return "Company [companyId=" + companyId + ", symbol=" + symbol
				+ ", name=" + name + ", lastSale=" + lastSale + ", marketCap="
				+ marketCap + ", adrTso=" + adrTso + ", ipoYear=" + ipoYear
				+ ", sector=" + sector + ", industry=" + industry
				+ ", summaryQuote=" + summaryQuote + ", exchange=" + exchange
				+ "]";
	}
}