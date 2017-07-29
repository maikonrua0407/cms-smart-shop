function InputOnlyNumeric(event) {
    // Allow special chars + arrows 
    if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9
        || event.keyCode == 27 || event.keyCode == 13 || event.keyCode == 188 || event.keyCode == 190
        || (event.keyCode == 65 && event.ctrlKey === true)
        || (event.keyCode >= 35 && event.keyCode <= 39)) {
        return;
    } else {
        // If it's not a number stop the keypress
        if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
            event.preventDefault();
        }
    }
}

$(document).ready(function ($) {
    // tmcuong add - Only input number
    // with input type = number
    $('input[type=number]').keydown(function (event) {
        InputOnlyNumeric(event);
    });
});


// Clone object
var clone = function (o) {
    var n = {};
    for (i in o)
        n[i] = (typeof o[i] == 'object') ? arguments.callee(o[i]) : o[i];
    return n;
};

function NavigatePaging(currentPage, pageCount) {
    var iFrom = currentPage - 4;
    if (iFrom <= 0) iFrom = 1;
    var sPaging = '<div class="row"><div class="twelve columns paging-box">';
    if (currentPage == 1)
        sPaging += '<a class="page-panel first-panel" onclick="return false;">&lt;&lt;&nbsp;</a> <a class="page-panel" onclick="return false;">&lt;&nbsp;</a>';
    else
        sPaging += '<a class="page-panel first-panel" href="javascript:void(0);" onclick="gotoPage(1);">&lt;&lt;&nbsp;</a> <a class="page-panel" href="javascript:void(0);" onclick="gotoPage(' + (currentPage - 1).toString() + ');">&lt;&nbsp;</a>';
    for (var i = iFrom; i <= iFrom + 8; i++) {
        if (i <= pageCount) {
            // Nếu i không phải trang đầu tiên thì thêm ... ở đầu
            if (i == iFrom && i > 1) {
                sPaging += '<a class="page-panel" ><span>...</span>    </a>'
            }
            var href = (i == currentPage) ? "" : "href='javascript:void(0);'";
            var clsCSS = (i == currentPage) ? "page-panel current" : "page-panel";
            var funcJs = (i == currentPage) ? "return false;" : "gotoPage(" + i + ");";
            sPaging += '<a id="page_' + i + '" class="' + clsCSS + '" ' + href + ' onclick="' + funcJs + '"><span>' + i.toString() + '</span></a>';
            if (i == iFrom + 8 && iFrom + 8 < pageCount) {
                sPaging += ' <a class="page-panel" ><span>...</span>    </a>';
            }
        }
    }
    if (currentPage < pageCount) {
        sPaging += '<a class="page-panel" href="javascript:void(0);" onclick="gotoPage(' + (currentPage + 1).toString() + ');">&nbsp;&gt;</a>';
        sPaging += '<a class="page-panel last-panel" href="javascript:void(0);" onclick="gotoPage(' + pageCount + ');">&nbsp;&gt;&gt;</a>';
    }
    else {
        sPaging += '<a class="page-panel" onclick="return false;">&nbsp;&gt;</a>';
        sPaging += '<a class="page-panel last-panel" onclick="return false;">&nbsp;&gt;&gt;</a>';
    }
    sPaging += '</div></div>';
    return sPaging;
}
// Ham string format
String.format = function () {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
}
// jquery dialog
function CloseDialog(model) {
    if (model != null && model != undefined)
        model.dialog("close");
    else
        $("#SmartShopDialogContent").dialog("close");
}

function DialogYesNoConfirm(model, width, yesButtonText, noButtonText, callBackFunctionName, notClose) {
    if (width == undefined || width == null || width == 0) width = 500;
    if (yesButtonText == undefined || yesButtonText == null || yesButtonText == '') yesButtonText = "Đồng ý";
    if (noButtonText == undefined || noButtonText == null || noButtonText == '') noButtonText = "Hủy";
    //add class
    $(_contDialogFormat.Icon).attr("class", "ui-icon dialog-icon 'icon-none'");

    model.dialog({
        width: width,
        modal: true,
        // position: "center",
        dialogClass: _contDialogFormat.CustomStype,
        buttons: [
    {
        text: yesButtonText,
        //icons: { primary: "ui-icon-check" },
        'class': 'btn btn-app btn-info btn-small no-radius',
        click: function () {
            callBackFunctionName();
            if (notClose == undefined || notClose == null || notClose == false) $(this).dialog("close");
        }
    },
    {
        text: noButtonText,
        //icons: { primary: "ui-icon-closethick" },
        'class': 'btn btn-app btn-info btn-small no-radius',
        click: function () { $(this).dialog("close"); }
    }
        ]
    });
}


