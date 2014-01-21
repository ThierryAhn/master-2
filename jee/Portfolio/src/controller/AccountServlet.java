package controller;

import java.io.IOException;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import model.jpa.Account;
import model.jpa.User;
import model.services.IUserService;
import model.services.UserService;

/**
 * Servlet implementation class AccountServlet
 */
@WebServlet("/AccountServlet")
public class AccountServlet extends HttpServlet {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	/**
	 * User service
	 */
	private IUserService userService;

	/**
	 * @see HttpServlet#HttpServlet()
	 */
	public AccountServlet() {
		super();
		userService = new UserService();
	}

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		//System.out.println("user : " +request.getParameter("userId"));

		User client = userService.getUser(
				Integer.parseInt(request.getParameter("userId")));

		// close user account
		client.closeAccount();
		userService.update(client);

		// injection bean
		request.setAttribute("client", client);

		// getting dispatcher
		RequestDispatcher dispatcher = getServletContext().
			getRequestDispatcher("/WEB-INF/ServicePortfolio.jsp");

		// sending to portfolio jsp page
		dispatcher.include(request, response); 

	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		double amount = Double.parseDouble(request.getParameter("amount"));

		User client = userService.getUser(
				Integer.parseInt(request.getParameter("userId")));

		// opening account
		if(request.getParameter("accountSubmit").equals("Creer")){
			Account account = new Account(amount);
			client.setAccount(account);
		}


		// deposit cash
		if(request.getParameter("accountSubmit").equals("Ajouter")){
			client.deposit(amount);
		}

		// withdraw cash
		if(request.getParameter("accountSubmit").equals("Retirer")){
			client.withdraw(amount);
		}

		// updating user
		userService.update(client);

		// injection bean
		request.setAttribute("client", client);

		// getting dispatcher
		RequestDispatcher dispatcher = getServletContext().
				getRequestDispatcher("/WEB-INF/Portfolio.jsp");

		// sending to portfolio jsp page
		dispatcher.include(request, response);
	}

}
