package model.util;

import java.io.IOException;
import java.util.Calendar;
import java.util.GregorianCalendar;

import model.jpa.Company;
import model.services.CompanyService;
import model.services.ICompanyService;

public class TestFillActionTable {

	public static void main(String[] args) throws IOException {
		
		ICompanyService service = new CompanyService();
		for(Company company : service.getAllCompany()){
			
			String symbol = company.getSymbol();
			
			// récupération de la date courante
			Calendar now = GregorianCalendar.getInstance();
			int year = now.get(Calendar.YEAR);
			int month = now.get(Calendar.MONTH); // Note: zero based!
			int day = now.get(Calendar.DAY_OF_MONTH);
			
			// getting the date of the last client conncect
			// TODO have to change this in the servlet
			int lastYear = 1100;
			int lastMonth = 1;
			int lastDay = 1;
			
			
			
			String url = "http://ichart.finance.yahoo.com/table.csv?s="+symbol+"&d=" +month 
					+"&e=" +day +"&f=" +year + "&g=d&a=" +lastMonth +"&b=" +lastDay +"&c=" +lastYear
					+ "&ignore=.csv";
			
			
			
			Download.getActionFile(url);
			
			
			
			//System.out.println(symbol);
			
			
			//new FillTable(Configuration.getInstance().getActionDirectoryName()).fillActionTable(company.getSymbol());
		}
		
		
		
		System.err.println("End");
		
	}

}
