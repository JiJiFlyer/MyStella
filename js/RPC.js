var _xmldoc = null;
function XMLRecord(sFieldName,targetWindow)
{
	var oXmlDoc;
	if (targetWindow!=undefined)
	{oXmlDoc = targetWindow._xmldoc;}
	else
	{oXmlDoc = _xmldoc;}
	if (oXmlDoc != null)
	{
		var oRoot = oXmlDoc.documentElement;
		var oNode = oRoot.childNodes(0);
		var sReturnValue = "";
		var cReturnValueSeparator = "~~@@##$$";
		sReturnValue = oNode.getAttribute(sFieldName.toUpperCase());
		if (sFieldName.toUpperCase()=="RETURNCODE") {return sReturnValue};
		if (sReturnValue.indexOf(cReturnValueSeparator)>=0)
		{return sReturnValue.split(cReturnValueSeparator);}
		else
		{return sReturnValue;}
		
	}
	else
	{
		throw new Error(0,"XMLRecord is null");
	}
}
	
//RPC.js message definition
var sRPC_ErrMsg = new Array();
sRPC_ErrMsg[0] ="Error:please specify the path of RPC function's page";
sRPC_ErrMsg[1] ="Error:please specify the RPC function's name";
/* server object */
function server(sURL,sFunctionName,oTargetWindow)
{
	this.URL = sURL;
	this.functionName = sFunctionName;
	this.targetWindow = oTargetWindow;
	this.call = _call;
	this.responseText = _responseText;
}
function _call()
{
	_assert(this.URL != "" && this.URL != null , window.sRPC_ErrMsg[0]);
	_assert(this.functionName != "" && this.functionName != null , window.sRPC_ErrMsg[1]);
	var i,j;
	var sPostXML;
	var sURL;
	var sArg = "";
	var cSeparator = "~~@@##$$";//此处出现重复的几率很小
	sPostXML = "<root functionName=\"" + this.functionName + "\"";
	for(i=0;i<arguments.length;i++)
	{
		if (arguments[i] instanceof Array)
		{
			(arguments[i])[0] = (arguments[i])[0]==null? "":(arguments[i])[0];
			sArg = window.encodeScript((arguments[i])[0].toString());
			for (j=1;j<(arguments[i]).length;j++)
			{
				(arguments[i])[j] = (arguments[i])[j]==null? "":(arguments[i])[j];
				sArg += (cSeparator + window.encodeScript((arguments[i])[j].toString()));
			}
		}
		else
		{
			arguments[i] = arguments[i]==null? "":arguments[i];
			sArg = window.encodeScript(arguments[i].toString());
		}
		sPostXML = sPostXML + " arg" + i.toString() + "=\"" + sArg + "\"";
	}
	sPostXML = sPostXML + "></root>";
	var xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
	if (window.dialogArguments!=null)
	{
		with(window.dialogArguments)
		{
			if(this.URL.indexOf("?")<0)
				sURL = this.URL + '?callType=RPC';
			else
				sURL = this.URL + '&callType=RPC';
		}
	}
	else
	{
		if(this.URL.indexOf("?")<0)
			sURL = this.URL + '?callType=RPC';
		else
			sURL = this.URL + '&callType=RPC';
	}
	xmlhttp.open("POST",sURL,false);
	xmlhttp.send(sPostXML);
	var targetWindow;
	if (this.targetWindow==undefined)
		targetWindow = window.self;
	else
		targetWindow = this.targetWindow;
	targetWindow._xmldoc = xmlhttp.responseXML;
	return XMLRecord(this.functionName,targetWindow);
}
function _responseText()
{
    if (this.URL==null || this.URL=="") throw new Error(0,window.sRPC_ErrMsg[0]);
	var xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
	var sUrl = this.URL;
	xmlhttp.open("POST",sUrl,false);
	xmlhttp.send();
	return xmlhttp.responseText;
}