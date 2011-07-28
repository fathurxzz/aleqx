<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DjSzk.Models.MusicContent>" %>

<div class="playerContainer">


<table class="player">
    <tr>
        <td filename="<%=Model.FileSource%>">
            <a href="#" class="play" >
            </a>
        </td>
        <td>
            <div class="bg">
                <%=Model.Title%>
            </div>
        </td>
        <td>
            <a href="#" class="stop">
            </a>
        </td>
    </tr>
</table>
<%if (Request.IsAuthenticated)
  { %>
  <div>
  <%=Model.SortOrder %>
  </div>
  <div>
  <%=Html.ActionLink("Удалить", "DeleteMusicContent", "Content", new { id = Model.Id, area = "Admin" }, new { @class = "adminLink", onclick = "return confirm('Вы действительно хотите удалить запись?')" })%>
  </div>

<% } %>
</div>
<div class="descriptionContainer separator">
<%=Model.Description %>
</div>