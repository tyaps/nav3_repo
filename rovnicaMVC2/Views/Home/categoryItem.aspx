<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="rovnicaMVC.Domain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	category
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
    
        cItem item = (cItem)ViewData["item"];
        var colorList = new SelectList(item.Colors, "Id", "Name");
        var sizeList = new SelectList(item.Sizes, "Id", "Name");

       %>
 <div class="itemsContainer <%=(item.Images.Count <= 3) ? "" : "slideShow" %>">
        <div class="slideShowLeft"></div>
        <div class="slideShowRight"></div>
        <div class="slideShowRotator">
        <%
            var picWidth = 186;
            var picBorder = 2;
            var picMargin = 70;
            //var lastPicMargin = picMargin; //последняя картинка без отступа, но для ie отступ есть, поэтому 0
            var lastPicMargin = 0;
            var leftAndRightMargin = 0; //общий отступ от левого и правого края
             %>
        <div class="slideShowBody" style="width:<%=(item.Images.Count <= 3) ? "auto" : (item.Images.Count * (picWidth + picBorder + picMargin ) - lastPicMargin + (leftAndRightMargin)).ToString() + "px" %>">
         
         
         
         <ul class="items highslide-gallery">
    <%
      
       
        for (int i = 0; i < item.Images.Count; i++)
        {
     %>
     <li>
     
     <a href="<%=Url.Content("~/Content/i/" + item.Images[i].FileName) %>" class="highslide" onclick="return img_expand(this);" >

     <div class="itemPicture" style="background-image: url(<%=Url.Content("~/Content/i/" + item.Images[i].FileName) %>)"  ></div>
     </a>
     
     </li>
        
        <%} %>
        </ul>
        </div>
    </div>
</div> 
        
<div class="itemData">
    <h1 class="itemTitle"><%=item.Name %></h1>
    <div class="descriptionCommon"><%=item.DescriptionCommon %></div>
    <div class="descriptionMaterial"><span>Материалы:</span> <%=item.DescriptionMaterial %></div>
    <span class="color"><span>Цвет:</span> <%= Html.DropDownList("ddlColor", colorList)%> </span>
    <span class="size"><span>Размер:</span> <%= Html.DropDownList("ddlSize", sizeList)%> </span>
    
    
    <div class="price"><span>Цена:</span> <%=item.Price.ToString() %> <span class="priceOpt" onclick="$('.priceOptHelp').show();" title="Оптовая цена"> / (<%=item.PriceOpt.ToString()%>)</span>
    <span class="status s<%=item.Status %>" title="<%=item.Status==1 ? "Есть в наличии" : "Нет в наличии" %>">&nbsp;</span>
     </div>    
    <div class="btn_purchase" onclick="purchase(<%=item.Id %>)"><%--Купить--%></div>
    <%--<div class="priceOptHelp" onclick="$('.priceOptHelp').hide();">
    Условия оптовых продаж уточните по телефону в разделе <%=Html.ActionLink("Оптом", "Opt") %>
    </div>--%>
</div>


</asp:Content>
