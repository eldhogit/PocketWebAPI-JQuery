$(document).ready(function () {

    $('#btnLogin').click(function () {

        var bearer = "";
        var credentials = {
            grant_type: 'password',
            username: $('#txtUsername').val(),
            password: $('#txtPassword').val(),
        };

        //$.ajax({
        //    type: 'POST',
        //    url: 'token',
        //    data: credentials,
        //    contentType: "application/x-www-form-urlencoded",
        //    dataType: "json",
        //    success: function (data) {
        //        console.log('success');
        //        console.table({ data });
        //        bearer = JSON.parse(JSON.stringify(data));
        //        bearer = bearer.access_token;
               
        //    },
        //    failure: function (response) {
        //        alert(response.responseText);
        //    },
        //    error: function (response) {
        //        alert(response.responseText);
        //    }

        //});

        $.ajax({
            type: 'POST',
            url: 'token',
            data: credentials,
            contentType: "application/x-www-form-urlencoded",
            dataType: "json",
      

        }).done(function (data) {
            console.table(data);
            self.user(data.sUsername);
            // Cache the access token in session storage.
            sessionStorage.setItem(tokenKey, data.access_token);
            }).fail(function () {
                console.log('fail');
            });

    });

});


