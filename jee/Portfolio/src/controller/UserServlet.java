package controller;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import javax.persistence.NoResultException;
import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import model.jpa.Action;
import model.jpa.ConfidenceLevel;
import model.jpa.Transaction;
import model.jpa.User;
import model.services.ITransactionService;
import model.services.IUserService;
import model.services.TransactionService;
import model.services.UserService;

/**
 * Servlet implementation class ClientServlet
 * @author ABALINE & AHOUNOU
 * 28 d�c. 2013
 */
@WebServlet("/UserServlet")
public class UserServlet extends HttpServlet {
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
	public UserServlet() {
		super();
		userService = new UserService();
	}


	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, 
			HttpServletResponse response) throws ServletException, IOException {
		
		RequestDispatcher dispatcher = getServletContext().getRequestDispatcher("/WEB-INF/user/Register.jsp");
		
		// sending to connection or register jsp page
		dispatcher.include(request, response);
	}


	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, 
			HttpServletResponse response) throws ServletException, IOException {
		
		User user = null;

		// connecting user
		if(request.getParameter("submit").equals("Se connecter")){
			// getting connection data
			String login = request.getParameter("login");
			String password = request.getParameter("password");

			try{
				user = userService.getUser(login, password);

				RequestDispatcher dispatcher = getServletContext().
						getRequestDispatcher("/WEB-INF/portfolio/Portfolio.jsp");
				
				ITransactionService transactionService = new TransactionService();

				List<Transaction> transactions = transactionService.getTransactionOfUser(user);
				
				List<Action> actions = new ArrayList<Action>();
				for(Transaction t : transactions){
					actions.add(t.getAction());
				}
				
				request.setAttribute("actions", actions);
				
				// injection bean
				HttpSession session = request.getSession(true);
				session.setAttribute("user", user);

				// sending to portfolio jsp page
				dispatcher.include(request, response);

			}catch(NoResultException e){
				
				RequestDispatcher dispatcher = getServletContext().
						getRequestDispatcher("/WEB-INF/user/LogOn.jsp");
				
				String erreur = "Login et/ou mot de passe erron�(s) !";
				
				request.setAttribute("erreur", erreur);
				
				// sending to logon jsp page
				dispatcher.include(request, response);
			}
		}

		// new user
		if(request.getParameter("submit").equals("S'inscrire")){

			// getting inscription data
			String lastName = request.getParameter("lastName");
			String firstName = request.getParameter("firstName");
			String login = request.getParameter("login");
			String password = request.getParameter("password");
			String confirmPassword = request.getParameter("confirmPassword");
			
			
			RequestDispatcher dispatcher = getServletContext().
					getRequestDispatcher("/WEB-INF/user/Register.jsp");
			
			// password differents
			if(!password.equals(confirmPassword)){
				
				String erreur = "Mot de passe diff�rents !";
				request.setAttribute("erreur", erreur);
				
				request.setAttribute("lastName", lastName);
				request.setAttribute("firstName", firstName);
				request.setAttribute("login", login);
				
				// sending to logon jsp page
				dispatcher.include(request, response);
				
			}else{
				try{
					user = userService.getUser(login);
					
					String erreur = "Login indisponible !";
					request.setAttribute("erreur", erreur);
					
					request.setAttribute("lastName", lastName);
					request.setAttribute("firstName", firstName);
					request.setAttribute("login", login);
					
					// sending to logon jsp page
					dispatcher.include(request, response);

				}catch(NoResultException e){ // if the login is free

					// creating new client object
					user = new User(lastName, firstName, login, password);
					
					if(request.getParameter("confidence").equals("PRIVILEGIE"))
						user.setConfidenceLevel(ConfidenceLevel.PRIVILEDGED);
					
					// persist client
					userService.insert(user);

					// getting dispatcher
					dispatcher = getServletContext().getRequestDispatcher(
							"/WEB-INF/user/LogOn.jsp");

					// sending to connection jsp page
					dispatcher.include(request, response);
				}
			}
		}

	}

}
