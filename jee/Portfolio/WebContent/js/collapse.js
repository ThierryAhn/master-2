$(document).ready(function() {

	//Content boxes expand/collapse
	$(".initial-expand").hide();

	$("div.content-module-heading").click(function(){
		$(this).next("div.content-module-main").slideToggle();

		$(this).children(".expand-collapse-text").toggle();
	});
	
});


function toggle_visibility(id) {
	
	var e = document.getElementById(id);
	
	// opening account
	if(id == 'openHidden'){
		document.getElementById('withdrawHidden').style.display = 'none';
		document.getElementById('depositHidden').style.display = 'none';
		
		if(e.style.display == 'block')
			e.style.display = 'none';
		else
			e.style.display = 'block';
	}else{
		// deposit
		if(id == 'depositHidden'){
			document.getElementById('withdrawHidden').style.display = 'none';
			document.getElementById('openHidden').style.display = 'none';
			
			if(e.style.display == 'block')
				e.style.display = 'none';
			else
				e.style.display = 'block';
		}else{
			// deposit
			if(id == 'withdrawHidden'){
				document.getElementById('depositHidden').style.display = 'none';
				document.getElementById('openHidden').style.display = 'none';
				
				if(e.style.display == 'block')
					e.style.display = 'none';
				else
					e.style.display = 'block';
			}
		}
	}
}

function hide(id) {
	var e = document.getElementById(id);
	e.style.display = 'none';
}