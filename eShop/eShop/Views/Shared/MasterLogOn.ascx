<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div style="padding:10px;">
<%using (Html.BeginForm("LogOn", "Account", FormMethod.Post))
  { %>
    <table>
    <tr>
        <td class="loginTitle" colspan="3">Зарегистрироваться &nbsp;&nbsp;&nbsp; Напомнить пароль</td>
        
    </tr>
    <tr>
        <td><input value="login" type="text" id="userName" name="userName" onfocus="javascript:if(this.value=='login')this.value=''" onblur="javascript:if(this.value=='')this.value='login'" class="logonInput" name="" /></td>
        <td><input value="password" type="password" id="password" name="password" onfocus="javascript:if(this.value=='password')this.value=''" onblur="javascript:if(this.value=='')this.value='password'" class="logonInput" name="" /></td>
        <td><input type="submit" id="btnLogin" value="Войти" /></td>
    </tr>
    </table>
<%} %>
</div>