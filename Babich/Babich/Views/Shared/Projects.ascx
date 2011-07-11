<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    $(function () {

        $(".menuItem").mousemove(function () {
            $(this).addClass("selected");
            $("#projectPictureContainer").attr("src", "/Content/img/projects/" + this.id+".gif");
        });
        $(".menuItem").mouseleave(function () {
            $(this).removeClass("selected");
        });
    });
  </script>

<div id="projectsContainer">
    <div id="menu">



    <%
//      int cnt = 1;
//        foreach (var item in Model)
//{
//          if(cnt>11)break;  
  %>
        <a id="p1" href="#" class="menuItem "></a>
        <div class="separator"></div>
        <a id="p2" href="#" class="menuItem "></a>
        <div class="separator"></div>
        <a id="p3" href="#" class="menuItem "></a>
        <div class="separator"></div>
        <a id="p4" href="#" class="menuItem "></a>
        <div class="separator"></div>
        <a id="p5" href="#" class="menuItem "></a>
        <div class="separator"></div>
        <a id="p6" href="#" class="menuItem "></a>
        <div class="separator"></div>
        <a id="p7" href="#" class="menuItem "></a>
        <div class="separator"></div>
        <a id="p8" href="#" class="menuItem "></a>
        <div class="separator"></div>
        <a id="p9" href="#" class="menuItem "></a>
        <div class="separator"></div>
        <a id="p10" href="#" class="menuItem "></a>
        <div class="separator"></div>
        <a id="p11" href="#" class="menuItem "></a>
  <%
    //cnt++;
//}
       %>

        
        


    </div>
    <div id="content">
        <img src="../../Content/img/projects/p1.gif" alt="" id="projectPictureContainer" />
    </div>
</div>
