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

</div>
<div class="descriptionContainer separator">
<%=Model.Description %>
</div>