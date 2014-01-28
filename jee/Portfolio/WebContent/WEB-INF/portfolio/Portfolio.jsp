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
		<script src="js/jquery.min.js"></script>
		<script src="js/skel.min.js"></script>
		<script src="js/skel-panels.min.js"></script>
		<script src="js/init.js"></script>
		<script src="js/collapse.js"></script>
		
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
		<link rel="stylesheet" href="css/style-login.css" />
		
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
							<span class="byline">
								Mon portefeuille boursier
							</span>
						</header>
						
						<div class="info">
							
							<ul class="page-name">
								<li class="letter-name">P</li>
								<li class="letter-name">O</li>
								<li class="letter-name">R</li>
								<li class="letter-name">T</li>
								<li class="letter-name">F</li>
								<li class="letter-name">O</li>
								<li class="letter-name">L</li>
								<li class="letter-name">I</li>
								<li class="letter-name">O</li>
								
							</ul>
						</div>
						
						<div class="size-column fl">
							<div class="content-module">
								
								<div class="content-module-heading cf">
									
									<h3 class="fl">
										Mon Compte
										<c:if test="${not empty user.account.amount}">
											,&nbsp; ${user.account.amount} &#36;
										</c:if>		
									</h3>
									
									<span class="fr expand-collapse-text">Fermer</span>
									<span class="fr expand-collapse-text initial-expand">Ouvrir</span>
								</div>
								
								
								<div class="content-module-main cf">
									<!-- account -->
									<c:choose>
										<c:when test="${empty user.account}">
											<a href="#" onclick="toggle_visibility('openHidden');">
												 Ouvrir compte
											</a>
										</c:when>
										<c:otherwise>
											<a href="AccountServlet?userId=${user.userId}"> 
												Fermer compte 
											</a>
										</c:otherwise>
									</c:choose>
									
									<br/>
									
									<c:if test="${not empty user.account}">
										<a href="#" onclick="toggle_visibility('depositHidden');">Déposer</a> - 
										<a href="#" onclick="toggle_visibility('withdrawHidden');">Retirer</a>
									</c:if>
									
									<!-- open account -->
									<div id="openHidden" hidden=hidden>
										<form method="post" action="AccountServlet">
											Montant initial <input type="text" name="amount"/>
													
											<br/>
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
											Montant <input type="text" name="amount"/>
													
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
											Montant <input type="text" name="amount"/>
													
											<input type="submit" name="accountSubmit" value="Retirer"/>
											<button type="button" onclick="hide('withdrawHidden');">
												Cancel
											</button>
										</form>
										<hr/>
									</div>
								
								</div>
							</div>
						</div>
					</article>
	
				</div>
			</div>
	
			<!-- Sidebar -->
			<div id="sidebar">
	
				<!-- Logo -->
				<div id="logo">
					<h1>PORTFOLIO</h1>
				</div>
				<br/>
				
				Bienvenue ${user.lastName}
	
				<!-- Nav -->
				<nav id="nav">
					<ul>
						<li><a href="exchange.do?exchange=Nasdaq">NASDAQ</a></li>
						<li><a href="exchange.do?exchange=Nyse">NYSE</a></li>
						<li><a href="exchange.do?exchange=Amex">AMEX</a></li>
					</ul>
						
					<ul>
						<li class="current_page_item"><a href="PortfolioServlet">Mon portfolio</a></li>
					</ul>
					
				</nav>
	
				<!-- Search -->
				<section class="is-search">
					<form method="post" action="#">
						<input type="text" class="input-large text" name="search" placeholder="Search" />
					</form>
				</section>
	
				<!-- Text -->
				<section class="is-text-style1">
					<div class="inner">
						<p>
							<strong>Info:</strong> Achat
						</p>
					</div>
				</section>
				
				<!-- Copyright -->
				<div id="copyright">
					<p>
						&copy; 2014 ABALINE &amp; AHOUNOU
					</p>
				</div>
	
			</div>
	
		</div>
	
	</body>

</html>