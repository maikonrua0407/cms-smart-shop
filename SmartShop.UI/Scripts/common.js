$(document).ready(function() {
    //Tooltip
    $('.tooltip').tooltip();
});
function ClientSwitchLanguage(cLanguage, tLanguage)
{
    url = Portal.FrontWebParts.FSwitchLanguage.SwitchLanguage.Switch(cLanguage, tLanguage).value;
    window.open(url, "_self");  
}
function ClientRefreshCaptcha(captchaimageurl) {
    document.getElementById('imgCaptcha').src = captchaimageurl + '@rnd=' + Math.random();
}
function ClientRefreshCaptchaComment(captchaimageurl) {
    document.getElementById('imgCaptcha').src = captchaimageurl + '@rnd=' + Math.random();
}



//start event adv-image frontend

/*--PopupCenterAdvs--*/
var popupStatus = 0;
//loading popup with jQuery magic!
function loadPopup(){
//loads popup only if it is disabled
if(popupStatus==0){
$("#backgroundPopup").css({
"opacity": "0.7",
"position": "fixed",
"top": 0,
"left": 0,
"background": "black"
});
$("#backgroundPopup").fadeIn();
$("#popupContact").fadeIn();
popupStatus = 1;
}
}
//disabling popup with jQuery magic!
function disablePopup1(){
//disables popup only if it is enabled
if(popupStatus==1){
$("#backgroundPopup").fadeOut("slow");
$("#popupContact").fadeOut("slow");
popupStatus = 0;
}
}
//centering popup
function centerPopup1(){
//request data for centering
var windowWidth = document.documentElement.clientWidth;
var windowHeight = document.documentElement.clientHeight;
var popupHeight = $("#popupContact").height();
var popupWidth = $("#popupContact").width();
//centering
$("#popupContact").css({
    "position": "fixed",
    "top": (windowHeight - popupHeight)/2,
    "left": (windowWidth-popupWidth)/2
//"background": "white"
});
//only need force for IE6
 
$("#backgroundPopup").css({
    "height": windowHeight,
    "width": windowWidth
});
}
//CONTROLLING EVENTS IN jQuery
$(document).ready(function(){
// $("#popupData").hide(0);						   
$("#backgroundPopup").hide(0);
$("#popupContact").hide(0);
//LOADING POPUP
//centering with css
centerPopup1();
//load popup
loadPopup();
    
//CLOSING POPUP
//Click the x event!
$("#popupContactClose").click(function(){
    disablePopup1();
    return false;
});
//Click out event!
$("#backgroundPopup").click(function(){
//disablePopup();
});
//Press Escape event!
$(document).keypress(function(e){
    if(e.keyCode==27 && popupStatus==1){
    disablePopup1();
    }
});
});
/*--PopupCenterAdvs--*/

/*--FloatAdvs--*/
function FloatTopDiv() {
    startLX = ((document.body.clientWidth - MainContentW) / 2) - LeftBannerW - LeftAdjust, startLY = TopAdjust + 80;
    startRX = ((document.body.clientWidth - MainContentW) / 2) + MainContentW + RightAdjust, startRY = TopAdjust + 80;
    var d = document;
function ml(id) {
    var el = d.getElementById ? d.getElementById(id) : d.all ? d.all[id] : d.layers[id];
    el.sP = function(x, y) { this.style.left = x + 'px'; this.style.top = y + 'px'; };
    el.x = startRX;
    el.y = startRY;
    return el;
}
function m2(id) {
    var e2 = d.getElementById ? d.getElementById(id) : d.all ? d.all[id] : d.layers[id];
    e2.sP = function(x, y) { this.style.left = x + 'px'; this.style.top = y + 'px'; };
    e2.x = startLX;
    e2.y = startLY;
    return e2;
}
window.stayTopLeft = function() {
    if (document.documentElement && document.documentElement.scrollTop)
    var pY = document.documentElement.scrollTop;
    else if (document.body)
    var pY = document.body.scrollTop;
    if (document.body.scrollTop > 30) { startLY = 3; startRY = 3; } else { startLY = TopAdjust; startRY = TopAdjust; };
    ftlObj.y += (pY + startRY - ftlObj.y) / 16;
    ftlObj.sP(ftlObj.x, ftlObj.y);
    ftlObj2.y += (pY + startLY - ftlObj2.y) / 16;
    ftlObj2.sP(ftlObj2.x, ftlObj2.y);
    setTimeout("stayTopLeft()", 1);
    }
    ftlObj = ml("divAdRight");
    //stayTopLeft();      
    ftlObj2 = m2("divAdLeft");
    stayTopLeft();
}
function ShowAdDiv() {
    var objAdDivRight = document.getElementById("divAdRight");
    var objAdDivLeft = document.getElementById("divAdLeft");
    if (document.body.clientWidth < 1000) {
    objAdDivRight.style.display = "none";
    objAdDivLeft.style.display = "none";
    }
    else {
        objAdDivRight.style.display = "block";
        objAdDivLeft.style.display = "block";
        FloatTopDiv();
    }
}
/*--FloatAdvs--*/


