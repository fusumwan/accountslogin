window["app.domain.models"] = {
    uuidv4:function () {
		return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
			var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
			return v.toString(16);
		});
	},
	create:function(table,json_data){
		var _result=null;
		if(table!=undefined && json_data!=undefined){
			var fieldNames = Object.keys(json_data);
			var formData = new FormData();
			csrf_token=$('[name="__RequestVerificationToken"]').val()
        	console.log("CSRF Token:", csrf_token);
	        // add other form fields
	        formData.append("_method", "PUT"); // Simulate a PUT request
	        for(var i=0;i<fieldNames.length; i++ ){
				formData.append(fieldNames[i], json_data[fieldNames[i]]);
			}
			var method = "POST";
		    var tableName = table;
		    var CreateMethod = "create";
            var url = "";
	        if (app.runat != "" && app.runat != undefined) {
				url += app.runat;
			}
			if (tableName != "" && tableName!= undefined) {
				url += "/" + tableName + "/" + CreateMethod+ "/";
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
                	data=app.domain.utils.JWT.json_process_jwt(data);  
					_result=data;
	            },
	            error: function(xhr, status, error) {
	                console.log(xhr.responseText);
	            }
	        });
        }
		return _result;
	},
    get:function(table,datakeyname,id){
		var _result=null;
		if(app.runat!=undefined && table!=undefined){
			
			var formData = new FormData();
	        // add other form fields
	        formData.append("_method", "PUT"); // Simulate a PUT request
	        formData.append(datakeyname, id);
			csrf_token=$('[name="__RequestVerificationToken"]').val()
        	console.log("CSRF Token:", csrf_token);
			var method = "POST";
		    var tableName = table;
		    var GetMethod = "get";
			
            var url = "";
	        if (app.runat != "" && app.runat != undefined) {
				url += app.runat;
			}
			if (tableName != "" && tableName!= undefined) {
				url += "/" + tableName + "/" + GetMethod+ "/";
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
                	data=app.domain.utils.JWT.json_process_jwt(data);
					_result=data;
	            },
	            error: function(xhr, status, error) {
	                console.log(xhr.responseText);
	            }
	        });
        }
		return _result;
	},
    update:function(table,json_data){
		var _result=null;
		if(table!=undefined && json_data!=undefined){
			var fieldNames = Object.keys(json_data);
			var formData = new FormData();
	        // add other form fields
	        formData.append("_method", "PUT"); // Simulate a PUT request
			csrf_token=$('[name="__RequestVerificationToken"]').val()
        	console.log("CSRF Token:", csrf_token);
	        for(var i=0;i<fieldNames.length; i++ ){
				formData.append(fieldNames[i], json_data[fieldNames[i]]);
			}
			var method = "POST";
		    var tableName = table;
		    var UpdateMethod = "update";
            var url = "";
            if (app.runat != "" && app.runat != undefined) {
				url += app.runat;
			}
            if (tableName != "" && tableName!= undefined) {
                url += "/" + tableName + "/" + UpdateMethod+ "/";
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
                	data=app.domain.utils.JWT.json_process_jwt(data);
					_result=data;
	            },
	            error: function(xhr, status, error) {
	                console.log(xhr.responseText);
	            }
	        });
        }
		return _result;
	},
    delete:function(table,datakeyname,id){
		var _result=false;
		if(table!=undefined && datakeyname!=undefined && id!=undefined){
			var formData = new FormData();
	        // add other form fields
	        formData.append("_method", "PUT"); // Simulate a PUT request
	        formData.append(datakeyname, id);
			csrf_token=$('[name="__RequestVerificationToken"]').val()
        	console.log("CSRF Token:", csrf_token);
			var method = "POST";
		    var tableName = table;
		    var DeleteMethod = "delete";

            var url = "";
            if (app.runat != "" && app.runat != undefined) {
				url += app.runat;
			}
            if (tableName != "" && tableName!= undefined) {
                url += "/" + tableName + "/" + DeleteMethod+ "/";
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
					data=app.domain.utils.JWT.json_process_jwt(data);
					_result=true;
	            },
	            error: function(xhr, status, error) {
					_result=false;
	                console.log(xhr.responseText);
	            }
	        });
        }
		return _result;
	},
    mapInputTypes:[
    {
        "TableName": "account",
        "Fields": [
            {
                "FieldName": "account_id",
                "HtmlInputType": "text"
            },
            {
                "FieldName": "first_name",
                "HtmlInputType": "text"
            },
            {
                "FieldName": "last_name",
                "HtmlInputType": "text"
            },
            {
                "FieldName": "username",
                "HtmlInputType": "text"
            },
            {
                "FieldName": "email",
                "HtmlInputType": "text"
            },
            {
                "FieldName": "contact_number",
                "HtmlInputType": "text"
            },
            {
                "FieldName": "password",
                "HtmlInputType": "text"
            },
            {
                "FieldName": "user_type",
                "HtmlInputType": "text"
            }
        ]
    }
	]
    ,
	account: {
		new: function() {
			var obj = {
			"account_id": null,
			"first_name": ""
			,
			"last_name": ""
			,
			"username": ""
			,
			"email": ""
			,
			"contact_number": ""
			,
			"password": ""
			,
			"user_type": "" };
			return obj;
		},
		create: function(data) {
			return app.domain.models.create("account", data);
		},
		get: function(id) {
			return app.domain.models.get("account","account_id", id);
		},
		update: function(data) {
			return app.domain.models.update("account", data);
		},
		delete: function(id) {
			return app.domain.models.delete("account","account_id", id);
		}
	}
};
