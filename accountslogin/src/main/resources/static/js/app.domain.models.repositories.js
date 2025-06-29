window["app.domain.models.repositories"]={
	accountdao:{
        getByAccountUsernamePassword:function(username_00,password_01,Limit){
            var formData = new FormData();
            // add other form fields
            formData.append("_method", "PUT"); // Simulate a PUT request
            formData.append("username_00", username_00);
            formData.append("password_01", password_01);
            formData.append("Limit", Limit);
            csrf_token=$('[name="__RequestVerificationToken"]').val()
            console.log("CSRF Token:", csrf_token);
            var method = "POST";
            var tableName = "account";
            var getMethod = "getByAccountUsernamePassword";
            var result=null;
            var url="";
            if(app.runat!="" && app.runat!=undefined){
                url +=  app.runat;
            }
            if(tableName!="" && tableName!=undefined){
                url += "/" + tableName+ "/" + getMethod+ "/";
            }
            $.ajax({
                url: url,
                type: method,
                dataType: "json",
                data: formData,
                processData: false,
                contentType: false,
                headers: app.domain.utils.JWT.headers("FORMDATA"),
                async: false,
                beforeSend: function(xhr) {
                    xhr.setRequestHeader("X-CSRFToken", csrf_token);
                },
                success: function(data) {
                    console.log(data);
                    data=app.domain.utils.JWT.json_process_jwt(data);
				    result=data;
                },
                error: function(xhr, status, error) {
                    alert('An error occurred while loading the ' + tableName);
                    console.log(xhr.responseText);
                }
            });
            return result;
        }
		,
        getByAccountUsername:function(username_00,Limit){
            var formData = new FormData();
            // add other form fields
            formData.append("_method", "PUT"); // Simulate a PUT request
            formData.append("username_00", username_00);
            formData.append("Limit", Limit);
            csrf_token=$('[name="__RequestVerificationToken"]').val()
            console.log("CSRF Token:", csrf_token);

            var method = "POST";
            var tableName = "account";
            var getMethod = "getByAccountUsername";
            var result=null;
            var url="";
            if(app.runat!="" && app.runat!=undefined){
                url +=  app.runat;
            }
            if(tableName!="" && tableName!=undefined){
                url += "/" + tableName+ "/" + getMethod+ "/";
            }
            $.ajax({
                url: url,
                type: method,
                dataType: "json",
                data: formData,
                processData: false,
                contentType: false,
                headers: app.domain.utils.JWT.headers("FORMDATA"),
                async: false,
                beforeSend: function(xhr) {
                    xhr.setRequestHeader("X-CSRFToken", csrf_token);
                },
                success: function(data) {
                    console.log(data);
                    data=app.domain.utils.JWT.json_process_jwt(data);
				    result=data;
                },
                error: function(xhr, status, error) {
                    alert('An error occurred while loading the ' + tableName);
                    console.log(xhr.responseText);
                }
            });
            return result;
        }
		,
        getByAccount:function(Limit){
            var formData = new FormData();
            // add other form fields
            formData.append("_method", "PUT"); // Simulate a PUT request
            formData.append("Limit", Limit);
            csrf_token=$('[name="__RequestVerificationToken"]').val()
            console.log("CSRF Token:", csrf_token);
            var method = "POST";
            var tableName = "account";
            var getMethod = "getByAccount";
            var result=null;
            var url="";
            if(app.runat!="" && app.runat!=undefined){
                url +=  app.runat;
            }
            if(tableName!="" && tableName!=undefined){
                url += "/" + tableName+ "/" + getMethod+ "/";
            }
            $.ajax({
                url: url,
                type: method,
                dataType: "json",
                data: formData,
                processData: false,
                contentType: false,
                headers: app.domain.utils.JWT.headers("FORMDATA"),
                async: false,
                beforeSend: function(xhr) {
                    xhr.setRequestHeader("X-CSRFToken", csrf_token);
                },
                success: function(data) {
                    console.log(data);
                    data=app.domain.utils.JWT.json_process_jwt(data);
				    result=data;
                },
                error: function(xhr, status, error) {
                    alert('An error occurred while loading the ' + tableName);
                    console.log(xhr.responseText);
                }
            });
            return result;
        }
	}
}
