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
								Liste des sociétés
							</span>
						</header>
						
						<div class="info">
							
							<ul class="page-name">
								<li class="letter-name">N</li>
								<li class="letter-name">A</li>
								<li class="letter-name">S</li>
								<li class="letter-name">D</li>
								<li class="letter-name">A</li>
								<li class="letter-name">Q</li>
							</ul>
						</div>
						
						
						<!-- table -->
						<table class="style1">
								<thead>
									<tr>
										<th>Nom</th>
										<th>Dernière vente</th>
										<th>Volume</th>
										<th>(Aujourd'hui)Haut/Bas</th>
										<th>Cours-marché</th>
									</tr>
								</thead>
								
								<tbody>
									<c:forEach var="action"  items="${actionList}" >
										<tr>
											<td>${action.company.name}(${action.company.symbol})</td>
											<td>${action.company.lastSale}</td>
											<td>${action.volume}</td>
											<td>${action.high}/${action.low}</td>
											<td>${action.company.marketCap}</td>
										</tr>
									</c:forEach>
								</tbody>
						</table>
						
						
					</article>
	
					<!-- Pager -->
					<!-- <div class="pager">
						<a href="#" class="button previous">Previous Page</a>
						<div class="pages">
							<a href="#" class="active">1</a> <a href="#">2</a> <a href="#">3</a>
							<a href="#">4</a> <span>&hellip;</span> <a href="#">20</a>
						</div>
						<a href="#" class="button next">Next Page</a>
					</div>   -->
	
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
						<li class="current_page_item"><a href="exchange.do?exchange=Nasdaq">NASDAQ</a></li>
						<li><a href="exchange.do?exchange=Nyse">NYSE</a></li>
						<li><a href="exchange.do?exchange=Amex">AMEX</a></li>
					</ul>
					
					<ul>
						<li><a href="PortfolioServlet">Mon portfolio</a></li>
					</ul>
				</nav>
	
				<!-- Search -->
				<section class="is-search">
					<form method="post" action="#">
						<input type="text" class=" input-large text" name="search" placeholder="Search" />
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