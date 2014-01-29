package model.util;

import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.sql.Date;
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
	
	/**
	 * Fill action table
	 * @param symbol
	 * @throws IOException
	 */
	public void fillActionTable(String symbol) throws IOException{
		
		// loop on all files in directory data/action
		for(int i = 0; i < directory.list().length; i++){

			String filename = folderName + "/"+directory.list()[i];

			IService dao = new Service();

			// get the exchange
			//String exchangeName = directory.list()[i].substring(0, directory.list()[i].length() - 4).toUpperCase();


			//IExchangeService daoExchange = new ExchangeService();
			//Exchange exchange = daoExchange.getExchange(exchangeName);

			// load file
			CSVReader reader = new CSVReader(new FileReader(filename));

			String [] nextLine;

			reader.readNext(); // skip labels lines

			while ( ((nextLine = reader.readNext()) != null) ) {
				
				String []dateSplit = nextLine[0].split("-");
				
				@SuppressWarnings("deprecation")
				Date date = new Date(Integer.parseInt(dateSplit[0]), Integer.parseInt(dateSplit[1]), 
						Integer.parseInt(dateSplit[2]));
				
				// TODO change date to GregorianCalendar
				Calendar cal=GregorianCalendar.getInstance();
				cal.set(Integer.parseInt(dateSplit[0]), Integer.parseInt(dateSplit[1]), 
						Integer.parseInt(dateSplit[2]));
				
				// getting company by his symbol
				ICompanyService service = new CompanyService();
				Company company = service.getCompanyBySymbol(symbol);
				
				Action action = new Action(date, Double.parseDouble(nextLine[1]), Double.parseDouble(nextLine[2]), 
						Double.parseDouble(nextLine[3]), Double.parseDouble(nextLine[4]), 
						Integer.parseInt(nextLine[5]), Double.parseDouble(nextLine[6]), company);
				
				System.out.println(action);
				
				/* String delim = "," +"\"";

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

				*/

			}
			reader.close();
		}
	}
}
