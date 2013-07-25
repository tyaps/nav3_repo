<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%--<ul class="selectedCategory<%= ViewData["catId"]!=null ? ViewData["catId"].ToString() : "" %>">
<li><%= Html.ActionLink(" ", "category", "Home", new { id = 0 }, new { @class = "lm_gallery" })%></li>
<li><%= Html.ActionLink(" ", "category", "Home", new { id = 1 }, new { @class = "lm_coat" })%></li>
<li><%= Html.ActionLink(" ", "category", "Home", new { id = 2 }, new { @class = "lm_cardigan" })%></li>
<li><%= Html.ActionLink(" ", "category", "Home", new { id = 3 }, new { @class = "lm_kofta_sviter" })%></li>
<li><%= Html.ActionLink(" ", "category", "Home", new { id = 4 }, new { @class = "lm_palantin_nakidka" })%></li>
<li><%= Html.ActionLink(" ", "category", "Home", new { id = 5 }, new { @class = "lm_platya" })%></li>
<li><%= Html.ActionLink(" ", "category", "Home", new { id = 6 }, new { @class = "lm_yubki" })%></li>
<li><%= Html.ActionLink(" ", "category", "Home", new { id = 7 }, new { @class = "lm_accessories" })%></li>
<li><%= Html.ActionLink(" ", "category", "Home", new { id = 8 }, new { @class = "lm_for_men" })%></li>
<li><%= Html.ActionLink(" ", "category", "Home", new { id = 0, status = 1 }, new { @class = "lm_availible" })%></li>
</ul>--%>


<ul class="selectedCategory<%= ViewData["catId"]!=null ? ViewData["catId"].ToString() : "" %>">
<li><%= Html.ActionLink("Вся одежда", "category", "Home", new { id = 0 }, new { @class = "lm_all" })%></li>
<li><%= Html.ActionLink("Пальто", "category",  new { id = 1 })%></li>
<li><%= Html.ActionLink("Кардиганы", "category",  new { id = 2 })%></li>
<li><%= Html.ActionLink("Кофты", "category",  new { id = 3 })%></li>
<li><%= Html.ActionLink("Палантины", "category",  new { id = 4 })%></li>
<li><%= Html.ActionLink("Платья", "category",  new { id = 5 })%></li>
<li><%= Html.ActionLink("Юбки", "category",  new { id = 6 })%></li>
<li><%= Html.ActionLink("Аксессуары", "category",  new { id = 7 })%></li>
<li><%= Html.ActionLink("Для мужчин", "category",  new { id = 8 })%></li>
<li><%= Html.ActionLink("В наличии", "category", "Home", new { id = 0, status = 1 }, new { @class = "lm_availible" })%></li>


<li><a href="<%=Url.Action("basket") %>" id="shopping_chart">Корзина</a></li>
</ul>