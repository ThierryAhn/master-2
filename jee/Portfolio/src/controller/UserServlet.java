package controller;

import java.io.IOException;

import javax.persistence.NoResultException;
import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import model.jpa.Address;
import model.jpa.User;
import model.services.UserService;
import model.services.IUserService;

/**
 * Servlet implementation class ClientServlet
 * @author ABALINE & AHOUNOU
 * 28 déc. 2013
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
		
		// getting dispatcher
		RequestDispatcher dispatcher = getServletContext().getRequestDispatcher(
     			"/WEB-INF/ServiceNewUser.jsp");
		
		// sending to new client jsp page
		dispatcher.include(request, response);
	}


	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, 
			HttpServletResponse response) throws ServletException, IOException {

		User client = null;
		
		// connecting user
		if(request.getParameter("submit").equals("Se connecter")){
			// getting connection data
			String login = request.getParameter("login");
			String password = request.getParameter("password");
			
			try{
				client = userService.getUser(login);
				
				// if wrong password
				if(client.getPassword().equals(password)){
					// getting dispatcher
					RequestDispatcher dispatcher = getServletContext().
							getRequestDispatcher("/WEB-INF/ServicePortfolio.jsp");
					
					// injection bean
					request.setAttribute("client", client);
					
					// sending to portfolio jsp page
					dispatcher.include(request, response);
				
				}else{
					System.out.println("Login et/ou mot de passe erroné(s)");
				}
				
			}catch(NoResultException e){
				System.out.println("Login et/ou mot de passe erroné(s)");
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
			
			// password differents
			if(!password.equals(confirmPassword)){
				System.out.println("Mot de passe différents");
			}else{
				try{
					client = userService.getUser(login);
					
					System.out.println("Login indisponible");
				
				}catch(NoResultException e){ // if the login is free
					
					// address data
					int number = Integer.parseInt(request.getParameter("number"));
					String street = request.getParameter("street");
					String city = request.getParameter("city");
					String country = request.getParameter("country");
					
					// creating new client object
					Address address = new Address(number, street, city, 
								country);
					
					client = new User(lastName, firstName, address, 
							login, password);
					
					// persist client
					userService.insert(client);
					
					
					// getting dispatcher
					RequestDispatcher dispatcher = getServletContext().getRequestDispatcher(
			     			"/WEB-INF/ServiceLogin.jsp");
					
					// sending to connection jsp page
					dispatcher.include(request, response);
				}
			}
		}
		
	}

}
