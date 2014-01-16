package controller;

import java.io.IOException;

import javax.persistence.NoResultException;
import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

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

				// injection bean
				request.setAttribute("client", user);

				// sending to portfolio jsp page
				dispatcher.include(request, response);

			}catch(NoResultException e){
				
				RequestDispatcher dispatcher = getServletContext().
						getRequestDispatcher("/WEB-INF/user/LogOn.jsp");
				
				String erreur = "Login et/ou mot de passe erroné(s) !";
				
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
				
				String erreur = "Mot de passe différents !";
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
