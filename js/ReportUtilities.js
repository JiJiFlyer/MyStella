﻿var DXagent = navigator.userAgent.toLowerCase();
var DXopera = (DXagent.indexOf("opera") > -1);
var DXopera9 = (DXagent.indexOf("opera/9") > -1);
var DXsafari = DXagent.indexOf("safari") > -1;
var DXie = (DXagent.indexOf("msie") > -1 && !DXopera);
var DXIE55 = (DXagent.indexOf("5.5") > -1 && DXie);
var DXns = (DXagent.indexOf("mozilla") > -1 || DXagent.indexOf("netscape") > -1 || DXagent.indexOf("firefox") > -1) && !DXsafari && !DXie && !DXopera;
var DXDefaultThemeCookieName = "DemoCurrentTheme";

function fixPng(element) {
    if (/MSIE (5\.5|6).+Win/.test(navigator.userAgent)) {
        if (element.tagName == 'IMG' && /\.png$/.test(element.src)) {
            var src = element.src;
            element.src = '../Images/blank.gif';
            element.runtimeStyle.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(src='" + src + "')";
        }
    }
}

var WidthCorrectAllowed = true;
function CorrectWidth() {
    if (WidthCorrectAllowed) {
        WidthCorrectAllowed = false;
        var divSpacer = document.getElementById('divSpacer');
        if (divSpacer != null)
            return divSpacer.offsetWidth > 800 ? '800px' : 'auto';
        return 'auto';
    }
}

function isIE() {
    return (document.all && !window.opera) ? true : false;
}
function MoveFooter() {
    var spacer = document.getElementById("SpacerDiv");
    var footer = document.getElementById("Footer");
    if (!DXDemoIsExists(spacer) || !DXDemoIsExists(footer))
        return;

    if (!isIE())
        footer.style.visibility = "hidden";

    spacer.style.height = "0px";

    var lastChildHeight = 0;
    var lastChild = null;
    lastChild = GetLastChild(document.body.lastChild);
    if (lastChild != null)
        lastChildHeight = DXGetAbsoluteY(lastChild) + lastChild.offsetHeight;
    spacer.top = DXGetDocumentClientHeight() - lastChildHeight;
    if (Math.abs(spacer.top) == spacer.top)
        spacer.style.height = spacer.top + "px";

    if (!isIE())
        footer.style.visibility = "";
}
function GetLastChild(element) {
    if (element != null) {
        var top = DXGetAbsoluteY(element);
        var height = element.offsetHeight;
        if (top == 0 && height == 0 || element.nodeName == "#text")
            return GetLastChild(element.previousSibling);
    }
    return element;
}

DXattachEventToElement(window, "resize", DXWindowOnResize);
function DXWindowOnResize(evt) {
    MoveFooter();
}

function trace_event(sender, args, event_name) {
    var s = "";
    for (var i in args) {
        if ("inherit" != i && "constructor" != i)
            s += i + " = " + eval("args." + i) + "<br />";
    }
    if (s == "") s = "";

    var name = sender.name;
    var pos = name.lastIndexOf("_");
    if (pos > -1)
        name = name.substring(pos + 1);
    change_monitor_value("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tr><td valign=\"top\" style=\"width: 100px;\">Sender:</td><td valign=\"top\">" + name + "</td></tr><tr><td valign=\"top\">EventType:</td><td valign=\"top\"><b>" + event_name + "</b></td></tr><tr><td valign=\"top\">Arguments:</td><td valign=\"top\">" + s + "</td></tr></table><br />", false);
}
function change_monitor_value(val, is_need_clear) {
    var memo = document.getElementById("Events");
    if (memo != null) {
        memo.innerHTML = Trim(is_need_clear ? val : memo.innerHTML + val);
        memo.scrollTop = 100000;
        if (is_need_clear)
            memo.scrollTop = 0;
    }
}
function clear_monitor() {
    change_monitor_value('', true);
}
function LTrim(value) {
    var re = /\s*((\S+\s*)*)/;
    return value.replace(re, "$1");
}
function RTrim(value) {
    var re = /((\s*\S+)*)\s*/;
    return value.replace(re, "$1");
}
function Trim(value) {
    return LTrim(RTrim(value));
}

