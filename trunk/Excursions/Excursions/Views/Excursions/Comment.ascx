<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Excursions.Models.Comments>" %>
<div class="comment">
    <div class="author">
        <%=Model.Author %>
        <div class="date">
            <%=Model.Date %>
        </div>
    </div>
    <%=Model.Text %>
</div>
