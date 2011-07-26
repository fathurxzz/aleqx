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
                backgroundColor:"#f1f1f1",
                ready: function () {
                    /*$(this).jPlayer("setMedia", {
                        mp3: "../Content/Files/01.mp3" // Defines the mp3 url

                    }
                    );*/
                }
            });

            //$("#jpId").css("display", "none");

            $("#playPauseButton").click(function () {
                
                $("#jpId").jPlayer("play", 0);
            });

            
            $("#stopButton").click(function () {
                $("#jpId").jPlayer("stop");
            });
            

        });

       

    </script>
</asp:Content>
