﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
  
           <div class="search">
          <span>Site Search</span><%=Html.TextBox("q1")%>
          </div>
     
 
 <div id="cse" style="width: 100%;">Loading</div>
<script src="http://www.google.com/jsapi" type="text/javascript"></script>
<script type="text/javascript">

    $("#q1").keyup(function (event) {
        if (event.keyCode == 13) {
            $(".gsc-search-button").click();
        }
        else {
            $(".gsc-input").val(this.value);
        }
    })

    google.load('search', '1', { language: 'en', style: google.loader.themes.MINIMALIST });
    google.setOnLoadCallback(function () {
        var customSearchControl = new google.search.CustomSearchControl('012547704737589669486:pozbfahyxas');
        customSearchControl.setResultSetSize(google.search.Search.FILTERED_CSE_RESULTSET);
        customSearchControl.draw('cse');
        //$(".gsc-input").focus();
        $("#q1").val('<%=Request.Form["q"]%>');
        $(".gsc-input").val('<%=Request.Form["q"]%>');//insert into search field requested search text
        $(".gsc-search-button").click(); //call button click event, show results
    }, true);
</script>
 <style type="text/css">

.gsc-search-box
{
    visibility:hidden;
    height:1px;
}

  .gsc-control-cse {
    font-family: Verdana, sans-serif;
    /*border-color: #FFFFFF;*/
    /*background-color: #FFFFFF;*/
    border-color:transparent;
    background-color:transparent;
    width:746px;
    
  }
  
  
  
  .gsc-webResult.gsc-result
  {
  	border-left:none !important;
}

.gs-no-results-result .gs-snippet, .gs-error-result .gs-snippet
{
	margin:0 !important;
	
}
  
  input.gsc-input {
    border-color: #777777;
    width:633px;
    font-size:12px;
  }
  input.gsc-search-button {
    border-color: #ffffff;
    background-color: #66cccc;
    
    visibility:hidden;
    
    
  }
  .gsc-tabHeader.gsc-tabhInactive {
    border-color: #777777;
    background-color: #777777;
    
  }
  
  .gsc-clear-button
  {
      visibility:hidden;
    }
    
  .gsc-tabHeader.gsc-tabhActive {
    border-color: #333333;
    background-color: #333333;
  }
  .gsc-tabsArea {
    border-color: #333333;
  }
  .gsc-webResult.gsc-result {
    border-color: #FFFFFF;
    background-color: #FFFFFF;
  }
  .gsc-webResult.gsc-result:hover {
    /*border-color: #000000;*/
    background-color: #FFFFFF;
  }
  .gs-webResult.gs-result a.gs-title:link,
  .gs-webResult.gs-result a.gs-title:link b {
    color: #444444;
  }
  .gs-webResult.gs-result a.gs-title:visited,
  .gs-webResult.gs-result a.gs-title:visited b {
    color: #444444;
  }
  .gs-webResult.gs-result a.gs-title:hover,
  .gs-webResult.gs-result a.gs-title:hover b {
    color: #444444;
  }
  .gs-webResult.gs-result a.gs-title:active,
  .gs-webResult.gs-result a.gs-title:active b {
    color: #777777;
  }
  .gsc-cursor-page {
    color: #444444;
  }
  a.gsc-trailing-more-results:link {
    color: #444444;
  }
  .gs-webResult.gs-result .gs-snippet {
    color: #333333;
    width:633px;
  }
  .gs-webResult.gs-result .gs-visibleUrl {
    color: #000000;
  }
  .gs-webResult.gs-result .gs-visibleUrl-short {
    color: #000000;
  }
  .gsc-cursor-box {
    border-color: #FFFFFF;
  }
  .gsc-results .gsc-cursor-page {
    border-color: #777777;
    background-color: #FFFFFF;
  }
  .gsc-results .gsc-cursor-page.gsc-cursor-current-page {
    border-color: #333333;
    background-color: #333333;
  }
  .gs-promotion.gs-result {
    border-color: #CCCCCC;
    background-color: #E6E6E6;
  }
  .gs-promotion.gs-result a.gs-title:link {
    color: #0000CC;
  }
  .gs-promotion.gs-result a.gs-title:visited {
    color: #0000CC;
  }
  .gs-promotion.gs-result a.gs-title:hover {
    color: #444444;
  }
  .gs-promotion.gs-result a.gs-title:active {
    color: #00CC00;
  }
  .gs-promotion.gs-result .gs-snippet {
    color: #333333;
  }
  .gs-promotion.gs-result .gs-visibleUrl,
  .gs-promotion.gs-result .gs-visibleUrl-short {
    color: #00CC00;
  }
  
  gsc-wrapper
  {
  	width:600px;
  	}
</style> 
      