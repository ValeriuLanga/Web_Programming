<%@ page import="domain.User" %>

<%--
  Created by IntelliJ IDEA.
  User: vlanga
  Date: 20-Jun-18
  Time: 9:03 PM
  To change this template use File | Settings | File Templates.
--%>

<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Title</title>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="./libs/jquery/1.10.1/jquery.min.js"></script>
    <script src="ajax_utils.js"></script>

</head>

<body>
    <% User user; %>
    <% user = (User) session.getAttribute("user"); %>

    <form class="container">
        <button type="button" id="show_urls_button" class="btn btn-info">Display urls</button>
        <br>
    </form>

    <form class="container" id="add_" style="align-self: center">
        <div class="col-xs-2">
            <label>Url:</label>
            <input type="text" id="url_input" class="form-control"/>
        </div>

        <div class="col-xs-5">
            <label>Topic description:</label>
            <input type="text" id="topic-description" class="form-control"/>
        </div>

        <br>
        <button type="button" id="add_url_button" class="col-xs-1 btn btn-info">Add url</button>

    </form>

    <br>
    <form class="container" id="action-result-section"></form>

    <form id="submit-form" action="UrlController" method="post">
        Enter id : <input id="topic-id" type="text" name="id" class="form-control"> <br>
        <input type="submit" value="Login" style="max-width: 100px" class="btn btn-info"/>
    </form>

</body>

</html>

