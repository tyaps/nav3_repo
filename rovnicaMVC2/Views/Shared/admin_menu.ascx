<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%--<ul id="categories_menu">
<li class="<%= ViewData["selectedCatMenu"]=="1" ? "selected" : "" %>"><%= Html.ActionLink("Пальто", "category", "Home", new { id = 1 }, null)%></li>
<li class="<%= ViewData["selectedCatMenu"]=="2" ? "selected" : "" %>"><%= Html.ActionLink("Кофты", "category", "Home", new { id = 2 }, null)%></li>
<li class="<%= ViewData["selectedCatMenu"]=="3" ? "selected" : "" %>"><%= Html.ActionLink("Юбки", "category", "Home", new { id = 3 }, null)%></li>


</ul>--%>
<ul class="selectedCategory<%= ViewData["catId"]!=null ? ViewData["catId"].ToString() : "" %>">
<li><%= Html.ActionLink("Заказы", "index", "Admin")%></li>
<li><%= Html.ActionLink("Товары", "items", "Admin")%></li>
<li><%= Html.ActionLink("Пользователи", "index", "Admin")%></li>

</ul>