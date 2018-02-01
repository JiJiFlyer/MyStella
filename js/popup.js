var popAction = "";
var popupTimeout
var boxTimeout 
var menuTimeOut   
var thisEvent = null;

// Netscape Navigator Handling
document.onclick = setupEvent;
function setupEvent( e ) {	thisEvent = e; }     

function getEvent()
{
	var e = window.event;
	if ( ! e) e = thisEvent;
	return ( e );
}



function adjustPosition(e, element)
{
	wnd_height=document.body.clientHeight;
	wnd_width = document.body.clientWidth;
	
	tooltip_width =(element.style.pixelWidth) ? element.style.pixelWidth  : element.offsetWidth;
	tooltip_height=(element.style.pixelHeight)? element.style.pixelHeight : element.offsetHeight;

	offset_y = (e.clientY + tooltip_height - document.documentElement.scrollTop + 30 >= wnd_height) ? -5 - tooltip_height : 5;

	element.style.left = Math.min(wnd_width - tooltip_width - 5, Math.max(3, e.clientX + 6)) + document.body.scrollLeft + 'px';

	element.style.top = e.clientY + offset_y + document.documentElement.scrollTop + 'px';
	element.style.display = "block";
}

function displayLater( menuId) {
	document.getElementById(menuId).style.filter="alpha(opacity=100)";
}

function displayDiv( menuId, popAction, timeOut) {
	var menu;
	
	menuId = (menuId)?menuId:"popmenu";
	
	e = getEvent();
	if ( (menu = document.getElementById(menuId)) != null )
	{
	    
		if ( timeOut )
		{
			menu.style.filter="alpha(opacity=0)";
			adjustPosition(e, menu)
			popupTimeout = window.setTimeout("displayLater('" + menuId + "')", timeOut)
		}
		else
			adjustPosition(e, menu)
		menu.popAction = popAction;
	}	
	e.cancelBubble = true;
	return( false);
}

function switchMenu(el) {
	if (el.className=="menuover") {
		el.className="menu";
	} else if (el.className=="menu") {
		el.className="menuover";
	}
}

function displayMenu() {
	var menu;
	if ( document.getElementById )
	{
		if ( (menu = document.getElementById("pulldown")) != null )
		{
			menu.style.display = "block";		
			setCloseMenu();	
		}	
	}
	return( false);
}

function cancelCloseMenu()
{
	window.clearTimeout(menuTimeout)
}

function setCloseMenu()
{
	menuTimeout = window.setTimeout("closeDiv('pulldown')", 1000)
}

function closeDiv( menuId)
{
	window.clearTimeout(popupTimeout);
	menuId = (menuId) ? menuId : "popmenu";
	if ( (menu = document.getElementById(menuId)) != null )
	{
		menu.style.display="none";
    }
    return( true);
}

function dispBox( popAction, colorName)
{
	if (document.getElementById )
	{
		if ( ! document.getElementById("ctl00_phContent_Chart1").complete )
			return;

		cancelPie();
		cancelBox();
		drillData = document.getElementById("tbDrill").value;
		document.getElementById("topboxInfo").src = "topBoxInfo.aspx?" + popAction + "&drill=" + drillData;
		document.getElementById("tbParams").value = popAction;
		document.getElementById("tbColor").value = colorName;
		
		setCloseBox();
		
		return( displayDiv( "popbox") )
	}
}

function cancelCloseBox()
{
	window.clearTimeout(boxTimeout)
}

function setCloseBox()
{
	boxTimeout = window.setTimeout("cancelBox()", 3000)
}

function cancelBox()
{
    cancelCloseBox();
	return( closeDiv("popbox"))
}


function dispPie( popAction)
{
	if (document.getElementById && document.getElementById("popbox").style.display == "none")
	{
		if ( ! document.getElementById("ctl00_phContent_Chart1").complete )
			return;

		drillData = document.getElementById("tbDrill").value;
		document.getElementById("popupPie").innerHTML = "<img width=\"220\" height=\"180\" src=\"smallPie.aspx?"+popAction+"&drill="+drillData+"\"></img>";
		document.onmousemove = adjustPiePosition;
		return( displayDiv( "popupPie", null, 750) )
	}
}

function adjustPiePosition(e)
{
	if (document.getElementById)
	{
		if ( !e )
			e = window.event;
		adjustPosition(e, document.getElementById("popupPie"))		
	}	
}

function cancelPie()
{
	document.onmousemove = null;
	return( closeDiv("popupPie"))
}

function drill( info, filter, color )
{
	if ( ! color) color = '';
	document.location.href = "analysis.aspx?drillBlob=" + info + "&filter=" + filter + "&tbc=" + color;
}
