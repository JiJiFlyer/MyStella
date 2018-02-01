<!----------------------------------------------------------------------------
//  Copyright (c) 2005-2008 TopSpeed Info & Tech.Co.,Ltd.  All Rights Reserved.
//--------------------------------------------------------------------------->

/* colorSquare object */
function colorSquare(height,width,color)
{
	this.outerHTML = "";
	this.outerHTML += "<TABLE width='" + width + "' height='" + height + "' cellSpacing='0' cellPadding='0' bgColor='" + color + "' border='0'>";
	this.outerHTML += "<TR>";
	this.outerHTML += "<TD></TD>";
	this.outerHTML += "</TR>";
	this.outerHTML += "</TABLE>";
}
colorSquare.prototype={
	outerHTML:""
}

/* schedule object*/
function schedule(){}
schedule.prototype={
	outerHTML:"",
	nodes:null,
	startNodeColor:"black",
	width:"",
	nodeWidth:2,
	nodeHeight:5,
	lineHeight:2,
	backgroundColor:"",
	borderColor:"",
	addNode:function()
	{
		with (this)
		{
			if (nodes==null) return;
			var _oNode;
			if (arguments[1]==false)
				_oNode = new scheduleNode(nodeHeight,nodeWidth,arguments[0],arguments[1],null,null,null);
			else
				_oNode = new scheduleNode(nodeHeight,nodeWidth,arguments[0],arguments[1],lineHeight,arguments[2],arguments[3]);
			nodes.addNode(_oNode);
		}
	},
	create:function()
	{
		with (this)
		{
			if (nodes!=null && nodes.nodeCount>0) return;
			nodes = new scheduleNodes();
			var startNode;
			startNode = new scheduleNode(nodeHeight,nodeWidth,startNodeColor,false,null,null,null);
			nodes.addNode(startNode);
		}
	},
	render:function()
	{
		var i;
		var oNode;
		var oNodes = this.nodes._oNodes;
		var iNodeCount = this.nodes.nodeCount;
		
		this.outerHTML = "";
		this.outerHTML += "<TABLE width='" + this.width + "' height='" + this.nodeHeight + "' cellSpacing='0' cellPadding='0' border='0' bgColor='" + this.backgroundColor + "'  style='TABLE-lAYOUT:FIXED;BORDER-RIGHT:" + this.borderColor + " 1px solid; BORDER-TOP:" + this.borderColor + " 1px solid;BORDER-LEFT:" + this.borderColor + " 1px solid; BORDER-BOTTOM:" + this.borderColor + " 1px solid'>";
		this.outerHTML += "<TR>";
		
		for (i=0;i<iNodeCount;i++)
		{
			oNode = oNodes[i];
			if (oNode.hasLine==true)
			{
				//line
				this.outerHTML += "<TD valign='center' width='" + oNode.lineWidth + "'>";
				this.outerHTML += "<TABLE width='" + oNode.lineWidth + "' height='" + oNode.lineHeight + "' cellSpacing='0' cellPadding='0' bgColor='" + oNode.lineColor + "' border='0'><TR><TD></TD></TR></TABLE>";
				this.outerHTML += "</TD>";
				//node
				this.outerHTML += "<TD valign='center' width='" + oNode.nodeWidth + "'>";
				this.outerHTML += "<TABLE width='" + oNode.nodeWidth + "' height='" + oNode.nodeHeight + "' cellSpacing='0' cellPadding='0' bgColor='" + oNode.nodeColor + "' border='0'><TR><TD></TD></TR></TABLE>";
				this.outerHTML += "</TD>";
			}
			else
			{
				//node
				this.outerHTML += "<TD valign='center' width='" + oNode.nodeWidth + "'>";
				this.outerHTML += "<TABLE width='" + oNode.nodeWidth + "' height='" + oNode.nodeHeight + "' cellSpacing='0' cellPadding='0' bgColor='" + oNode.nodeColor + "' border='0'><TR><TD></TD></TR></TABLE>";
				this.outerHTML += "</TD>";
			}
		}
		
		this.outerHTML += "<td></td></TR>";
		this.outerHTML += "</TABLE>";
	}
}

