<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<bigs.Models.ButtonStatuses>>" %>
<script src="../../Scripts/jquery.js" type="text/javascript"></script>
<script src="../../Scripts/MicrosoftAjax.js" type="text/javascript"></script>
<script src="../../Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
<script type="text/javascript">
    var enables = {};
    function collectStatusChanges() {
        var enablities = $get("enablities");
        enablities.value = Sys.Serialization.JavaScriptSerializer.serialize(enables);
        return true;
    }

    function updateEnables(check, id) {
        if (check.checked) {
            enables[id] = true;
        }
        else {
            enables[id] = false;
        }
    }
</script>

<div id="buttonContainer">

<%
    
    using (Html.BeginForm())
    {

        if (Request.IsAuthenticated)
        {
            %>
            <%= Html.Hidden("enablities")%>
            
            <input type="submit" value="Сохранить" onclick="return collectStatusChanges()"/>
            <br />
            <%
        }

        if (Model != null)
            foreach (var button in Model)
            {
                if (Request.IsAuthenticated)
                    Response.Write(Html.CheckBox("cb" + button.Id, button.SwitchedOn, new { onclick = "updateEnables(this, " + button.Id + ")" }));
                string color = button.SwitchedOn ? "green" : "red";

                using (bigs.Models.DataStorage context = new bigs.Models.DataStorage())
                {
                    string contentUrl = (from contentNames in context.SiteContent where contentNames.Name == button.ControllerName && contentNames.Language == bigs.Controllers.SystemSettings.CurrentLanguage select contentNames.Url).First();

                    Response.Write(button.SwitchedOn || Request.IsAuthenticated ? Html.ActionLink(" ", "Index", button.ControllerName,  new{contentUrl = contentUrl}, new { @class = button.Language.Substring(0, 2) + button.Name + color }) : "<a class=\"" + button.Language.Substring(0, 2) + button.Name + color + "\"></a>");
                }
            }
    }
%>
</div>