function handleReferenceClick(advImageId, IndexImg, IndexShow) 
{
retValue = Portal.FrontWebParts.FAdvImage.AdvImage.GetContentReferenceShower(advImageId, IndexImg, IndexShow).value;
document.getElementById("ShowReference" + IndexShow).innerHTML = retValue;
document.getElementById("ShowReference" + IndexShow).className = "VisibleClass";
}
function closeOpen(IndexShow) {
document.getElementById("ShowReference" + IndexShow).className = "HiddenClass";
}
//end event adv-image frontend

//start event news frontend
function ClientViewContentTab1Column(GroupCategoryId,LinkType,PageModuleId)
{
retValue = Portal.FrontWebParts.FNews.ListNewsByGroup.GetContentMenuTabNewsShow1Column(GroupCategoryId,LinkType).value;
if (retValue.indexOf("ERROR") != -1) {
alert(retValue);
return;
}
document.getElementById("TabNewsByGroup"+PageModuleId).innerHTML=retValue;
}
function ClientViewContentTab2Column(GroupCategoryId,LinkType,PageModuleId)
{
retValue = Portal.FrontWebParts.FNews.ListNewsByGroup.GetContentMenuTabNewsShow2Column(GroupCategoryId,LinkType).value;
if (retValue.indexOf("ERROR") != -1) {
alert(retValue);
return;
}
document.getElementById("TabNewsByGroup"+PageModuleId).innerHTML=retValue;
} 
//end event news frontend

//start event product frontend
function handleReferenceClick(ProductId, IndexImg, IndexShow) {
retValue = Portal.FrontWebParts.FProduct.ProductByCategory.GetContentReferenceShower(ProductId, IndexImg, IndexShow).value;
document.getElementById("ShowReference" + IndexShow).innerHTML = retValue;
document.getElementById("ShowReference" + IndexShow).className = "VisibleClass";
}
//function closeOpen(IndexShow) {
//    document.getElementById("ShowReference" + IndexShow).className = "HiddenClass";
//    var linkList = $('#tabContaier ul.active'); 
//    for (i = 0; i < linkList.length; i++) {
//    linkList[i].className = "";
//    linkList[i].className = "cssproductitem"; 
//}
function ClientViewList(PageModuleId,rgc,gc, st, p) {
Portal.FrontWebParts.FCommon.Common.SetStyleView(0);
sc = document.getElementById("drpShowProduct").value;
rValue = Portal.FrontWebParts.FProduct.ProductByCategory.GetContentProductByCategory(PageModuleId,rgc, gc, sc, st, p).value;
document.getElementById("divProductByCategoryContent" + PageModuleId).innerHTML = rValue;
}
function ClientViewGrid(PageModuleId, rgc, gc, st, p) {
Portal.FrontWebParts.FCommon.Common.SetStyleView(1);
sc = document.getElementById("drpShowProduct").value;
rValue = Portal.FrontWebParts.FProduct.ProductByCategory.GetContentProductByCategory4on1(PageModuleId,rgc, gc, sc, st, p).value;
document.getElementById("divProductByCategoryContent" + PageModuleId).innerHTML = rValue;
}
function ClientShowProduct() {
rgc = document.getElementById("txtrgc").value;
gc = document.getElementById("txtgc").value;
stIndex = document.getElementById("drpSortType").selectedIndex;
st = document.getElementById("drpSortType").options[stIndex].value;
scIndex = document.getElementById("drpShowProduct").selectedIndex;
sc = document.getElementById("drpShowProduct").options[scIndex].value;
Url = Portal.FrontWebParts.FCommon.Common.GetFilterProUrl(rgc,gc,sc, st).value;
window.open(Url, '_self');
}
function ClientSortProduct() {
rgc = document.getElementById("txtrgc").value;
gc = document.getElementById("txtgc").value;
stIndex = document.getElementById("drpSortType").selectedIndex;
st = document.getElementById("drpSortType").options[stIndex].value;
scIndex = document.getElementById("drpShowProduct").selectedIndex;
sc = document.getElementById("drpShowProduct").options[scIndex].value;
Url = Portal.FrontWebParts.FCommon.Common.GetFilterProUrl(rgc, gc, sc, st).value;
window.open(Url, '_self');
}
function ClientAddToListLastItems(belongSite, ProductId, PageModuleId, CurrentPageIndex) {
Value = Portal.FrontWebParts.FCommon.Common.ServerSideAddToListLastItems(belongSite, ProductId).value;
cValue = Portal.FrontWebParts.FCommon.Common.UpdateListLastItems(PageModuleId, CurrentPageIndex).value;
document.getElementById("cssListLastItems").innerHTML = cValue;
document.getElementById("cssListLastItems").className = "cssStatuDivListItems";
}
function ClientRemoveItemsToListLastItems(belongSite, ProductId, PageModuleId, CurrentPageIndex) {
Value = Portal.FrontWebParts.FProduct.ProductByCategory.ServerSideRemoveItemsToListLastItems(belongSite, ProductId).value;
cValue = Portal.FrontWebParts.FProduct.ProductByCategory.UpdateListLastItems(PageModuleId, CurrentPageIndex).value;
document.getElementById("cssListLastItems").innerHTML = cValue;
document.getElementById("cssListLastItems").className = "cssStatuDivListItems";
}
function ClientOrderProduct(belongSite, ProductCode) {
rValue = Portal.FrontWebParts.FCommon.Common.ServerSideAddShoppingCart(belongSite, ProductCode).value;
cValue = Portal.FrontWebParts.FCommon.Common.CountProInShopCart().value;
qValue = Portal.FrontWebParts.FCommon.Common.QuickProInShopCart().value;
document.getElementById("countproinshop").innerHTML = cValue;
document.getElementById("showproinshop").innerHTML = qValue;
PathRoot = Portal.FrontWebParts.FCommon.Common.HttpRoot().value;
window.open(PathRoot+'/gio-hang.html', '_self');
}

