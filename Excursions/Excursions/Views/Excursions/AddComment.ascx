<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Excursions.Models.AddCommentFormModel>" %>
<script type="text/javascript" src="/Scripts/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/Scripts/MicrosoftMvcValidation.js"></script>
    <script type="text/javascript" src="/Scripts/MvcCaptchaValidation.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-1.4.1.js"></script>
    <script type="text/javascript">
        function OnCaptchaValidationError() {
            $.get("/Captcha/Draw", function (data) { $("#captchaImage").html(data); });
        }
    </script>
    <% Html.EnableClientValidation(); %>
    <% using (Html.BeginForm()) {
           
           
           
           %>
        <%= Html.ValidationSummary(true) %>
        <%=Html.Hidden("excursionId", ViewData["excursionId"])%>
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Name) %>
                <%= Html.ValidationMessageFor(model => model.Name) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Text) %>
            </div>
            <div class="editor-field">
                <%= Html.TextAreaFor(model => model.Text) %>
                <%= Html.ValidationMessageFor(model => model.Text) %>
            </div>
            <div id="captchaImage">
                <%= Html.Action("Draw", new {area="", controller="Captcha"})%>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Captcha) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Captcha) %>
                <%= Html.ValidationMessageFor(model => model.Captcha) %>
            </div>
            
            <div>
                <input type="submit" value="Send" />
            </div>

    <% } %>