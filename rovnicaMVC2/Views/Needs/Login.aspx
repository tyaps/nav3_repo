﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login</title>
</head>
<body>
    <div>
    Login
    <%using (Html.BeginForm())
      {%>
    <input type="text" name="userName" />
    <input type="password" name="password" />
    <input type="submit" value="Войти" />
    
    <%} %>
    </div>
</body>
</html>
