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
						<span class="byline"> Liste des sociétés </span>
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
								<th>Symbole</th>
								<th>Nom</th>
								<th>Market cap</th>
								<th>Secteur</th>
							</tr>
						</thead>

						<tbody>
							<c:forEach var="company" items="${listCompany}">
								<tr>
									<td>${company.symbol}</td>
									<td>${company.name}</td>
									<td>${company.marketCap}</td>
									<td>${company.sector}</td>
									<td>
										<a href="action.do?symbol=${company.symbol}">Histor actions</a>
									</td>
								</tr>
							</c:forEach>
						</tbody>
					</table>


				</article>

				<!-- pagination -->
				<div class="pager">
					<%--For displaying Previous link except for the previous page page --%>
					<c:if test="${currentPage != 1}">
						<a href="exchange.do?page=${currentPage - 1}&exchange=${currentExchange}"
							class="button previous"> Previous Page </a>
					</c:if>
	
					<div class="pages">
						<c:forEach begin="1" end="${noOfPages}" var="i">
							<c:choose>
								<c:when test="${currentPage eq i}">
									<a href="exchange.do?page=${i}&exchange=${currentExchange}"
										class="active">${i}</a>
								</c:when>
	
								<c:otherwise>
									<a href="exchange.do?page=${i}&exchange=${currentExchange}">${i}</a>
								</c:otherwise>
	
							</c:choose>
						</c:forEach>
					</div>
	
					<%--For displaying Next link --%>
					<c:if test="${currentPage lt noOfPages}">
						<a
							href="exchange.do?page=${currentPage + 1}&exchange=${currentExchange}"
							class="button next"> Next Page </a>
					</c:if>
				</div>
				<!-- end pagination -->

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
					<li class="current_page_item"><a
						href="exchange.do?exchange=Nasdaq">NASDAQ</a></li>
					<li><a href="exchange.do?exchange=Nyse">NYSE</a></li>
					<li><a href="exchange.do?exchange=Amex">AMEX</a></li>
					<li><a href="TransactionServlet">Actions</a></li>
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