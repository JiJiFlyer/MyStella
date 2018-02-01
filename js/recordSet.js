/* recordset object */
function _openrecordset()
{
	var xmlhttp;
	var sURL;
	var root;
	try
	{
		if(this.sqlSource==null || this.sqlSource=="" || this.xmlEngine==null || this.xmlEngine=="")
			throw "xmlEngine or sqlSource property is not allowed to be null";
		
		xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
		sURL = this.xmlEngine + "?callType=CLTRS&_t=r";
		sSql = this.sqlSource;
		sSql = sSql.replace(/</g, "[_L_]");
		sSql = sSql.replace(/>/g, "[_R_]");
		sSql = sSql.replace(/&/g, "[_AND_]");
		sSql = sSql.replace(/"/g, "[_DQ_]");
		sPostXML = "<root sqlsource=\"" + sSql + "\"></root>";
		xmlhttp.open("POST", sURL.toUpperCase(), false);
		xmlhttp.send(sPostXML);
		this.xmldoc = xmlhttp.responseXML;
		root = this.xmldoc.documentElement;
		if(root.hasChildNodes())
		{this.recordCount = root.childNodes.length;}
	}
	catch(e)
	{
		throw new Error(0,e.message);
	}
}
function _closerecordset()
{
	this.xmldoc=null;
	this.recordCount=0;
}

function _getitem(iIndex,sAttrName)
{
	try
	{
		var root;
		var node;
		root = this.xmldoc.documentElement;
		if(root.hasChildNodes())
		{
			node = root.childNodes(iIndex);
			return node.getAttribute(sAttrName.toLowerCase());
		}
		else
		{throw new Error(0,"the recordset is null");}
	}
	catch(e)
	{
		throw new Error(0,e.message);
	}
}

function recordset(sSql)
{
	this.sqlSource=sSql;
	this.xmlEngine=window.ROOT_PATH + "/xmlEngine/xmlpipe.aspx";
	this.xmldoc=null;
	this.recordCount=0;
	this.open=_openrecordset;
	this.item=_getitem;
	this.close=_closerecordset;
}
/*end def of recordset*/