function ClientOrderProductWithQuantity(belongSite, ProductCode) {
Quantity = document.getElementById("drpQuantity").value;
rValue = Portal.FrontWebParts.FCommon.Common.ServerSideAddShoppingCartWithQuantity(belongSite, ProductCode, Quantity).value;
cValue = Portal.FrontWebParts.FCommon.Common.CountProInShopCart().value;
qValue = Portal.FrontWebParts.FCommon.Common.QuickProInShopCart().value;
document.getElementById("countproinshop").innerHTML = cValue;
document.getElementById("showproinshop").innerHTML = qValue;

PathRoot = Portal.FrontWebParts.FCommon.Common.HttpRoot().value;
window.open(PathRoot + '/gio-hang.html', '_self');
}
function ClientAddToShoppingCart(belongSite, ProductCode) {
rValue = Portal.FrontWebParts.FCommon.Common.ServerSideAddShoppingCart(belongSite, ProductCode).value;
cValue = Portal.FrontWebParts.FCommon.Common.CountProInShopCart().value;
qValue = Portal.FrontWebParts.FCommon.Common.QuickProInShopCart().value;
alert(rValue);
document.getElementById("countproinshop").innerHTML = cValue;
document.getElementById("showproinshop").innerHTML = qValue;
}
function ClientAddToShoppingCartWithQuantity(belongSite, ProductCode) {
Quantity = document.getElementById("drpQuantity").value;
rValue = Portal.FrontWebParts.FCommon.Common.ServerSideAddShoppingCartWithQuantity(belongSite, ProductCode, Quantity).value;
cValue = Portal.FrontWebParts.FCommon.Common.CountProInShopCart().value;
qValue = Portal.FrontWebParts.FCommon.Common.QuickProInShopCart().value;
alert(rValue);
document.getElementById("countproinshop").innerHTML = cValue;
document.getElementById("showproinshop").innerHTML = qValue;
}
function ClientUpdateCheckShop(ProductId) {
Quantity = document.getElementById(ProductId).value;
rValue = Portal.FrontWebParts.FProduct.CheckShop.ServerSideUpdateCheckShop(ProductId, Quantity).value;
document.getElementById("divContentShop").innerHTML = rValue;
}
function ClientRemove(ProductId) {
if (confirm('Bạn đã chắc chắn chưa?') == false) return;
Quantity = document.getElementById(ProductId).value;
rValue = Portal.FrontWebParts.FProduct.CheckShop.ServerSideRemove(ProductId).value;
document.getElementById("divContentShop").innerHTML = rValue;
}
/*checkshop*/
function ClientProcessinIndexOrder(belongSite) {
    retValue = Portal.FrontWebParts.FProduct.CheckShop.ProcessinIndexOrder1(belongSite).value;
    if (retValue.indexOf("ERROR") != -1) {
        alert(retValue);
        return;
    }
    document.getElementById("widget-sanpham").innerHTML = retValue;
}
function ClientSingInAndSendOrderProcessing2(belongSite) {

    if (document.getElementById('txtEmail').value == "") {
        alert('Nhập địa chỉ Email!');
        document.getElementById('txtEmail').focus();
        return;
    }
    else {
        var str = document.getElementById('txtEmail').value;
        var er = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
        if (!er.test(str)) {
            alert('Định dạng Email chưa đúng !');
            document.getElementById('txtEmail').focus();
            return;
        }
    }
    if (document.getElementById('txtPassword').value == "") {
        alert('Nhập mật khẩu!');
        document.getElementById('txtPassword').focus();
        return;
    }
    if (document.getElementById('txtRePassword').value == "") {
        alert('Nhập lại mật khẩu!');
        document.getElementById('txtRePassword').focus();
        return;
    }
    if (document.getElementById('txtRePassword').value != document.getElementById('txtPassword').value) {
        alert('Mật khẩu chưa trùng khớp!');
        document.getElementById('txtRePassword').focus();
        return;
    }
    if (document.getElementById('txtFullName').value== "") {
        alert('Nhập họ và tên!');
        document.getElementById('txtFullName').focus();
        return;
    }
    if (document.getElementById('txtDateOfIssue').value != document.getElementById('txtDateOfIssue').value) {
        alert('Nhập ngày sinh!');
        document.getElementById('txtDateOfIssue').focus();
        return;
    }
    if (document.getElementById('txtMobile').value != document.getElementById('txtMobile').value) {
        alert('Nhập số di động!');
        document.getElementById('txtMobile').focus();
        return;
    }
    if (document.getElementById('txtAddress').value != document.getElementById('txtAddress').value) {
        alert('Nhập địa chỉ!');
        document.getElementById('txtAddress').focus();
        return;
    }
    if (document.getElementById('txtcode').value == "") {
        alert('Nhập mã an toàn!');
        document.getElementById('txtcode').focus();
        return;
    }
    Email = document.getElementById("txtEmail").value;
    Password = document.getElementById("txtPassword").value;
    RePassword = document.getElementById("txtRePassword").value;
    Sex = document.getElementById("drpSex").options[document.getElementById("drpSex").selectedIndex].value;
    FullName = document.getElementById("txtFullName").value;
    BirthDay = document.getElementById("txtBirthDay").value;
    IDCard = document.getElementById("txtIDCard").value;
    DateOfIssue = document.getElementById("txtDateOfIssue").value;
    Telephone = document.getElementById("txtTelephone").value;
    Mobile = document.getElementById("txtMobile").value;
    City = document.getElementById("drpCity").options[document.getElementById("drpCity").selectedIndex].value;
    District = document.getElementById("drpDistrict").options[document.getElementById("drpDistrict").selectedIndex].value;
    Address = document.getElementById("txtAddress").value;
    AddressOrder = document.getElementById("txtAddressOrder").value;
    Note = document.getElementById("txtNote").value;
    codeCaptcha = document.getElementById('txtcode').value;

    rValue = Portal.FrontWebParts.FProduct.CheckShop.RegisterNewsAcount(belongSite, Email, Password, FullName, Sex, BirthDay, IDCard, DateOfIssue, Telephone, Mobile, City, District, Address, AddressOrder, Note, codeCaptcha).value;
    if (rValue.indexOf("ERROR") != -1) {
        alert(rValue);
        return;
    }
    alert(rValue);
    ClientSendOrderProcessing2(belongSite);
}
function ClientSendOrderProcessing2(belongSite) {
    retValue = Portal.FrontWebParts.FProduct.CheckShop.ProcessinIndexOrder2(belongSite).value;
    if (retValue.indexOf("ERROR") != -1) {
        alert(retValue);
        return;
    }
    document.getElementById("widget-sanpham").innerHTML = retValue;

}
function ClientSendOrderProcessing3(belongSite) {
    if (document.getElementById('txtcode').value == "") {
        alert('Nhập mã an toàn!');
        document.getElementById('txtcode').focus();
        return;
    }
    codeCaptcha = document.getElementById("txtcode").value;
    PaymentType = document.getElementById("drpPaymentType").options[document.getElementById("drpPaymentType").selectedIndex].value;
    retValue = Portal.FrontWebParts.FProduct.CheckShop.ClienSendOrderProcessing3(belongSite, PaymentType, codeCaptcha).value;
    if (retValue.indexOf("ERROR") != -1) {
        alert(retValue);
        return;
    }
    document.getElementById("widget-sanpham").innerHTML = retValue;

}
function ClientSaleNext() {
    window.open("default.aspx", "_self");
}

