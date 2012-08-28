<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<DjSzk.Models.MusicContent>" %>


<div class="mItem mItem<%=Model.Id%>"  filename="<%=Model.FileSource%>">
    <div class="playpauseContainer">
        <div class="play"></div>
    </div>
</div>
<div style="height: 60px;"></div>