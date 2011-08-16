<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Exchange.Models.Client>>" %>
<%@ Import Namespace="Exchange.Models.Helpers" %>
<%
    if (Model == null)
    {
        Response.Write(ViewData["errorMsg"]);
    }
    else if (Model.Count() == 0)
    {
    }
    else
    {
        var clientList = Model.Select(item => 
            new ClientsSelectListItem
                {
                    Text = item.ClientName, 
                    Value = item.ClientName, 
                    CntrCode = item.ContrCode, 
                    SubjId = item.SubjId, 
                    Okpo = item.ClientCode
                }).ToList();


        var s = new StringBuilder();
        foreach (var item in clientList)
        {
            s.AppendFormat("<option value=\"{0}\" subjId=\"{1}\" cntrCode=\"{2}\" okpo=\"{3}\" {4} >{5}</option>", HttpUtility.HtmlEncode(item.Value), item.SubjId, item.CntrCode, item.Okpo, item.Selected ? " selected " : "", item.Text);
        }
        Response.Write(s.ToString());
    }%>