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
import javax.servlet.http.HttpSession;

import model.jpa.Action;
import model.jpa.Transaction;
import model.jpa.TransactionType;
import model.jpa.User;
import model.services.ITransactionService;
import model.services.TransactionService;

/**
 * Servlet implementation class PortfolioServlet
 */
@WebServlet("/PortfolioServlet")
public class PortfolioServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	/**
	 * @see HttpServlet#HttpServlet()
	 */
	public PortfolioServlet() {
		super();
		// TODO Auto-generated constructor stub
	}

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		
		
		RequestDispatcher dispatcher = null;
		
		HttpSession session = request.getSession();
		
		User user = (User) session.getAttribute("user");
		if(user == null){
			// getting dispatcher
			dispatcher = getServletContext().getRequestDispatcher("/WEB-INF/user/LogOn.jsp");
		}else{
			
			ITransactionService transactionService = new TransactionService();

			List<Transaction> transactions = transactionService.getTransactionOfUser(user);
			
			List<Action> actionsBuy = new ArrayList<Action>();
			
			List<Action> actionsSell = new ArrayList<Action>();
			
			for(Transaction t : transactions){
				if(t.getType().equals(TransactionType.SELL)){
					actionsSell.add(t.getAction());
				}else{
					actionsBuy.add(t.getAction());
				}
				
				
			}
			
			request.setAttribute("actionsBuy", actionsBuy);
			request.setAttribute("actionsSell", actionsSell);
			
			
			// getting dispatcher
			dispatcher = getServletContext().getRequestDispatcher("/WEB-INF/portfolio/Portfolio.jsp");
		}
		
		// sending to login jsp page
		dispatcher.include(request, response);
	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		
	}

}
