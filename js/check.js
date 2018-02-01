/* for verify data  */

// for chinese in the string
String.prototype.dataLength = function()
{
	var i = 0;
	var str = "";
	var iULength = 0;
	str = this.toString();
	for(i=0;i<str.length;i++)
	{
		if(str.charCodeAt(i)>=256)
		{iULength = iULength + 2;}
		else
		{iULength++;}
	}
	return iULength;
}
function checkFieldDataType(oField)
{
	var _cInit;
	var _sDataType;
	var bReturnValue = true;
	if(oField==null) return bReturnValue;
	_cInit = oField.id.toString().toLowerCase().charAt(0);
	switch(_cInit)
	{
		case "i":
			if(isInt(oField.value)!=true) {window.alert(sCheck_ErrMsg[0]);bReturnValue=false;}
			break;
		case "f":
			if(isFloat(oField.value)!=true) {window.alert(sCheck_ErrMsg[2]);bReturnValue=false;}
			break;
		case "d":
			if(isDate(oField.value)!=true) {window.alert(sCheck_ErrMsg[1]);bReturnValue=false;}
			break;
		default:
			_sDataType = oField.getAttribute("datatype");
			switch(_sDataType)
			{
				case "int":
					if(isInt(oField.value)!=true) {window.alert(sCheck_ErrMsg[0]);bReturnValue=false;}
					break;
				case "float":
					if(isFloat(oField.value)!=true) {window.alert(sCheck_ErrMsg[2]);bReturnValue=false;}
					break;
				case "datetime":
					if(isDate(oField.value)!=true) {window.alert(sCheck_ErrMsg[1]);bReturnValue=false;}
					break;
				case "currency":
					if(isFloat(oField.value)!=true) {window.alert(sCheck_ErrMsg[2]);bReturnValue=false;}
					break;
			}
			break;
	}
	return bReturnValue;
}
function checkFieldIsRequired(oField)
{
	var bReturnValue = true;
	if(oField==null) return bReturnValue;
	if(oField.getAttribute("isrequired")!="true") return bReturnValue;
	if(trim(oField.value)==""){window.alert(sCheck_ErrMsg[4]);bReturnValue=false;}
	return bReturnValue;
}

