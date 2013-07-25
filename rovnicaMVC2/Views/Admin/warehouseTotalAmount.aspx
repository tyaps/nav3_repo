<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<admin_itemWareHouseAmount>" %>
<%@ Import Namespace="rovnicaMVC.Domain" %>
<%@ Import Namespace="rovnicaMVC.Models" %>

<div id="warehouseTotalAmount">

<table>
<tr>
<th></th>
<%foreach (var s in Model.item.Sizes)
  { %>
  <th><%=s.Name %></th>
<%} %>
</tr>

<%foreach (var c in Model.item.Colors)
  { %>
  <tr><th class="color"><%=c.Name %></th>
  
  <%foreach (var s in Model.item.Sizes)
  { 
    var w = Model.warehouseAmounts.Where(wa => wa.ColorId == c.Id && wa.SizeId == s.Id).Single();
    
        %>
  <td>
  <input type="text" value="<%=w.Amount.ToString() %>" waId="<%=w.Id.ToString() %>" />
  <input type="checkbox" title="Есть в наличии" <%=(w.Status == WarehouseAmoountTotalStatus.Exists) ? "checked" : "" %>/>
  </td>
<%} %>
  
  </tr>
<%} %>


</table>






<input type="button" id="btnSave" value="Сохранить" onclick="saveWarehouseAmounts();"/>
<input type="button" id="btnCancel" value="Отмена" onclick="$(this).parent().remove();"/>


</div>





