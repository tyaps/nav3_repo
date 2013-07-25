<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<purchasedItem>>" %>
<%@ Import Namespace="rovnicaMVC.Domain" %>
<%@ Import Namespace="rovnicaMVC.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Basket
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="basket">
    <h2>Корзина покупок</h2>
    
    <%if (Model.Count == 0)
      { %>
      <% 
          string s = "";
          
          if (ViewData["resultSuccess"] == null)
         {%>
      <div class="empty">Ваша корзина пуста</div>
      <%}
         else
         {
             if ((bool)ViewData["resultSuccess"] == true)
             {
             %>
             <div class="empty">Ваш заказ успешно зарегистрирован</div>
             <%}%>
             
      <%} %>
    <%}
      else
      {
          if (ViewData["resultSuccess"]!=null && (bool)ViewData["resultSuccess"] == false)
          {
          %>
          <div class="empty">Произошла ошибка при регистрации заказа</div>
          <%}else{ %>
    <div class="not_empty">Ваша корзина содержит следующие товары: </div>
    
    <table class="basketItems">
    <tr><th>Наименование</th><th>Цвет</th><th>Размер</th><th>Цена</th></tr>
    <%
          var summaryPrice=0;
    foreach (var item in Model)
      {%>
      <tr><td class="c1"><%=item.itemData.Name %></td><td class="c2"><%=item.colorName %></td><td class="c3"><%=item.sizeName %></td><td class="c4"><%=item.itemData.Price.ToString()%></td></tr>
     
    <%
        summaryPrice += item.itemData.Price;
    } %>
    <tr><td colspan="2"></td><td class="summary">Итого</td><td class="summaryPrice"><%=summaryPrice.ToString()%></td></tr>
    </table>

<div class="contactDataTitle">Укажите Ваши контактные данные:</div>

<% using (Html.BeginForm("CreateOrder", "Home"))
   {%>
<table class="contactData">
<tr><th>ФИО</th><td><input type="text" name="customerName" maxlength="50" /></td></tr>
<tr><th>Телефон</th><td><input type="text" name="customerPhone" maxlength="50"/></td></tr>
<tr><th>Email</th><td><input type="text" name="customerEmail" maxlength="50"/></td></tr>
<tr><th>Дополнительно</th><td><textarea name="comments" maxlength="5000"></textarea></td></tr>
<tr><td colspan="2"><input type="submit" value="" id="submit" /></td></tr>
</table>


<%} %>
<%} %>
<%} %>

</div>
</asp:Content>
