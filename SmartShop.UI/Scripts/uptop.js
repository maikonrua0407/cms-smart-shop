$(document).ready(function() {
    //$('#wrapper').append('<div id="top">Back to Top</div>');
    $('#wrapper').append('<div class="to_top" style="display: block;"><div class="go_to_top" onclick="jQuery(\'html,body\').animate({scrollTop: 0},1000);"><div class="arrow_top"></div></div></div>');
   $(window).scroll(function() {
    if($(window).scrollTop() != 0) {
        $('#top').fadeIn();
    } else {
        $('#top').fadeOut();
    }
   });
   $('#top').click(function() {
    $('html, body').animate({scrollTop:0},500);
   });
});