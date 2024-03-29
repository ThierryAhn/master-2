<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
	
	
<%@ taglib uri="http://java.sun.com/jsp/jstl/core" prefix="c"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt"%>
<%@ taglib uri="http://java.sun.com/jsp/jstl/functions" prefix="fn"%>

<!DOCTYPE HTML>
<html>
	<head>
	
		<title>LogOn</title>
		
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<meta name="description" content="" />
		<meta name="keywords" content="" />
		
		<!-- js -->
		<script src="js/jquery.min.js"></script>
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
								Veuillez vous connecter pour acc�der � votre portefeuille !
							</span>
						</header>
						
						<div class="info">
							
							<ul class="page-name">
								<li class="letter-name">L</li>
								<li class="letter-name">O</li>
								<li class="letter-name">G</li>
								<li class="letter-name">I</li>
								<li class="letter-name">N</li>
							</ul>
						</div>
						
						<div>
							
							<!--<div id="exchange-images">
								<div><img class="roundedImage" src="images/nasdaq.jpg" alt="Nasdaq"/></div>
								<div><img class="roundedImage" src="images/nyse.jpg" alt="Nyse"/></div>
								<div><img class="roundedImage" src="images/amex.jpg" alt="Amex"/> </div>
							</div> -->
							
							<div>
								<form action="UserServlet" method="POST" id="form">
			
									<p>
										<label for="login">Login</label>
										<input type="text" id="login" name="login" 
										class="input-large round full-width-input" autofocus required/>
									</p>
						
									<p>
										<label for="login">password</label>
										<input type="password" id="password" name="password" 
											class="input-large round full-width-input" required/>
									</p>
								
									<p style="color:red;">${erreur}</p>
								
									<input type="submit" name="submit" 
										class="input-large button round image-right ic-right-arrow" 
										value="Se connecter" />
								
									<!--<p>Nouveau <a href="UserServlet">Cr�er un compte</a>.</p> -->
									
									<p>Nouveau <a href="UserServlet">Cr�er un compte</a>.</p>
									
									
								</form>
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