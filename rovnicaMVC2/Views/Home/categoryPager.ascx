<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<rovnicaMVC.Models.categoryInfo>" %>

<ul>
     <%


         //стрелка влево
         
             %>
             <li><a href="<%=Url.Action("category", new { id = Model.categoryId, pageNumber = Model.pager.currentPage-1 }) %>" <%=(Model.pager.currentPage==1) ? "class='leftArrow disabled' onclick='return false;'" : "class='leftArrow'" %>> < </a></li>
             <%
         
         for (int i = 1; i <= Model.pager.pageTotalCount; i++)
         {
             //многоточие до currentPage
             if (i != 1 && (i + 1) < Model.pager.currentPage)
             {
                 if (i == 2) //нарисовать многоточие только один раз, остальные цифры просто пропускать
                 {%>
                    <li>...</li>
                 <%}
                 continue;
             }

             //многоточие после currentPage
             if (i != Model.pager.pageTotalCount && (i - 1) > Model.pager.currentPage)
             {
                 if (i == Model.pager.pageTotalCount-1) //нарисовать многоточие только один раз, остальные цифры просто пропускать
                 {%>
                    <li>...</li>
                 <%}
                 continue;
             }
                 
             %>
            <li><%= Html.ActionLink(i.ToString(), "category", new { id = Model.categoryId, pageNumber = i }, new { @class = (i == Model.pager.currentPage) ? "current" : "" })%></li>
   
            <%
         }
         //стрелка вправо
         %>
             <li><a href="<%=Url.Action("category", new { id = Model.categoryId, pageNumber = Model.pager.currentPage+1 }) %>" <%=(Model.pager.currentPage==Model.pager.pageTotalCount) ? "class='leftArrow disabled' onclick='return false;'" : "class='leftArrow'" %>> > </a></li>
             
     </ul>
