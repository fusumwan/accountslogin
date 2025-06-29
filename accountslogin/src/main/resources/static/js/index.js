/**************************************************
 // Author: Sum Wan,FU
 // Date: 27-5-2019
 // Description: Index javascript
 **************************************************/

window.addEventListener("load", function(){
    document.getElementById("home_li").className="active";
    //-- script-for-search --
	document.getElementById("search_btn").addEventListener("click", function(event){
    	error.protection.preventDefault(event);

    	window.UserSession=app.domain.utils.GetUserSession();
		if(UserSession==undefined || UserSession.account_id=="" || UserSession.account_id==null){		    
		    var result = confirm('Please kindly login our OrderTable account system!');
	        if(result) {
	            // User clicked 'OK'
	            window.location=contextPath+"/login";
	        } else {
	            // User clicked 'Cancel'
	            alert("If you don't have an account yet, please register!");
	        }
		}else{
			search();
		}

    });
	//-- script-for-search --
});

function search(){
	var search_criteria_1=document.getElementById("search_criteria_1_txt");
	var search_criteria_2=document.getElementById("search_criteria_2_drp");
	if(search_criteria_1!=undefined && search_criteria_2!=undefined){
		if(search_criteria_1.value!="" && search_criteria_2.value!=""){
			var PageSession = app.domain.utils.EmptyPageSession();
			PageSession.index["search_criteria_1"]=search_criteria_1.value;
			PageSession.index["search_criteria_2"]= search_criteria_2.value;
			var PageSession=app.domain.utils.SetPageSession(PageSession);
			if(PageSession!=null && PageSession!=undefined){
				window.location=window.contextPath+"/result";
			}
		}
	}
}