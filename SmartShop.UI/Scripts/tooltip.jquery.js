(function($) {
    jQuery.fn.tooltip = function(options) {
        var defaults = {
            offsetX: 15,  //X Offset value
            offsetY: 20,  //Y Offset value
            fadeIn: '200', //Tooltip fadeIn speed, can use, slow, fast, number
            fadeOut: '100', //Tooltip fadeOut speed, can use, slow, fast, number
            dataAttr: 'data', //Used when we create seprate div to hold your tooltip data, so plugin search div tage by using id 'data' and current href id on whome the mouse pointer is so if your href id is '_tooltip_1' then the div which hold that tooltips content should have id 'data_tooltip_1', if you change dataAttr from default then you need to build div tag with id 'current dataAttr _tooltip_1' without space
            bordercolor: '#D52B34', // tooltip border color
            bgcolor: '#FFF', //Tooltip background color
            fontcolor: '#696969', //Tooltip Font color
            fontsize: '12px', // Tooltip font size
            folderurl: 'NULL', // Folder url, where the tooltip's content file is placed, needed with forward slash in the last (/), or can be use as http://www.youwebsitename.com/foldername/ also.
            filetype: 'txt', // tooltip's content files type, can be use html, txt
            height: 'auto', // Tooltip's width
            width: '300px', //Tooltip's Height
            cursor: 'cursor' // Mouse cursor
        };
        var options = $.extend(defaults, options);
        //Runtime div building to hold tooltip data, and make it hidden
        var $tooltip = $('<div id="divToolTip"></div>');
        return this.each(function() {
            $('body').append($tooltip);
            $tooltip.hide();
            //Runtime variable definations
            var element = this;
            var id = $(element).attr('id');
            var filename = options.folderurl + id + '.' + options.filetype;
            var dialog_id = '#divToolTip';
            //Tooltips main function
            $(this).hover(function(e) {
                //var size = "Windows Width : " + $(document).width() + " Tip Width : " + e.pageX + "\n" + "Windows Height : " + $(document).height() + " Tip Height : " + e.pageY;
                //alert(size);
                //to check whether the tooltips content files folder is defined or not
                if (options.folderurl != "NULL") {
                    $(dialog_id).load(filename);

                } else {
                    if ($('#' + options.dataAttr + '_' + id).length > 0) {
                        $(dialog_id).html($('#' + options.dataAttr + '_' + id).html());
                        //$(dialog_id).html(size);
                    } else {
                        $(dialog_id).html(id);
                        //$(dialog_id).html(size);
                    }
                }
                //assign css value to div
                $(element).css({ 'cursor': options.cursor });
                if ($(document).width() / 2 < e.pageX) {
                    $(dialog_id).css({
                        'position': 'absolute',
                        'border': '1px solid ' + options.bordercolor,
                        'background-color': options.bgcolor,
                        'padding': '0',
                        '-moz-border-radius': '0 0 5px 5px',
                        '-webkit-border-radius': '0 0 5px 5px',
                        'border-radius': '0 0 5px 5px',
                        'top': e.pageY + options.offsetY,
                        'left': e.pageX - $(dialog_id).width() + options.offsetX,
                        'color': options.fontcolor,
                        'font-size': options.fontsize,
                        'height': options.height,
                        'width': options.width,
                        'z-index': '9999'
                    });
                    //alert(size);
                } else {
                    $(dialog_id).css({
                        'position': 'absolute',
                        'border': '1px solid ' + options.bordercolor,
                        'background-color': options.bgcolor,
                        'padding': '0',
                        '-moz-border-radius': '0 0 5px 5px',
                        '-webkit-border-radius': '0 0 5px 5px',
                        'border-radius': '0 0 5px 5px',
                        'top': e.pageY + options.offsetY,
                        'left': e.pageX + options.offsetX,
                        'color': options.fontcolor,
                        'font-size': options.fontsize,
                        'cursor': options.cursor,
                        'height': options.height,
                        'width': options.width,
                        'z-index': '9999'
                    });
                    //alert(size);
                }
                //enable div block
                $(dialog_id).stop(true, true).fadeIn(options.fadeIn);
            }, function() {
                // when mouse out remove all data from div and make it hidden
                $(dialog_id).stop(true, true).fadeOut(options.fadeOut);
            }).mousemove(function(e) {
                // to make tooltip moveable with mouse	
                if ($(document).width() / 2 < e.pageX) {
                    $(dialog_id).css({
                        'top': e.pageY + options.offsetY,
                        'left': e.pageX - $(dialog_id).width(),
                        'height': options.height,
                        'width': options.width,
                        'z-index': '9999'
                    });
                    //$(dialog_id).html(e.pageX - $(dialog_id).width());
                } else {
                    $(dialog_id).css({
                        'top': e.pageY + options.offsetY,
                        'left': e.pageX + options.offsetX,
                        'height': options.height,
                        'width': options.width,
                        'z-index': '9999'
                    });
                }
            });
        });
    };
})(jQuery);

//FINISH, simple isnt it ??
//if you like it or have any suggestions / comments , or you have some idea to make it better, 
//or you need some more fetures in it PLS PLS PLS let me know that at
//i.rizvi@hotmail.com
//Thank you for using my plugin