function screenshot(src) {
    var screenLeft = document.all && !document.opera ? window.screenLeft : window.screenX;
    var screenWidth = screen.availWidth;
    var screenHeight = screen.availHeight;
    var zeroX = Math.floor((screenLeft < 0 ? 0 : screenLeft) / screenWidth) * screenWidth;

    var windowWidth = 475;
    var windowHeight = 325;
    var windowX = parseInt((screenWidth - windowWidth) / 2);
    var windowY = parseInt((screenHeight - windowHeight) / 2);
    if (windowX + windowWidth > screenWidth)
        windowX = 0;
    if (windowY + windowHeight > screenHeight)
        windowY = 0;

    windowX += zeroX;

    var popupwnd = window.open(src, '_blank', "left=" + windowX + ",top=" + windowY + ",width=" + windowWidth + ",height=" + windowHeight + ", scrollbars=no, resizable=no", true);
    if (popupwnd != null && popupwnd.document != null && popupwnd.document.body != null) {
        popupwnd.document.body.style.margin = "0px";
    }
}
function DXDemoIsExists(obj) {
    return (typeof (obj) != "undefined") && (obj != null);
}
function DXDemoIsFocusableTag(tagName) {
    tagName = tagName.toLowerCase();
    return (tagName == "input" || tagName == "textarea" || tagName == "select" ||
		tagName == "button" || tagName == "a");
}
function DXDemoIsFocusable(element) {
    if (!DXDemoIsExists(element) || !DXDemoIsFocusableTag(element.tagName))
        return false;
    var current = element;
    while (DXDemoIsExists(current)) {
        if (current.tagName.toLowerCase() == "body")
            return true;
        if (current.disabled || element.style.display == "none" || element.style.visibility == "hidden")
            return false;
        current = current.parentNode;
    }
    return true;
}
function DXDemoActivateFormControl(controlId) {
    var control = document.getElementById(controlId);
    if (DXDemoIsExists(control) && DXDemoIsFocusable(control))
        control.focus();
}
function DXDemoActivateLabels() {
    var labels = document.getElementsByTagName("label");
    for (var index = 0; index < labels.length; index++) {
        labels[index].onclick = function () {
            DXDemoActivateFormControl(this.getAttribute('htmlfor') || this.getAttribute('for'));
        }
    }
}
function DXDemoHideFocusRects(container) {
    if (container == null)
        return;
    hyperlinks = container.getElementsByTagName("a");
    for (var index = 0; index < hyperlinks.length; index++) {
        hyperlinks[index].onfocus = function () { this.blur(); }
    }
}
DXattachEventToElement(window, "load", DXWindowOnLoad);
function DXWindowOnLoad(evt) {
    DXDemoActivateLabels();
    MoveFooter();
    DXPrepareThemes();
}
function DXPrepareThemes() {
}
function DXGetCookieValue(name) {
    var cookie = " " + document.cookie;
    var search = " " + name + "=";
    var setStr = null;
    var offset = 0;
    var end = 0;
    if (cookie.length > 0) {
        offset = cookie.indexOf(search);
        if (offset != -1) {
            offset += search.length;
            end = cookie.indexOf(";", offset)
            if (end == -1) {
                end = cookie.length;
            }
            setStr = unescape(cookie.substring(offset, end));
        }
    }
    return setStr;
}
function DXGetCurrentThemeCookieName() {
    if (_aspxIsExists(DXCurrentThemeCookieName))
        return DXCurrentThemeCookieName;
    return DXDefaultThemeCookieName;
}
function DXGetCurrentThemeFromCookies() {
    return DXGetCookieValue(DXGetCurrentThemeCookieName());
}
function DXSaveCurrentThemeToCookies(name) {
    document.cookie = DXGetCurrentThemeCookieName() + "=" + name + "; expires=Thu, 13 Sep 3007 14:07:07 GMT; path=/";
}

