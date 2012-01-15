﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

03150, Україна, м. Київ<br />
вул. Горького, 91/14, оф. 139
<div class="mapLink">
<%=Html.ActionLink("Схема проїзду", "Map", "Home", null, new { @class = "fancyMap iframe" })%>
</div>
(044) 247-44-29<br/>
(044) 206-21-25<br/>
(093) 505-12-37
<div class="mailto">
<a href="mailto:c.a.w.babich@gmail.com">c.a.w.babich@gmail.com</a>
</div>

<script type="text/javascript">
    $(function () {
        $(".fancyMap").fancybox({ hideOnContentClick: false, showCloseButton: true, cyclic: true, showNavArrows: false, padding: 0, margin: 0, centerOnScroll: true, height:520, width:650 });
    });
</script>

