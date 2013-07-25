<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Needs.Master" Inherits="System.Web.Mvc.ViewPage<p_needs>"%>
<%@ Import Namespace="rovnicaMVC.Domain" %>
<%@ Import Namespace="rovnicaMVC.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	items
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="user_permissions">
<input type="checkbox" id="user_is_admin"  checked="checked"/><label for="user_is_admin">Вид для волонтера штаба</label>
</div>

<div class="feed" id="hq_need">
<div class="feed_filter">
<table cellpadding="0" cellspacing="0">
<tr><th><%=Model.test%>Приоритет</th><th>Готовность</th><th>Мое участие</th></tr>
<tr><td><select id="hq_need_filter_sel_priority"><option value="-1">Все</option><option value="1">Было бы не плохо</option><option value="2">Достаточно важно</option><option value="3">Критично</option></select></td>
<td><select id="hq_need_filter_sel_readiness"><option value="-1">Все</option><option value="0">0%</option><option value="1">20%</option><option value="2">40%</option><option value="3">60%</option><option value="4">80%</option><option value="5">100%</option></select></td>
<td><select id="hq_need_filter_sel_participation"><option value="-1">Все</option><option value="1">Я участвую</option><option value="2">Я не участвую</option></select></td>
</tr>
</table>
</div>

<div class="item header" >
<table cellpadding="0" cellspacing="0" class="">
<tr><td class="title">Наименование</td><td class="amount">Кол-во</td><td class="deadline"> Срок </td>
<td class="details">Участники (новые)</td><td class="buttons"></td></tr>
</table>
</div>
<%foreach (var item in Model.hq_needs)
  { %>
<div class="item" id="i<%=item.Id %>">
<table cellpadding="0" cellspacing="0" class="">
<tr><td class="priority priority<%=(int)item.Priority %>"  title="Достаточно важно"></td><td class="title"><%=item.Title%></td><td class="amount"><%=item.Amount %></td><td class="deadline"> <%=Formatting.hq_need_dueDate(item.DueDate) %> </td>
<td class="details">
<table>
<tr><td class="total_people_expexted"  colspan="2" title="Ожидается участников">3</td><td class="total_amount_expexted"  colspan="2" title="Ожидается кол-во">3 </td></tr>
<tr><td class="experience3" title="Надежные"></td><td class="experience2" title="Проверенные"></td><td class="experience1" title="Опыт 2-3">1</td><td class="experience0" title="Новые">2</td></tr>
</table>
<div class="total_readiness state3"><span>готовность 60%</span></div>
<div class="total_income state3"><span>получение 60%</span></div>
</td><td class="buttons">
<table cellpadding="0" cellspacing="2">
<tr ><td><a href="javascript:void(0);" class="button btn_participate">Я приду</a></td></tr>
<tr ><td><a href="javascript:void(0);" class="button btn_info">Инфо</a></td></tr>
<tr ><td><a href="javascript:void(0);" class="button btn_update">Изменить</a></td></tr>
</table>
</td></tr>
</table>
</div>
<%} %>

<div class="item" id="i755">

<div class="my_participation" title="Я участвую"></div>

<table cellpadding="0" cellspacing="0" class="">
<tr><td class="priority priority3" title="Критично"></td><td class="title ">Картриджди для HP 1010</td><td class="amount">10</td><td class="deadline"> 17.07.2013 </td>
<td class="details">
<table>
<tr><td class="total_people_expexted"  colspan="2" title="Ожидается участников">1</td><td class="total_amount_expexted"  colspan="2" title="Ожидается кол-во">2</td></tr>
<tr><td class="experience3" title="Надежные"></td><td class="experience2" title="Проверенные"></td><td class="experience1" title="Опыт 2-3">1</td><td class="experience0" title="Новые"></td></tr>
</table>
<div class="total_readiness state1"><span>готовность 20%</span></div>
<div class="total_income state0"><span>получение 40%</span></div>
</td><td class="buttons">
<table cellpadding="0" cellspacing="2">
<tr ><td><a href="javascript:void(0);" class="button btn_participate">Я приду</a></td></tr>
<tr ><td><a href="javascript:void(0);" class="button btn_info">Инфо</a></td></tr>
<tr ><td><a href="javascript:void(0);" class="button btn_update">Изменить</a></td></tr>
</table>
</td></tr>
</table>
</div>


<div class="item" id="i753">
<table cellpadding="0" cellspacing="0" class="">
<tr><td class="priority priority1" title="Было бы не плохо"></td><td class="title">Бумага для принтера (пачки)</td><td class="amount">50</td><td class="deadline"> - </td>
<td class="details">
<table>
<tr><td class="total_people_expexted"  colspan="2" title="Ожидается участников">3 (1)</td><td class="total_amount_expexted"  colspan="2" title="Ожидается кол-во">32 (12)</td></tr>
<tr><td class="experience3" title="Надежные">2</td><td class="experience2" title="Проверенные">3</td><td class="experience1" title="Опыт 2-3">5</td><td class="experience0" title="Новые">15</td></tr>
</table>
<div class="total_readiness state4"><span>готовность 80%</span></div>
<div class="total_income state2"><span>получение 40%</span></div>
</td><td class="buttons">
<table cellpadding="0" cellspacing="2">
<tr ><td><a href="javascript:void(0);" class="button btn_participate">Я приду</a></td></tr>
<tr ><td><a href="javascript:void(0);" class="button btn_info">Инфо</a></td></tr>
<tr ><td><a href="javascript:void(0);" class="button btn_update">Изменить</a></td></tr>
</table>
</td></tr>
</table>
</div>

