import model.jpa.Company;
import model.jpa.Exchange;
import model.services.ExchangeService;
import model.services.IExchangeService;
import model.services.IService;
import model.services.Service;


public class TestCompany {
	
	public static void main(String [] args){
		IService dao = new Service();
		
		IExchangeService daoExchange = new ExchangeService();
		
		Exchange exchange = daoExchange.getExchange("NASDAQ");
		
		Company company = new Company("FLWS","1-900 FLOWERS.COM, Inc.", 5.48,
				361424160.72, 1999, "Consumer Services","Other Specialty Stores", 
				"http://www.nasdaq.com/symbol/flws", exchange);
				
		
		dao.insert(company);
	}
}	
