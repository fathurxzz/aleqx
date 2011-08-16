<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Exchange.Models.Views.SplitDeal>" %>

<%@ Import Namespace="Exchange.Models.Helpers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        <%=Html.ResourceString("DealEditing")%></h2>
    <%using (Html.BeginForm())
      {%>
    <%=Html.HiddenFor(x=>x.DealId) %>
    <%=Html.HiddenFor(x=>x.OrderId) %>
    <table>
        <tr>
            <th colspan="2"><%=Html.ResourceString("CurrentOrder")%></th>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("Number")%>
            </td>
            <td>
                <%=Model.CurrentOrder.Number%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("Currency")%>
            </td>
            <td>
                <%=Model.CurrentOrder.CurId%>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("DealSum")%>
            </td>
            <td>
                <%=Html.TextBox("newSum",Model.OldSum)%>
            </td>
        </tr>
        <tr>
            <th colspan="2"  style="padding-top:10px;">Перенести сделку в другое распоряжение</th>
        </tr>
        <tr>
            <td colspan="2">
                <table>
                    <tr>
                        <th><%=Html.ResourceString("Number") %></th>
                        <th><%=Html.ResourceString("Currency") %></th>
                        <th><%=Html.ResourceString("DocSum") %></th>
                        <th><%=Html.ResourceString("Course") %></th>
                        <th><%=Html.ResourceString("Equiv") %></th>
                    </tr>
                    <%foreach (var order in Model.MatchedOrders)
                      {
                    %>
                    <tr>
                        <td>
                            <%=order.Number %>
                        </td>
                        <td>
                            <%=order.CurId %>
                        </td>
                        <td class="sum">
                            <%=order.DocSum.ToString("N") %>
                        </td>
                        <td>
                            <%=order.Course %>
                        </td>
                        <td class="sum">
                            <%=order.Equivalent.ToString("N") %>
                        </td>
                    </tr>
                    <%
                        } 
                    %>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("OrderNumber") %>
            </td>
            <td>
                <%=Html.TextBoxFor(x=>x.NewOrderNumber) %>
            </td>
        </tr>
        <tr>
            <td>
                <%=Html.ResourceString("DealSum") %>
            </td>
            <td>
                <%=Html.TextBoxFor(x=>x.NewOrderDealSum) %>
            </td>
        </tr>
    </table>
    <div class="buttonsContainer">
        <%=Html.ResourceSubmitButton("Execute", FormAction.Execute)%>
        <span class="UIButton">
            <%=Html.ResourceActionLink("Back", "Index")%>
        </span>
    </div>
    <%
        }%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Includes" runat="server">
<%=Html.RegisterCss("Tenders.css") %>
</asp:Content>