function JumpPageProductNew(PageModuleId, MaxItem, ItemPerRow, VisibleItem, CurrentPageIndex, Javascript) {
    rValue = Portal.FrontWebParts.FProduct.NewProduct.GetContent(PageModuleId, CurrentPageIndex).value;
    document.getElementById("divNewProductContent" + PageModuleId).innerHTML = rValue;
}
function JumpPageProductBestBuy(PageModuleId, MaxItem, ItemPerRow, VisibleItem, CurrentPageIndex, Javascript) {
    rValue = Portal.FrontWebParts.FProduct.BestBuy.GetContent(PageModuleId, CurrentPageIndex).value;
    document.getElementById("divBestBuyContent" + PageModuleId).innerHTML = rValue;
}
function JumpPageProduct3on1(PageModuleId,MaxItem,ItemPerRow,VisibleItem,CurrentPageIndex,Javascript)
{
    rValue=Portal.FrontWebParts.FProduct.ProductByCategory.GetContentProductByCategory3on1(PageModuleId,CurrentPageIndex).value;
    document.getElementById("divProductByCategoryContent"+PageModuleId).innerHTML=rValue;
}
function JumpPageUpdateLastItems(PageModuleId,MaxItem,ItemPerRow,VisibleItem,CurrentPageIndex,Javascript)
{
    rValue = Portal.FrontWebParts.FCommon.Common.UpdateListLastItems(PageModuleId, CurrentPageIndex).value;
    document.getElementById("cssListLastItems").innerHTML=rValue;
}
//end event product frontend

