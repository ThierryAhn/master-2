package controller;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import model.jpa.Action;
import model.jpa.Company;
import model.jpa.ConfidenceLevel;
import model.jpa.Transaction;
import model.jpa.TransactionType;
import model.jpa.User;
import model.services.ActionService;
import model.services.CompanyService;
import model.services.IActionService;
import model.services.ICompanyService;
import model.services.IService;
import model.services.ITransactionService;
import model.services.Service;
import model.services.TransactionService;

/**
 * Servlet implementation class TransactionServlet
 */
@WebServlet("/TransactionServlet")
public class TransactionServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	/**
	 * @see HttpServlet#HttpServlet()
	 */
	public TransactionServlet() {
		super();
	}

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

		HttpSession session = request.getSession(true);
		User user = (User) session.getAttribute("user");

		ITransactionService transactionService = new TransactionService();

		List<Transaction> transactions = transactionService.getTransactionOfUser(user);

		// getting user actions
		List<Action> actionSelling = new ArrayList<Action>();

		List<Action> actionBuying = new ArrayList<Action>();

		for(Transaction t : transactions){
			if(user.getUserId() == t.getUser().getUserId()){
				actionSelling.add(t.getAction());
			}
		}


		// getting all actions distinct
		IActionService service = new ActionService();
		List<Action> actions = new ArrayList<Action>();

		ICompanyService serviceCompany = new CompanyService();
		for(Company company : serviceCompany.getAllCompany()){

			List<Action> actionList = new ArrayList<Action>();
			actionList.addAll(service.getActionsByCompany(company));

			Action action = new Action();
			action = actionList.get(0);
			
			actions.add(action);

			// adding of actions that not belong to the user
			if(user.getConfidenceLevel().equals(ConfidenceLevel.PRIVILEDGED)){
				if(!actionSelling.contains(action)){
					actionSelling.add(action);
				}
			}

			
			actionBuying.add(action);
			
			actionList = null;

		}
		
		request.setAttribute("actions", actions);
		request.setAttribute("actionSelling", actionSelling);
		request.setAttribute("actionBuying", actionBuying);

		// getting dispatcher
		RequestDispatcher dispatcher = getServletContext().getRequestDispatcher("/WEB-INF/action/Actions.jsp");

		// sending to portfolio jsp page
		dispatcher.include(request, response); 

	}

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

		RequestDispatcher dispatcher = null;


		int volume = Integer.parseInt(request.getParameter("volume"));
		int actionId = Integer.parseInt(request.getParameter("actionChoose")); 

		System.out.println(actionId + " --- "+volume);

		IActionService service = new ActionService();
		Action action = service.getActionById(actionId);

		HttpSession session = request.getSession(true);
		User user = (User) session.getAttribute("user");

		double fee = volume * action.getAdj() * 0.5;

		double price = action.getAdj() * volume;

		System.out.println(price +"  -  "+fee);

		IService persistService = new Service();

		Transaction transaction = null;


		ITransactionService transactionService = new TransactionService();
		// getting user actions
		List<Transaction> transactions = transactionService.getTransactionOfUser(user);

		List<Action> actionBuying = new ArrayList<Action>();

		// getting all actions distinct
		List<Action> actions = new ArrayList<Action>();
		
		// getting user actions to sell
		List<Action> actionSelling = new ArrayList<Action>();
		
		// if buying
		if(request.getParameter("actionSubmit").equals("Acheter")){
			String erreur ="";
			if(user.getAccount().getAmount() < price){
				erreur = "Vous n'avez pas assez d'argent sur votre compte !";
			}else{
				if(volume > action.getVolume()){
					erreur = "Erreur, volume maximal " +action.getVolume() +" !";
				}else{
					
					Action actionNew = new Action(action);
					actionNew.setVolume(volume);
					
					transaction = new Transaction(TransactionType.BUY, new Date(), price, volume, fee, actionNew,
							user);
					
					user.withdraw(price + fee);
					persistService.update(user);
					
					persistService.insert(transaction);
				}
			}

			request.setAttribute("erreur", erreur);
		}else{
			
			Action actionNew = new Action(action);
			actionNew.setVolume(actionNew.getVolume() - volume);
			
			transaction = new Transaction(TransactionType.SELL, new Date(), price, volume, fee, actionNew, user);
			
			user.deposit(price);
			user.withdraw(fee);
			persistService.update(user);
			
			persistService.insert(transaction);
			
			transactions = transactionService.getTransactionOfUser(user);
			
			for(Transaction t : transactions){
				if(t.getUser().getUserId() == user.getUserId()){
					
					for(Action a : actionSelling){
						if(a.getActionId() != t.getAction().getActionId()){
							actionSelling.add(t.getAction());
						}
					}
				}

			}

		}
		
		
		ICompanyService serviceCompany = new CompanyService();
		for(Company company : serviceCompany.getAllCompany()){
			List<Action> actionList = new ArrayList<Action>();
			actionList.addAll(service.getActionsByCompany(company));

			Action actionTemp = new Action();
			actionTemp = actionList.get(0);

			actions.add(actionTemp);

			// adding of actions that not belong to the user
			if(user.getConfidenceLevel().equals(ConfidenceLevel.PRIVILEDGED)){
				for(Action a : actionSelling){
					if(a.getActionId() != actionTemp.getActionId()){
						actionSelling.add(actionTemp);
					}
				}
			}
			
			actionBuying.add(action);
			
			actionList = null;
		}

		request.setAttribute("actionSelling", actionSelling);
		
		
		request.setAttribute("actions", actions);
		request.setAttribute("actionBuying", actionBuying);

		// getting dispatcher
		dispatcher = getServletContext().getRequestDispatcher("/WEB-INF/action/Actions.jsp");

		// sending to portfolio jsp page
		dispatcher.include(request, response);

	}

}
