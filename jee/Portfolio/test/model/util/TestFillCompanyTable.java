package model.util;

import java.io.IOException;
import java.util.List;

import model.jpa.Exchange;
import model.services.ExchangeService;
import model.services.IExchangeService;

public class TestFillCompanyTable {
	public static void main(String [] args) throws IOException{
		
		// téléchargement des fichiers
		IExchangeService service = new ExchangeService();
		List<Exchange> exchangeList = service.getAllExchange();
		
		for(Exchange exchange : exchangeList){
			String exchangeName = exchange.getName().toLowerCase();
			
			Download.getCompanyFile("http://www.nasdaq.com/screening/companies-by-industry.aspx?exchange=" +
					exchangeName+"&render=download", exchange, 2);
			
		}
		
		System.out.println("---> End <---");
	}
}
