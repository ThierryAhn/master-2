package model.jpa;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

@Entity
public class LastConnected {
	@Id
	private int id;
	@Column(name = "DATE_FIELD")
	@Temporal(TemporalType.DATE)
	private Date date;
	
	
	public LastConnected(){
		this.id = 1;
		this.date = new Date();
		
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
	 * @return the id
	 */
	public int getId() {
		return id;
	}
}
