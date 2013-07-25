<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<ul>
<li class="<%= ViewData["selectedMenu"]=="main" ? "selected" : "" %>"><%= Html.ActionLink("Главная", "Index", "Home")%></li>
<li class="<%= ViewData["selectedMenu"]=="about" ? "selected" : "" %>"><%= Html.ActionLink("О нас", "about", "Home") %></li>
<li class="<%= ViewData["selectedMenu"]=="contacts" ? "selected" : "" %>"><%= Html.ActionLink("Контакты", "contacts", "Home") %></li>
<li class="<%= ViewData["selectedMenu"]=="opt" ? "selected" : "" %>"><%= Html.ActionLink("Оптом", "opt", "Home") %></li>
</ul>

<%--<ul id="menu_additional">
<li><a href="#">Вязание</a></li>
<li><a href="#">Шитье</a></li>
</ul>--%>