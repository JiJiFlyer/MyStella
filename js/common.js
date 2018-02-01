document.write("<script language='javascript' src='../js/My97DatePicker/WdatePicker.js'></script>");
var MAX_LENGTH_QUERYSTRING=2000;
var DEF_LANGUAGECODE="cn";
var ROOT_PATH="/singsong/";
var uDelimiter="@|@";
var sDelimiter="@||@";
var returnValueAndCode_Delimiter="@|||@";
var SINGLE_QUOTATION_REPLACEMENT="SINGLE_QUOTATION";
var DOUBLE_QUOTATION_REPLACEMENT="DOUBLE_QUOTATION";
var LEFT_TAG_REPLACEMENT="LEFT_TAG";
var RIGHT_TAG_REPLACEMENT="RIGHT_TAG";
var SINGLE_AND_REPLACEMENT="SINGLE_AND";
var ERR_CODE="0";
var SUCC_CODE="1";
var SELECTED_COLOR="#21b208";
var UNSELECTED_COLOR="#828282";
var DISPLAYNAME_WIDTH="80";
var sErrCode=new Array();
sErrCode[0]="\u5f53\u524d\u6570\u636e\u96c6\u6570\u636e\u4fdd\u5b58\u6210\u529f";
sErrCode[1]="\u60a8\u786e\u5b9a\u8981\u5220\u9664\u5f53\u524d\u6570\u636e\u96c6\u8bb0\u5f55\u5417?";
sErrCode[2]="\u65b0\u5efa\u6570\u636e\u96c6\u8bb0\u5f55\u65f6\u5fc5\u987b\u521d\u59cb\u5efa\u7acb\u4e3b\u8bb0\u5f55";
sErrCode[3]="\u65b0\u5efa\u6570\u636e\u96c6\u8bb0\u5f55\u65f6\u5fc5\u987b\u4fdd\u5b58\u4e3b\u8bb0\u5f55\u4fe1\u606f";
sErrCode[4]="\u8bf7\u81f3\u5c11\u9009\u62e9\u4e00\u6761\u6570\u636e";
sErrCode[5]="\u5f53\u524d\u4e3b\u8bb0\u5f55\u72b6\u6001\u4e0b\u4e0d\u5141\u8bb8\u65b0\u5efa\u6570\u636e\u96c6\u8bb0\u5f55\u65f6";
function trimLeft(a,b){if(b==null||b==""){return a}while(a.indexOf(b)==0){a=a.substr(b.length)}return a}
function trimRight(a,b){if(b==null||b==""){return a}while(a.lastIndexOf(b)==a.length-b.length){a=a.substring(0,a.length-b.length)}return a}
function trim(a){return vbTrim(a)}
function IsNull(a){return("undefined"==typeof(a)||"unknown"==typeof(a)||null==a)}
function isArray(a){return a!=null&&((a instanceof Array)||(typeof a.splice)=="function")}
function isInt(b){var a;if(b==null){return true}if(typeof b!="string"){return false}b=vbTrim(b);if(b==""){return true}for(a=0;a<b.length;a++){if(a==0){if(b.charAt(a)!="-"&&(b.charAt(a)>"9"||b.charAt(a)<"0")){return false}}else{if(b.charAt(a)>"9"||b.charAt(a)<"0"){return false}}}return true}
function isFloat(c){var b;var a=0;if(c==null){return true}if(typeof c!="string"){return false}c=vbTrim(c);if(c==""){return true}if((c.charAt(0)>"9"||c.charAt(0)<"0")&&c.charAt(0)!="."&&c.charAt(0)!="-"){return false}else{if(c.charAt(0)=="."){a++}}for(b=1;b<c.length;b++){if((c.charAt(b)>"9"||c.charAt(b)<"0")&&c.charAt(b)!="."){return false}else{if(c.charAt(b)=="."){a++}}}if(a>1){return false}return true}
function isDate(a){if(a==null){return true}if(typeof a!="string"){return false}a=vbTrim(a);if(a==""){return true}return vbIsDate(a)}
function openDialog(d,a,e)
{var c=Math.random();
var f;
if(d.indexOf("?")<0){f=d+"?ran="+c.toString()}else{f=d+"&ran="+c.toString()}var b;var g=window.self;
b=window.showModalDialog(f,g,"dialogHeight:"+a+"px;dialogWidth:"+e+"px;edge:Raised;center:Yes;help:No;resizable:No;status:No;scroll:No");return b}
function openWindow(c,b,a,d,f,e){if(f){if((parseInt(navigator.appVersion)>=4)){xposition=(screen.width-d)/2;yposition=(screen.height-a)/2}}else{xposition=0;yposition=0}if(e==null||e==""){e="width="+d+",height="+a+",location=0,menubar=0,resizable=1,scrollbars=1,status=0,titlebar=0,toolbar=0,hotkeys=0,screenx="+xposition+",screeny="+yposition+",left="+xposition+",top="+yposition}else{e="width="+d+",height="+a+","+e+",screenx="+xposition+",screeny="+yposition+",left="+xposition+",top="+yposition}return window.open(c,b,e)}
function encodeURL(a){a=a.replace(/\+/g,"[]");a=a.replace(/%/g,"[%]");return a}
function encodeSql(a){a=a.replace(/&/g,"&amp;");a=a.replace(/>/g,"&gt;");a=a.replace(/</g,"&lt;");a=a.replace(/"/g,"&quot;");return a}
function encodeScript(a){a=a.replace(/"/g,DOUBLE_QUOTATION_REPLACEMENT);a=a.replace(/'/g,SINGLE_QUOTATION_REPLACEMENT);a=a.replace(/</g,LEFT_TAG_REPLACEMENT);a=a.replace(/>/g,RIGHT_TAG_REPLACEMENT);a=a.replace(/&/g,SINGLE_AND_REPLACEMENT);return a}
function getHostname(){return window.location.host}
function moveColumn(f,d,e){var c,a;var g,b;if(f==null||d<0||d>=f.rows(0).cells.length||e>=f.rows(0).cells.length||e<0){return}for(c=0;c<f.rows.length;c++){g=f.rows(c).cells(d);b=f.rows(c).cells(e);g.swapNode(b)}}function fillItemsIntoDropDownList(a,j,g,c){var d,l;var k,b;var h;try{l=new window.recordset();l.sqlSource=a;l.open();for(d=0;d<l.recordCount;d++){k=l.item(d,j);b=l.item(d,g);h=document.createElement("OPTION");c.appendChild(h);h.innerText=b;h.value=k}}catch(f){window.alert("fillItemsIntoDropDownList Error:"+f.message)}}function addItemIntoDropDownList(f,a,b){var d;try{d=document.createElement("OPTION");b.appendChild(d);d.innerText=a;d.value=f}catch(c){window.alert("addItemIntoDropDownList Error:"+c.message)}}function clearItemsFromDropDownList(a){try{a.options.length=0}catch(b){window.alert("clearItemsFromDropDownList Error:"+b.message)}}function htmlFormatText(b,a){document.execCommand(b,true,a)}
function _assert(b,a){if(b!=true){throw new Error(0,a)}}function translateSqlDataType(a){if(a==null||a==""){return""}a=a.toLowerCase();switch(a){case"bigint":case"int":return"int";case"decimal":case"float":return"float";case"bit":return"bool";case"varchar":case"nvarchar":case"char":case"nchar":case"uniqueidentifier":return"string";case"smalldatetime":case"datetime":return"datetime";default:return"string"}}function postScript(timerIndex,script,containerId){var bVisible=true;var parentObj=document.getElementById(containerId);while(parentObj!=null&&parentObj.tagName.toUpperCase()!="BODY"){if(parentObj.currentStyle.display=="none"){bVisible=false;break}parentObj=parentObj.parentElement}if(bVisible==false){if(timerIndex>=timerId.length){timerId[timerIndex]=""}if(timerId[timerIndex]==""){timerId[timerIndex]=window.setInterval("postScript("+timerIndex.toString()+",'"+script+"','"+containerId+"')",200)}else{window.clearInterval(timerId[timerIndex]);timerId[timerIndex]="";timerId[timerIndex]=window.setInterval("postScript("+timerIndex.toString()+",'"+script+"','"+containerId+"')",200)}}else{if(timerId[timerIndex]!=""){window.clearInterval(timerId[timerIndex]);timerId[timerIndex]=""}eval(script)}}function doPrint(f,b,a,d,e){if((f.toString()=="")||(f.toString()=="0")){var c=getAlertMsg("\u4e0d\u80fd\u6267\u884c\u6253\u5370\u64cd\u4f5c","\u6ca1\u6709\u9009\u62e9\u4e3b\u8bb0\u5f55","\u8bf7\u67e5\u770b\u4f7f\u7528\u5e2e\u52a9!");window.alert(c)}else{if(e){if((parseInt(navigator.appVersion)>=4)){xposition=(screen.width-d)/2;yposition=(screen.height-a)/2}}else{xposition=0;yposition=0}sFeatures="width="+d+",height="+a+",location=0,menubar=1,resizable=1,scrollbars=1,status=0,titlebar=0,toolbar=0,hotkeys=0,screenx="+xposition+",screeny="+yposition+",left="+xposition+",top="+yposition;if(b.indexOf("?")<0){window.open(b+"?AutoInc="+f.toString(),"",sFeatures)}else{window.open(b+"&AutoInc="+f.toString(),"",sFeatures)}}}var sCheck_ErrMsg=new Array();sCheck_ErrMsg[0]="\u8f93\u5165\u7684\u5b57\u6bb5\u503c\u5fc5\u987b\u4e3a\u6574\u6570\u7c7b\u578b(0123456789)";sCheck_ErrMsg[1]="\u8f93\u5165\u7684\u5b57\u6bb5\u503c\u5fc5\u987b\u4e3a\u65e5\u671f\u7c7b\u578b(yyyy-MM-dd)";sCheck_ErrMsg[2]="\u8f93\u5165\u7684\u5b57\u6bb5\u503c\u5fc5\u987b\u4e3a\u6570\u503c\u7c7b\u578b(.0123456789)";sCheck_ErrMsg[3]="\u7ea2\u8272\u6846\u7684\u5b57\u6bb5\u503c\u662f\u5fc5\u586b\u9879,\u8bf7\u5b8c\u6574\u586b\u5199\u540e\u518d\u4fdd\u5b58";sCheck_ErrMsg[4]="\u60a8\u6b63\u8f93\u5165\u6216\u7f16\u8f91\u7684\u503c\u4e0d\u5141\u8bb8\u4e3a\u7a7a,\u8bf7\u5b8c\u6574\u586b\u5199";sCheck_ErrMsg[5]="\u8f93\u5165\u7684\u5b57\u6bb5\u503c\u5185\u5bb9\u8d85\u957f,\u65e0\u6cd5\u4fdd\u5b58";var sDatagrid_ErrMsg=new Array();sDatagrid_ErrMsg[0]="Error:dscName,xmlEngine or sqlSource property is not allowed to be set null";sDatagrid_ErrMsg[1]="Warning:if tableName or keyfieldname property is null,then the datagrid is not allowed to be updated";sDatagrid_ErrMsg[2]="Error:Error in DSO object creating";sDatagrid_ErrMsg[3]="\u5f53\u524d\u6ca1\u6709\u6570\u636e\u8bb0\u5f55\u53ef\u4f9b\u5220\u9664";sDatagrid_ErrMsg[4]="Error:xml recordset is not loaded completed";sDatagrid_ErrMsg[5]="Error:xml src string or QueryString is too long";sDatagrid_ErrMsg[6]="Warning:the data has been edited and changed\nDo you want to proceed and discard the changed data?";sDatagrid_ErrMsg[7]="\u6807\u8bb0\u7ea2\u8272(*)\u7684\u5217\u7684\u503c\u4e0d\u5141\u8bb8\u4e3a\u7a7a,\u8bf7\u5b8c\u6574\u586b\u5199\u540e\u518d\u4fdd\u5b58";sDatagrid_ErrMsg[8]="the function's parameter is not correct";sDatagrid_ErrMsg[9]="\u8bf7\u81f3\u5c11\u9009\u62e9\u4e00\u6761\u6570\u636e";var sWfl_ErrMsg=new Array();sWfl_ErrMsg[0]="\u5de5\u4f5c\u6d41\u65e0\u6cd5\u6b63\u5e38\u64cd\u4f5c,\u539f\u56e0\uff1a\u51fd\u6570\u53c2\u6570\u4e0d\u8db3";sWfl_ErrMsg[1]="\u5de5\u4f5c\u6d41\u65e0\u6cd5\u6b63\u5e38\u64cd\u4f5c,\u539f\u56e0\uff1a\u65e0\u6ee1\u8db3\u6d41\u8f6c\u6761\u4ef6\u7684\u4e0b\u4e00\u73af\u8282,\u5728\u6d41\u8f6c\u8def\u5f84\u4e2d\u5fc5\u987b\u6307\u5b9a\u4e00\u4e2a\u7b26\u5408\u6761\u4ef6\u7684\u4e0b\u4e00\u73af\u8282";sWfl_ErrMsg[2]="\u5de5\u4f5c\u6d41\u65e0\u6cd5\u6b63\u5e38\u64cd\u4f5c,\u539f\u56e0\uff1a\u5b9e\u4f53\u4e0d\u652f\u6301\u5de5\u4f5c\u6d41\u64cd\u4f5c";sWfl_ErrMsg[3]="\u8fdb\u884c\u5de5\u4f5c\u6d41\u64cd\u4f5c\u524d\u5fc5\u987b\u4fdd\u5b58\u5b9e\u4f53\u5b9e\u4f8b";sWfl_ErrMsg[4]="\u53ea\u6709\u5b9e\u4f53\u7684\u5f55\u5165\u4eba\u5141\u8bb8\u542f\u52a8\u5de5\u4f5c\u6d41";

function point(iX, iY){
    this.x = iX;
    this.y = iY;
}
function f_GetX(e) {
    var l = e.offsetLeft;
    while (e = e.offsetParent) {
        l += e.offsetLeft;
    }
    return l;
}
function f_GetY(e) {
    var t = e.offsetTop;
    while (e = e.offsetParent) {
        t += e.offsetTop;
    }
    return t;
}
function f_GetXY(e)
{
    var pt = new point(0,0);
    pt.x = e.offsetLeft;
    pt.y = e.offsetTop;
    while (e = e.offsetParent) {
        pt.x += e.offsetLeft;
        pt.y += e.offsetTop;
    }
    return pt;
}
function validateDate(date,format){
    var time=date;
    if(time=="") return null;
    var reg=format;
    var reg=reg.replace(/yyyy/,"[0-9]{4}");
    var reg=reg.replace(/yy/,"[0-9]{2}");
    var reg=reg.replace(/MM/,"((0[1-9])|1[0-2])");
    var reg=reg.replace(/M/,"(([1-9])|1[0-2])");
    var reg=reg.replace(/dd/,"((0[1-9])|([1-2][0-9])|30|31)");
    var reg=reg.replace(/d/,"([1-9]|[1-2][0-9]|30|31))");
    var reg=reg.replace(/HH/,"(([0-1][0-9])|20|21|22|23)");
    var reg=reg.replace(/H/,"([0-9]|1[0-9]|20|21|22|23)");
    var reg=reg.replace(/mm/,"([0-5][0-9])");
    var reg=reg.replace(/m/,"([0-9]|([1-5][0-9]))");
    var reg=reg.replace(/ss/,"([0-5][0-9])");
    var reg=reg.replace(/s/,"([0-9]|([1-5][0-9]))");
    reg=new RegExp("^"+reg+"$");
    if(reg.test(time)==false){//验证格式是否合法
        return false;
    }
    return true;
}

function downloadFile(url) {
    var elemIF = document.createElement("iframe");
    elemIF.src = url;
    elemIF.style.display = "none";
    document.body.appendChild(elemIF);
}