function DXSetClientWidth(element, clientWidth) {
    var currentStyle = _aspxGetCurrentStyle(element);
    var newClientWidth = clientWidth - _aspxPxToInt(currentStyle.paddingLeft) - _aspxPxToInt(currentStyle.paddingRight) -
        _aspxPxToInt(currentStyle.borderLeftWidth) - _aspxPxToInt(currentStyle.borderRightWidth);
    element.style.width = newClientWidth + "px";
}
function DXGetAbsoluteX(curEl) {
    var pos = 0;
    var isFirstCycle = true;
    while (curEl != null) {
        pos += curEl.offsetLeft;
        if (curEl.offsetParent != null && !DXopera && !DXopera9) {
            pos -= curEl.scrollLeft;
        }
        if (DXie && !isFirstCycle && curEl.tagName != "TABLE")
            pos += curEl.clientLeft;
        isFirstCycle = false;

        curEl = curEl.offsetParent;
    }
    return pos;
}
function DXGetAbsoluteY(curEl) {
    var pos = 0;
    while (curEl != null) {
        pos += curEl.offsetTop;
        if (curEl.offsetParent != null && !DXopera && !DXopera9) {
            pos -= curEl.scrollTop;
        }
        curEl = curEl.offsetParent;
    }
    return pos;
}
function DXGetDocumentClientHeight() {
    if (DXsafari)
        return window.innerHeight;
    if (DXIE55 || DXopera || document.documentElement.clientHeight == 0)
        return document.body.clientHeight;
    return document.documentElement.clientHeight;
}
function DXGetDocumentScrollTop() {
    if (!DXsafari && (DXIE55 || document.documentElement.scrollTop == 0))
        return document.body.scrollTop;
    return document.documentElement.scrollTop;
}
function DXGetDocumentScrollLeft() {
    if (!DXsafari && (DXIE55 || document.documentElement.scrollLeft == 0))
        return document.body.scrollLeft;
    return document.documentElement.scrollLeft;
}
function DXattachEventToElement(element, eventName, func) {
    if (DXns || DXsafari)
        element.addEventListener(eventName, func, true);
    else {
        if (eventName.toLowerCase().indexOf("on") != 0)
            eventName = "on" + eventName;
        element.attachEvent(eventName, func);
    }
}

// Images
var DXFakeImage = "/Image.gif";
function DXHideImages(obj) {
    if (obj.hasChildNodes()) {
        for (var i = 0; i < obj.childNodes.length; i++)
            DXHideImages(obj.childNodes[i]);
    }
    if (obj.tagName != null && obj.tagName.toLowerCase() == "img") {
        DXHideImage(obj);
        DXClearSize(obj);
    }
}
function DXRestoreImages(obj) {
    if (obj.hasChildNodes()) {
        for (var i = 0; i < obj.childNodes.length; i++)
            DXRestoreImages(obj.childNodes[i]);
    }
    if (obj.tagName != null && obj.tagName.toLowerCase() == "img") {
        DXRestoreImage(obj);
        DXRestoreSize(obj);
    }
}

function DXHideImage(obj) {
    if (!DXDemoIsExistAttribute(obj, "_dxsrc")) {
        obj.setAttribute("_dxsrc", obj.getAttribute("src"));
        obj.setAttribute("src", DXFakeImage);
    }
}
function DXRestoreImage(obj) {
    if (DXDemoIsExistAttribute(obj, "_dxsrc")) {
        obj.setAttribute("src", obj.getAttribute("_dxsrc"));
        obj.removeAttribute("_dxsrc");
    }
}
function DXClearSize(obj) {
    if (!DXDemoIsExistAttribute(obj, "_dxWidth") && obj.style.width != null) {
        obj.setAttribute("_dxWidth", obj.style.width);
        if (DXsafari || DXopera)
            obj.style.width = "80px";
        else
            obj.style.width = "";
    }
    if (!DXDemoIsExistAttribute(obj, "_dxHeight") && obj.style.height != null) {
        obj.setAttribute("_dxHeight", obj.style.height);
        if (DXsafari)
            obj.style.height = "60px";
        else if (DXopera)
            obj.style.height = "16px";
        else
            obj.style.height = "";
    }
}
function DXRestoreSize(obj) {
    if (DXDemoIsExistAttribute(obj, "_dxWidth")) {
        obj.style.width = obj.getAttribute("_dxWidth");
        obj.removeAttribute("_dxWidth");
    }
    if (DXDemoIsExistAttribute(obj, "_dxHeight")) {
        obj.style.height = obj.getAttribute("_dxHeight");
        obj.removeAttribute("_dxHeight");
    }
}
function DXDemoIsExistAttribute(obj, attrName) {
    return DXDemoIsExists(obj) && obj.getAttribute(attrName) != null;
}


