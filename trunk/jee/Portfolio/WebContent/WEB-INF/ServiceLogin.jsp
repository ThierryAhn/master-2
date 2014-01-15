<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
    pageEncoding="ISO-8859-1"%>
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<title>Login</title>
		
		<!-- css -->
		<link href="css/style.css" rel="stylesheet" type="text/css">
		
	</head>
	
	<body>
		<h3>Connectez vous pour accéder à Portfolio !</h3>
		<form method="post" action="UserServlet">
			
			<!-- login -->
			<input type="text" id="login" name="login" placeholder="Login" 
				autofocus  required />
			<br/>
			
			<!-- password -->
			<input type="password" id="password" name="password" 
				placeholder="Password" required />
			<br/>	
				
			<input type="submit" id="submit" name="submit" 
				value="Se connecter" />
			
		</form>	
		
		<a href="UserServlet"> Créer un compte </a>
		
		
	</body>
	
</html>