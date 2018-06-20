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
<%! User user; %>
<%! user = (User) session.getAttribute("user"); %>

</body>

</html>

