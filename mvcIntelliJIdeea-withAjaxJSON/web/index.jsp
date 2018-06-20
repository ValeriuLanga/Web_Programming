<%--
  Created by IntelliJ IDEA.
  User: vlanga
  Date: 20-Jun-18
  Time: 7:21 PM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
    <head>
        <title>Login Page For URL Manager</title>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    </head>

    <body style="text-align: center">

    <div class="container" style="width: 350px">
        <form action="LoginController" method="post">
        Enter username : <input type="text" name="userName" class="form-control"> <br>
        Enter password : <input type="password" name="password" class="form-control"> <br>
        <input type="submit" value="Login" style="max-width: 100px" class="btn btn-info"/>
        </form>
    </div>

    </body>
</html>
