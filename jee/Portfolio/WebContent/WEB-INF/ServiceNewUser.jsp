<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
	pageEncoding="ISO-8859-1"%>
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
		<title>Nouveau client</title>
		<!-- css -->
		
	</head>
	
	<body>
		<h3>Créer un nouveau compte !</h3>
		<form method="post" action="UserServlet">
			
			<!-- nom -->
			<label>Nom</label>
			<br/>
			<input type="text" id="lastName" name="lastName" 
				placeholder="Prenom" required />
			
			<input type="text" id="firstName" name="firstName" 
				placeholder="Nom de famille" required />
			<br/>
			
			<!-- login -->
			<label for="login">Login</label>
			<br/>
			<input type="text" id="login" name="login" required />
			<br/>
			
			<!-- password -->
			<label for="password">Mot de passe</label>
			<br/>
			<input type="password" id="password" name="password" required />
			<br/>
			
			<!-- confirm password -->
			<label for="confirmPassword">Confirmer votre mot de passe !</label>
			<br/>
			<input type="password" id="confirmPassword" name="confirmPassword" 
				required />
			<br/>
			
			<!-- address -->
			<fieldset>
				<legend> Adresse </legend>
				
				<label>Rue</label>
				<br/>
				<input type="text" id="number" name="number" 
					placeholder="Numero"/>
				
				<input type="text" id="street" name="street" 
					placeholder="Nom de la rue"/>
				<br/>	
				
				<label>Localisation</label>
				<br/>
				<input type="text" id="city" name="city" 
					placeholder="Ville"/>
				
				<input type="text" id="country" name="country" 
					placeholder="Pays"/>
			</fieldset>
			
			
			
			<input type="submit" name="submit" value="S'inscrire" />
					
		</form>	
	</body>
</html>