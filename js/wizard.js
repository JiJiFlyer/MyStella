    window.onload = function()
    {
        Form1.sSysStep.value = 0;
        var sStep = parseInt(Form1.sSysStep.value);
        lbltitle.innerHTML =sTitle[sStep];
        lblinfo.innerHTML=sInfo[sStep];
        for (var i = 1; i <= sTitle.length ;i++)
        {
			var obj=tbStep.insertRow(i - 1);
			var newCell = null; 
			newCell = obj.insertCell();
			newCell.innerHTML = "<table bgcolor='white'><tr><td style=' padding:4px 1px 4px 1px'><table><tr><td style=' padding:4px 0 4px 1px'></td></tr></table></td></tr></table>";
			newCell = obj.insertCell();
			newCell.innerHTML = "第"+i+"步";
			tbStep.rows(i-1).cells(0).style.padding="1px 1px 1px 1px";
			tbStep.rows(i-1).cells(1).style.padding="5px 5px 5px 5px";
        }
        
        tbStep.rows(0).cells(1).style.color="#FFFFFF";
        tbStep.rows(0).cells(0).childNodes[0].rows(0).cells(0).childNodes[0].bgColor="#6693CF"
        var sbsure = true;
        if(self.onafteronload instanceof Function)
        {
            sbsure = onafteronload();
        }
        if (sbsure == false)
        {
            window.returnValue=sReturnValue;
            window.close();
//            if(typeof(window.dialogArguments.ondialogclosed_picklist_s_customercontactor)=='function')
//            {
//                
//            }
        }
        //后台调用专用方法
        if(self.onbeforeonload instanceof Function)
        {
            onbeforeonload();
        }
    }
    function onCancel()
    {
        var sbsure = true;
        if(self.onbeforeCancel instanceof Function)
        {
            sbsure = onbeforeCancel();
        }
        if (sbsure != false)
        {
            if(window.confirm("提示信息:系统将会取消你所选择的所有信息,是否取消?"))
            {
                window.returnValue=sReturnValue;
                sUnloadValue = "2";
                window.close();
            }
        }
    }
    var sReturnValue = "1";
    var sUnloadValue = "0";
    function onComplete()
    {
        var sbsure = true;
        if(self.onbeforeComplete instanceof Function)
        {
            sbsure = onbeforeComplete();
        }
        if (sbsure != false )
        {
            window.returnValue=sReturnValue;
            sUnloadValue = "1";
            window.close();
        }
    }
    //value = 0为正常操作，value = 1为没有onbeforeNext，value = 2为没有onafterNext，value = 3为没有onbeforeNext和onafterNext
    function onNext(value,sSteps)
    {
        var sStepLength = tbStep.rows.length;
        var sbsure = true;
        var sStep = parseInt(Form1.sSysStep.value);
        if (value == void 0)
        {
            value = 0;
        }
        else
        {
            value = value;
        }
        if (sSteps != void 0)
        {
            sStep = sSteps;
        }
        if(sStep < sStepLength -1)
        {
            if(value != 1 && value != 3)
            {
                if(self.onbeforeNext instanceof Function)
                {
                    sbsure = onbeforeNext(sStep);
                }
            }
            if (sbsure != false )
            {
                for (var i = 0 ;i <= sStep;i++)
                {
                    tbStep.rows(i).cells(1).style.color="";
                    tbStep.rows(i).cells(0).childNodes[0].rows(0).cells(0).childNodes[0].bgColor=""
                    tbinfo.rows(i).style.display="none";
                }
                tbStep.rows(sStep+1).cells(1).style.color="#FFFFFF";
                tbStep.rows(sStep+1).cells(0).childNodes[0].rows(0).cells(0).childNodes[0].bgColor="#6693CF"
                tbinfo.rows(sStep+1).style.display="";
                sStep = sStep + 1;
                Form1.sSysStep.value = sStep;
                lbltitle.innerHTML =sTitle[sStep];
                lblinfo.innerHTML=sInfo[sStep];
                if(sStep == sStepLength -1)
                {
                    Form1.btnBack.style.display="";
                    Form1.btnNext.style.display="none";
                    Form1.btnComplete.style.display="";
                } else {
                    Form1.btnBack.style.display="";
                    Form1.btnNext.style.display="";
                    Form1.btnComplete .style.display="none";
                }
                if(value != 2 && value != 3)
                {
                    if(self.onafterNext instanceof Function)
                    {
                        onafterNext(sStep);
                    }
                }
            }
        }
    }
    //value = 0为正常操作，value = 1为没有onbeforeNext，value = 2为没有onafterNext，value = 3为没有onbeforeNext和onafterNext
    function onBack(value,sSteps)
    {
        var sStepLength = tbStep.rows.length;
        var sbsure = true;
        var sStep = parseInt(Form1.sSysStep.value);
        if (value == void 0)
        {
            value = 0;
        }
        else
        {
            value = value;
        }
        if (sSteps != void 0)
        {
            sStep = sSteps;
        }
        if (sStep > 0)
        {
            if(value != 1 && value != 3)
            {
                if(self.onbeforeBack instanceof Function)
                {
                    sbsure = onbeforeBack(sStep);
                }
            }
            if (sbsure != false )
            {
                for (var i = sStepLength - 1 ;i >= sStep;i--)
                {
                    tbStep.rows(i).cells(1).style.color="";
                    tbStep.rows(i).cells(0).childNodes[0].rows(0).cells(0).childNodes[0].bgColor=""
                    tbinfo.rows(i).style.display="none";
                }
                tbStep.rows(sStep-1).cells(1).style.color="#FFFFFF";
                tbStep.rows(sStep-1).cells(0).childNodes[0].rows(0).cells(0).childNodes[0].bgColor="#6693CF"
                tbinfo.rows(sStep-1).style.display="";
                sStep = sStep - 1;
                Form1.sSysStep.value = sStep;
                lbltitle.innerHTML =sTitle[sStep];
                lblinfo.innerHTML=sInfo[sStep];
                if(sStep == 0)
                {
                    Form1.btnBack.style.display="none";
                    Form1.btnNext.style.display="";
                    Form1.btnComplete.style.display="none";
                } else {
                    Form1.btnBack.style.display="";
                    Form1.btnNext.style.display="";
                    Form1.btnComplete.style.display="none";
                }
                if(value != 2 && value != 3)
                {
                    if(self.onafterBack instanceof Function)
                    {
                        onafterBack(sStep);
                    }
                }
            }
        }
    }
    function ontextblur()
    {
        var bReturnValue = true;
	    var _obj = null;
	    _obj = event.srcElement;
	    if(checkFieldIsRequired(_obj)==false) {event.srcElement.focus();} else {_obj.parentElement.runtimeStyle.border = "";}
	    if(checkFieldDataType(_obj)==false) {event.srcElement.focus();}
	    if(checkFieldDataLength(_obj)==false) {event.srcElement.focus();}
        //
    }
    window.onunload = function()
    {
        var sbsure = true;
        if(self.onbeforeClose instanceof Function)
        {
            sbsure = onbeforeClose(sUnloadValue);
        }
        if(sbsure == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }