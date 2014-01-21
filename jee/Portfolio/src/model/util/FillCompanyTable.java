package model.util;

import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import javax.persistence.ElementCollection;

import model.jpa.Company;
import model.jpa.Exchange;
import model.services.ExchangeService;
import model.services.IExchangeService;
import model.services.IService;
import model.services.Service;
import au.com.bytecode.opencsv.CSVReader;


/**
 * Class FillCompanyTable, helps to manage excel files
 * @author ABALINE & AHOUNOU
 * 19 janv. 2014
 */
public class FillCompanyTable {
	
	@ElementCollection
	private List<Company> listCompany = new ArrayList<Company>();
	
	@ElementCollection
	private Set<Company> set = new HashSet<Company>();
	
	public FillCompanyTable(String filename, String exchangeName) throws IOException{
		
		
		
		
		
		
		IService dao = new Service();
		
		// get the exchange
		IExchangeService daoExchange = new ExchangeService();
		Exchange exchange = daoExchange.getExchange(exchangeName);
		
		// load file
		CSVReader reader = new CSVReader(new FileReader(filename));
		
		String [] nextLine;
		
		reader.readNext(); // skip labes lines
	    
		while ((nextLine = reader.readNext()) != null) {
			
			String delim = "," +"\"";
	    	
			String [] values = nextLine[0].split(delim);
			for(int i = 1; i < 9; i++){
				values[i] = values[i].replace("\"", "");
			}
			values[8] = values[8].replace(";", "");
			values[8] = values[8].replace(",", "");
	    	
			
	    	
	    	Company company = new Company(values[0], values[1], values[2], 
	    			Double.parseDouble(values[3]), values[4], values[5], 
	    			values[6], values[7], values[8], exchange);
	    	
	    	
	    	//listCompany.add(company);
	    	//set.add(company);
	    	dao.insert(company);
	        
	    }
		
		//dao.insert(set);
		
		System.err.println("End");
		reader.close();
	}
	
	
	
	public static void main(String [] args) throws IOException{
		new FillCompanyTable("C:\\Users\\Folabi\\workspace\\master2\\JEE\\Portfolio\\data\\Nyse.csv", "NYSE");
		new FillCompanyTable("C:\\Users\\Folabi\\workspace\\master2\\JEE\\Portfolio\\data\\Nasdaq.csv", "NASDAQ");
		new FillCompanyTable("C:\\Users\\Folabi\\workspace\\master2\\JEE\\Portfolio\\data\\Amex.csv", "AMEX");
		System.err.println("End");
	}
	
	
}
