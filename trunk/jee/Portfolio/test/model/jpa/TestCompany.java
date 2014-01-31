package model.jpa;
import model.services.CompanyService;
import model.services.ExchangeService;
import model.services.ICompanyService;
import model.services.IExchangeService;
import model.services.IService;
import model.services.Service;


public class TestCompany {
	
	public static void main(String [] args){
		IService dao = new Service();
		
		IExchangeService daoExchange = new ExchangeService();
		
		ICompanyService daoCompany = new CompanyService();
		
		Exchange exchange = daoExchange.getExchange("NASDAQ");
		
		Company company = new Company("FLWS","1-900 FLOWERS.COM, Inc.", "5.48",
				361424160.72, "n/a", "1999", "Consumer Services","Other Specialty Stores", 
				"http://www.nasdaq.com/symbol/flws", exchange);
				
		
		dao.insert(company);
		
		exchange = daoExchange.getExchange("AMEX");
		company = new Company("FAX","Aberdeen Asia-Pacific Income Fund Inc",
				"5.88", 1572358616.64, "n/a", "1986", "n/a", "n/a", 
				"http://www.nasdaq.com/symbol/fax",exchange);
		dao.insert(company);
		
		company = new Company("IAF","Aberdeen Australia Equity Fund Inc", 
				"8.92", 201361141.48, "n/a", "n/a", "n/a", "n/a",
				"http://www.nasdaq.com/symbol/iaf",exchange);
		dao.insert(company);
		
		
		exchange = daoExchange.getExchange("NYSE");
		company = new Company("DDD","3D Systems Corporation", "90.53", 
				9304748811.49, "n/a", "n/a", 
				"Technology","Computer Software: Prepackaged Software",
				"http://www.nasdaq.com/symbol/ddd", exchange);
		dao.insert(company);
		
		
		// get company
		company = daoCompany.getCompany("Aberdeen Asia-Pacific Income Fund Inc");
		
		// force lazy mode to load
		company.getActionList().isEmpty();
		System.out.println(company.getActionList().get(0));
		
	}
}	
