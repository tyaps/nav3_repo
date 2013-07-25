<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<cImage>" %>
<%@ Import Namespace="rovnicaMVC.Domain" %>
<%@ Import Namespace="rovnicaMVC.Models" %>


<table class="imgTemplate">
<tr><td class="imgNumber">#<%=Model.imgNumber %></td></tr>
<tr><td>Полная</td></tr>
<tr><td>

<form class="imageform" method="post" enctype="multipart/form-data" action="<%=Url.Action("uploadImage", new {itemId = Model.Item.Id, picNumber=Model.imgNumber, mode="img"})%>">
<input type="file" name="photoimg" class="photoimg" />
<div class="preview"><img src="<%=string.IsNullOrEmpty(Model.FileName) ? "" : Url.Content("~/Content/i/") + Model.FileName %>" class="resultImg" /></div>
</form>

</td></tr>
<tr><td>Превью</td></tr>
<tr><td>

<form class="imageform" method="post" enctype="multipart/form-data" action="<%=Url.Action("uploadImage", new {itemId = Model.Item.Id, picNumber=Model.imgNumber, mode="preview"})%>">
<input type="file" name="photoimg" class="photoimg" />
<div class="preview"><img src="<%=string.IsNullOrEmpty(Model.FileNamePreview) ? "" : Url.Content("~/Content/i/") + Model.FileNamePreview %>" class="resultImg" /></div>
</form>


</td></tr>
</table>
