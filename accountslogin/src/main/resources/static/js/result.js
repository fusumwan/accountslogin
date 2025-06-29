$( document ).ready(function() {
	document.getElementById("banner").className="banner banner_small"
    document.getElementById("search-container").style.display="none";
    
    
	var UserSession=app.domain.utils.GetUserSession();
	if(UserSession==undefined || UserSession.account_id==""){
	    alert("Please kindly login our OrderTable account system!");
	    window.location=contextPath+"/login";
	}
}); 