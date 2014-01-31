package model.util;

import java.io.IOException;
import java.util.List;

import model.jpa.Exchange;
import model.services.ExchangeService;
import model.services.IExchangeService;

public class TestFillCompanyTable {
	public static void main(String [] args) throws IOException{
		
		// downloading company files
		IExchangeService service = new ExchangeService();
		List<Exchange> exchangeList = service.getAllExchange();
		
		
		long start = System.currentTimeMillis();
		for(Exchange exchange : exchangeList){
			String exchangeName = exchange.getName().toLowerCase();
			
			Download.getCompanyFile("http://www.nasdaq.com/screening/companies-by-industry.aspx?exchange=" +
					exchangeName+"&render=download", exchange, 2);
			
		}
		long end = System.currentTimeMillis();
		
		System.err.println("End in "+(end - start)/1000 +" s");
	}
}
