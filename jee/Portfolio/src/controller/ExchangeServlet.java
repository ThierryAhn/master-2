package controller;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import model.jpa.Company;
import model.jpa.Exchange;
import model.services.CompanyService;
import model.services.ExchangeService;
import model.services.ICompanyService;
import model.services.IExchangeService;

/**
 * Servlet implementation class ExchangeServlet
 */
@WebServlet("/ExchangeServlet")
public class ExchangeServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
    
	/**
	 * Company service
	 */
	private ICompanyService companyService;
	/**
	 * Exchange service
	 */
	private IExchangeService exchangeService;
	
    /**
     * @see HttpServlet#HttpServlet()
     */
    public ExchangeServlet() {
    	super();
    	companyService = new CompanyService();
    	exchangeService = new ExchangeService();
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		
		//System.out.println("get");
		
		//System.out.println("exchange : " +request.getParameter("exchange"));
		
		RequestDispatcher dispatcher = getServletContext().
				getRequestDispatcher("/WEB-INF/exchange/Nasdaq.jsp");
		
		Exchange exchange = null;
		List<Company> listCompany = new ArrayList<Company>();
		
		
		int page = 1;
        int recordsPerPage = 5;
        
        if(request.getParameter("page") != null){
            page = Integer.parseInt(request.getParameter("page"));
        }
		
       
        if(request.getParameter("exchange").equals("Nasdaq") || request.getParameter("exchange").equals("Nyse")
        		|| request.getParameter("exchange").equals("Amex")){
        	
        	String choiceExchange = request.getParameter("exchange");
        	
        	dispatcher = getServletContext().
					getRequestDispatcher("/WEB-INF/exchange/"+choiceExchange+".jsp");
        	
        	exchange = exchangeService.getExchange(choiceExchange);
        	
        	//System.out.println("exchange : "+exchange);
        	
        	int noOfRecords = companyService.count(exchange);
        	
        	listCompany = companyService.getAllCompanyByExchange(exchange, 
					(page-1)*recordsPerPage, recordsPerPage);
        	
        	
        	for(Company company : listCompany){
        		company.getActionList().isEmpty();
        	}
        	
        	
        	//int noOfPages = 0;
            int noOfPages = (int) Math.ceil(noOfRecords * 1.0 / recordsPerPage);
    		
            request.setAttribute("noOfPages", noOfPages);
            request.setAttribute("currentPage", page);
        	request.setAttribute("currentExchange", choiceExchange);
        	request.setAttribute("listCompany", listCompany);
 
        	dispatcher.include(request, response);
        	
        }
        
        
		// filling company tables
		/* if(request.getParameter("exchange").equals("nasdaq")){
			
			exchange = exchangeService.getExchange("NASDAQ");
			
			listCompany = companyService.getAllCompanyByExchange(exchange, 
					(page-1)*recordsPerPage, recordsPerPage);
			
			request.setAttribute("listCompany", listCompany);
			
			dispatcher.include(request, response);
		}else{
			
			if(request.getParameter("exchange").equals("nyse")){
				dispatcher = getServletContext().
						getRequestDispatcher("/WEB-INF/exchange/Nyse.jsp");
				
				exchange = exchangeService.getExchange("NYSE");
				listCompany = companyService.getAllCompanyByExchange(exchange, 
						(page-1)*recordsPerPage, recordsPerPage);
				
				
				request.setAttribute("listCompany", listCompany);
				
				
				dispatcher.include(request, response);
			}else{
				dispatcher = getServletContext().
						getRequestDispatcher("/WEB-INF/exchange/Amex.jsp");
				
				exchange = exchangeService.getExchange("AMEX");
				
				listCompany = companyService.getAllCompanyByExchange(exchange, 
						(page-1)*recordsPerPage, recordsPerPage);
				
				request.setAttribute("listCompany", listCompany);
				
				
				dispatcher.include(request, response);
			}
			
		}*/
		
		
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		//System.out.println("post");
		
		//System.out.println("exchange : " +request.getParameter("exchange"));
	}

}