function checkFieldDataLength(oField)
{
	var bReturnValue = true;
	var sDatalength = "0";
	if(oField==null) return bReturnValue;
	sDatalength = oField.getAttribute("datalength");
	if(oField.value.toString().dataLength()>parseInt(sDatalength)){window.alert(sCheck_ErrMsg[5]);bReturnValue=false;}
	return bReturnValue;
} 
//
function document.onbeforeupdate()
{
	var bReturnValue = true;
	var _obj = null;
	_obj = event.srcElement;
	if(checkFieldIsRequired(_obj)==false) {event.returnValue = false;return;} else {_obj.parentElement.runtimeStyle.border = "";}
	if(checkFieldDataType(_obj)==false) {event.returnValue = false;return;}
	if(checkFieldDataLength(_obj)==false) {event.returnValue = false;return;}
}
//
function checkDatafieldIsRequired(oDatasrc)
{
	var i = 0;
	var oDivs = null;
	var oParentDIV = null;
	var sType = "";
	var sDatasrc = "";
	var sDatafield = "";
	var sIsRequired = "";
	var oObj = null;
	var oMarked;
	var sTagName = "";
	try
	{
		oDivs = document.getElementsByTagName("DIV");
		for(i=0;i<oDivs.length;i++)
		{
			sType = oDivs[i].getAttribute("type");
			sDatasrc = oDivs[i].getAttribute("datasrc");
			if(sType!=null && sDatasrc!=null && sType.toLowerCase()=="dataconsumer" && sDatasrc==oDatasrc.dsoName)
			{oParentDIV = oDivs[i];break;}
		}
		if(oParentDIV==null) return;
		
		var j;
		var iLength;
		var k = oParentDIV.childNodes.length;
		var sDisplay,sIsSectionEnabled;
		var oSection,oInputs,oTextAreas,oSelects;
		for (i=0;i<k;i++)
		{
			oSection = oParentDIV.childNodes[i];
			sDisplay = oSection.currentStyle.display;
			sIsSectionEnabled = oSection.getAttribute("isenabled");
			if (sDisplay!="none" && (sIsSectionEnabled==null || sIsSectionEnabled=="true"))
			{
				oInputs = oSection.getElementsByTagName("INPUT");
				iLength = oInputs.length;
				for (j=0;j<iLength;j++)
				{
					oObj = oInputs[j];
					if(oObj.dataSrc==oDatasrc.dsoName)
					{
						sIsRequired = oObj.getAttribute("isrequired");
						if(sIsRequired=="true")
						{
							sDatafield = oObj.dataFld;
							if(oDatasrc.fields(sDatafield)!=null && oDatasrc.fields(sDatafield).value=="")
							{
								//±ê¼Ç¸Ã×Ö¶Î
								oMarked = oObj;
								//ÅÐ¶Ï¸Ã×Ö¶ÎÊÇ·ñ´¦ÓÚÒþ²Ø×´Ì¬
								while (oObj!=null && oObj.currentStyle.display!="none" && !(oObj===oSection))
								{
									oObj = oObj.parentElement;
								}
								if (oObj.currentStyle.display!="none")
								{
									oMarked.parentElement.runtimeStyle.border = "1px solid red";
									window.alert(window.sCheck_ErrMsg[3]);
									return false;
								}
							}
						}
					}
				}
				oTextAreas = oSection.getElementsByTagName("TEXTAREA");
				iLength = oTextAreas.length;
				for (j=0;j<iLength;j++)
				{
					oObj = oTextAreas[j];
					if(oObj.dataSrc==oDatasrc.dsoName)
					{
						sIsRequired = oObj.getAttribute("isrequired");
						if(sIsRequired=="true")
						{
							sDatafield = oObj.dataFld;
							if(oDatasrc.fields(sDatafield)!=null && oDatasrc.fields(sDatafield).value=="")
							{
								//±ê¼Ç¸Ã×Ö¶Î
								oMarked = oObj;
								//ÅÐ¶Ï¸Ã×Ö¶ÎÊÇ·ñ´¦ÓÚÒþ²Ø×´Ì¬
								while (oObj!=null && oObj.currentStyle.display!="none" && !(oObj===oSection))
								{
									oObj = oObj.parentElement;
								}
								if (oObj.currentStyle.display!="none")
								{
									oMarked.parentElement.runtimeStyle.border = "1px solid red";
									window.alert(window.sCheck_ErrMsg[3]);
									return false;
								}
							}
						}
					}
				}
				oSelects = oSection.getElementsByTagName("SELECT");
				iLength = oSelects.length;
				for (j=0;j<iLength;j++)
				{
					oObj = oSelects[j];
					
					if(oObj.dataSrc==oDatasrc.dsoName)
					{
						sIsRequired = oObj.getAttribute("isrequired");
						if(sIsRequired=="true")
						{
							sDatafield = oObj.dataFld;
							if(oDatasrc.fields(sDatafield)!=null && oDatasrc.fields(sDatafield).value=="")
							{
								//±ê¼Ç¸Ã×Ö¶Î
								oMarked = oObj;
								//ÅÐ¶Ï¸Ã×Ö¶ÎÊÇ·ñ´¦ÓÚÒþ²Ø×´Ì¬
								while (oObj!=null && oObj.currentStyle.display!="none" && !(oObj===oSection))
								{
									oObj = oObj.parentElement;
								}
								if (oObj.currentStyle.display!="none")
								{
									oMarked.parentElement.runtimeStyle.border = "1px solid red";
									window.alert(window.sCheck_ErrMsg[3]);
									return false;
								}
							}
						}
					}
				}
			}
		}
		return true;
	}
	catch(e)
	{window.alert("checkdatafieldisrequired Error:" + e.message);return false;}
}
//