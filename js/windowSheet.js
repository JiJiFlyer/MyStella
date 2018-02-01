var _oActiveTab = null;
var _iMaxTab = 1;
var _oActiveDiv = null;
function selTabsheet(oTable)
{
	try
	{
		if(oTable.className=="selectedTabsheet") return;
		_oActiveTab.className="unselectedTabsheet";
		oTable.className="selectedTabsheet";
		_oActiveTab=oTable;
		if(_oActiveDiv!=null) _oActiveDiv.style.display="none";
		
		_oActiveDiv=eval(_oActiveTab.id+"_Div");
		_oActiveDiv.style.display="";
	}
	catch(e)
	{window.alert(e.message);}
}
function addDiv(tabURL)
{
	var oDiv;
	var _oIFrame;
	oDiv=document.createElement("div")
	oDiv.setAttribute("id","Tab"+_iMaxTab+"_Div")
	oDiv.style.width="100%"
	oDiv.style.border="1 #808080"
	oDiv.style.backgroundcolor="#F7F3E9"
	_oIFrame=document.createElement("iframe")
	if(tabURL != "" && tabURL != null && tabURL != undefined)
	{_oIFrame.setAttribute("src",tabURL);}
	
	_oIFrame.setAttribute("frameBorder","0")
	_oIFrame.setAttribute("scrolling","0")
	_oIFrame.style.border="0 solid black"
	_oIFrame.style.width="100%"
	_oIFrame.style.height="100%"
	_oIFrame.style.overflow="hidden"
	_oIFrame.style.zindex="1"
	oDiv.appendChild(_oIFrame)
	if(_oActiveDiv!=null)
	    _oActiveDiv.style.display = "none"
	oDiv.style.height = "100%"
	_oActiveDiv=oDiv
	_oActiveDiv.style.display=""
	IFrameContainer.appendChild(oDiv)
}

function addTabSheet(sTabText,sTabURL,sCmdId)
{
	var sURL = "";
	if(_oActiveTab!=null)
	{_oActiveTab.className="unselectedTabsheet";}
	else
	{emptyTabsheet(false);}

	var obj = TabsheetTable.children(0).children(0);
	var length = obj.children.length;
	if(length>0)
	{
		for(var i=0;i<length;i++)
		{
			if(vbTrim(obj.children(i).innerText)==sTabText)
			{
				//如果当前tabsheet已经打开则置前
				selTabsheet(obj.children(i).children(0));
				//判断URL是否发生变化,有则重新载入
				if (_oActiveDiv.children(0)!=null && _oActiveDiv.children(0).tagName=="IFRAME")
				{
					if(sTabURL.indexOf("?")<0)
					{sURL = window.ROOT_PATH + sTabURL+"?entityid="+sCmdId;}
					else
					{sURL = window.ROOT_PATH + sTabURL+"&entityid="+sCmdId;}
					if (sURL.toLowerCase()!=_oActiveDiv.children(0).src.toLowerCase()) 
					{_oActiveDiv.children(0).src = sURL;}
				}
				return;
			}
		}
	}
	
	oTable=document.createElement("table")
	oTable.className="selectedTabsheet"
	oTable.setAttribute("cellpadding","0")
	oTable.setAttribute("id","Tab"+_iMaxTab)
	oTable.setAttribute("cellspacing","0")
	oTable.attachEvent("onclick",BindingTabEvent)
	oTable.attachEvent("ondblclick",closeTabsheet)
	oTable.attachEvent("onContextMenu",BindingPopEvent)
	oNobr=document.createElement("nobr")
	otBody=document.createElement("tbody")
	oTr=document.createElement("tr")
	oTd=document.createElement("td")
	oTd.innerHTML="<nobr>&#160;"+ sTabText + "&#160;</nobr>"
	oTr.appendChild(oTd)
	otBody.appendChild(oTr)
	oTable.appendChild(otBody)
	_oActiveTab=oTable
	oTd=document.createElement("td")
	oTd.appendChild(oTable)
	TabsheetTable.children(0).children(0).appendChild(oTd)
	
	if(sTabURL.indexOf("?")<0) 
	{sURL = window.ROOT_PATH + sTabURL + "?entityid=" + sCmdId;}
	else
	{sURL = window.ROOT_PATH + sTabURL + "&entityid=" + sCmdId;}
	
	addDiv(sURL)
	_iMaxTab++
}

function BindingPopEvent()
{}

function BindingTabEvent()
{
	try
	{
		oTable=window.event.srcElement
		while(oTable.tagName!="TABLE")
		{
			oTable=oTable.parentElement
		}
		selTabsheet(oTable)
	}
	catch(e)
	{
		alert("The BindingTabEvent Function produce a Error!")
	}			
}

function tabsheetControllerOver(oTd)
{
	oTd.className="tabsheetControllerOver"
}
function tabsheetControllerOut(oTd)
{
	oTd.className="tabsheetController"
}
function tabsheetControllerClick(oTd)
{
	oTd.className="tabsheetControllerClick"
}
function closeTabsheet()
{
	try
	{
		if(TabsheetTable.rows(0).cells.length>0)
		{
			_oActiveDiv.children(0).contentWindow.document.write("");
			if (_oActiveDiv.children(0).contentWindow.document.body==null)
			{
				TabsheetTable.rows(0).deleteCell(_oActiveTab.parentElement.cellIndex);
				_oActiveDiv.parentElement.removeChild(_oActiveDiv);
				if(TabsheetTable.rows(0).cells.length!=0)
				{
					_oActiveTab=TabsheetTable.rows(0).cells(TabsheetTable.rows(0).cells.length-1).children(0);
					_oActiveTab.className="selectedTabsheet";
					_oActiveDiv=eval(_oActiveTab.id+"_Div");
					_oActiveDiv.style.display="";
				}
				else
				{
					_oActiveTab=null;
					_oActiveDiv=null;
					emptyTabsheet(true);
				}
			}
		}
	}
	catch(e)
	{}
}

function emptyTabsheet(oBool)
{
	if(oBool)
	{
		TabSheetDiv.style.backgroundColor="#808080";
		TabSheetController.style.display="none";
		TabTR.style.display="none";
	}
	else
	{
		TabSheetDiv.style.backgroundColor="#F7F3E9";
		TabSheetController.style.display="";
		TabTR.style.display="";
		TabBody.style.width=TabTR.offsetWidth-TabSheetController.style.width;
		IFrameContainer.style.backgroundColor="";
	}
}

function tabsheetMoveRight()
{
	try
	{
		selTabsheet(eval("Tab"+(parseInt(_oActiveTab.id.charAt(3))+1)));
	}
	catch(e)
	{}
}

function tabsheetMoveLeft()
{
	try
	{
		selTabsheet(eval("Tab"+(parseInt(_oActiveTab.id.charAt(3))-1)));
	}
	catch(e)
	{}
}
function refreshFrame()
{_oActiveDiv.children(0).Refresh();}