function scheduleNodes(){}
scheduleNodes.prototype={
	_oNodes:null,
	nodeCount:0,
	addNode:function()
	{
		if (arguments.length<=0) return;
		with (this)
		{
			if (_oNodes==null) _oNodes = new Array();
			_oNodes[_oNodes.length] = arguments[0];
			nodeCount = _oNodes.length;
		}
	},
	removeNode:function()
	{
		with (this)
		{
			if (_oNodes==null || _oNodes.length==0) return;
			if (arguments.length==0)
			{_oNodes.pop();}
			else if (arguments.length>0)
			{_oNodes.splice(arguments[0],1);}
			nodeCount = _oNodes.length;
		}
	}
}

function scheduleNode(nodeHeight,nodeWidth,nodeColor,hasLine,lineHeight,lineWidth,lineColor)
{
	this.nodeHeight = nodeHeight;
	this.nodeWidth = nodeWidth;
	this.nodeColor = nodeColor;
	
	this.hasLine = hasLine;
	this.lineHeight = lineHeight;
	this.lineWidth = lineWidth;
	this.lineColor = lineColor;
}
scheduleNode.prototype={
	nodeHeight:"",
	nodeWidth:"",
	nodeColor:"",
	hasLine:true,
	lineHeight:"",
	lineWidth:"",
	lineColor:""
}

/* messageBox object*/
function messageBox()
{
	switch (arguments.length)
	{
		case 0:
			return;
		case 1:
			this.title = arguments[0];
		case 2:
			this.title = arguments[0];
			this.buttons = arguments[1];
			break;
		case 3:
			this.title = arguments[0];
			this.buttons = arguments[1];
			this.icon = arguments[2];
			break;
		case 4:
			this.title = arguments[0];
			this.buttons = arguments[1];
			this.icon = arguments[2];
			this.message = arguments[3];
			break;
		case 5:
			this.title = arguments[0];
			this.buttons = arguments[1];
			this.icon = arguments[2];
			this.message = arguments[3];
			this.height = arguments[4];
			break;
		case 6:
			this.title = arguments[0];
			this.buttons = arguments[1];
			this.icon = arguments[2];
			this.message = arguments[3];
			this.height = arguments[4];
			this.width = arguments[5];
			break;
	}
}

messageBox.prototype={
	title:"",
	buttons:null,
	icon:null,
	message:"",
	height:0,
	width:0,
	show:function()
	{
		if (this.buttons==null || this.buttons==null) return;
		var sURL = window.ROOT_PATH + "popup_process/customDialog.aspx?ran=" + Math.random().toString();
		var returnString;
		var oDialogParam = new Array();
		oDialogParam[0] = this;
		oDialogParam[1] = window.self;
		returnString = window.showModalDialog(sURL,oDialogParam,'dialogHeight:'+this.height+'px;dialogWidth:'+this.width+'px;edge:Raised;center:Yes;help:No;resizable:No;status:No;scroll:No');
		return returnString;
	}
}
function messageBoxButton()
{
	this.text = arguments[0];
	this.accessKey = arguments[1];
	this.code = arguments[2];
}
messageBoxButton.prototype={
	text:"",
	accessKey:""
}
function messageBoxIcon()
{
	this.imageURL = arguments[0];
	this.align = arguments[1];
}
messageBoxIcon.prototype={
	imageURL:"",
	align:"left"
}
/* end of messageBox */

/* tooltip box*/
var __tipPopup = null;
function tooltipBox(){}
tooltipBox.prototype={
	innerHTML:"",
	show:function()
	{
		if (__tipPopup==null) {__tipPopup = window.createPopup();}
		var oPopupBody = __tipPopup.document.body;
		oPopupBody.innerHTML = this.innerHTML;
		__tipPopup.show(0,0,0,0);
		var realWidth = oPopupBody.scrollWidth-1;
		var realHeight = oPopupBody.scrollHeight-1;
		__tipPopup.hide();
		__tipPopup.show(event.clientX,event.clientY,realWidth,realHeight,document.body);
	},
	hide:function()
	{
		if (__tipPopup==null) return;
		__tipPopup.hide();
	}
}
/* end of tooltip */
/* progress bar */
/* end of progress bar */