<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<admin_items>" %>
<%@ Import Namespace="rovnicaMVC.Domain" %>
<%@ Import Namespace="rovnicaMVC.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	items
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<ul class="categories">
<%
    var selectList = new SelectList(Model.categories, "Id", "Name");
    
    foreach (var c in Model.categories)
  {%>
<li><%=Html.ActionLink(c.Name,"items",new{Id=c.Id}) %>  </li>
<%} %>
</ul>
    
    
    <table class="categoryItems">
    <tr><th>Товар</th><th>Цена</th><th></th></tr>
    
    <%foreach (var item in Model.items)
  {%>
<tr><td><%=item.Name %></td><td><%=item.Price.ToString() %></td><td><input type="button" value="Изменить" onclick="showItemDetails(<%=item.Id %>)" /></td></tr>
<tr class="d<%=item.Id.ToString() %>"><td colspan="3"></td></tr>
<%} %>
    
</table>

<div class="createNewItem">
<div class="title">Создать товар</div>
<table>
<tr><th>Категория:</th><td><%= Html.DropDownList("ddlCategory", selectList)%></td></tr>
<tr><th>Наименование:</th><td><input type="text" id="tbItemName" /></td></tr>
<tr><th>Цена:</th><td> <input type="text" id="tbItemPrice" /></td></tr>
<tr><th></th><td><input type="button" value="Сохранить" onclick="createNewItem()" /></td></tr>
</table>
<div class="help_comment">После сохранения будут доступны дополнительные параметры</div>
</div>


</asp:Content>
