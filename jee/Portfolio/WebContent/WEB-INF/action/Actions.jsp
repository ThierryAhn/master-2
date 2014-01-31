<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>

<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/functions" prefix="fn"%>

<!DOCTYPE HTML>
<html>
	<head>
	
		<title>Portfolio</title>
		
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<meta name="description" content="" />
		<meta name="keywords" content="" />
		
		<!-- js -->
		<!--<script src="js/jquery.min.js"></script>-->
		<script src="js/skel.min.js"></script>
		<script src="js/skel-panels.min.js"></script>
		<script src="js/init.js"></script>
		
		<!-- css -->
		<noscript>
			<link rel="stylesheet" href="css/skel-noscript.css" />
			<link rel="stylesheet" href="css/style.css" />
			<link rel="stylesheet" href="css/style-desktop.css" />
			<link rel="stylesheet" href="css/style-wide.css" />
		</noscript>
		
		<link
			href="http://fonts.googleapis.com/css?family=Source+Sans+Pro:400,400italic,700|Open+Sans+Condensed:300,700"
			rel="stylesheet" />
			
			
		<!-- fenetre modale css -->
		<link rel="stylesheet" href="css/reveal.css">
		<!-- scripts fenetre modale -->
		<script type="text/javascript" src="js/jquery-1.4.4.min.js"></script>
		<script type="text/javascript" src="js/jquery.reveal.js"></script>
		
	</head>
	
	<body class="left-sidebar">
	
		<!-- Wrapper -->
		<div id="wrapper">
	
			<!-- Content -->
			<div id="content">
				<div id="content-inner">
	
					<!-- Post -->
					<article class="is-post is-post-excerpt">
						<header>
							<span class="byline"> Actions d'aujourd'hui </span>
						</header>
	
						<div class="info">
	
							<ul class="page-name">
								<li class="letter-name">M</li>
								<li class="letter-name">A</li>
								<li class="letter-name">R</li>
								<li class="letter-name">C</li>
								<li class="letter-name">H</li>
								<li class="letter-name">E</li>
							</ul>
						</div>
						
						<!-- Company complete informations -->
						
						<c:choose>
							<c:when test="${not empty user.account.amount}">
								<a href="#" data-reveal-id="buyModal">Acheter</a> | 
								<a href="#" href="#" data-reveal-id="sellModal">Vendre</a>
								<p style="color:red;">${erreur}</p>
							</c:when>
							<c:otherwise>
								<p style="color:red;">
									Info : Pour acheter des actions, veuillez ouvrir un compte à partir de 
									votre espace Mon portfolio !
								</p>
							</c:otherwise>	
						</c:choose>
						<!-- table -->
						<table class="style1">
							<thead>
								<tr>
									<th>Date</th>
									<th>Symbole</th>
									<th>Volume</th>
									<th>Prix d'ouverture</th>
									<th>Plus haut</th>
									<th>Plus bas</th>
									<th>Prix de fermeture</th>
									<th>Valeur actuelle</th>
								</tr>
							</thead>
	
							<tbody>
								<c:forEach var="action" items="${actions}">
									<tr>
										<td><fmt:formatDate pattern="yyyy-MM-dd" value="${action.date}"/></td>
										<td>${action.company.symbol}</td>
										<td>${action.volume}</td>
										<td>${action.open}</td>
										<td>${action.high}</td>
										<td>${action.low}</td>
										<td>${action.close}</td>
										<td>${action.adj}</td>
									</tr>
								</c:forEach>
							</tbody>
						</table>
								
					</article>
					
					
					<div id="buyModal" class="reveal-modal">
						<form method="post" action="TransactionServlet">
							
							<select name="actionChoose">
								<c:forEach var="action" items="${actionBuying}">
									<option value="${action.actionId}">${action.company.symbol}&nbsp;&nbsp;&nbsp;&nbsp;${action.company.exchange.name}</option>
								</c:forEach>
							</select>
							
							Quantité <input type="text" name="volume"/>
							<input type="submit" name="actionSubmit" value="Acheter"/>
						</form>
						<a class="close-reveal-modal">&#215;</a>
					</div>
					
					<div id="sellModal" class="reveal-modal">
						<form method="post" action="TransactionServlet">
							
							<select name="actionChoose">
								<c:forEach var="action" items="${actionSelling}">
									<option value="${action.actionId}">${action.company.symbol}&nbsp;&nbsp;&nbsp;&nbsp;${action.company.exchange.name}</option>
								</c:forEach>
							</select>
							
							Quantité <input type="text" name="volume"/>
							<input type="submit"  name="actionSubmit" value="Vendre"/>
						</form>
						<a class="close-reveal-modal">&#215;</a>
					</div>
				
				</div>
			</div>
	
	
			
	
			<!-- Sidebar -->
			<div id="sidebar">
	
				<!-- Logo -->
				<div id="logo">
					<h1>PORTFOLIO</h1>
				</div>
	
				Bienvenue ${user.lastName}
	
				<!-- Nav -->
				<nav id="nav">
					<ul>
						<li><a href="exchange.do?exchange=Nasdaq">NASDAQ</a></li>
						<li><a href="exchange.do?exchange=Nyse">NYSE</a></li>
						<li><a href="exchange.do?exchange=Amex">AMEX</a></li>
						<li class="current_page_item"><a href="TransactionServlet">Actions</a></li>
						<li><a href="PortfolioServlet">Mon portfolio</a></li>
					</ul>
					
				</nav>
	
				<!-- Copyright -->
				<div id="copyright">
					<p>&copy; 2014 ABALINE &amp; AHOUNOU</p>
				</div>
	
			</div>
	
		</div>
	
	</body>

</html>