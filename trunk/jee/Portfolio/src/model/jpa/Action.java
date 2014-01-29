package model.jpa;

import java.util.Calendar;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.OneToOne;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

/**
 * Class Action
 * @author ABALINE & AHOUNOU
 * 23 d�c. 2013
 */
@Entity
public class Action {
	@Id
	@GeneratedValue
	private int actionId;
	
	/**
	 * 
	 */
	@Column(name = "DATE_FIELD")
	@Temporal(TemporalType.DATE)
	private Calendar date;
	/**
	 * Minimal value to start bidding
	 */
	private double open;
	/**
	 * The highest bid
	 */
	private double high;
	/**
	 * The lowest bid
	 */
	private double low;
	/**
	 * 
	 */
	private double close;
	/**
	 * Volume of the action
	 */
	private int volume;
	/**
	 * The adjusted closing value
	 */
	private double adj;
	
	@ManyToOne(fetch=FetchType.LAZY)
	@JoinColumn(name="COMPANY_ID")
	private Company company;
	
	/**
	 * Default constructor
	 */
	Action(){
	}

	/**
	 * Constructor with parameters
	 * @param date
	 * @param open
	 * @param high
	 * @param low
	 * @param close
	 * @param volume
	 * @param adj
	 * @param company
	 */
	public Action(Calendar date, double open, double high, double low, double close,
			int volume, double adj, Company company) {
		this.date = date;
		this.open = open;
		this.high = high;
		this.low = low;
		this.close = close;
		this.volume = volume;
		this.adj = adj;
		this.company = company;
	}
	
	/**
	 * @return the company
	 */
	public Company getCompany() {
		return company;
	}

	/**
	 * @return the date
	 */
	public Calendar getDate() {
		return date;
	}

	/**
	 * @param date the date to set
	 */
	public void setDate(Calendar date) {
		this.date = date;
	}

	/**
	 * @return the open
	 */
	public double getOpen() {
		return open;
	}

	/**
	 * @param open the open to set
	 */
	public void setOpen(double open) {
		this.open = open;
	}

	/**
	 * @return the high
	 */
	public double getHigh() {
		return high;
	}

	/**
	 * @param high the high to set
	 */
	public void setHigh(double high) {
		this.high = high;
	}

	/**
	 * @return the low
	 */
	public double getLow() {
		return low;
	}

	/**
	 * @param low the low to set
	 */
	public void setLow(double low) {
		this.low = low;
	}

	/**
	 * @return the close
	 */
	public double getClose() {
		return close;
	}

	/**
	 * @param close the close to set
	 */
	public void setClose(double close) {
		this.close = close;
	}

	/**
	 * @return the volume
	 */
	public int getVolume() {
		return volume;
	}

	/**
	 * @param volume the volume to set
	 */
	public void setVolume(int volume) {
		this.volume = volume;
	}

	/**
	 * @return the adj
	 */
	public double getAdj() {
		return adj;
	}

	/**
	 * @param adj the adj to set
	 */
	public void setAdj(double adj) {
		this.adj = adj;
	}

	/* (non-Javadoc)
	 * @see java.lang.Object#toString()
	 */
	@Override
	public String toString() {
		return "Action [actionId=" + actionId + ", date=" + date + ", open="
				+ open + ", high=" + high + ", low=" + low + ", close=" + close
				+ ", volume=" + volume + ", adj=" + adj + "]";
	}
}
