//获得链接参数
String.prototype.getQueryString = function (name) {
    var reg = new RegExp("(^|&|\\?)" + name + "=([^&]*)(&|$)"), r;
    if (r = this.match(reg))
        return r[2];
        //return unescape(r[2]);
    return null;
};

//获得全局变量
function GetGlobalVar(key) {
    try{
    var _value=iPanel.getGlobalVar(key);
    return _value;
    }
    catch(e){return null;}
}

//保存全局变量,断电后消失
function SetGlobalVar(key, value) {
    try{
    iPanel.setGlobalVar(key, value);
    }
    catch(e){}
}

//读取永久存储
function ReadFlashVar(key) {
    try {
        var _value = iPanel.misc.getGlobal(key);
        return _value;
    }
    catch (e) { }
}

//写入永久存储
function WriteFlashVar(key,value) {
    try {
        iPanel.misc.setGlobal(key, value);
        iPanel.misc.save();
    }
    catch (e) {}
}

//读取机顶盒号
function ReadStbidNo() {
    try {
        var stbid = hardware.STB.serial;
        return stbid;
    }
    catch (e) { return null; }
}

//读取智能卡号
function ReadCardNo() {
    try {
        var cardno = CA.card.serialNumber
        return cardno;
    }
    catch (e) { return null; }
}

//ajax封装
function $AJAX(request) {
    var req;
    var url = request.url;
    if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
        req = new XMLHttpRequest();
    }
    else {// code for IE6, IE5
        req = new ActiveXObject("Microsoft.XMLHTTP");
    }
    req.onreadystatechange = function () {
        if (req.readyState == 4) {  //0: 请求未初始化1: 服务器连接已建立2: 请求已接收3: 请求处理中4: 请求已完成，且响应已就绪
            if (req.status == 200)
                request.success(req);
            else
                request.failed(req);
        }
    }
    var method = "GET";
    if (request.method != undefined)
        method = request.method;
    var async = true;
    if (request.async != undefined)
        async = request.async;
    req.open(method, url, async); //与服务端建立连接(请求方式post或get，地址,true表示异步)
    req.send(); //发送请求
}