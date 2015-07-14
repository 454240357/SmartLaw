
//详情页面处理JS
var btnArray = [
	{ moveFocus: "url('images/icon.png')", focus: "url('images/icon_1.png')" }, //上一页
	{moveFocus: "url('images/icon.png')", focus: "url('images/icon_1.png')" }, //下一页
	{moveFocus: "url('images/icon.png')", focus: "url('images/icon_1.png')" }, //返回
	{moveFocus: "url('images/icon.png')", focus: "url('images/icon_1.png')"} //首页;
	]
var btnPos = 0;
var PREV_URL = ""; //上一页
var NEXT_URL = ""; //下一页
var BACK_URL = ""; //返回
var INDEX_URL = "/Redirect.aspx"; //首页
//var url = window.location.href;
var thisText = "";
var thisTitle = "";
var buttonArray = ["上一页", "下一页", "返回", "首页"]
var wordSize = "26"; //标题超过25个字 显示滚动

var topvalue = 0;
var totalHeight = 0;
var pageHeight = 470;
var moveHeight = 446;


function pageUp() {
    if (topvalue < 0) {
        topvalue += moveHeight;
        document.getElementById("inner").style.top = topvalue + "px";
        $("pageindex").innerHTML = (-topvalue / moveHeight + 1) + "/" + ((totalHeight - pageHeight) <= 0 ? 1 : Math.ceil((totalHeight - pageHeight) / moveHeight) + 1);
    }
    btnFocus(-btnPos);
}

function pageDown() {
    if (-topvalue + pageHeight < totalHeight) {
        topvalue -= moveHeight;
        document.getElementById("inner").style.top = topvalue + "px";
        $("pageindex").innerHTML = (-topvalue / moveHeight + 1) + "/" + ((totalHeight - pageHeight) <= 0 ? 1 : Math.ceil((totalHeight - pageHeight) / moveHeight) + 1);
    }
    btnFocus(-btnPos + 1);
}

function goback() {
    BACK_URL = window.location.href.getQueryString("returnUrl");
    if (BACK_URL == null || BACK_URL == undefined) {
        history.go(-1);
    } 
    else {
        location.href = decodeURIComponent(BACK_URL);
    }
}

function doSelect() {

    if (btnPos == 0) {
        if (topvalue < 0) {
            topvalue += moveHeight;
            document.getElementById("inner").style.top = topvalue + "px";
            $("pageindex").innerHTML = (-topvalue / moveHeight + 1) + "/" + ((totalHeight - pageHeight) <= 0 ? 1 : Math.ceil((totalHeight - pageHeight) / moveHeight) + 1);
        }

    }

    else if (btnPos == 1) {

        if (-topvalue + pageHeight < totalHeight) {
            topvalue -= moveHeight;
            document.getElementById("inner").style.top = topvalue + "px";
            $("pageindex").innerHTML = (-topvalue / moveHeight + 1) + "/" + ((totalHeight - pageHeight) <= 0 ? 1 : Math.ceil((totalHeight - pageHeight) / moveHeight) + 1);
        }
    }


    else if (btnPos == 2) {


    goback();


    }
    else if (btnPos == 3) {
        location.href = INDEX_URL;

    }

}

//键盘响应
document.onkeydown = grabEvent;
//document.onkeypress = grabEvent;
document.onsystemevent = grabEvent;
document.sonirkeypres = grabEvent;

function grabEvent() {
    var key_code = event.keyCode;

    switch (key_code) {
        case 1: //up
        case 38:
        case 28:
        case 269:
            btnFocus(1);
            return 0;
            break;
        case 2: //down
        case 40:
        case 31:
        case 270:
            btnFocus(-1);
            return 0;
            break;
        case 3: //left
        case 37:
        case 29:
        case 271:
            btnFocus(-1);
            return 0;
            break;
        case 4: //right
        case 39:
        case 30:
        case 272:
            btnFocus(1);
            return 0;
            break;
        case 13: //enter
            doSelect();
            return 0;
            break;
        case 340: //back
            goback();
            return 0;
            break;
        case 33:
        case 372: //上一页
            pageUp();
            return 0;
            break;

        case 34:
        case 373:
            pageDown();
            return 0;
            break;
        case 283:
            //location.href = BACK_URL + "?" + indexPos;
            return 0;
            break;
        case 832: //red
            location.href = "";
            return 0;
            break;
        case 833: //green
            location.href = "";
            return 0;
            break;
        case 835: //blue
            location.href = "";
            return 0;
            break;
        case 834: //yellow
            location.href = "";
            return 0;
            break;
    }
}
function $(id) {
    return document.getElementById(id);
}
function btnFocus(__num) {
    $("btn" + btnPos).style.background = btnArray[btnPos].moveFocus;
    btnPos += __num;
    if (btnPos < 0) {
        btnPos = 0;
    }
    else if (btnPos > btnArray.length - 1) {
        btnPos = btnArray.length - 1;
    }
    $("btn" + btnPos).style.background = btnArray[btnPos].focus;
}


function init() {
    // thisText = mainText[0].thisText;
    // thisTitle = mainText[0].thisTitle;
    // $('inner').innerHTML = thisText;
    // $('showTitle').innerHTML = thisTitle;



    if ($('showTitle').innerHTML.length > wordSize) {

        $('showTitle').innerHTML = '<marquee direction="left" width="100%" scrollamonut="15" scrolldelay="100"> ' + $('showTitle').innerHTML + '</marquee>';
    }

    for (i = 0; i < 4; i++) {
        $("btn" + i).innerHTML = buttonArray[i];
    }


    //show(1);
    btnFocus(1);
    totalHeight = $("inner").clientHeight;
    $("pageindex").innerHTML = (-topvalue / moveHeight + 1) + "/" + ((totalHeight - pageHeight) <= 0 ? 1 : Math.ceil((totalHeight - pageHeight) / moveHeight) + 1);

}



