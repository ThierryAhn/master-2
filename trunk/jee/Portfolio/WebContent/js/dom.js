function toggle_visibility(id) {
	
	alert("function");
	
	var e = document.getElementById(id);
	
	// deposit
	if(id == 'depositHidden'){
		document.getElementById('withdrawHidden').style.display = 'none';
		document.getElementById('openHidden').style.display = 'none';
		
		if(e.style.display == 'block')
			e.style.display = 'none';
		else
			e.style.display = 'block';
	}
	
	// withdraw
	if(id == 'withdrawHidden'){
		document.getElementById('depositHidden').style.display = 'none';
		document.getElementById('openHidden').style.display = 'none';
		
		if(e.style.display == 'block')
			e.style.display = 'none';
		else
			e.style.display = 'block';
	}
	
	// opening account
	if(id == 'openHidden'){
		
		alert("here");
		
		document.getElementById('depositHidden').style.display = 'none';
		document.getElementById('withdrawHidden').style.display = 'none';
		
		if(e.style.display == 'block')
			e.style.display = 'none';
		else
			e.style.display = 'block';
	}
	
	// transaction
	if(id == 'transactionHidden'){
		document.getElementById('depositHidden').style.display = 'none';
		document.getElementById('withdrawHidden').style.display = 'none';
		document.getElementById('openHidden').style.display = 'none';
		
		if(e.style.display == 'block')
			e.style.display = 'none';
		else
			e.style.display = 'block';
	}
	
}

function hide(id) {
	var e = document.getElementById(id);
	e.style.display = 'none';
}