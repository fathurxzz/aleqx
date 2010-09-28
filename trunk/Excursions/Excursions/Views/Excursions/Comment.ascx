<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Excursions.Models.Comments>" %>
<div class="comment">
    <div class="author">
        <%=Model.Author %>

        <%
            if(Request.IsAuthenticated)
            {
                %>
                <%= Html.ActionLink("Удалить", "Delete", "Comments", new { area = "Admin", id = Model.Id, excursionId = Model.Excursion.Id }, new { @class = "adminLink", onclick = "return confirm('Удалить комментарий?')" })%><br/>
                <%
            }
        %>
        <div class="date">
            <%=Model.Date %>
        </div>
    </div>
    <%=Model.Text %>
</div>
