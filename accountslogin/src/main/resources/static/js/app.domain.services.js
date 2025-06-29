window["app.domain.services"]={
	BusinessIntelligence:{
        UpdateAccount:function(account){
            var result = null;
            result=app.domain.models.account.update(account);
            return result;
        },
        SignIn:function(account){
            account=app.domain.models.account.create(account);
            if (account!=null){
                return true;
            }
            return false;
        },
        Login: function(username, password, callback) {
            var method = "POST";
            var data = JSON.stringify({
                username: username,
                password: password
            });
            var url = window.contextPath + "/authenticateTheUser/";
            csrf_token=$('[name="__RequestVerificationToken"]').val()
            console.log("CSRF Token:", csrf_token);
            $.ajax({
                url: url,
                type: method,
                contentType: "application/json", // Ensure the content type is set to application/json
                data: data, // Use the JSON stringified data directly
                dataType: "json", // Expect a JSON response
                async: false, // Consider using async: true in production for better user experience
                beforeSend: function(xhr) {
                    xhr.setRequestHeader("RequestVerificationToken", csrf_token);
                },
                success: function(response) {
                    response = app.domain.utils.JWT.json_callback_process_jwt(response);
                    callback(response);
                },
                error: function(xhr, status, error) {
                    console.error("Error: ", error);
                    console.error("Status: ", status);
                    console.error("Response: ", xhr.responseText);
                }
            });
        },
        LoginWithOutJWT:function(username,password, callback){
            var method = "POST";
            var data = {
                username: username,
                password: password
            };
            var url = window.contextPath + "/authenticateTheUser/";
            csrf_token=$('[name="__RequestVerificationToken"]').val()
            console.log("CSRF Token:", csrf_token);
            $.ajax({
                url: url,
                type: method,
                dataType: "json",
                contentType: "application/json", // Specify the data type being sent
                data: JSON.stringify(data), // Convert the JavaScript object into a JSON string
                async: false,
                beforeSend: function(xhr) {
                    xhr.setRequestHeader("X-CSRFToken", csrf_token);
                },
                success: function(response) {
                    callback(response);
                },
                error: function(xhr, status, error) {
                    console.error("Status: ", status);
                    console.error("Error: ", error);
                    console.error("Response: ", xhr.responseText);
                }
            });
        },
        Logout:function(callback){
            csrf_token=$('[name="__RequestVerificationToken"]').val()
            console.log("CSRF Token:", csrf_token);
            // Send POST request using Fetch API and use contextPath variable
            $.ajax({
                url: contextPath + "/logout/",
                type: "POST",
                contentType: "application/x-www-form-urlencoded",
                data: null, // Since you have body as null, you might omit this if there's really no data to send
                beforeSend: function(xhr) {
                    xhr.setRequestHeader("X-CSRFToken", csrf_token);
                },
                success: function(response) {
                    callback(response);
                },
                error: function(xhr, status, error) {
                    console.error('Error:', error);
                }
            });
        }
    },
    commonservice:{
    },
	accountservice:{
        getByAccountUsernamePassword:function(username_00,password_01,Limit){
            var result = null;
            result=app.domain.models.repositories.accountdao.getByAccountUsernamePassword(username_00,password_01,Limit);
            return result;
        }
		,
        getByAccountUsername:function(username_00,Limit){
            var result = null;
            result=app.domain.models.repositories.accountdao.getByAccountUsername(username_00,Limit);
            return result;
        }
		,
        getByAccount:function(Limit){
            var result = null;
            result=app.domain.models.repositories.accountdao.getByAccount(Limit);
            return result;
        }
	}
}
