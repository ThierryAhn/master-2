package controller;

import java.io.IOException;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

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
		HttpSession session = request.getSession(true);
		
		User user = (User) session.getAttribute("user");

		// close user account
		user.closeAccount();
		userService.update(user);

		// getting dispatcher
		RequestDispatcher dispatcher = getServletContext().getRequestDispatcher("/WEB-INF/portfolio/Portfolio.jsp");

		// sending to portfolio jsp page
		dispatcher.include(request, response); 
	}
	
	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		double amount = Double.parseDouble(request.getParameter("amount"));
		
		HttpSession session = request.getSession(true);
		
		User user = (User) session.getAttribute("user");
		
		// opening account
		if(request.getParameter("accountSubmit").equals("Creer")){
			Account account = new Account(amount);
			user.setAccount(account);
		}


		// deposit cash
		if(request.getParameter("accountSubmit").equals("Ajouter")){
			user.deposit(amount);
		}

		// withdraw cash
		if(request.getParameter("accountSubmit").equals("Retirer")){
			user.withdraw(amount);
		}

		// updating user
		userService.update(user);

		// getting dispatcher
		RequestDispatcher dispatcher = getServletContext().getRequestDispatcher("/WEB-INF/portfolio/Portfolio.jsp");

		// sending to portfolio jsp page
		dispatcher.include(request, response);
	}

}
