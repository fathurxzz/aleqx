<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="bigs.Helpers" %>
<%@ Import Namespace="bigs.Models" %>
<%@ Import Namespace="bigs.Controllers" %>
<%
    string shortLang = SystemSettings.CurrentLanguageShort;
    string controllerName = ViewContext.RouteData.Values["controller"].ToString().ToUpperInvariant();

    using (DataStorage context = new DataStorage())
    {
        List<SiteContent> siteContent = (from content in context.SiteContent.Include("Parent") where content.Parent.Name == controllerName && content.Language == SystemSettings.CurrentLanguage orderby content.SortOrder ascending select content).ToList();
        if (siteContent.Count > 0)
        {
            %>
            <div id="subMenu">
            <div id="subMenuTopBorder"></div>
            <div id="subMenuContainer">
            <%
            foreach (var content in siteContent)
            {

                string contentUrl = (string)ViewData["contentUrl"];

                string contentName = (from contentNames in context.SiteContent where contentNames.Url == contentUrl && contentNames.Language == SystemSettings.CurrentLanguage select contentNames.Name).First();

                if (content.Name != contentName)
                {
                    %>
                    <div class="subMenuLink">
                        <%=Html.ActionLink(content.Title, "Index", content.Parent.Name, new { contentUrl = content.Url }, null)%>
                        <%if (Request.IsAuthenticated)
                          { %>
                        <%=Html.ActionLink("x", "DeleteSubMenuItem", "Admin", new { id = content.Id, redirectUrl = Request.Url.AbsolutePath }, new { onclick = "return confirm('Удалить объект?');", style = "color:red" })%>
                        <%} %>
                    </div>
                    <%
                }
                else
                {
                    %>
                    <div class="subMenuLink active">
                    <%=content.Title%>
                    </div>
                    <%
                     
                }
                %>
                
                
                <%
            }
            %>
            
            <%
                using (Html.BeginForm("AddSubMenuItem", "Admin", FormMethod.Post))
                {
                    if (Request.IsAuthenticated)
                    {

                  %>
                    <%= Html.Hidden("redirectUrl", Request.Url.AbsolutePath) %>
                    <fieldset>
                    <legend style="font-size:x-small">Добавить пункт</legend>
                    Имя (в бд)<br />
                    <%=Html.TextBox("tName", "", new {size="13" })%>
                    <br /><br />
                    Название<br />
                    <%=Html.TextBox("tTitle", "", new { size = "13" })%>
                    <br />
                    
                    <input type="submit" value="Добавить"/>
                    </fieldset>
                <%}
              }%>
            </div>
            <div id="subMenuBottomBorder"></div>
            </div>
            <%
        }

    }
%>
