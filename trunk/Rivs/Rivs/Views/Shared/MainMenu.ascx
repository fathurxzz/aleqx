<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="menuBox">

    <div class="menuHeader">
    ������� ����
    </div>
    
    <ul class="menu">
        <li><a href="#">�������</a></li>
        <li><a href="#">�������</a></li>
        <li><a href="#">����������� �����������</a></li>
        <li><a href="#">�������� �����</a></li>
    </ul>
    
    <%
        if(Request.IsAuthenticated)
        {%>
        <div>
        <%=Html.ActionLink("[��������]", "AddContentItem", "Admin", null, new { @class = "adminLink" })%>
        </div>
        <%
        }        
    %>

</div>