//start event result search product frontend
function ClientDoQuickSearchProByCat() {
    GroupProduct = document.getElementById("GroupCategoryId").options[document.getElementById("GroupCategoryId").selectedIndex].value;
    keyword = document.getElementById("txtKeyword").value;
    Url = Portal.FrontWebParts.FSearch.Search.GetQuickSearchProByCatUrl(GroupProduct, keyword).value;
    if (Url.indexOf("ERROR:") != -1) {
        alert(Url);
        return;
    }
    window.open(Url, '_self');
}

function ClientViewListOnSearch(PageModuleId, gc, k, sc, st, p) {
    Portal.FrontWebParts.FCommon.Common.SetStyleViewOnSearch(0);
    rValue = Portal.FrontWebParts.FSearch.SearchResultProduct.GetResultSearchProList(PageModuleId, gc, k, sc, st, p).value;
    document.getElementById("divResultQuickSearchPro" + PageModuleId).innerHTML = rValue;
}
function ClientViewGridOnSearch(PageModuleId, gc, k, sc, st, p) {
    Portal.FrontWebParts.FCommon.Common.SetStyleViewOnSearch(1);
    sc = document.getElementById("drpShowProduct").value;
    rValue = Portal.FrontWebParts.FSearch.SearchResultProduct.GetResultSearchPro4on1(PageModuleId, gc,k, sc, st, p).value;
    document.getElementById("divResultQuickSearchPro" + PageModuleId).innerHTML = rValue;
}   
function ClientShowListProductOnSearch(PageModuleId, gc,k, st, p) {
    sc = document.getElementById("drpShowProduct").value;
    rValue = Portal.FrontWebParts.FSearch.SearchResultProduct.GetResultSearchProList(PageModuleId, gc, k, sc, st, p).value;
    document.getElementById("divResultQuickSearchPro" + PageModuleId).innerHTML = rValue;
}
function ClientShowGridProductOnSearch(PageModuleId, gc,k, st, p) {
    sc = document.getElementById("drpShowProduct").value;
    rValue = Portal.FrontWebParts.FSearch.SearchResultProduct.GetResultSearchPro4on1(PageModuleId, gc,k, sc, st, p).value;
    document.getElementById("divResultQuickSearchPro" + PageModuleId).innerHTML = rValue;
}
function ClientShowProductOnSearch() {
    gc = document.getElementById("GroupCategoryId").value;
    stIndex = document.getElementById("drpSortType").selectedIndex;
    st = document.getElementById("drpSortType").options[stIndex].value;
    scIndex = document.getElementById("drpShowProduct").selectedIndex;
    sc = document.getElementById("drpShowProduct").options[scIndex].value;
    k = document.getElementById("txtKeyword").value;
    Url = Portal.FrontWebParts.FCommon.Common.GetFilterProOnSearchUrl(gc, k, sc, st).value;
    window.open(Url, '_self');
}

function ClientSortProductOnSearch() {
    gc = document.getElementById("GroupCategoryId").value;
    stIndex = document.getElementById("drpSortType").selectedIndex;
    st = document.getElementById("drpSortType").options[stIndex].value;
    scIndex = document.getElementById("drpShowProduct").selectedIndex;
    sc = document.getElementById("drpShowProduct").options[scIndex].value;
    k = document.getElementById("txtKeyword").value;
    Url = Portal.FrontWebParts.FCommon.Common.GetFilterProOnSearchUrl(gc, k, sc, st).value;
    window.open(Url, '_self');
}
//end event result search product frontend

//start event detect city frontend
function DetectCity() {
    CityId = document.getElementById("drpCity").options[document.getElementById("drpCity").selectedIndex].value;
    retValue = Portal.FrontWebParts.FCommon.Common.GetDistrict(CityId).value;
    document.getElementById("district").innerHTML = retValue;
}
//end event detect city frontend