// define FunctionTwo as needed
function ShowProcess(funcName, autoClose) {
    $("#dialog-process").show();
    if (autoClose == undefined || autoClose == null) autoClose = true;
    if (funcName) funcName();
    if (autoClose == true) setTimeout(function () { $("#dialog-process").hide(); }, 1500);
}
function CloseProcess() {
    $("#dialog-process").hide();
}
function DialogWithSubmit(model, width, yesButtonText, callBackFunctionName, notClose) {
    var defer = $.Deferred();
    if (width == undefined || width == null || width == 0) width = 500;
    if (yesButtonText == undefined || yesButtonText == null || yesButtonText == '') yesButtonText = "Đồng ý";
    //add class
    $(_contDialogFormat.Icon).attr("class", "ui-icon dialog-icon 'icon-none'");

    model.dialog({
        width: width,
        modal: true,
        // position: "center", //"center",
        dialogClass: _contDialogFormat.CustomStype,
        buttons: [
    {
        text: yesButtonText,
        //icons: { primary: "ui-icon-check" },
        'class': 'btn btn-app btn-info btn-small no-radius',
        click: function () {

            callBackFunctionName();

            //callbacks.add(function () { $("#dialog-process").hide(); });
            //callbacks.fire();
            if (!notClose) $(this).dialog("close");
        }
    },
        ],
        close: function () {
            // $("#dialog-process").hide();
        }
    });
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
var _contDialogFormat = new DialogFormat();
function DialogFormat() {
    this.Content = "#SmartShopDialogContent";    //div content dùng để show dialog
    this.Message = "#SmartShopDialogMesage";     //div nội dung message của dialog
    this.Icon = "#SmartShopIconDialog";          //div icon của dialog
    this.Title = "SmartShop.vn";                 //tiêu đề dialog
    this.Width = 380;                       //width mặc định của dialog
    this.CustomStype = "dialogCustomStyle"; //điều khiển style của dialog
}

/*
Loại dialog
*/
var _contDialogType = new DialogType();
function DialogType() {
    this.Alert = 1;
    this.ConfirmYesNo = 2;
    this.ConfirmYesNoCancel = 3;
}

/*
Loại icon của dialog
*/
var _contDialogIconType = new DialogIconType();
function DialogIconType() {
    this.Information = 1;
    this.Warning = 2;
    this.Error = 3;
    this.Confirm = 4;
    this.None = 5;
}

/*
sinh dialog icon theo icon type truyền vào.
*/
function getDialogIconClass(icon, defaultIcon) {
    var iconClass = '';
    switch (icon) {
        case _contDialogIconType.Information:
            iconClass = 'fa fa-info';
            break;
        case _contDialogIconType.Warning:
            iconClass = 'fa-warning';
            break;
        case _contDialogIconType.Error:
            iconClass = 'fa fa-frown-o';
            break;
        case _contDialogIconType.Confirm:
            iconClass = 'fa fa-check-square-o';
            break;
        case _contDialogIconType.None:
            iconClass = 'fa fa-cog';
            break;

        default:
            if (defaultIcon)
                //iconClass = getDialogIconClass(defaultIcon);
                iconClass = 'fa fa-cog';
            break;
    }

    return iconClass;
}

function ShowWarning(message, dialogWidth) {
    ShowDialog_Alert(message, _contDialogIconType.Warning, dialogWidth);
}

/*
 show dialog thông báo
dialogIcon:        Icon của dialog
dialogWidth:       Width của dialog
message:            Nội dung dialog
commandName:       Command name sẽ truyền trong param ajax request xuống server
*/
var _tabIndexFocus = null;
function ShowDialog_Alert(message, dialogIcon, dialogWidth, closeCommandName) {
    setTimeout(function () {
        $(function () {
            //add message
            $(_contDialogFormat.Message).html(message);

            //add class
            $(_contDialogFormat.Icon).attr("class", "ui-icon dialog-icon " + getDialogIconClass(dialogIcon, _contDialogIconType.Information))

            //show dialog
            var element = document.querySelector(":focus");
            try {
                if (_tabIndexFocus && !checkUndefined(_tabIndexFocus)) {
                    element = $(":input[tabindex='" + _tabIndexFocus + "']");
                }
            } catch (err) { }
            $(_contDialogFormat.Content).dialog({
                title: _contDialogFormat.Title,
                width: getDialogWidth(dialogWidth),
                // position: "center", //"center",
                dialogClass: _contDialogFormat.CustomStype, //'dialogCustomStyle',
                resizable: false,
                modal: true,
                beforeClose: function (event, ui) {
                    //server
                    callServerFunction(closeCommandName);
                },
                buttons:
                [{
                    text: 'Đóng',
                    'class': 'btn btn-app btn-info btn-small no-radius',
                    click: function () {
                        $(this).dialog("close");

                        if (element) {
                            element.focus();
                        }

                        //server
                        //callServerFunction(closeCommand);
                    }
                }]
            });
        });
    }, 0);
}
function ShowDialog_AlertClientCallBack(message, dialogIcon, dialogWidth, closeCommandName) {
    setTimeout(function () {
        $(function () {
            //add message
            $(_contDialogFormat.Message).html(message);

            //add class
            $(_contDialogFormat.Icon).attr("class", "ui-icon dialog-icon " + getDialogIconClass(dialogIcon, _contDialogIconType.Information))

            //show dialog
            var element = document.querySelector(":focus");
            try {
                if (_tabIndexFocus && !checkUndefined(_tabIndexFocus)) {
                    element = $(":input[tabindex='" + _tabIndexFocus + "']");
                }
            } catch (err) { }
            $(_contDialogFormat.Content).dialog({
                title: _contDialogFormat.Title,
                width: getDialogWidth(dialogWidth),
                // position: "center", //"center",
                dialogClass: _contDialogFormat.CustomStype, //'dialogCustomStyle',
                resizable: false,
                modal: true,
                beforeClose: function (event, ui) {
                    //server
                    callClientFunction(closeCommandName);
                },
                buttons:
                [{
                    text: 'Đóng',
                    'class': 'btn btn-app btn-info btn-small no-radius',
                    click: function () {
                        $(this).dialog("close");

                        if (element) {
                            element.focus();
                        }

                        //server
                        //callServerFunction(closeCommand);
                    }
                }]
            });
        });
    }, 0);
}
/*
show dialog confirm yes no
*/
function ShowDialog_ConfirmYesNo(message, yesCommandName, noCommandName, dialogIcon, dialogWidth, closeCommandName) {
    var defer = $.Deferred();
    setTimeout(function () {
        $(function () {
            //add message
            $(_contDialogFormat.Message).html(message);

            //add class
            $(_contDialogFormat.Icon).attr("class", "ui-icon dialog-icon " + getDialogIconClass(dialogIcon, _contDialogIconType.Confirm))

            //show dialog
            $(_contDialogFormat.Content).dialog({
                title: _contDialogFormat.Title,
                width: getDialogWidth(dialogWidth),
                // position: "center", //"center",
                dialogClass: _contDialogFormat.CustomStype, //'dialogCustomStyle',
                resizable: false,
                modal: true,
                beforeClose: function (event, ui) {
                    //server
                    callServerFunction(noCommandName);
                },
                buttons:
                [{
                    text: 'Đồng ý',
                    'class': 'btn btn-app btn-info btn-small no-radius',
                    click: function () {
                        defer.resolve(true);

                        $(this).dialog("close");

                        //server
                        callServerFunction(yesCommandName);
                    }
                }, {
                    text: 'Hủy bỏ',
                    'class': 'btn btn-app btn-info btn-small no-radius',
                    click: function () {
                        defer.resolve(false);

                        $(this).dialog("close");

                        //server
                        //callServerFunction(noCommandName);
                    }
                }]
            });
        });
    }, 0);

    return defer.promise(); //important to return the deferred promise
}
/*
*/
function ShowDialog_ConfirmYesNo_Second(message, yesCommandName, noCommandName, dialogIcon, dialogWidth) {
    var defer = $.Deferred();
    setTimeout(function () {
        $(function () {
            //add message
            $(_contDialogFormat.Message).html(message);

            //add class
            $(_contDialogFormat.Icon).attr("class", "ui-icon dialog-icon " + getDialogIconClass(dialogIcon, _contDialogIconType.Confirm))

            //show dialog
            $(_contDialogFormat.Content).dialog({
                title: _contDialogFormat.Title,
                width: getDialogWidth(dialogWidth),
                // position: "center", //"center",
                dialogClass: _contDialogFormat.CustomStype, //'dialogCustomStyle',
                resizable: false,
                modal: true,
                buttons:
                [{
                    text: 'Có',
                    'class': 'btn btn-app btn-info btn-small no-radius',
                    click: function () {
                        defer.resolve(true);

                        $(this).dialog("close");

                        //server
                        callServerFunction(yesCommandName);
                    }
                }, {
                    text: 'Không',
                    'class': 'btn btn-app btn-info btn-small no-radius',
                    click: function () {
                        defer.resolve(false);

                        $(this).dialog("close");

                        //server
                        callServerFunction(noCommandName);
                    }
                }]
            });
        });
    }, 0);
    return defer.promise(); //important to return the deferred promise
}
/*
show dialog 3 nút
*/
function ShowDialog_ConfirmYesNoCancel(message, yesCommandName, noCommandName, dialogIcon, dialogWidth, isCheckChange) {
    var defer = $.Deferred();
    setTimeout(function () {
        $(function () {
            //add message
            $(_contDialogFormat.Message).html(message);

            //add class
            $(_contDialogFormat.Icon).attr("class", "ui-icon dialog-icon " + getDialogIconClass(dialogIcon, _contDialogIconType.Confirm))
            //show dialog focus
            var element = document.querySelector(":focus");
            try {
                if (_tabIndexFocus && !checkUndefined(_tabIndexFocus)) {
                    element = $(":input[tabindex='" + _tabIndexFocus + "']");
                }
            } catch (err) { }
            //show dialog        
            $(_contDialogFormat.Content).dialog({
                title: _contDialogFormat.Title,
                width: getDialogWidth(dialogWidth),
                // position: "center",
                dialogClass: _contDialogFormat.CustomStype, //'dialogCustomStyle',
                resizable: false,
                modal: true,
                //'verify': true,
                buttons:
                [{
                    text: 'Có',
                    'class': 'btn btn-app btn-info btn-small no-radius',
                    click: function () {
                        defer.resolve(true);

                        $(this).dialog("close");

                        //server
                        callServerFunction(yesCommandName);
                    }
                }, {
                    text: 'Không',
                    'class': 'btn btn-app btn-info btn-small no-radius',
                    click: function () {
                        defer.resolve(false);

                        $(this).dialog("close");
                        if (element) {
                            element.focus();
                        }
                        //server
                        callServerFunction(noCommandName);
                    }
                }, {
                    text: 'Hủy bỏ',
                    'class': 'btn btn-app btn-info btn-small no-radius',
                    click: function () {
                        defer.resolve(null);

                        $(this).dialog("close");
                    }
                }]
            });
        });
    }, 0);

    return defer.promise(); //important to return the deferred promise
}


// Xu ly voi call back client function
function ShowDialog_ConfirmYesNo_ClientCallBack(message, dialogWidth, YesButtonText, NoButtonText, dialogIcon, yesCommandName, noCommandName) {
    var sYes = 'Đồng ý';
    var sNo = 'Hủy bỏ';
    if (YesButtonText && YesButtonText != '') sYes = YesButtonText;
    if (NoButtonText && NoButtonText != '') sNo = NoButtonText;
    var defer = $.Deferred();
    setTimeout(function () {
        $(function () {
            //add message
            $(_contDialogFormat.Message).html(message);

            //add class
            $(_contDialogFormat.Icon).attr("class", "ui-icon dialog-icon " + getDialogIconClass(dialogIcon, _contDialogIconType.Confirm))

            //show dialog
            $(_contDialogFormat.Content).dialog({
                title: _contDialogFormat.Title,
                width: getDialogWidth(dialogWidth),
                // position: "center", //"center",
                dialogClass: _contDialogFormat.CustomStype, //'dialogCustomStyle',
                resizable: true,
                modal: true,
                buttons:
                [{
                    text: sYes,
                    'class': 'btn btn-app btn-info btn-small no-radius',
                    click: function () {
                        defer.resolve(true);

                        $(this).dialog("close");

                        //server
                        if (yesCommandName)
                            return yesCommandName();
                    }
                }, {
                    text: sNo,
                    'class': 'btn btn-app btn-info btn-small no-radius',
                    click: function () {
                        defer.resolve(false);

                        $(this).dialog("close");

                        if (noCommandName)
                            return noCommandName();
                    }
                }]
            });
        });
    }, 0);
    return defer.promise(); //important to return the deferred promise
}

//ShowDialog(string message, SmartShopDialogType dialogType, SmartShopDialogIconType iconType, int? dialogWidth = null, params object[] commandName)
/*
Hàm show dialog, tùy  thuộc vào dialogType sẽ show ra dialog tương ứng
*/
function showDialog(message, dialogType, dialogIcon, dialogWidth, yesCommand, noCommand, closeCommand) {
    var chArr = ',';   //Ký tự ngăn cách các phần tử trong mảng

    switch (dialogType) {
        case _contDialogType.Alert:
            ShowDialog_Alert(message, dialogIcon, dialogWidth, closeCommand);
            break;

        case _contDialogType.ConfirmYesNo:
            ShowDialog_ConfirmYesNo(message, yesCommand, noCommand, dialogIcon, dialogWidth, closeCommand);
            break;

        case _contDialogType.ConfirmYesNoCancel:
            ShowDialog_ConfirmYesNoCancel(message, yesCommand, noCommand, dialogIcon, dialogWidth);
            break;

        default:
            ShowDialog_Alert("Dialog Type: " + dialogType + " không phù hợp!", _contDialogIconType.Error);
            break;
    }
}

/*
 Hiển thị thông báo lỗi của control validate summary
*/
function showValidateSumary(arrValidateSummaryClientID, dialogWidth) {
    if (arrValidateSummaryClientID) {
        var sMessage = '';
        for (var i = 0; i < arrValidateSummaryClientID.length; i++) {
            sMessage += $('#' + arrValidateSummaryClientID[i]).html();
        }
        ShowDialog_Alert(sMessage, _contDialogIconType.Warning, dialogWidth);
    } else {
        ShowDialog_Alert("arrValidateSummaryClientID: " + arrValidateSummaryClientID + " không phù hợp!", _contDialogIconType.Error);
    }
}

/*
 Sinh width cho dialog
*/
function getDialogWidth(dialogWidth) {
    var iWidth = _contDialogFormat.Width;
    if (dialogWidth && dialogWidth > 0) {
        iWidth = dialogWidth;
    }
    return iWidth;
}

/*
Kiểm tra object
*/
function checkUndefined(obj) {
    return typeof (obj) == 'undefined';
}

/*
Gọi thực thi 1 function truyền vào
*/
function callClientFunction(_function) {
    if (!checkUndefined(_function)) {
        var func = _function;
        func();
    }
}

/*
Gọi ajax xuống server
*/
function callServerFunction(commandName) {
    if (!checkUndefined(commandName) && !commandName.isNullOrEmpty() && !commandName.isLastTrimEmpty()) {
        if (!checkUndefined(callAjaxRequest)) {
            callAjaxRequest(commandName);
        }
    }
}

function cancelEvent(args) {
    if (args && !checkUndefined(args)) {
        args.set_cancel(true);
    }
}

/*
tính position của dialog so với hiển thị của form
*/
function getTopOffset() {
    return (parent.document.documentElement.clientHeight) / 2 + parent.window.pageYOffset - 200;
}

/*
Di chuyển dialog ra giữa màn hình đang thao tác
*/
function getDialogPosition(isCheckChange) {
    if (isCheckChange && isCheckChange == true)
        return "center"
    else
        return ["top", getTopOffset()]; //"center",
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*
Định nghĩa lại hàm trim do lỗi trên IE
*/
String.prototype.trim = function () {
    return this.replace(/(?:(?:^|\n)\s+|\s+(?:$|\n))/g, '').replace(/\s+/g, ' ');
};

/*
Định nghĩa lại hàm string format
*/
String.prototype.format = function () {
    var formatted = this;
    for (var i = 0; i < arguments.length; i++) {
        var regexp = new RegExp('\\{' + i + '\\}', 'gi');
        formatted = formatted.replace(regexp, arguments[i]);
    }
    return formatted;
};

/*
Kiểm tra string empty
*/
String.prototype.isEmpty = function () {
    return this.length == 0;
};

/*
Kiểm tra string null Or empty
*/
String.prototype.isNullOrEmpty = function () {
    return !this;
};

/*
Kiểm tra string empty sau khi trim 
*/
String.prototype.isLastTrimEmpty = function () {
    return this.trim().length == 0;
};

var amount = '';
var tax = '';

function NumericDotOnly(inputObj, e) {
    var isAmount = inputObj.id == "amountVal";
    var e = (!e) ? window.event : e;
    var key = e.keyCode;
    if ((key < 48 || key > 57) && key != 8 && key != 110 && key != 190 || ((key == 110 || key == 190) && ((isAmount) ? amount : tax).indexOf('.') != -1)) {
        inputObj.value = (isAmount) ? amount : tax;
    }

    if (isAmount) {
        amount = inputObj.value;
    }
    else {
        tax = inputObj.value;
    }
}

function formatCurrency(inputObj) {
    if (inputObj.id == "amountVal") {
        amount = convertToFloat(inputObj.value);
    }
    else {
        tax = convertToFloat(inputObj.value);
    }
    inputObj.value = (inputObj.value != '') ? addCommas(convertToFloat(inputObj.value)) : '';
}

function convertToFloat(num) {
    return (num != '') ? parseFloat(num.replace(/,/g, "")).toFixed(2) : num;
}

function addCommas(num) {
    var numParts = num.split('.');
    var numArr = numParts[0].split('').reverse();
    var newArr = [];
    var count = -1;
    for (var i = 0; i < numArr.length; i++) {
        if (i % 3 == 0) {
            newArr[++count] = ',';
        }
        newArr[++count] = numArr[i];
    }
    return newArr.reverse().join('').replace(/((.+?)(,?))$/, '$2') + '.' + numParts[1];
}

function focusCampo(id) {
    var inputField = document.getElementById(id);
    if (inputField != null && inputField.value.length != 0) {
        if (inputField.createTextRange) {
            var fieldRange = inputField.createTextRange();
            fieldRange.moveStart('character', inputField.value.length);
            fieldRange.collapse();
            fieldRange.select();
        } else if (inputField.selectionStart || inputField.selectionStart == '0') {
            var elemLen = inputField.value.length;
            inputField.selectionStart = elemLen;
            inputField.selectionEnd = elemLen;
            inputField.focus();
        }
    } else {
        inputField.focus();
    }
}

function isDate(txtDate) {
    var currVal = txtDate;
    if (currVal == '')
        return false;

    //Declare Regex  
    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?

    if (dtArray == null)
        return false;

    //Checks for mm/dd/yyyy format.
    dtMonth = dtArray[3];
    dtDay = dtArray[1];
    dtYear = dtArray[5];

    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }
    return true;
}

function today() {
    var fullDate = new Date();
    var currentDate = parserDate(fullDate);
    return currentDate;
}

function parserDate(fullDate) {
    var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : '0' + (fullDate.getMonth() + 1);
    var date = twoDigitMonth + "/" + fullDate.getDate() + "/" + fullDate.getFullYear();
    return date;
}

function string2Date(strDate) {
    var arr = strDate.split("/");
    var date = new Date(arr[2], arr[1] - 1, arr[0]);
    return parserDate(date);
}

function formatMoney(totalValue) {
    if (totalValue == 0) return 0;
    var s = totalValue.toString().split("").reverse().reduce(function (acc, num, i, orig) {
        return num + (i && !(i % 3) ? "." : "") + acc;
    }, "");
    while (s.indexOf("-.") > -1) {
        s = s.replace("-.", "-");
    }
    return s;
}

function replaceMoney(strMoney) {
    if (strMoney == 0)
        return "0";
    else
        return strMoney.replace(/\./g, '');
}
function isValidEmail(email) {
    var pattern = new RegExp(/^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$/);
    return pattern.test(email);
}