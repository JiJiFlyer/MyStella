function __dragSplitter() {
    event.cancelBubble = true;
	if (__bStartDrag_splitter == true && splitterDragged!=null) {
	    switch (splitType)
	    {
	        case 'v':
	            splitterDragged.style.left = window.event.screenX - __iX_Left;
	            break;
	        case 'h':
	            splitterDragged.style.top = window.event.screenY - __iY_Top;
	            break;
	    }
	    
	}
}
var __iInitWidth_l,__iInitWidth_r;
var __bDragged_splitter = false;
function __restoreSplitter() {
	if (__bDragged_splitter==true)
	{
		__bDragged_splitter = false;
	}
}
var __iX_Left;
var __iY_Top;
var __iX_splitter_screen;
var __iY_splitter_screen;
var __bStartDrag_splitter = false;
var splitter=null;
var splitterDragged=null;
var splitPanel=null;
var splitType=null;
function __startDragSplitter(sType) {
    
    splitter=event.srcElement;
    while (splitter!=null && ((splitter.tagName!="BODY" && splitter.tagName!="TD") || splitter.getAttribute("collapse")==null))
    {
        splitter=splitter.parentElement;
    }
    if (splitter==null || splitter.tagName=="BODY") return;
    
    splitPanel=splitter.parentElement.parentElement.parentElement;
    splitType=sType;
    __iX_splitter_screen = event.screenX;
    __iY_splitter_screen = event.screenY;
    var iLeft = f_GetX(splitter);
	var iTop = f_GetY(splitter);
	
	__bStartDrag_splitter = true;
	splitterDragged = document.createElement("DIV");
	splitterDragged.style.backgroundColor = "#6080b7";
	splitterDragged.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity:80)";
	splitterDragged.style.height = splitter.clientHeight;
	splitterDragged.style.width = splitter.clientWidth;
	splitterDragged.style.position = "absolute";
	splitterDragged.style.lineHeight = "1px";
	splitterDragged.innerHTML = "&nbsp;"
	splitterDragged.style.left = iLeft;
	splitterDragged.style.top = iTop;
    splitter.appendChild(splitterDragged);
	switch (splitType)
	{
	    case 'v':
	        __iX_Left = __iX_splitter_screen - iLeft;
	        break;
	    case 'h':
	        __iY_Top = __iY_splitter_screen - iTop;
	        break;
	}
	splitPanel.setCapture();
}
function __stopDragSplitter() {
	event.cancelBubble = true;
	if (__bStartDrag_splitter == true) {
		__bStartDrag_splitter = false;
		if (splitterDragged != null) splitterDragged.removeNode(true);
		switch (splitType)
		{
		    case 'v':
		        
		        if (event.screenX != __iX_splitter_screen)
		        {
			        var tdl = splitPanel.rows(0).cells(0);
			        var tdr = splitPanel.rows(0).cells(2);
			        if (tdl.style.width!="")
			        {
			            tdl.style.width = tdl.offsetWidth + (event.screenX - __iX_splitter_screen);
			        }
			        else
			        {
			            if (tdr.style.width!="")
			            {
			                tdr.style.width = tdr.offsetWidth - (event.screenX - __iX_splitter_screen);
			            }
			            else
			            {
			                tdl.style.width = tdl.offsetWidth + (event.screenX - __iX_splitter_screen);
			            }
			        }
			        __bDragged_splitter=true;
		        }
		        break;
		    case 'h':
		        if (event.screenY != __iY_splitter_screen)
		        {
			        var tdu = splitPanel.rows(0).cells(0);
			        var tdd = splitPanel.rows(2).cells(0);
			        if (tdu.style.height!="")
			        {
			            tdu.style.height = tdu.offsetHeight + (event.screenY - __iY_splitter_screen);
			        }
			        else
			        {
			            if(tdd.style.height!="")
			            {
			                tdd.style.height = tdd.offsetHeight - (event.screenY - __iY_splitter_screen);
			            }
			            else
			            {
			                tdu.style.height = tdu.offsetHeight + (event.screenY - __iY_splitter_screen);
			            }
			        }
			        
			        __bDragged_splitter=true;
		        }
		        break;
		}
		splitter=null;
		splitterDragged=null;
		splitPanel.releaseCapture();
		splitPanel=null;
	}
}