//start event custormer register website frontend
function userLogInBlur(ctrName, str) {
    if (document.getElementById(ctrName).value == '') {
        //document.getElementById(ctrName).className='look_login';
        document.getElementById(ctrName).value = str;
    }
}
function userLogInFocus(ctrName, str) {
    if (document.getElementById(ctrName).value == str) {
        //document.getElementById(ctrName).className='look';
        document.getElementById(ctrName).value = '';

    }
}
function ClientDoRegister() {
    if (document.getElementById('txtEmail').value == "") {
        alert('Nhập địa chỉ Email!');
        document.getElementById('txtEmail').focus();
        return;
    }
    else {
        var str = document.getElementById('txtEmail').value;
        var er = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
        if (!er.test(str)) {
            alert('Định dạng Email chưa đúng !');
            document.getElementById('txtEmail').focus();
            return;
        }
    }
    if (document.getElementById('txtPassword').value == "") {
        alert('Nhập mật khẩu!');
        document.getElementById('txtPassword').focus();
        return;
    }
    if (document.getElementById('txtRePassword').value == "") {
        alert('Nhập lại mật khẩu!');
        document.getElementById('txtRePassword').focus();
        return;
    }
    if (document.getElementById('txtRePassword').value != document.getElementById('txtPassword').value) {
        alert('Mật khẩu chưa trùng khớp!');
        document.getElementById('txtRePassword').focus();
        return;
    }
    if (document.getElementById('txtFullName').value== "") {
        alert('Nhập họ và tên!');
        document.getElementById('txtFullName').focus();
        return;
    }
    if (document.getElementById('txtBirthDay').value != document.getElementById('txtBirthDay').value) {
        alert('Nhập ngày sinh!');
        document.getElementById('txtBirthDay').focus();
        return;
    }
    if (document.getElementById('txtMobile').value != document.getElementById('txtMobile').value) {
        alert('Nhập số di động!');
        document.getElementById('txtMobile').focus();
        return;
    }
    if (document.getElementById('txtAddress').value != document.getElementById('txtAddress').value) {
        alert('Nhập địa chỉ!');
        document.getElementById('txtAddress').focus();
        return;
    }
    if (document.getElementById('txtcode').value == "") {
        alert('Nhập mã an toàn!');
        document.getElementById('txtcode').focus();
        return;
    }
    Email = document.getElementById("txtEmail").value;
    Password = document.getElementById("txtPassword").value;
    RePassword = document.getElementById("txtRePassword").value;
    Sex = document.getElementById("drpSex").options[document.getElementById("drpSex").selectedIndex].value;
    FullName = document.getElementById("txtFullName").value;
    BirthDay = document.getElementById("txtBirthDay").value;
    IDCard = document.getElementById("txtIDCard").value;
    DateOfIssue = document.getElementById("txtDateOfIssue").value;
    Telephone = document.getElementById("txtTelephone").value;
    Mobile = document.getElementById("txtMobile").value;
    City = document.getElementById("drpCity").options[document.getElementById("drpCity").selectedIndex].value;
    District = document.getElementById("drpDistrict").options[document.getElementById("drpDistrict").selectedIndex].value;
    Address = document.getElementById("txtAddress").value;
    Company = document.getElementById("txtCompany").value;
    codeCaptcha = document.getElementById('txtcode').value;
    retValue = Portal.FrontWebParts.FLogin.Register.ServerSideRegisterMember(Email, Password, FullName, IDCard, DateOfIssue, Sex, BirthDay, Telephone, Mobile, City, District, Address, Company, codeCaptcha).value;
    if (retValue.indexOf("ERROR") != -1) {
        alert(retValue);
        return;
    }
    alert(retValue);
    PathRoot = Portal.FrontWebParts.FCommon.Common.HttpRoot().value;
    window.open(PathRoot + '/dang-nhap.html', '_self');
}
function ViewInfoCompany(CompanyId) {

    Url = Portal.FrontWebParts.FLogin.InfoMember.GetShowViewRestaurantOfCustomer(CompanyId).value;
    if (Url.indexOf("ERROR") != -1) {
        alert(Url);
        return;
    }
    window.open(Url, "_self")
}
function ClientDoLogin() {
    Email = document.getElementById("txtEmail").value;
    Password = document.getElementById("txtPassword").value;
    RetValue = Portal.FrontWebParts.FLogin.Login.ServerSideLogin(Email, Password).value;
    if (RetValue.indexOf("ERROR") != -1) {
        alert(RetValue);
        return;
    }
    window.open("default.aspx", "_self");
}
function ClientDoLogOut(PageModuleId) {
    retValue = Portal.FrontWebParts.FLogin.LoginUserInfo.ServerSideLogOut(PageModuleId).value;

    if (retValue.indexOf("ERROR") != -1) {
        alert(retValue);
        return;
    }
    document.getElementById("nav-login-reg").innerHTML = retValue;
}
function ClientGoRegister() {
    PathRoot = Portal.FrontWebParts.FCommon.Common.HttpRoot().value;
    window.open(PathRoot + '/dang-ky.html', '_self');
}
function userLogInBlur(ctrName, str) {
    if (document.getElementById(ctrName).value == '') {
        //document.getElementById(ctrName).className='look_login';
        document.getElementById(ctrName).value = str;
    }
}
function userLogInFocus(ctrName, str) {
    if (document.getElementById(ctrName).value == str) {
        //document.getElementById(ctrName).className='look';
        document.getElementById(ctrName).value = '';

    }
}
function emailCheck(str) {
    var at = "@"
    var dot = "."
    var lat = str.indexOf(at)
    var lstr = str.length
    var ldot = str.indexOf(dot)
    if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr) {
        alert("  Email chưa nhập đúng định dạng")
        return false
    }
    if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr) {
        alert("  Email chưa nhập đúng định dạng")
        return false
    }
    if (str.indexOf(at, (lat + 1)) != -1) {
        alert("  Email chưa nhập đúng định dạng")
        return false
    }
    if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {
        alert("  Email chưa nhập đúng định dạng")
        return false;
    }
    if (str.indexOf(dot, (lat + 2)) == -1) {
        alert("  Email chưa nhập đúng định dạng")
        return false
    }
} function email_validate(email) {
    var regMail = /^([_a-zA-Z0-9-]+)(\.[_a-zA-Z0-9-]+)*@([a-zA-Z0-9-]+\.)+([a-zA-Z]{2,3})$/;
    if (regMail.test(email) == false) {
        alert("  Email chưa nhập đúng định dạng");
        document.getElementById("txtEmail").value = "";
        document.getElementById("txtEmail").focus();
    }
}
function checkNumber(e, id) {
    var alphaNumericCode = e.keyCode ? e.keyCode : e.charCode
    if ((alphaNumericCode < 48 || alphaNumericCode > 57) && (alphaNumericCode <= 95 || alphaNumericCode > 106) && (alphaNumericCode != 8) && (alphaNumericCode < 37 || alphaNumericCode > 40) && (alphaNumericCode != 46) && (alphaNumericCode != 27) && (alphaNumericCode != 9) & (alphaNumericCode != 13)) {
        alert("Chỉ được phép nhập số!!!");
        id.value = "";
        document.getElementById("txtTel").focus();
    }
}
//end event custormer register website frontend

