package controller;

import java.io.File;
import java.io.IOException;
import java.io.OutputStream;
import java.util.List;

import javax.servlet.RequestDispatcher;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import model.jpa.Action;
import model.jpa.Company;
import model.services.ActionService;
import model.services.CompanyService;
import model.services.IActionService;
import model.services.ICompanyService;

import org.jfree.chart.ChartFactory;
import org.jfree.chart.ChartRenderingInfo;
import org.jfree.chart.ChartUtilities;
import org.jfree.chart.JFreeChart;
import org.jfree.chart.entity.StandardEntityCollection;
import org.jfree.data.general.DefaultPieDataset;

/**
 * Servlet implementation class ActionServlet
 */
@WebServlet("/ActionServlet")
public class ActionServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	/**
	 * @see HttpServlet#HttpServlet()
	 */
	public ActionServlet() {
		super();
	}

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) 
			throws ServletException, IOException {
		
		String  symbol = request.getParameter("symbol");
		
		ICompanyService serviceCompany = new CompanyService();
		Company company = serviceCompany.getCompanyBySymbol(symbol);

		IActionService serviceAction = new ActionService();
		List<Action> actions = serviceAction.getActionsByCompnay(company);

		System.out.println("Taille : "+actions.size() +"\n" +actions);

		
		request.setAttribute("actions", actions);
		request.setAttribute("company", company);
		
		//createChart(response);
		
		// getting dispatcher
		RequestDispatcher dispatcher = getServletContext().getRequestDispatcher("/WEB-INF/action/Action.jsp");

		// sending to action jsp page
		dispatcher.include(request, response);
	}

	
	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		// TODO Auto-generated method stub
		System.out.println("here");
	}
	
	private void createChart(HttpServletResponse response) throws IOException{
		OutputStream out = response.getOutputStream();
		
		try {
			DefaultPieDataset myServletPieChart = new DefaultPieDataset();
			myServletPieChart.setValue("Maths", 74);
			myServletPieChart.setValue("Physics", 87);
			myServletPieChart.setValue("Chemistry", 62);
			myServletPieChart.setValue("Biology", 92);
			myServletPieChart.setValue("English", 51);        
			
			JFreeChart mychart = ChartFactory.createPieChart("Programming - Colored Pie Chart Example",myServletPieChart,true,true,false);  
			
			response.setContentType("image/png"); /* Set the HTTP Response Type */
			
			final ChartRenderingInfo info = new ChartRenderingInfo(new StandardEntityCollection());
			
			final File file = new File("./data/areachart.png");
			
			//ChartUtilities.saveChartAsPNG(file, mychart, 400, 300, info);
			
			
			ChartUtilities.writeChartAsPNG(out, mychart, 400, 300);/* Write the data to the output stream */
		
		}
		catch (Exception e) {
			System.err.println(e.toString()); /* Throw exceptions to log files */
		}
		finally {
			out.close();/* Close the output stream */
		}
	}

}
