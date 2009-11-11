<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<CollectStickers.Models.StickerPresentation>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	UserPage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>UserPage</h2>

    <table>
        <tr>
            <th>
                Number
            </th>
            <th>
                isNeed
            </th>
            <th>
                isFree
            </th>
        </tr>



    <% 
        bool existitem = false;
        for (int i = 1; i <= 564; i++)
        {
            existitem = false;
            if(Model!=null)
            {
                foreach (var item in Model)
                {
                    if (item.Number == i)
                    {
                        existitem = true;
                %>
                <tr>
                    <td>
                        <%= Html.Encode(item.Number)%>
                    </td>
                    <td>
                        <%= Html.CheckBox("chNeed_" + i, item.isNeed)%>
                    </td>
                    <td>
                        <%= Html.CheckBox("chFree_" + i, item.isFree)%>
                    </td>
                </tr>
                <%
                    }
                
                }
            }

            if (!existitem)
            {
                %>
                <tr>
                    <td>
                        <%= Html.Encode(i)%>
                    </td>
                    <td>
                        <%= Html.CheckBox("chNeed_" + i, false)%>
                    </td>
                    <td>
                        <%= Html.CheckBox("chFree_" + i, false)%>
                    </td>
                </tr>
                <%
            }
        }
        %>
    </table>

    <p>
        <%= Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="leftMenuContent" runat="server">
</asp:Content>

