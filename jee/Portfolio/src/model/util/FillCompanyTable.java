package model.util;

import java.io.File;
import java.io.FileReader;
import java.io.IOException;

import model.jpa.Company;
import model.jpa.Exchange;
import model.services.ExchangeService;
import model.services.IExchangeService;
import model.services.IService;
import model.services.Service;
import properties.Configuration;
import au.com.bytecode.opencsv.CSVReader;


/**
 * Class FillCompanyTable, helps to manage excel files
 * @author ABALINE & AHOUNOU
 * 19 janv. 2014
 */
public class FillCompanyTable {
	
	/**
	 * Constructor
	 * @param folderName
	 * @param exchangeName, the exchange of the company
	 * @param number, number of company to persist
	 * @throws IOException
	 */
	public FillCompanyTable(String folderName, int number) throws IOException{
		
		File directory = new File(folderName);
		
		for(int i = 0; i < directory.list().length; i++){
			
			String filename = Configuration.getInstance().getCompanyDirectoryName() + "/"+directory.list()[i];
			
			IService dao = new Service();
			
			// get the exchange
			String exchangeName = directory.list()[i].substring(0, directory.list()[i].length() - 4).toUpperCase();
			
			
			IExchangeService daoExchange = new ExchangeService();
			Exchange exchange = daoExchange.getExchange(exchangeName);
			
			// load file
			CSVReader reader = new CSVReader(new FileReader(filename));
			
			String [] nextLine;
			
			reader.readNext(); // skip labels lines
		    
			int j = 0;
			
			while ( ((nextLine = reader.readNext()) != null) && (j != number) ) {
				
				String delim = "," +"\"";
		    	
				String [] values = nextLine[0].split(delim);
				for(int k = 1; k < 9; k++){
					values[k] = values[k].replace("\"", "");
				}
				values[8] = values[8].replace(";", "");
				values[8] = values[8].replace(",", "");
		    	
		    	Company company = new Company(values[0], values[1], values[2], 
		    			Double.parseDouble(values[3]), values[4], values[5], 
		    			values[6], values[7], values[8], exchange);
		    	
		    	dao.insert(company);
		    	
		    	j++;
		        
		    }
			
			reader.close();
		}
		
		
	}
}
