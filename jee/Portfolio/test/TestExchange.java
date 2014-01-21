import model.jpa.Exchange;
import model.services.ExchangeService;
import model.services.IService;
import model.services.Service;


public class TestExchange {
	public static void main(String [] args){
		IService dao = new ExchangeService();
		
		Exchange exchange = new Exchange("NASDAQ");
		dao.insert(exchange);
		
		exchange = new Exchange("NYSE");
		dao.insert(exchange);
		
		exchange = new Exchange("AMEX");
		dao.insert(exchange);
		
		System.err.println("End");
		
	}
}
