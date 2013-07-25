<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<IList<cOrder>>" %>
<%@ Import Namespace="rovnicaMVC.Domain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	items
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  <%
        //список для дропдаунов в каждой строчке
        var listAction = new[] { 
   
    new  { Id = 1, Name = "Новые" }, 
    new  { Id = 2, Name = "В процессе" }, 
    new  { Id = 3, Name = "Сделаны" }, 
    new  { Id = 4, Name = "Закрыты" }
};

        var listUsers = new[] { 
   
    new  { Id = 1, Name = "Тяпкина" }, 
    new  { Id = 2, Name = "Светка" }, 
};
    
         %>
    <%using (Html.BeginForm()) { %>
    
    <%

        var list = new[] { 
    new  { Id = 5, Name = "Все" },   
    new  { Id = 1, Name = "Новые" }, 
    new  { Id = 2, Name = "В процессе" }, 
    new  { Id = 3, Name = "Сделаны" }, 
    new  { Id = 4, Name = "Закрыты" }
};

        var selectList = new SelectList(list, "Id", "Name", ViewData["currentStatus"]);

          
          
           %>
           
           <%= Html.DropDownList("status", selectList)%>
           
    
    
    <input type="submit" value="Поиск" />
    <%} 
        
        
        %>
    
    <table class="orders">
    <tr><th>№</th><th>Дата</th><th>ФИО</th><th>Исполнитель</th><th>Статус</th><th></th><th></th></tr>
   
    <%foreach (var o in Model)
      {%>
      <tr>
      <td><%=o.Id.ToString() %></td>
      <td><%=o.date.ToString() %></td>
      <td><%=o.customerName ?? "Неизвестный" %></td>
      <td>
      <% var selectList1 = new SelectList(listUsers, "Id", "Name", "1"); %>
      <%= Html.DropDownList("user", selectList1)%>
      </td>
      <td>
      <% var selectList2 = new SelectList(listAction, "Id", "Name", ((int)o.status).ToString());%>
      <%= Html.DropDownList("action", selectList2, new {@class="status" })%>
      
      </td>
      <td><input type="button" value="Применить" onclick="saveOrderDetails(<%=o.Id.ToString() %>, this)"/></td>
      <td><input type="button" value="Показать" onclick="showOrderDetails(<%=o.Id.ToString() %>)" /></td>
      </tr>
      
      <tr><td colspan="6">
      
      <table id="c<%=o.Id.ToString() %>" class="orderDetails">
      <tr><th>Телефон</th><th>Email</th><th>Комментарии</th></tr>
      <tr><td><%=o.customerPhone %></td><td><%=o.customerEmail %></td><td><%=o.comments %></td></tr>
      </table>
      
      <table id="d<%=o.Id.ToString() %>" class="details">
      <tr><th>Товар</th><th>Цена</th><th>Цвет</th><th>Размер</th></tr>
      
      <%foreach (var item in o.Items)
        { %>
      <tr><td><%=item.Item.Category.Name %> <b><%=item.Item.Name %></b></td><td><%=item.Item.Price %></td>
      <td><%=item.ColorName %></td><td><%=item.SizeName %></td></tr>
      <%} %>
      
      </table>
      
      </td></tr>
    <%} %>
     </table>
    </div>

</asp:Content>
