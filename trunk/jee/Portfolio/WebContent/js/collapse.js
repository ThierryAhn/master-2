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