//Begin Expand/Collapse
var sectionStates = new Array();
function ExpandCollapse(imageItemId) {
    noReentry = true; // Prevent entry to OnLoadImage
    var imageItem = _aspxGetElementById(imageItemId);
    if (ItemCollapsed(imageItemId) == true) {
        imageItem.src = "../Images/ExpandedButton.gif";
        imageItem.alt = "Collapse";
        ExpandSection(imageItem);
    }
    else {
        imageItem.src = "../Images/CollapsedButton.gif";
        imageItem.alt = "Expand";
        CollapseSection(imageItem);
    }
    noReentry = false;
}
function ExpandCollapse_CheckKey(imageItemId) {
    var imageItem = _aspxGetElementById(imageItemId);
    if (window.event.keyCode == 13)
        ExpandCollapse(imageItem);
}
function ChangeExpanded(imageItem, state, style) {
    try {
        var element = imageItem.parentNode.parentNode;
        var span = element.nextSibling;
        span.style.display = style;
        sectionStates[imageItem.id] = state;
    }
    catch (e) {
    }
}
function ExpandSection(imageItem) {
    ChangeExpanded(imageItem, "e", "");
}

function CollapseSection(imageItem) {
    ChangeExpanded(imageItem, "c", "none");
}
function ItemCollapsed(imageId) {
    return sectionStates[imageId] != "e";
}
function CorrectCodeRenderWidth(pageControl) {
    var tabContent = pageControl.GetContentElement(pageControl.activeTabIndex);
    var divCodeRender = _aspxGetChildsByClassName(tabContent, "cr-div");
    for (var index = 0; index < divCodeRender.length; index++) {
        if ((divCodeRender[index].offsetWidth) != pageControl.GetContentsCell().clientWidth)
            DXSetClientWidth(divCodeRender[index], pageControl.GetContentsCell().clientWidth);
    }
}