//start feedback form contact
function ClientSubmitContactSimple(requestName, requestEmail ,validEmail,requestDescription,requestCharacter,urlredirect)
{ 

	if(document.getElementById('txtname').value==""|| document.getElementById('txtname').value==requestName)
			{
				alert(requestName);
				document.getElementById('txtname').focus();
				return ;
			}
	if(document.getElementById('txtuser_email').value=="" || document.getElementById('txtuser_email').value==requestEmail)
			{
				alert(requestEmail);
				document.getElementById('txtuser_email').focus();
				return ;
			}
	else
			{
				var str=document.getElementById('txtuser_email').value;
				var er=/^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
				if(!er.test(str))
				{	
				alert(validEmail);	
				document.getElementById('txtuser_email').focus();
				return ;
				}
			 }
		if(document.getElementById('txtcomments').value=="" ||document.getElementById('txtcomments').value==requestDescription)
			{
				alert(requestDescription);
				document.frm_email.comments.focus();
				return ;
			}
		if(document.getElementById('txtcode').value=="" || document.getElementById('txtcode').value==requestDescription )
			{
				alert(requestDescription);
				document.getElementById('txtcode').focus();
				return ;
			}

    name=document.getElementById('txtname').value ;
    email=document.getElementById('txtuser_email').value ;
    comments=document.getElementById('txtcomments').value ;
    codeCaptcha=document.getElementById('txtcode').value ;
    RetValue=Portal.FrontWebParts.FContact.Contact.SubmitContactSimple(name,email,comments,codeCaptcha).value
    if (RetValue.indexOf("ERROR") != -1)
    { 
        alert(RetValue);
        return; 
    } 
    window.open(urlredirect, "_self");  
} 
//end feedback form contact

//start export file pdf and exel
function Exportpdf(belongSite) {
    var ie = document.all;
    if (ie) {
        window.open('Bufferpdf.aspx', '_blank', 'width=800, height=800, status=yes,resizable =yes');
    }
    else {
        window.showModalDialog('Bufferpdf.aspx', '_blank', 'width=800, height=800, status=yes,resizable =yes');
    }
}
function ExportExcels() {
    var ie = document.all;
    if (ie) {
        window.location.href = "BufferExcel.aspx";
    }
    else {
        window.open('BufferExcel.aspx', '_self', 'width=400, height=120, status=no');
    }
}
//end export file pdf and exel


