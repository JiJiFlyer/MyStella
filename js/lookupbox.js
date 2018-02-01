var Class = function()
{
    var klass = function () {
        this.init.apply(this, arguments);
    };
    klass.prototype.init = function () { };
    klass.fn = klass.prototype;
    klass.fn.parent = klass;
    klass.extend = function (obj) {
        var extended = obj.extended;
        for (var i in obj) {
            klass[i] = obj[i];
        }
        if (extended) extended(klass);
    };
    klass.include = function (obj) {
        var included = obj.included;
        for (var i in obj) {
            klass.fn[i] = obj[i];
        }
        if (included) included(klass);
    };
    return klass;
};

//控件 LookupBox
var LookupBox=new Class;
LookupBox.include({
    id: "",
    clientName: "",
    valueField: "",
    textField: "",
    init: function () { this.id = arguments[0] },
    value: function () {
        var _value = arguments[0];
        var _obj = document.getElementById(this.id + "Value");
        if (_value != undefined) {
            _obj.value = _value;
        }
        else {
            return _obj.value;
        }
    },
    text: function () {
        var _value = arguments[0];
        var _obj = document.getElementById(this.id + "Text");
        if (_value != undefined) {
            _obj.value = _value;
        }
        else {
            return _obj.value;
        }
    }
});