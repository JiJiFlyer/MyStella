var Drag={
dragged:false,
  ao:null,
  tdiv:null,
  obj:null,
  stage:0,
dragStart:function(){
 if (Drag.stage!=0) return;
 Drag.stage=1;
 Drag.ao=event.srcElement;
 if((Drag.ao.tagName=="TD")||(Drag.ao.tagName=="TR")||(Drag.ao.tagName=="SPAN")){
  Drag.ao=Drag.ao.offsetParent.offsetParent;
  Drag.ao.style.zIndex=100;
 }else
  return;
 Drag.dragged=true;
 Drag.tdiv=document.createElement("div");
 Drag.tdiv.innerHTML = "<table height='" + Drag.ao.height + "' style='" + Drag.ao.runtimeStyle.cssText + "'>" + Drag.ao.rows(0).outerHTML + "</table>";
 Drag.ao.runtimeStyle.border="1px dashed red";
 Drag.tdiv.style.display="block";
 Drag.tdiv.style.position="absolute";
 Drag.tdiv.style.filter="alpha(opacity=70)";
 Drag.tdiv.style.cursor="move";
 Drag.tdiv.style.border="1px solid #000000";
 Drag.tdiv.style.width=Drag.ao.offsetWidth;
 Drag.tdiv.style.height=Drag.ao.offsetHeight;
 Drag.tdiv.style.top=Drag.getInfo(Drag.ao).top;
 Drag.tdiv.style.left=Drag.getInfo(Drag.ao).left;
 document.body.appendChild(Drag.tdiv);
 Drag.lastX=event.clientX;
 Drag.lastY=event.clientY;
 Drag.lastLeft=Drag.tdiv.style.left;
 Drag.lastTop=Drag.tdiv.style.top;
},
 draging:function(){//重要:判断MOUSE的位置
 if (!Drag.dragged || Drag.ao==null) return;
 if (event.button!=1) Drag.dragEnd();
 Drag.stage=2;
 var tX=event.clientX;
 var tY=event.clientY;
 Drag.tdiv.style.left=parseInt(Drag.lastLeft)+tX-Drag.lastX;
 Drag.tdiv.style.top=parseInt(Drag.lastTop)+tY-Drag.lastY;
 for(var i=0;i<Drag.obj.cells.length;i++){
  var parentCell=Drag.getInfo(Drag.obj.cells[i]);
  if(tX>=parentCell.left && tX<=parentCell.right && tY>=parentCell.top && tY<=parentCell.bottom){
   //var subTables=Drag.obj.cells[i].getElementsByTagName("table");
   var subTables = Drag.obj.cells[i].children;
   if(subTables.length==0){
    if(tX>=parentCell.left && tX<=parentCell.right && tY>=parentCell.top && tY<=parentCell.bottom){
     Drag.obj.cells[i].appendChild(Drag.ao);
    }
    break;
   }
   for(var j=0;j<subTables.length;j++){
    var subTable=Drag.getInfo(subTables[j]);
    if(tX>=subTable.left && tX<=subTable.right && tY>=subTable.top && tY<=subTable.bottom){
     Drag.obj.cells[i].insertBefore(Drag.ao,subTables[j]);
     break;
    }else{
     Drag.obj.cells[i].appendChild(Drag.ao);
    } 
   }
  }
 }
}
,
 dragEnd:function(){
 if(!Drag.dragged) return;
 Drag.dragged=false;
 Drag.mm=Drag.repos(150,15);
 Drag.ao.runtimeStyle.border = Drag.ao.style.border
 Drag.tdiv.style.borderWidth="0px";
 Drag.ao.style.zIndex=1;
},
getInfo:function(o){//取得坐标
 var to=new Object();
 to.left=to.right=to.top=to.bottom=0;
 var twidth=o.offsetWidth;
 var theight=o.offsetHeight;
 while(o!=null && o!=document.body){
  to.left+=o.offsetLeft;
  to.top+=o.offsetTop;
  o=o.offsetParent;
 }
  to.right=to.left+twidth;
  to.bottom=to.top+theight;
 return to;
},
repos:function(aa,ab){
 var f=Drag.tdiv.filters.alpha.opacity;
 var tl=parseInt(Drag.getInfo(Drag.tdiv).left);
 var tt=parseInt(Drag.getInfo(Drag.tdiv).top);
 var kl=(tl-Drag.getInfo(Drag.ao).left)/ab;
 var kt=(tt-Drag.getInfo(Drag.ao).top)/ab;
 var kf=f/ab;
 return setInterval(function(){if(ab<1){
       clearInterval(Drag.mm);
       Drag.tdiv.removeNode(true);
       Drag.ao=null;
       Drag.stage=0;
       return;
      }
     ab--;
     tl-=kl;
     tt-=kt;
     f-=kf;
     Drag.tdiv.style.left=parseInt(tl)+"px";
     Drag.tdiv.style.top=parseInt(tt)+"px";
     Drag.tdiv.filters.alpha.opacity=f;
    }
,aa/ab)
},
 inint:function(dragObj){//初始化
 Drag.obj = dragObj;
 for(var i=0;i<Drag.obj.cells.length;i++){
  var subTables = Drag.obj.cells[i].children;
  for(var j=0;j<subTables.length;j++){
   if(subTables[j].className.toLowerCase()!="webpart") break;
   subTables[j].rows[0].attachEvent("onmousedown",Drag.dragStart);
  }
 }
 document.onmousemove=Drag.draging;
 document.onmouseup=Drag.dragEnd;
}
//end of Object Drag
}
function __refreshWebPart()
{
	var oCtrlImg = event.srcElement;
	var oWebPart = oCtrlImg.parentElement.parentElement.parentElement.parentElement;
	
	var sEditMode = __webPartCustTool.getAttribute("editMode");
	if (sEditMode==null || sEditMode=="false") {sEditMode=false;} else {sEditMode=true;}
	var oRefresh;
	if (sEditMode==true)
		oRefresh = oWebPart.rows(2).cells(0);
	else
		oRefresh = oWebPart.rows(1).cells(0);
	
	var sType = oWebPart.getAttribute("type");
	switch (sType)
	{
		case "v":
			//oRefresh.bind();
			break;
		case "l":
			//oRefresh.src = oRefresh.src;
			break;
	}
}
function __expandWebPart()
{
	var oCtrlImg = event.srcElement;
	var oWebPart = oCtrlImg.parentElement.parentElement.parentElement.parentElement;
	var sCollapse = oWebPart.getAttribute("collapse");
	if (sCollapse==null || sCollapse=="false") {sCollapse=false;} else {sCollapse=true;}
	if (sCollapse==true)
	{
		oWebPart.rows(oWebPart.rows.length-1).style.display = "block";
		oWebPart.setAttribute("collapse","false");
		oCtrlImg.src = "cssClass/images/webpart/expand.gif";
	}
	else
	{
		oWebPart.rows(oWebPart.rows.length-1).style.display = "none";
		oWebPart.setAttribute("collapse","true");
		oCtrlImg.src = "cssClass/images/webpart/collapse.gif";
	}
}
function __editWebPart()
{
	var oCtrlImg = event.srcElement;
	var oWebPart = oCtrlImg.parentElement.parentElement.parentElement.parentElement;
	var sEditMode = __webPartCustTool.getAttribute("editMode");
	if (sEditMode==null || sEditMode=="false") {sEditMode=false;} else {sEditMode=true;}
	if (sEditMode==false)
	{
		var oRow = oWebPart.insertRow(1);
		var oCell = oRow.insertCell();
		oCell.colSpan = 3;
		oCell.appendChild(__webPartCustTool);
		__webPartCustTool.style.display = "block";
		__webPartCustTool.setAttribute("editMode","true");
	}
}
function __applyWebPartStyle(sStyle)
{
	var oSrcElement = event.srcElement;
	var oWebPart = __webPartCustTool.parentElement.parentElement.parentElement.parentElement;
	var sEditedStyle = oWebPart.getAttribute("editedStyle");
	sEditedStyle=(sEditedStyle==null?"":sEditedStyle);
	switch (sStyle)
	{
		case "bordercolor":
			if (oSrcElement.tagName!="DIV") return;
			oWebPart.style.borderColor = oSrcElement.style.backgroundColor;
			if (sEditedStyle.indexOf("bordercolor")<0)
			{
				if (sEditedStyle=="")
				{
					sEditedStyle = "bordercolor";
				}
				else
				{
					sEditedStyle += ("," + "bordercolor");
				}
			}
			break;
		case "captionbgcolor":
			if (oSrcElement.tagName!="DIV") return;
			oWebPart.rows(0).cells(0).filters(0).StartColorStr = oSrcElement.style.backgroundColor;
			oWebPart.rows(0).cells(1).filters(0).StartColorStr = oSrcElement.style.backgroundColor;
			oWebPart.rows(0).cells(2).filters(0).StartColorStr = oSrcElement.style.backgroundColor;
			if (sEditedStyle.indexOf("captionbgcolor")<0)
			{
				if (sEditedStyle=="")
				{
					sEditedStyle = "captionbgcolor";
				}
				else
				{
					sEditedStyle += ("," + "captionbgcolor");
				}
			}
			break;
		case "captionfontcolor":
			if (oSrcElement.tagName!="DIV") return;
			oWebPart.rows(0).cells(1).children(0).style.color = oSrcElement.style.backgroundColor;
			if (sEditedStyle.indexOf("captionfontcolor")<0)
			{
				if (sEditedStyle=="")
				{
					sEditedStyle = "captionfontcolor";
				}
				else
				{
					sEditedStyle += ("," + "captionfontcolor");
				}
			}
			break;
		case "captionfontweight":
			if (oSrcElement.tagName!="DIV") return;
			if (oWebPart.rows(0).cells(1).children(0).style.fontWeight=="bold")
				oWebPart.rows(0).cells(1).children(0).style.fontWeight = "normal";
			else
				oWebPart.rows(0).cells(1).children(0).style.fontWeight = "bold";
			
			if (sEditedStyle.indexOf("captionfontweight")<0)
			{
				if (sEditedStyle=="")
				{
					sEditedStyle = "captionfontweight";
				}
				else
				{
					sEditedStyle += ("," + "captionfontweight");
				}
			}
			break;
		case "captionfontstyle":
			if (oSrcElement.tagName!="DIV") return;
			if (oWebPart.rows(0).cells(1).children(0).style.fontStyle=="italic")
				oWebPart.rows(0).cells(1).children(0).style.fontStyle = "normal";
			else
				oWebPart.rows(0).cells(1).children(0).style.fontStyle = "italic";
			
			if (sEditedStyle.indexOf("captionfontstyle")<0)
			{
				if (sEditedStyle=="")
				{
					sEditedStyle = "captionfontstyle";
				}
				else
				{
					sEditedStyle += ("," + "captionfontstyle");
				}
			}
			break;
		case "captiondecoration":
			if (oSrcElement.tagName!="DIV") return;
			if (oWebPart.rows(0).cells(1).children(0).style.textDecoration=="underline")
				oWebPart.rows(0).cells(1).children(0).style.textDecoration = "none";
			else
				oWebPart.rows(0).cells(1).children(0).style.textDecoration = "underline";
			
			if (sEditedStyle.indexOf("captiondecoration")<0)
			{
				if (sEditedStyle=="")
				{
					sEditedStyle = "captiondecoration";
				}
				else
				{
					sEditedStyle += ("," + "captiondecoration");
				}
			}
			break;
	}
	oWebPart.setAttribute("editedStyle",sEditedStyle);
}
function __saveWebPartEdit()
{
	try
	{
		var oWebPart = __webPartCustTool.parentElement.parentElement.parentElement.parentElement;
		var sWebPartId = oWebPart.getAttribute("webpartid");
		var sEditedStyle = oWebPart.getAttribute("editedStyle");
		sEditedStyle=(sEditedStyle==null?"":sEditedStyle);
		if (sEditedStyle!="")
		{
			var oEditedStyle = sEditedStyle.split(",");
			var oCaptionStyle = oWebPart.rows(0).cells(1).children(0).style;
			for (var i=0;i<oEditedStyle.length;i++)
			{
				switch (oEditedStyle[i])
				{
					case "bordercolor":
						oEditedStyle[i] = "bordercolor=" + oWebPart.style.borderColor;
						break;
					case "captionbgcolor":
						oEditedStyle[i] = "captionbgcolor=" + oWebPart.rows(0).cells(0).filters(0).StartColorStr;
						break;
					case "captionfontcolor":
						oEditedStyle[i] = "captionfontcolor=" + oCaptionStyle.color;
						break;
					case "captionfontweight":
						oEditedStyle[i] = "captionfontweight=" + oCaptionStyle.fontWeight;
						break;
					case "captionfontstyle":
						oEditedStyle[i] = "captionfontstyle=" + oCaptionStyle.fontStyle;
						break;
					case "captiondecoration":
						oEditedStyle[i] = "captiondecoration=" + oCaptionStyle.textDecoration;
						break;
				}
			}
			var oRPC = new server(__RpcURL,"SaveWebPartUI");
			oRPC.call(Macro_VAR.LoginUserId,sWebPartId,oEditedStyle.join(","));
			
			if(XMLRecord("returnCode")=="0")
			{throw new Error(0,XMLRecord("SaveWebPartUI"));}
		}
	}
	catch(e)
	{
		window.alert(e.message);
	}
}
function __cancelWebPartEdit()
{
	var oWebPart = __webPartCustTool.parentElement.parentElement.parentElement.parentElement;
	__webPartCustTool.style.display = "none";
	document.body.appendChild(__webPartCustTool);
	oWebPart.deleteRow(1);
	__webPartCustTool.setAttribute("editMode","false");
}