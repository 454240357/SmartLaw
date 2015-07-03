function confirmCallBackFn(arg) {
    radalert("<strong>radconfirm</strong> returned the following result: <h3 style='color: #ff0000;'>" + arg + "</h3>", null, null, "Result");
}
function promptCallBackFn(arg) {
    radalert("After 7.5 million years, <strong>Deep Thought</strong> answers:<h3 style='color: #ff0000;'>" + arg + "</h3>", null, null, "Deep Thought");
}
function OpenAlert(content) {
    radalert(content, 330, 100, '九峰山智慧社区管理平台');
    return false;
}
function OpenConfirm() {
    radconfirm('<h3 style=\'color: #333399;\'>确认删除吗?</h3>', confirmCallBackFn, 330, 100, null, '九峰山智慧社区管理平台');
    return false;
}
function OpenPrompt() {
    radprompt('<span style=\'color: #333399;\'>What is the answer of Life, Universe and Everything?</span>', promptCallBackFn, 330, 160, null, 'The Question', '42');
    return false;
}
function OpenWindow() {
    var wnd = window.radopen("", null);
    wnd.setSize(400, 400);
    return false;
}
function controlWindowButtons() {
    var oManager = GetRadWindowManager();
    var selitem = null;
    var e = document.forms[0].elements["RadioButtonList1"];
    for (var i = 0; i < e.length; i++) {
        if (e[i].checked) {
            selitem = e[i].value;
        }
    }
    eval("oManager." + selitem);
}

function openRadWindow(Address, RadWindowId) {
    var oWnd = radopen(Address, RadWindowId);
    oWnd.center();
}

function openRadWindowSize(Address, w, h) {
    var oWnd = radopen(Address, null);
    oWnd.setSize(w, h);
    oWnd.set_modal(true);
    oWnd.center();
}
//the following code use radconfirm to mimic the blocking of the execution thread.
//The approach has the following limitations:
//1. It works inly for elements that have *click* method, e.g. links and buttons
//2. It cannot be used in if(!confirm) checks
window.blockConfirm = function(text, mozEvent, oWidth, oHeight, callerObj) {
    var ev = mozEvent ? mozEvent : window.event; //Moz support requires passing the event argument manually 
    //Cancel the event 
    ev.cancelBubble = true;
    ev.returnValue = false;
    if (ev.stopPropagation) ev.stopPropagation();
    if (ev.preventDefault) ev.preventDefault();

    //Determine who is the caller 
    var callerObj = ev.srcElement ? ev.srcElement : ev.target;

    //Call the original radconfirm and pass it all necessary parameters 
    if (callerObj) {
        //Show the confirm, then when it is closing, if returned value was true, automatically call the caller's click method again. 
        var callBackFn = function(arg) {
            if (arg) {
                callerObj["onclick"] = "";
                //if (callerObj.click) callerObj.click(); //Works fine every time in IE, but does not work for links in Moz
                //原先在ajax模式下无法触发按钮
                if (callerObj.tagName == "INPUT") {
                    __doPostBack(callerObj.name, '')
                }
                else if (callerObj.tagName == "A") //We assume it is a link button! 
                {
                    try {
                        eval(callerObj.href)
                    }
                    catch (e) { }
                }
            }
        }

        radconfirm(text, callBackFn, oWidth, oHeight, callerObj, "九峰山智慧社区管理平台");
    }
    return false;
}

function changeWindowSize() {
    var oWnd = GetRadWindow();
    if (!oWnd.isMaximized())
        oWnd.autoSize(true);
}

function maximizeWindow() {
    var oWnd = GetRadWindow();
    if (!oWnd.isMaximized())
        oWnd.maximize();
}

function restoreWindow() {
    var oWnd = GetRadWindow();
        oWnd.restore();
}

function GetRadWindow() {
    var oWindow = null;
    if (window.radWindow)
        oWindow = window.radWindow;
    else if (window.frameElement.radWindow)
        oWindow = window.frameElement.radWindow;
    return oWindow;
}

//function RestoreWinSize(sender, eventArgs) {
//    if (eventArgs._commandName == "Restore") {
//        sender.autoSize(true);
//    }
//}