<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<categoryInfo>" %>
<%@ Import Namespace="rovnicaMVC.Models" %>
<%@ Import Namespace="rovnicaMVC.Domain" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	category
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="category">

<div class="current_title">
<div class="title"><%=ViewData["selectedCategory"]%></div>
<div class="searchForm">
<%using (Html.BeginForm(new{id = Model.categoryId})){ %>
Цена от
<input type="text" id="PriceFromStr" name="PriceFromStr" value="<%=ViewData["PriceFrom"] %>"/>
до 

<input type="text" id="PriceToStr" name="PriceToStr" value="<%=ViewData["PriceTo"] %>"/>
<input type="submit" value="" class="search_button"/>
<%} %>
</div>
<div class="expand_collapse" onclick="$('.category .current_title').toggleClass('expanded')">Поиск</div>
</div>
    <div class="categoryItems">
    <ul class="items">
    <%
        for(int i = 0; i<Model.items.Count; i++)
        {
            var item = Model.items[i];
            if (i%3 == 0 && i>0)
            {%><li class="separator"></li><%}
     %>
     <li>
     
     <a href="<%= Url.Action("categoryItem", "Home", new { id = item.Id })%>">
 
     <%if(item.Images.Count>0) {%>
    <div class="itemPicture" style="background-image: url(<%=Url.Content("~/Content/i/" + item.Images[0].FileName) %>)"></div> 
    <%} else {%> 
    <div class="itemPicture" ></div>
    <%} %>
    
     </a>
     
     <%=Html.ActionLink("\"" + item.Name + "\"", "categoryItem", "Home", new { id = item.Id }, new { @class="title"})%>
     
     <div class="price"><%=item.Price %> / (<%=item.PriceOpt %>)<span class="status s<%=item.Status %>" title="<%=item.Status==1 ? "Есть в наличии" : "Нет в наличии. Готовы выполнить на заказ" %>">&nbsp;</span></div>
     
     </li>
     <%} %>
     </ul>
     </div>
     <div class="pager"><%Html.RenderPartial("categoryPager"); %></div>
</div>
</asp:Content>