//------------------------------------------Report_Toolbar,Start---------------------------------------------------------
function onToolBarClick(s, e) {
    switch (e.item.name.toLowerCase()) {
        case "search":
            if (checkInfo() == false) {
                e.processOnServer = false;
            }
            break;
        case "search_gridview":
            search_gridview();
            e.processOnServer = false;
            break;
        case "search_post":
            if (checkInfo() == false) {
                e.processOnServer = false;
            }
            break;
        case "search_pivotgrid":
            search_pivotgrid();
            e.processOnServer = false;
            break;
        case "gridviewfieldchooser":
            if (webGridView.IsCustomizationWindowVisible()) {
                webGridView.HideCustomizationWindow();
            }
            else {
                webGridView.ShowCustomizationWindow();
            }
            e.processOnServer = false;
            break;
        case "savetemplate":
            if (checkInfo() == false) {
                e.processOnServer = false;
                return;
            }
            pcSaveTemplate.Show();
            e.processOnServer = false;
            break;
        case "updatetemplate":
            if (checkInfo() == false) {
                e.processOnServer = false;
                return;
            }
            if (cbChooseTemplate.GetValue() != "" && cbChooseTemplate.GetValue() != null) {
                MyCallback.PerformCallback('update&' + cbChooseTemplate.GetValue());
            }
            else {
                pcSaveTemplate.Show();
            }
            e.processOnServer = false;
            break;
        case "deletetemplate":
            if (cbChooseTemplate.GetValue() != "" && cbChooseTemplate.GetValue() != null) {
                if (window.confirm("是否删除此模板？")) {
                    MyCallback.PerformCallback('delete&' + cbChooseTemplate.GetValue());
                }
            }
            e.processOnServer = false;
            break;
        case "enablegroup":
            pivotGrid.PerformCallback('EnableGroup');
            e.processOnServer = false;
            break;
    }
}
function search_gridview() {
    if (checkInfo() == false) {
        return;
    }
    hf.Set('status', 'SEARCH');
    webGridView.PerformCallback("Search");
}
function search_pivotgrid() {
    if (checkInfo() == false) {
        return;
    }
    hf.Set('status', 'SEARCH');
    pivotGrid.PerformCallback("Search");
}
function filterPanel_Load(data) {
    if (data == "" || data == null) {
        return;
    }
    var dataObj = eval("(" + data + ")");
    $.each(dataObj.filter, function (idx, item) {
        if (item.ID == "") {
            return true;
        }

        switch (item.Type) {
            case "ASPxComboBox":
                if (item.ID == "filterTitle" && item.Value == "") {
                    eval(item.ID).SetText("事业部");
                }
                else if (item.ID == "filterShipmenttype" && item.Value == "") {
                    eval(item.ID).SetText("全部");
                }
                else {
                    if (eval("window." + item.ID) != undefined) {
                        eval(item.ID).SetValue(item.Value);
                    }
                }
                break;
            case "RadioButtonList":
                $("input[name$='" + item.ID + "'][value='" + item.SelectedValue + "']").attr("checked", "true");
                break;
            case "RadioButton":
                if (item.Checked == "True") {
                    $("input[id$='" + item.ID + "']").attr("checked", true);
                }
                else if (item.Checked == "False") {
                    $("input[id$='" + item.ID + "']").attr("checked", false);
                }
                break;
            case "ASPxDateEdit":
                if (item.Value != "") {
                    eval(item.ID).SetDate(new Date(item.Value));
                }
                else {
                    eval(item.ID).SetDate(null);
                }
                break;
            case "LookupBox":
                eval(item.ID).value(item.Value);
                eval(item.ID).text(item.Text);
                break;
            case "TextBox":
                $("input[id$='" + item.ID + "']").val(item.Text);
                break;
            case "CheckBox":
                if (item.Checked == "True") {
                    $("input[id$='" + item.ID + "']").attr("checked", true);
                }
                else if (item.Checked == "False") {
                    $("input[id$='" + item.ID + "']").attr("checked", false);
                }
                break;
        }
    });
    setFilterDateEnabled();
    if (hf.Get("status") == "SEARCH") {
        if (window.pivotGrid != undefined) {
            search_pivotgrid();
        }
        else if (window.webGridView != undefined) {
            search_gridview();
        }
    }
}
function pivotGrid_EndCallback(s, e) {
    if ($("#filterText") != null) {
        $("#filterText").text(pivotGrid.cpTextString);
    }
    var data = pivotGrid.cpJsonString;
    if (data == "" || data == null) {
        return;
    }
    filterPanel_Load(data);
    pivotGrid.cpJsonString = null;
}
function webGridView_EndCallback(s, e) {
    var info = webGridView.cpInfo;
    if (info != "" && info != null) {
        alert(info);
        webGridView.cpInfo = null;
    }
    if ($("#filterText") != null) {
        $("#filterText").text(webGridView.cpTextString);
    }
    var data = webGridView.cpJsonString;
    if (data == "" || data == null) {
        return;
    }
    filterPanel_Load(data);
    webGridView.cpJsonString = null;
}
function MyCallback2_EndCallback(s, e) {
    var data = MyCallback2.cpJsonString;
    if (data == "" || data == null) {
        return;
    }
    filterPanel_Load(data);
    MyCallback2.cpJsonString = null;
}
//------------------------------------------Report_Toolbar,End---------------------------------------------------------
//从报表链接到业务表单
function openFormFromReport(sEntityId, sInstanceId) {
    var sSql = "";
    var oRecord = null;
    var sSheetName = "";
    var sSheetURL = "";
    try {
        sSql = "select commandoutername_" + Macro_VAR.LanguageCode + ",arg0 from sys_command where autoinc=" + sEntityId;
        oRecord = new window.recordset(sSql);
        oRecord.open();
        sSheetName = oRecord.item(0, "commandoutername_" + Macro_VAR.LanguageCode);
        sSheetURL = oRecord.item(0, "arg0");
        sSheetURL = sSheetURL + "?openreason=openFormFromReport&instanceid=" + sInstanceId;
        oRecord.close();
        var pFun = parent.parent.openWindowByTabPanel;
        pFun(sSheetName, sSheetURL, sEntityId, 'cssClass/images/workflow.gif');
    }
    catch (e)
			{ window.alert("openMsgSource Error:" + e.message); }
}
//从报表链接到报表
function openReportFromReport(sEntityId, sLinkString) {
    var sSql = "";
    var oRecord = null;
    var sSheetName = "";
    var sSheetURL = "";
    try {
        sSql = "select commandoutername_" + Macro_VAR.LanguageCode + ",arg0 from sys_command where autoinc=" + sEntityId;
        oRecord = new window.recordset(sSql);
        oRecord.open();
        sSheetName = oRecord.item(0, "commandoutername_" + Macro_VAR.LanguageCode);
        sSheetURL = oRecord.item(0, "arg0");
        sSheetURL = sSheetURL + "&openreason=openReportFromReport" + sLinkString;
        oRecord.close();
        var pFun = parent.parent.openWindowByTabPanel;
        pFun(sSheetName, sSheetURL, sEntityId, 'cssClass/images/workflow.gif');
    }
    catch (e)
			{ window.alert("openMsgSource Error:" + e.message); }
}