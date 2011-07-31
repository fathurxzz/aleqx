<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DjSzk.Models.Content>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <%=Model.PageTitle%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentTitle">
    <%=Model.Title%>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainMenu" runat="server">
    <% Html.RenderPartial("MainMenu"); %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="jpId">
    </div>
    <%if (Request.IsAuthenticated)
      { %>
    <%=Html.ActionLink("редактировать", "Edit", "Content", new { area = "Admin", id = Model.Id }, new { @class = "adminLink" })%>
    <% } %>
    <%if (!string.IsNullOrEmpty(Model.Text))
      { %>
    <div class="descriptionContainer">
        <%=Model.Text%>
    </div>
    <% } %>
    <% foreach (var mc in Model.MusicContent.OrderBy(c => c.SortOrder))
       {
           Html.RenderPartial("MusicItem", mc);
       } %>
    <%if (Request.IsAuthenticated)
      { %>
    <%=Html.ActionLink("добавить музыкальный контент", "AddMusicContent", "Content",
                                                   new {area = "Admin", id = Model.Id}, new {@class = "adminLink"})%>
    <% } %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Includes">
    <script type="text/javascript">
        /*
        $(function () {


        $("#playPause").click(function () {
        $("#mPlayer").attr("src", "");
        alert("playPause");
        });

        $("#stop").click(function () {
        $("#mPlayer").attr("src", "");
        alert("stop");
        $("#mPlayer").removeAttr("src");
        });

        });
        */

        $(function () {

            $("#jpId").jPlayer({
                swfPath: "../Content",
                backgroundColor: "#f1f1f1"/*,
                ready: function () {

                    
                    $(this).jPlayer("setMedia", {
                    mp3: "../Content/Files/01.mp3" // Defines the mp3 url

                    });
                    

                }*/
            });

            $(".play").click(function () {
                var filename = $(this).parent().parent().attr("filename");

                if ($(this).hasClass("play")) {

                    if (!$(this).hasClass("paused")) {
                        $("#jpId").jPlayer("setMedia", {
                            mp3: "../Content/Files/" + filename // Defines the mp3 url
                        });
                    }
                    
                    
                    
                    
                    
                    
                    


                    //alert($(obj > div).html());



                    if (!$(this).hasClass("paused")) {
                        $('.stop').each(function (index) {
                            $(this).removeClass("stop").addClass("szk");
                        });
                        var obj = $('.szk', $(this).parent().parent().parent());
                        $(obj).fadeOut("fast", function() {
                            obj.removeClass("szk").addClass("stop");
                            $(obj).fadeIn("fast", function() {

                            });
                        });
                    }


                    $("#jpId").jPlayer("play");
                    $('.pause').each(function (index) {
                        $(this).removeClass("pause");
                        $(this).removeClass("paused");
                        $(this).addClass("play");
                    });
                    /*
                    $(this).fadeIn('slow', function () {

                    });
                    */
                    //$(this).fadeTo("slow", 0.1);


                    $(this).fadeOut("fast", function () {
                        //alert('ok');
                        $(this).removeClass("play");
                        $(this).addClass("pause");
                        $(this).fadeIn("fast", function () {

                        });
                    });



                    //$(this).removeClass("play");
                    //$(this).addClass("pause");
                }
                else if ($(this).hasClass("pause")) {
                    $("#jpId").jPlayer("pause");
                    $(this).removeClass("pause");
                    $(this).addClass("play");
                    $(this).addClass("paused");
                }
            });





            $(".stopbutton").click(function () {
                if ($(this).hasClass("stop")) {
                    $("#jpId").jPlayer("stop");
                    $(this).removeClass("stop").addClass("szk");
                    $('.pause').each(function (index) {
                        $(this).removeClass("pause");
                        $(this).removeClass("paused");
                        $(this).addClass("play");
                    });
                    
                    $('.play').each(function (index) {
                        $(this).removeClass("pause");
                        $(this).removeClass("paused");
                    });
                    
                }
            });

        });

       

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="RootLink" runat="server">
    <%
               
        if (Model.Id != 1)
        {%>
    <a href="/" class="rootLink">
        <img src="../../Content/img/pixel.gif" alt="" /></a>
    <%
        }%>
</asp:Content>
