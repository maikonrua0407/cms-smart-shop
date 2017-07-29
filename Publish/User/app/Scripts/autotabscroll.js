/* AutoTabs
* Required: http://jquery.com/
* Written by: Contendia Comm.
* License: LGPL
*/

(function($) {
    $(document).ready(function() {
        //        var list = '<div id="autotab-list-container"><ul class="autotab-list">';
        //        $("div.autotab").each(function(index) {
        //            list += '<li><a href="#' + $(this).attr("id") + '">' + $(this).attr("id").replace(/-/g, " ") + '</a></li>';
        //        });
        //        list += '</ul></div>';
        //        $("div.autotab").wrapAll('<div class="autotabs-container" />');
        //        $("div.autotabs-container").before(list).append('<br style="clear:both;" />');
        //        $("div.autotab").css("display", "none");
        //        $("div.autotab").first().css("display", "block").addClass("active-autotab-panel");
        //        $("ul.autotab-list li").first().addClass("active-autotab");


        // scrolling tabs functionality
        var liWidth = 0;
        $("ul.autotab-list li").each(function(index) {
            liWidth += $(this).outerWidth();
        })
        if (liWidth > $("ul.autotab-list").innerWidth()) {
            $("div#autotab-list-container").prepend('<p id="left"> &laquo;</p>');
            $("p#left").addClass('disappear');
            $("div#autotab-list-container").prepend('<p id="right"> &raquo; </p>');
            //$("ul.autotab-list").addClass('padRight');
        }

        $("p#right").click(function() {
            var liWidth = 0;
            $(this).parent().find("ul.autotab-list li").each(function(index) {
                liWidth += $(this).outerWidth();
            })
            $(this).parent().find("ul.autotab-list").animate({ scrollLeft: "+=200" }, 1000);
            if (($(this).parent().find("ul.autotab-list").outerWidth() + 105) >= liWidth) {     // 75 is min-width on tab in css
                $(this).parent().find("p#right").slideUp('slow');
            }
            $(this).parent().find("p#left").slideDown('slow');    // the scroll navs are positioned absolute to the bottom so .slideDown is up and .slideUp is down
            //$("ul.autotab-list").addClass('padLeft');
        });


        $("p#left").click(function() {
            $(this).parent().find("ul.autotab-list").animate({ scrollLeft: "-=200" }, 1000)
            if (($(this).parent().find("ul.autotab-list").scrollLeft() - 10) <= 0) {
                $(this).parent().find("p#left").slideUp('slow');
                //$("ul.autotab-list").removeClass('padLeft');
            }
            $(this).parent().find("p#right").slideDown('slow');
            //$("ul.autotab-list").addClass('padRight');
        });


        // This method of navigating the tabs was retired in favor of the following method
        // which is a more general approach that will work regardless whether the click
        // is on a tab or a named anchor link anywhere on the page.
        //$("ul.autotab-list a").click(function(){
        // $("ul.autotab-list li").removeClass("active-autotab");
        //$(this).parent().addClass("active-autotab");
        // $("div.autotab").css("display", "none").removeClass("active-autotab-panel");
        //$("div#" + $(this).text().replace(/\s+/g, "-")).addClass("active-autotab-panel").fadeIn(600);
        //return false;
        //});

        // support links to IDs (named anchors) for navigating tabs
        // support links to IDs (named anchors) for navigating tabs
        $("a").click(function() {
            if (this.hash && $("div#" + this.hash + ".autotab").length) {
                $("ul.autotab-list a[href=" + this.hash + "]").parent().parent().find("li").removeClass("active-autotab");
                $("div#" + this.hash).parent().find("div.autotab").css("display", "none").removeClass("active-autotab-panel");
                $("ul.autotab-list a[href=" + this.hash + "]").parent().addClass("active-autotab");
                $("div#" + this.hash).css("display", "block").addClass("active-autotab-panel");
                // Tried the following to animate the top into view, but it's not working for some reason.
                //event.preventDefault();
                // $("div#" + div_id + " .autotab-list-container").animate({scrollTop : 0}, 600);
                return false;
            }
        });


    });
})(jQuery);

