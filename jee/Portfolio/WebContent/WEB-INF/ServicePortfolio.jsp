<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
    
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/functions" prefix="fn"%>

<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<title>Mon portefeuille boursier</title>
		
		<!-- css -->
		<link href="css/style.css" rel="stylesheet" type="text/css">
		
		<!-- script -->
		<script type="text/javascript" src="js/dom.js"></script>
	</head>
	
	<body>
		<h3>Mon portefeuille boursier !</h3>
		Welcome ${client.lastName} <a href="#" >Déconnexion</a>
		<br/>
		
		<button type="button">Supprimer</button>
		
		<table class="zebra">
			<thead>
		    	<tr>
		        	<th>
		        		<input type="checkbox" name="" value="">
		        	</th>        
		        	<th>IMDB Top 10 Movies</th>
		        	<th>Year</th>
				</tr>
		    </thead>
		    
		    <!-- <tfoot>
			    <tr>
			        <td>&nbsp;</td>        
			        <td></td>
			        <td></td>
			    </tr>
		    </tfoot>  -->
		    
		    <tr>
				<td>
					<input type="checkbox" name="" value="">
				</td>        
		        <td>The Shawshank Redemption</td>
		        <td>1994</td>
		    </tr>      
		    
		</table>
		
		<!-- account -->
		<c:choose>
			<c:when test="${empty client.account}">
				Compte -
				<a href="#" onclick="toggle_visibility('openHidden');">
					 Ouvrir compte
				</a> /
			</c:when>
			<c:otherwise>
				Compte( ${client.account.amount} $)-
				<a href="AccountServlet?userId=${client.userId}"> 
					Fermer compte 
				</a> /
					
			</c:otherwise>
		</c:choose>
		
		<c:if test="${not empty client.account}">
			<a href="#" onclick="toggle_visibility('depositHidden');"  >Déposer</a> / 
			<a href="#" onclick="toggle_visibility('withdrawHidden');">Retirer</a>
		</c:if>
		
		<br/>
		<hr/>
		
			<!-- open account -->
			<div id="openHidden" hidden=hidden>
				<form method="post" action="AccountServlet">
					
					<input type="hidden" name="userId" 
						value="${client.userId}" />
					
					Montant initial <input type="number" name="amount" 
						placeholder="0.00" />
							
					<input type="submit" name="accountSubmit" value="Creer"/>
				
					<button type="button" onclick="hide('openHidden');">
						Cancel
					</button>
				</form>
				<hr/>
			</div>
			
			<!-- deposit cash -->
			<div id="depositHidden" hidden=hidden>
				<form method="post" action="AccountServlet">
					<input type="hidden" name="userId" 
						value="${client.userId}" />
					
					Montant <input type="number" name="amount" placeholder="0.00" />
							
					<input type="submit" name="accountSubmit" value="Ajouter"/>
					
					<button type="button" onclick="hide('depositHidden');">
						Cancel
					</button>
				</form>
				<hr/>
			</div>
			
			<!-- withdraw cash -->
			<div id="withdrawHidden" hidden=hidden>
				<form method="post" action="AccountServlet">
					<input type="hidden" name="userId" 
						value="${client.userId}" />
					
					Montant <input type="number" name="amount" placeholder="0.00" />
							
					<input type="submit" name="accountSubmit" value="Retirer"/>
					<button type="button" onclick="hide('withdrawHidden');">
						Cancel
					</button>
				</form>
				<hr/>
			</div>
		
		<!-- transaction -->
		<a href="#" onclick="toggle_visibility('transactionHidden');" >
			Ajouter une transaction
		</a>
		<br/>
		
		<div id="transactionHidden" hidden=hidden>
			
			<form method="post" action="TransactionServlet">
				<input type="hidden" name="userId" 
					value="${client.userId}" />
				
				Symbole <input type="text" />
				Type 
				<select>
					<option value="Acheter">Acheter</option>
				  	<option value="Vendre">Vendre</option>
				</select>
				Date <input type="date" name="date" />
				Prix <input type="number" name="price" placeholder="0.00" />
				Frais supp <input type="number" name="fee" placeholder="0.00" />			
				
				<input type="submit" value="Ajouter"/>
				<button type="button" onclick="hide('transactionHidden');">
					Cancel
				</button>
			</form>
			<hr/>
		</div>
	
	</body>
</html>