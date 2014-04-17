jQuery(window).bind(
       "beforeunload",
       function () {
           //confirm("Do you really want to close?");
           return "При закрытии окна браузера, или при навигации с помощью кнопок \"Назад\" и \"Вперед\" данные будут утеряны.";
       }
   );
$('a').live('click', function () {
    jQuery(window).unbind("beforeunload");
    //inFormOrLink = true;
});

$('form').bind('submit', function () {
    jQuery(window).unbind("beforeunload");
    //inFormOrLink = true;
});