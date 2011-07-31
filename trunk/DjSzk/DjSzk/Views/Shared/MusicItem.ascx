<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DjSzk.Models.MusicContent>" %>
<div class="playerContainer">
    <table class="player">
        <tr>
            <td filename="<%=Model.FileSource%>">
                <div class="buttonLeft">
                    <div class="play">
                    </div>
                </div>
            </td>
            <td>
                <div class="bg">
                    <%=Model.Title%>
                </div>
            </td>
            <td>
                <div class="buttonRight">
                    <div class="szk stopbutton">
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <%if (Request.IsAuthenticated)
      { %>
    <div>
        <%=Model.SortOrder %>&nbsp;
        <%=Html.ActionLink("Редактировать", "EditMusicContent", "Content", new { id = Model.Id, area = "Admin" }, new { @class = "adminLink"})%>
        <%=Html.ActionLink("Удалить", "DeleteMusicContent", "Content", new { id = Model.Id, area = "Admin" }, new { @class = "adminLink", onclick = "return confirm('Вы действительно хотите удалить запись?')" })%>
    </div>
    <% } %>
</div>
<%if (!string.IsNullOrEmpty(Model.Description))
  { %>
<div class="descriptionContainer separator">
    <%=Model.Description%>
</div>
<% } %>