</div>

<div id="participate_window">
<div class="username">Сергей Иванов</div>
<table cellpadding="0" cellspacing="0">
<tr><th>Кол-во</th><td><input type="text" id="participate_window_amount" /></td></tr>
<tr><th>Дата/время</th><td><input type="text" id="participate_window_date" /> &nbsp; <input type="text" id="participate_window_time" /></td></tr>
<tr><th>Дополнительно</th><td><textarea id="participate_window_description"></textarea></td></tr>
</table>

<p class="description">
Укажите количество предметов, которые Вы хотите предоставить и время, когда Вы можете их подвести в штаб. Если Вы не можете 
доставить эти вещи сами, то напишите, откуда мы сможем их вывезти с помощью волонтеров.
</p>

<a href="javascript:void(0);" id="participate_window_btn_save" class="button">Сохранить</a>
<a href="javascript:void(0);" id="participate_window_btn_cancel" class="button">Отмена</a>

</div>



<div id="expected_people_window">
<div class="title">Список предполагаемых участников</div>
<table cellpadding="0" cellspacing="0">
<tr><th class="th1"></th><th class="th2">Имя</th><th class="th3">Кол-во</th><th class="th4">Дата</th><th class="th5">Телефон</th><th class="th6">Email</th><th class="th7">Статус</th></tr>
<tr id="i55" class="complete"><td ><div class="experience experience1" title="Опыт 2-3"></div></td><td >Сергей Скворцов</td><td>5</td><td>Сегодня 15:50</td><td>8 926 000 0000</td><td>skv@mail.ru</td><td class="td_buttons"><a href="javascript:void(0);" class="button btn_approve" style="display:none;">Принес</a><a href="javascript:void(0);" class="button btn_discard" style="display:none;">Отменить</a><a href="javascript:void(0);" class="button btn_back">Вернуть</a></td></tr>
<tr id="i56"><td ><div class="experience experience1" title="Опыт 2-3"></div></td><td>Буря Мглою</td><td>5</td><td>Сегодня 12:00</td><td>8 925 000 0230</td><td>bour@mail.ru</td><td class="td_buttons"><a href="javascript:void(0);" class="button btn_approve" >Принес</a><a href="javascript:void(0);" class="button btn_discard">Отменить</a><a href="javascript:void(0);" class="button btn_back invisible">Вернуть</a></td></tr>
<tr id="i57"><td ><div class="experience experience3" title="Надежный"></div></td><td>Александр Зверь</td><td>5</td><td>Завтра 19:50</td><td>8 926 022 0100</td><td>medved@mail.ru</td><td class="td_buttons"><a href="javascript:void(0);" class="button btn_approve">Принес</a><a href="javascript:void(0);" class="button btn_discard">Отменить</a><a href="javascript:void(0);" class="button btn_back invisible">Вернуть</a></td></tr>
<tr class="description"><td colspan="7" >Заберите все с ул. Самотечная 15, а то колесо сломалось</td></tr>
<tr id="i58"><td ><div class="experience experience3" title="Надежный"></div></td><td>Ходор Ондарчук</td><td>5</td><td class="overdue">Сегодня 8:50</td><td>8 926 100 0100</td><td >hodd@mail.ru</td><td class="td_buttons"><a href="javascript:void(0);" class="button btn_approve">Принес</a><a href="javascript:void(0);" class="button btn_discard">Отменить</a><a href="javascript:void(0);" class="button btn_back invisible">Вернуть</a></td></tr>
<tr id="i59"><td><div class="experience experience0" title="Новичок"></div></td><td>Иван Капустин</td><td>5</td><td>3 дня (19:40)</td><td>8 925 033 0150</td><td>onemore@mail.ru</td><td class="td_buttons"><a href="javascript:void(0);" class="button btn_approve">Принес</a><a href="javascript:void(0);" class="button btn_discard">Отменить</a><a href="javascript:void(0);" class="button btn_back invisible">Вернуть</a></td></tr>


</table>

<a href="javascript:void(0);" id="expected_people_window_btn_close" class="button">Закрыть</a>

</div>



<div id="hq_need_item_edit_form">
<div class="title">Редактирование</div>
<table cellpadding="0" cellspacing="0">
<tr><th>Ожидаемая готовнось</th><td>0%</td><td>20%</td><td>40%</td><td>60%</td><td>80%</td><td>100%</td></tr>
<tr><th></th>
<td><input type="radio" name="rb_readiness_state" id="rs0" checked="checked" /></td>
<td><input type="radio" name="rb_readiness_state" id="rs1" /></td>
<td><input type="radio" name="rb_readiness_state" id="rs2" /></td>
<td><input type="radio" name="rb_readiness_state" id="rs3" /></td>
<td><input type="radio" name="rb_readiness_state" id="rs4" /></td>
<td><input type="radio" name="rb_readiness_state" id="rs5" /></td></tr>
</table>

<a href="javascript:void(0);" id="hq_need_item_edit_form_btn_save" class="button">Сохранить</a>
<a href="javascript:void(0);" id="hq_need_item_edit_form_btn_cancel" class="button">Отмена</a>

</div>

</asp:Content>
