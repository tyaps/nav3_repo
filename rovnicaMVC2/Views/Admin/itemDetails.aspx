<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<admin_itemDetails>" %>
<%@ Import Namespace="rovnicaMVC.Domain" %>
<%@ Import Namespace="rovnicaMVC.Models" %>
<%
var selectList = new SelectList(Model.categories, "Id", "Name", Model.item.Category.Id);
var selectListColors = new SelectList(Model.item.Colors, "Id", "Name");
var selectListSizes = new SelectList(Model.item.Sizes, "Id", "Name");

var listStatus = new[] { 
   
    new  { Id = 1, Name = "В наличии" }, 
    new  { Id = 2, Name = "Нет в наличии" }, 
    new  { Id = 3, Name = "Не показывать" }
     
};

var selectListStatus = new SelectList(listStatus, "Id", "Name", Model.item.Status);
 %>
<div id="itemDetails">
<input type="button" id="btnCloseItemDetails" value="Закрыть" onclick="$(this).parent().remove();" />

<div class="title">Товар: <%=Model.item.Name%></div>

<div class="tabList">
<ul><li onclick="showItemDetailsTab(0)" class="selected">Общие</li>
<li onclick="showItemDetailsTab(1)">Изображения</li>
<li onclick="showItemDetailsTab(2); showWarehouseTotalAmount(<%=Model.item.Id%>);">Склад</li></ul>
</div>

<%--<div class="sectionTitle">Изображения товара: <input type="button" value="Скруть!" onclick="$('#itemImagesHolder, #btnAddMoreImageTemplate').toggle();" /> --%>
<%--Доступное кол-во: <input type="button" value="Показать" onclick="showWarehouseTotalAmount(<%=Model.item.Id%>)" /></div>--%>

<div class="tab selected">

<div>
<%foreach (var img in Model.item.Images.Take(8))
  { %>
  <img src="<%=string.IsNullOrEmpty(img.FileNamePreview) ? "" : Url.Content("~/Content/i/") + img.FileNamePreview %>" class="reviewImg" />
  <%} %>
</div>

<div id="itemDetailsHolder">
<table class="params">
<colgroup><col width="200px" /><col /></colgroup>
<tr><th>Название</th><td><input type="text" id="tbName" value="<%=Model.item.Name %>" /></td></tr>
<tr><th>Категория</th><td><%= Html.DropDownList("ddlCategory", selectList)%></td></tr>
<tr><th>Статус</th><td><%= Html.DropDownList("ddlStatus", selectListStatus)%></td></tr>
<tr><th>Цена Розничная</th><td><input type="text" id="tbPrice" value="<%=Model.item.Price %>" /></td></tr>
<tr><th>Цена Оптовая</th><td><input type="text" id="tbPriceOpt" value="<%=Model.item.PriceOpt %>" /></td></tr>
<tr><th>Описание</th><td><textarea id="tbDescriptionCommon"><%=Model.item.DescriptionCommon %></textarea></td></tr>
<tr><th>Материалы</th><td><textarea id="tbDescriptionMaterial"><%=Model.item.DescriptionMaterial %></textarea></td></tr>
<tr><th>Доступные цвета</th><td>

<table>
<tr><td><input type="text" id="tbAddColor" /><input type="button" value="+" onclick="addColor();"/><input type="button" value="-" onclick="removeColor();"/></td><td></td></tr>
<tr><td colspan="2"><%= Html.ListBox("lbColors", selectListColors)%></td></tr>
</table>
</td></tr>

<tr><th>Доступные Размеры</th><td>

<table>
<tr><td><input type="text" id="tbAddSize" /></td><td><input type="button" value="+" onclick="addSize();"/><input type="button" value="-" onclick="removeSize();"/></td></tr>
<tr><td colspan="2"><%= Html.ListBox("lbSizes", selectListSizes)%></td></tr>
</table>
</td></tr>


</table>
</div>
<input type="button" id="btnSave" value="Сохранить" onclick="saveItemDetails(<%=Model.item.Id %>);"/>
<input type="button" id="btnCancel" value="Отмена" onclick="$(this).parent().remove();"/>

</div>

<div class="tab">

<div id="itemImagesHolder">
<table id="itemImages">
<tr>
<%foreach (var img in Model.item.Images)
  { %>
  <td>
  <%Html.RenderPartial("addImageTemplate", img); %>
  </td>
<%} %>


</tr>
</table>
</div>

<input type="button" id="btnAddMoreImageTemplate" value="Добавить еще изображение" onclick="addMoreImageTemplate(<%=Model.item.Id %>)" />

</div>

<div class="tab" id="warehouse">
<!-- Для подгрузки Склада-->
</div>

</div>