function OnBlurTiny(ctrName, str) {
    if (document.getElementById(ctrName).value == '') {
        document.getElementById(ctrName).value = str;
        document.getElementById(ctrName).className = 'textbox tiny';
    }
}


function OnBlurSmall(ctrName, str) {
    if (document.getElementById(ctrName).value == '') {
        document.getElementById(ctrName).value = str;
        document.getElementById(ctrName).className = 'textbox small';
    }
}
function OnBlurMedium(ctrName, str) {
    if (document.getElementById(ctrName).value == '') {
        document.getElementById(ctrName).value = str;
        document.getElementById(ctrName).className = 'textbox medium';
    }
}
function OnBlurAdwordcomment(ctrName, str) {
    if (document.getElementById(ctrName).value == '') {
        document.getElementById(ctrName).value = str;
        document.getElementById(ctrName).className = 'area adwordcomment';
    }
}

function OnFocusTiny(ctrName, str) {
    if (document.getElementById(ctrName).value == str) {
        document.getElementById(ctrName).className = 'textbox tiny';
        document.getElementById(ctrName).value = '';
    }
}

function OnFocusSmall(ctrName, str) {
    if (document.getElementById(ctrName).value == str) {
        document.getElementById(ctrName).className = 'textbox small';
        document.getElementById(ctrName).value = '';
    }
}
function OnFocusMedium(ctrName, str) {
    if (document.getElementById(ctrName).value == str) {
        document.getElementById(ctrName).className = 'textbox medium';
        document.getElementById(ctrName).value = '';
    }
}
function OnFocusAdwordcomment(ctrName, str) {
    if (document.getElementById(ctrName).value == str) {
        document.getElementById(ctrName).className = 'area adwordcomment';
        document.getElementById(ctrName).value = '';
    }
}


function ClientSubmitComment(ItemId, isChanel, requestName, requestEmail, validEmail,title, requestDescription, requestCharacter, captchaimageurl) {

    if (document.getElementById('txtname').value == "" || document.getElementById('txtname').value == requestName) {
        alert(requestName);
        document.getElementById('txtname').focus();
        return;
    }

    if (document.getElementById('txtuser_email').value == "" || document.getElementById('txtuser_email').value == requestEmail) {
        alert(requestEmail);
        document.getElementById('txtuser_email').focus();
        return;
    }
    else {
        var str = document.getElementById('txtuser_email').value;
        var er = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
        if (!er.test(str)) {
            alert(validEmail);
            document.getElementById('txtuser_email').focus();
            return;
        }
    }
    if (document.getElementById('txtcode').value == "" || document.getElementById('txtcode').value == requestCharacter) {
        alert(requestCharacter);
        document.getElementById('txtcode').focus();
        return;
    }
    if (document.getElementById('txtcomments').value == "" || document.getElementById('txtcomments').value == requestDescription) {
        alert(requestDescription);
        document.getElementById('txtcomments').focus();
        return;
    }


    name = document.getElementById('txtname').value;
    email = document.getElementById('txtuser_email').value;
    comments = document.getElementById('txtcomments').value;
    codeCaptcha = document.getElementById('txtcode').value;
    RetValue = Portal.FrontWebParts.FCommon.Common.ServerSideSendComment(ItemId, isChanel, name, email, title,comments, codeCaptcha).value;
    if (RetValue.indexOf("ERROR") != -1) {
        alert(RetValue);
        return;
    }
    alert(RetValue);
    defaultInput();
    ClientRefreshCaptchaComment(captchaimageurl);

}
function defaultInput() {
    document.getElementById('txtname').value = "Họ tên";
    document.getElementById('txtname').className = 'adword-textbox';

    document.getElementById('txtuser_email').value = "Email";
    document.getElementById('txtuser_email').className = 'adword-textbox';

    document.getElementById('txttitle').value = "Tiêu đề";
    document.getElementById('txttitle').className = 'adword-textbox';

    document.getElementById('txtcode').value = "Mã xác nhận";
    document.getElementById('txtcode').className = 'adword-textbox';

    document.getElementById('txtcomments').value = "Nội dung";
    document.getElementById('txtcomments').className = 'adword-textbox';
}

function KeyPressForSearch(myfield, e) {
    var keycode;
    if (window.event) keycode = window.event.keyCode;
    else if (e) keycode = e.which;
    else return true;
    if (keycode == 13) {
        ClientDoQuickSearchProByCat();
        return false;
    }
    else {
        return true;
    }
}


