<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <%Html.RenderPartial("Player");%>
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
                swfPath: "Content",
                backgroundColor: "#f1f1f1",
                ready: function () {


                    $(this).jPlayer("setMedia", {
                        mp3: "../Content/Files/01.mp3" // Defines the mp3 url

                    });


                }
            });

            $(".play").click(function () {
                var filename = $(this).parent().attr("filename");
                $("#jpId").jPlayer("setMedia", {
                    mp3: "../Content/Files/" + filename // Defines the mp3 url
                });
                $("#jpId").jPlayer("play", 0);
                $('.pause').each(function (index) {
                    $(this).removeClass("pause");
                    $(this).addClass("play");
                });
                $(this).removeClass("play");
                $(this).addClass("pause");
            });


            $(".stop").click(function () {
                $("#jpId").jPlayer("stop");
                $('.pause').each(function (index) {
                    $(this).removeClass("pause");
                    $(this).addClass("play");
                });
            });

        });

       

    </script>
</asp:Content>
