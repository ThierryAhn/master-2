package model.util;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.URL;
import java.util.Calendar;
import java.util.GregorianCalendar;

import model.jpa.Action;
import model.jpa.Company;
import model.jpa.Exchange;
import model.services.IService;
import model.services.Service;
import au.com.bytecode.opencsv.CSVReader;

/**
 * Class Download which helps to download file on Internet
 * @author ABALINE & AHOUNOU
 * 15 janv. 2014
 */
public class Download
{
	/**
	 * Get company file from internet
	 * @param host host of the file
	 * @throws IOException 
	 */
	public static void getCompanyFile(String host, Exchange exchange, int number) throws IOException
	{
		IService service = new Service();
		
		URL url = new URL(host);
		BufferedReader buf = new BufferedReader(new InputStreamReader(url.openStream()));
		
		// load file
		try {
			CSVReader reader = new CSVReader(buf);

			String [] nextLine;

			reader.readNext(); // skip labels lines
			
			int j = 0;
			
			while ( ((nextLine = reader.readNext()) != null) && (j != number) ) {
				
				Company company = new Company(nextLine[0], nextLine[1], nextLine[2], 
						Double.parseDouble(nextLine[3]), nextLine[4], nextLine[5], 
						nextLine[6], nextLine[7], nextLine[8], exchange);
				
				System.out.println(company);
				
				//service.insert(company);
				
				j++;
			}
			reader.close();

		} catch (Exception e) {
			System.out.println("fichier non trouvé");
		}
	}

	/**
	 * Get action file from internet
	 * @param host host of the file
	 * @param company
	 * @throws IOException 
	 */
	public static void getActionFile(String host, Company company) throws IOException
	{
		IService service = new Service();
		
		URL url = new URL(host);
		BufferedReader buf = new BufferedReader(new InputStreamReader(url.openStream()));
		
		// load file
		try {
			CSVReader reader = new CSVReader(buf);

			String [] nextLine;

			reader.readNext(); // skip labels lines

			while ( ((nextLine = reader.readNext()) != null) ) {
				
				String []dateSplit = nextLine[0].split("-");
				
				Calendar cal = GregorianCalendar.getInstance();
				cal.set(Integer.parseInt(dateSplit[0]), Integer.parseInt(dateSplit[1]), 
						Integer.parseInt(dateSplit[2]));
			
				Action action = new Action(cal, Double.parseDouble(nextLine[1]), Double.parseDouble(nextLine[2]), 
						Double.parseDouble(nextLine[3]), Double.parseDouble(nextLine[4]), 
						Integer.parseInt(nextLine[5]), Double.parseDouble(nextLine[6]), company);
				
				
				System.out.println(action);
				
				//service.insert(action);

			}
			reader.close();




		} catch (Exception e) {
			System.out.println("fichier non trouvé");
		}




		/* int read;
		FileOutputStream writeFile = null;

		// construct file name (symbol of company + csv)
		String filename = host.split("&")[0].split("=")[1] +".csv";

		writeFile = new FileOutputStream(Configuration.getInstance().getActionDirectoryName()+"/" +filename);

		while ((read = buf.read(buffer)) > 0){
			writeFile.write(buffer, 0, read);
		}
		writeFile.flush();
		writeFile.close();*/





	}
}

