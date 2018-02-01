//与框架库有关的js代码
<!----------------------------------------------------------------------------
//  Copyright (c) 2003-2012 TopSpeed Info & Tech.Co.,Ltd.  All Rights Reserved.
//--------------------------------------------------------------------------->

//日期框弹出选择支持
function datePopup(oEvtElement)
{
    var oElement = oEvtElement.parentElement.cells(0).children(0);
    var sDataSrcId = oElement.dataSrc.substr(5);
    var sDataFld = oElement.dataFld;
    var oDataSrc = document.getElementById(sDataSrcId);
    if (oDataSrc!=null && (oDataSrc.status=="2" || oDataSrc.selectedRowStatus=="0"))
    {
        var pFun = window.eval("window.ondatepopup_" + sDataSrcId);
	    if (typeof(pFun)=='function') 
	    {
		    var _evt=document.createEventObject();
		    _evt.element = oElement
		    _evt.dataFld = sDataFld;
	        if (pFun(_evt)==false) return;
	    }
	    openDialog(Macro_VAR.RootPath + 'popup_process/calendar.aspx?controlid='+oElement.id,'260','400');
    }
}
//一般控件弹出选择支持
function pickPopup(oEvtElement)
{
    var oElement = oEvtElement.parentElement.cells(0).children(0);
    var sDataSrcId = oElement.dataSrc.substr(5);
    var sDataFld = oElement.dataFld;
    var oDataSrc = document.getElementById(sDataSrcId);
    if (oDataSrc!=null && (oDataSrc.status=="2" || oDataSrc.selectedRowStatus=="0"))
    {
        var pFun = window.eval("window.onpickpopup_" + sDataSrcId);
	    if (typeof(pFun)=='function') 
	    {
		    var _evt=document.createEventObject();
		    _evt.element = oElement;
		    _evt.dataFld = sDataFld;
		    return pFun(_evt);
	    }
    }
}
//设置表单字段属性
function makeFieldEnable(oDatagrid,oField,bEnabled)
{
    switch (bEnabled)
    {
        case true:
            oField.setAttribute("isenabled","true");
            if (oDatagrid.status == "2" || oDatagrid.selectedRowStatus == "0") {
		        oField.disabled = false;
		    }
            break;
        case false:
            oField.setAttribute("isenabled","false");
            oField.setAttribute("isrequired","false");
            oField.disabled = true;
            break;
    }
}
function makeFieldReadOnly(oDatagrid,oField,bReadOnly)
{
    if (oField.tagName=="SELECT")
    {
        makeFieldEnable(oDatagrid,oField,bReadOnly==true,false,true);
    }
    else
    {
        switch (bReadOnly)
        {
            case true:
                oField.setAttribute("isreadonly","true");
                oField.readOnly = true;
                break;
            case false:
                oField.setAttribute("isreadonly","false");
                if (oDatagrid.status == "2" || oDatagrid.selectedRowStatus == "0") {
		            oField.readOnly = false;
		        }
                break;
        }
    }
}
function makeFieldRequired(oDatagrid,oField,bIsRequired)
{
    switch (bIsRequired)
    {
        case true:
            oField.setAttribute("isrequired","true");
            oField.setAttribute("isenabled","true");
            if (oDatagrid.status == "2" || oDatagrid.selectedRowStatus == "0") {
		        oField.disabled = false;
		    }
            break;
        case false:
            oField.setAttribute("isrequired","false");
            break;
    }
}
function applyFieldCssClass(oDatagrid,oField)
{
    var bIsRequired = oField.getAttribute("isrequired");
    var bIsEnabled = oField.getAttribute("isenabled");
    
    bIsRequired = (bIsRequired=="true"?true:false);
    bIsEnabled = (bIsEnabled=="false"?false:true);
    if (bIsEnabled==true)
    {
        if (bIsRequired==true)
        {
            oField.className = "inputField required";
        }
        else
        {
            oField.className = "inputField";
        }
    }
    else
    {
        oField.className = "inputField disabled";
    }
}
function makeFieldVisible(oField,bVisible)
{
    var oFieldTable = oField.parentElement.parentElement.parentElement.parentElement;
    var bMakeRowVisible;
    var oRowTable = oFieldTable.parentElement.parentElement;
    switch (bVisible)
    {
        case true:
            if (oFieldTable!=null && oFieldTable.tagName=="TABLE")
            {
                oFieldTable.style.display = "block";
            }
            if (oRowTable.style.display.toLowerCase()=="none") oRowTable.style.display = "block";
            break;
        case false:
            if (oFieldTable!=null && oFieldTable.tagName=="TABLE")
            {
                oFieldTable.style.display = "none";
            }
            bMakeRowVisible = false;
            for (var i=0;i<oRowTable.cells.length;i++)
            {
                if (oRowTable.cells(i).children(0).style.display.toLowerCase()!="none")
                {
                    bMakeRowVisible = true;
                }
            }
            if (bMakeRowVisible==false)
            {
                oRowTable.style.display = "none";
            }
            else
            {
                oRowTable.style.display = "block";
            }
            break;
    }
}
function makeSectionEnable(oDataGrid,oSection,bEnabled)
{
    var oDiv = oSection.parentElement;
    var sDataSrc = oDiv.getAttribute("datasrc");
    if (oDataGrid==null)
    {
        var sDataGridId = sDataSrc.substr(5);
        oDataGrid = document.getElementById(sDataGridId);
    }
    if (oDataGrid==null) return;
    var i,iLength;
    var oCtrl,sCtrlType;
    var oInputs,oButtons,oTextAreas,oSelects;
    oInputs = oSection.getElementsByTagName("INPUT");
	oButtons = oSection.getElementsByTagName("BUTTON");
	oTextAreas = oSection.getElementsByTagName("TEXTAREA");
	oSelects = oSection.getElementsByTagName("SELECT");
	switch (bEnabled)
	{
	    case true:
	        oSection.setAttribute("isenabled","true");
	        if (oDataGrid.status == "2" || oDataGrid.selectedRowStatus == "0") {
		        //input
		        iLength = oInputs.length;
		        for (i=0;i<iLength;i++)
		        {
		           oCtrl = oInputs[i];
		           if (oCtrl.dataSrc==sDataSrc)
		           {
		               sCtrlType = oCtrl.type.toLowerCase();
		               if (oCtrl.onclick != null || oCtrl.ondblclick != null || oCtrl.onmousedown != null || sCtrlType == "checkbox") 
	                   { 
	                        var b = oCtrl.getAttribute("isenabled"); 
	                        if (b==null || b=="true") { oCtrl.disabled = false;} 
	                   } 
	                   else 
	                   { 
	                        var b = oCtrl.getAttribute("isreadonly"); 
	                        if (b==null || b=="false") 
	                        { oCtrl.readOnly = false; }
	                   } 
		           }
		        }
		        //button
		        iLength = oButtons.length;
		        for (i=0;i<iLength;i++)
		        {
		           oCtrl = oButtons[i];
		           if (oCtrl.dataSrc==sDataSrc)
		           {
		               var b = oCtrl.getAttribute("isenabled"); 
	                   if (b==null || b=="true") { oCtrl.disabled = false;} 
		           }
		        }
		        //textarea
		        iLength = oTextAreas.length;
		        for (i=0;i<iLength;i++)
		        {
		           oCtrl = oTextAreas[i];
		           if (oCtrl.dataSrc==sDataSrc)
		           {
		               var b = oCtrl.getAttribute("isenabled"); 
	                   if (b==null || b=="true") { oCtrl.disabled = false;} 
		           }
		        }
		        //select
		        iLength = oSelects.length;
		        for (i=0;i<iLength;i++)
		        {
		           oCtrl = oSelects[i];
		           if (oCtrl.dataSrc==sDataSrc)
		           {
		               var b = oCtrl.getAttribute("isenabled"); 
	                   if (b==null || b=="true") { oCtrl.disabled = false;} 
		           }
		        }
		    }
	        break;
	    case false:
	        oSection.setAttribute("isenabled","false");
	        if (oDataGrid.status == "2" || oDataGrid.selectedRowStatus == "0") {
	            //input
		        iLength = oInputs.length;
		        for (i=0;i<iLength;i++)
		        {
		           oCtrl = oInputs[i];
		           if (oCtrl.dataSrc==sDataSrc)
		           {
		               sCtrlType = oCtrl.type.toLowerCase();
		               if (oCtrl.onclick != null || oCtrl.ondblclick != null || oCtrl.onmousedown != null || sCtrlType == "checkbox") 
	                   { 
	                        var b = oCtrl.getAttribute("isenabled"); 
	                        if (b==null || b=="true") { oCtrl.disabled = true;} 
	                   } 
	                   else 
	                   { 
	                        var b = oCtrl.getAttribute("isreadonly"); 
	                        if (b==null || b=="false") 
	                        { oCtrl.readOnly = true; }
	                   } 
		           }
		        }
		        //button
		        iLength = oButtons.length;
		        for (i=0;i<iLength;i++)
		        {
		           oCtrl = oButtons[i];
		           if (oCtrl.dataSrc==sDataSrc)
		           {
		               var b = oCtrl.getAttribute("isenabled"); 
	                   if (b==null || b=="true") { oCtrl.disabled = true;} 
		           }
		        }
		        //textarea
		        iLength = oTextAreas.length;
		        for (i=0;i<iLength;i++)
		        {
		           oCtrl = oTextAreas[i];
		           if (oCtrl.dataSrc==sDataSrc)
		           {
		               var b = oCtrl.getAttribute("isenabled"); 
	                   if (b==null || b=="true") { oCtrl.disabled = true;} 
		           }
		        }
		        //select
		        iLength = oSelects.length;
		        for (i=0;i<iLength;i++)
		        {
		           oCtrl = oSelects[i];
		           if (oCtrl.dataSrc==sDataSrc)
		           {
		               var b = oCtrl.getAttribute("isenabled"); 
	                   if (b==null || b=="true") { oCtrl.disabled = true;} 
		           }
		        }
	        }
	        break;
	}
}
function makeGroupEnable(oDataGrid,oGroup,bEnabled)
{
    var sDataSrc = oGroup.getAttribute("datasrc");
    if (oDataGrid==null)
    {
        var sDataGridId = sDataSrc.substr(5);
        oDataGrid = document.getElementById(sDataGridId);
    }
    if (oDataGrid==null) return;
    var i,iLength;
    var oSection;
    iLength = oGroup.childNodes.length;
    for (i=0;i<iLength;i++)
    {
        oSection = oGroup.childNodes[i];
        makeSectionEnable(oDataGrid,oSection,bEnabled);
    }
}