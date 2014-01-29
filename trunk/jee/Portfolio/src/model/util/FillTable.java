package model.util;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.sql.Date;
import java.sql.Timestamp;
import java.util.Calendar;
import java.util.GregorianCalendar;

import model.jpa.Action;
import model.jpa.Company;
import model.jpa.Exchange;
import model.services.CompanyService;
import model.services.ExchangeService;
import model.services.ICompanyService;
import model.services.IExchangeService;
import model.services.IService;
import model.services.Service;
import au.com.bytecode.opencsv.CSVReader;


/**
 * Class FillCompanyTable, helps to manage excel files
 * @author ABALINE & AHOUNOU
 * 19 janv. 2014
 */
public class FillTable {

	private File directory;
	/**
	 * Directory to scan
	 */
	private String folderName;


	/**
	 * Constructor
	 * @param folderName
	 */
	public FillTable(String folderName){
		directory = new File(folderName);
		this.folderName = folderName;
	}

	/**
	 * Fill company table
	 * @param number
	 * @throws IOException
	 */
	public void fillCompanyTable(int number) throws IOException{

		// loop on all files in directory data/company
		for(int i = 0; i < directory.list().length; i++){

			String filename = folderName + "/"+directory.list()[i];

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
				Company company = new Company(nextLine[0], nextLine[1], nextLine[2], 
						Double.parseDouble(nextLine[3]), nextLine[4], nextLine[5], 
						nextLine[6], nextLine[7], nextLine[8], exchange);

				dao.insert(company);
				
				j++;
			}
			reader.close();
		}
	}

	/**
	 * Fill action table
	 * @param company, helps to get company symbol
	 * @throws IOException
	 */
	public void fillActionTable(Company company){
		
		// get file name
		String filename = folderName + "/"+company.getSymbol().toUpperCase() +".csv";

		IService dao = new Service();

		// load file
		try {
			CSVReader reader = new CSVReader(new FileReader(filename));
		
			String [] nextLine;

			reader.readNext(); // skip labels lines
			
			while ( ((nextLine = reader.readNext()) != null) ) {

				String []dateSplit = nextLine[0].split("-");

				// TODO change date to a simple date
				Calendar cal=GregorianCalendar.getInstance();
				cal.set(Integer.parseInt(dateSplit[0]), Integer.parseInt(dateSplit[1]), 
						Integer.parseInt(dateSplit[2]));
					
				Action action = new Action(cal, Double.parseDouble(nextLine[1]), Double.parseDouble(nextLine[2]), 
						Double.parseDouble(nextLine[3]), Double.parseDouble(nextLine[4]), 
						Integer.parseInt(nextLine[5]), Double.parseDouble(nextLine[6]), company);
				
				dao.insert(action);
			
			}
			reader.close();
			
			
		
		
		} catch (Exception e) {
			System.out.println("fichier non trouvé");
		}
	}
}
