<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DjSzk.Models.MusicContent>" %>


<div class="mItem mItem<%=Model.Id%>"  filename="<%=Model.FileSource%>">
    <div class="playpauseContainer">
        <div class="play"></div>
    </div>
</div>
    <%if (Request.IsAuthenticated)
      { %>
    <div>
        <%=Model.SortOrder %>&nbsp;
        <%=Html.ActionLink("Редактировать", "EditMusicContent", "Content", new { id = Model.Id, area = "Admin" }, new { @class = "adminLink"})%>
        <%=Html.ActionLink("Удалить", "DeleteMusicContent", "Content", new { id = Model.Id, area = "Admin" }, new { @class = "adminLink", onclick = "return confirm('Вы действительно хотите удалить запись?')" })%>
    </div>
    <% } %>

<div style="height: 60px;"></div>