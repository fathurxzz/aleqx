<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    $(function () {

        $(".menuItem").mousemove(function () {
            $(this).addClass("selected");
            $("#projectPictureContainer").attr("src", "/Content/Photos/Projects/" + this.id+".gif");
        });
        $(".menuItem").mouseleave(function () {
            $(this).removeClass("selected");
        });
    });
  </script>

  <%
      var items = new List<Babich.Models.Content>();
      if (ViewData["subMenuItems"]!=null)
      
      items = (List<Babich.Models.Content>) ViewData["subMenuItems"];
       %>

<div id="projectsContainer">
    <div id="menu">

    <%
      int cnt = 1;
        foreach (var item in items)
{
          if(cnt>11)break;  
  %>
        <a id="<%=item.Id%>" href="#" class="menuItem <%=cnt==1?"selected":"" %>"><%=item.Title%></a>
        <div class="separator"></div>
  <%
    cnt++;
} %>

        
        


    </div>
    <div id="content">
        <img src="" alt="" id="projectPictureContainer" />
    </div>
</div>
