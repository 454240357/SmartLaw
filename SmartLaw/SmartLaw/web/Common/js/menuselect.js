var area = 0;
var menuPos = 0; //记录菜单位置
var listPos = 0;
var btnPos = 0; //记录最下方按键位置：0,1
var listSize = 7;
var menuSize = 6;
var wordSize = 28;
var menuWordSize = 5;
var HOMEURL = "/Redirect.aspx";
var url = window.location.href;
var btnArray = ["上一页", "下一页", "返回", "首页"];
function $(id) {
    return document.getElementById(id);
}


//图层部分移动
function showMenu() {
    if (menuPos < menuSize && menuSize <= menuArray.length) {
        for (var i = 0; i < menuSize; i++) {
            $("menutext" + i).innerHTML = menuArray[i].menuTitle.slice(0, menuWordSize);
            $("menutext" + i).className = "menu_list";
        }
    }

    else if (menuPos >= menuSize && menuSize < menuArray.length) {
        for (var i = 0; i < menuSize; i++) {
            $("menutext" + i).innerHTML = "";
        }
        if ((menuArray.length - menuPos) <= menuSize) {
            if (parseInt(menuPos / 6) !== parseInt(menuArray.length / 6)) {
                for (var i = 0; i < menuSize; i++) {
                    $("menutext" + i).innerHTML = menuArray[(parseInt(menuPos / 6)) * 6 + i].menuTitle.slice(0, menuWordSize);
                    $("menutext" + i).className = "menu_list";
                }
            }
            else {
                for (var i = 0; i < (menuArray.length % menuSize); i++) {
                    $("menutext" + i).innerHTML = menuArray[(parseInt(menuPos / 6)) * 6 + i].menuTitle.slice(0, menuWordSize);
                    $("menutext" + i).className = "menu_list";
                }

            }
        }
        else {
            for (var i = 0; i < menuSize; i++) {
                $("menutext" + i).innerHTML = menuArray[(parseInt(menuPos / 6)) * 6 + i].menuTitle.slice(0, menuWordSize);
                $("menutext" + i).className = "menu_list";
            }

        }
    }

    else if (menuPos < menuSize && menuArray.length < menuSize) {
        for (var i = 0; i < menuArray.length; i++) {
            $("menutext" + i).innerHTML = menuArray[i].menuTitle.slice(0, menuWordSize);
            $("menutext" + i).className = "menu_list";
        }
    }

}






function showMenuRight() {
    for (var i = 0; i < menuSize; i++) {

        $("menutext" + i).innerHTML = "";
        $("menutext" + menuPos % menuSize).className = "menu_list";
    }

    if ((menuArray.length - menuPos) > menuSize) {
        for (var i = 0; i < menuSize; i++) {
            $("menutext" + i).innerHTML = menuArray[menuPos + i].menuTitle.slice(0, menuWordSize);
            $("menutext" + menuPos % menuSize).className = "menu_list";
        }

    }
    else {

        for (var i = 0; i < (menuArray.length - menuPos); i++) {
            $("menutext" + i).innerHTML = menuArray[menuPos + i].menuTitle.slice(0, menuWordSize);
            $("menutext" + menuPos % menuSize).className = "menu_list";


        }
    }

}

function showMenuLeft() {
    for (var i = 0; i < menuSize; i++) {
        $("menutext" + i).innerHTML = "";
        $("menutext" + menuPos % menuSize).className = "menu_list";
    }
    for (var i = 0; i < menuSize; i++) {
        var cot = menuSize - i - 1;
        $("menutext" + cot).innerHTML = menuArray[menuPos - i].menuTitle.slice(0, menuWordSize); ;
        $("menutext" + menuPos % menuSize).className = "menu_list";

    }

}



function menuFocus(__num) {
    $("menutext" + menuPos % menuSize).innerHTML = menuArray[menuPos].menuTitle.slice(0, menuWordSize);
    $("photo").style.background = "url('" + menuArray[menuPos].photo + "')";
    $("menutext" + menuPos % menuSize).className = "menu_list";
    menuPos += __num;
    if (menuPos > 0) {
        $('arrow0').style.opacity = "1";
    }
    else {
        $('arrow0').style.opacity = "0";
    }

    if (menuPos >= menuArray.length - 1) {
        $('arrow1').style.opacity = "0";
    }
    else {
        $('arrow1').style.opacity = "1";
    }
    if (menuPos < 0) {
        menuPos = 0;

    } else if (__num > 0 && (menuPos + 1) > menuArray.length) {
        menuPos -= __num;

    } else if (__num > 0 && (menuPos) % menuSize == 0) {
        showMenuRight();

    }
    else if (__num < 0 && (menuPos + 1) % menuSize == 0) {
        menuPos++;
        menuPos -= 1;
        showMenuLeft();

    }
    $("focus0").style.left = 139 + menuPos % 6 * 169 + "px";
    $("menutext" + menuPos % menuSize).className = "menu_list_focus";
    $("photo").style.background = "url('" + menuArray[menuPos].photo + "')";
    showList();
    if (menuArray[menuPos].menuTitle.length > menuWordSize) {
        $("menutext" + menuPos % menuSize).innerHTML = '<marquee direction="left" width="95%" scrollamonut="15" scrolldelay="100">' + menuArray[menuPos].menuTitle + '</marquee>';
    }

}


//条目
function showList() {
    var position = (parseInt((listPos + listSize) / listSize) - 1) * listSize; //当前页的第一个
    for (i = 0; i < listSize; i++) {
        if (position + i < menuArray[menuPos].listArray.length) {
            $("listtext" + i).innerHTML = ((i + 1) > 9 ? "0" : (i + 1))+ "." +menuArray[menuPos].listArray[position + i].title.slice(0, wordSize);
            $("listtext" + i).style.opacity = "1";
        }
        else {
            $("listtext" + i).style.opacity = "0";
        }
    }
    pageNum();
}

function listFocus(__num) {
    $("listtext" + listPos % listSize).innerHTML = ((listPos % listSize + 1) > 9 ? "0" : (listPos % listSize + 1)) + "." + menuArray[menuPos].listArray[listPos].title.slice(0, wordSize);
    $("focus1").style.opacity = "1";
    var tempPos = listPos;
    listPos += __num;
    if (listPos < 0) listPos = 0;
    else if (listPos > menuArray[menuPos].listArray.length - 1) {
        listPos = tempPos;
        $("focus1").style.opacity = "0";
        area = 2;
        btnFocus(0);
        return;
    }
    //如果翻了页，条目重新输出
    if (parseInt((listPos + listSize) / listSize) !== parseInt((tempPos + listSize) / listSize)) {
        if (__num < 0) {
            showList();
        }
        else {
            listPos = tempPos;
            $("focus1").style.opacity = "0";
            area = 2;
            btnFocus(0);
            return;
        }
    }
    $("focus1").style.top = 143 + listPos % 7 * 58 + "px";
    if (menuArray[menuPos].listArray[listPos].title.length > wordSize) {
        $("listtext" + listPos % listSize).innerHTML = '<marquee direction="left" width="95%" scrollamonut="15" scrolldelay="100">' + menuArray[menuPos].listArray[listPos].title + '</marquee>';
    }
}


//当前页最后一个
function pagelast() {
    listPos = (parseInt((listPos + listSize) / listSize) - 1) * listSize + listSize - 1;
    if (listPos > menuArray[menuPos].listArray.length - 1) {
        listPos = menuArray[menuPos].listArray.length - 1;
    }
}

//页码显示
function pageNum() {
    var listPage = parseInt((listPos + listSize) / listSize); //当前第几页
    var totalPage = parseInt((menuArray[menuPos].listArray.length - 1 + listSize) / listSize); //总共多少页
    $("page").innerHTML = listPage + "/" + totalPage;
    if (totalPage == 0) {
        $("page").innerHTML = "0" + "/" + "0";
    }
}

//上一页
function pageUp() {
    listPos -= listSize;
    if (listPos < 0) {
        listPos += listSize;
    }

    showList();
}

//下一页
function pageDown() {
    listPos += listSize;
    if (listPos > menuArray[menuPos].listArray.length - 1) {
        if (parseInt(listPos / listSize) != parseInt((menuArray[menuPos].listArray.length - 1 + listSize) / listSize)) {
            listPos = menuArray[menuPos].listArray.length - 1;
        }
        else listPos -= listSize;
    }
    showList();
}


//按钮控制
function btnFocus(__num) {
    if (btnPos + __num < 4 && btnPos + __num >= 0) {
        $("button" + btnPos).style.background = "url('images/icon.png')";
        btnPos += __num;
        $("button" + btnPos).style.background = "url('images/icon_1.png')";
    }
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


//回车响应 
function doselect() {
    if (area == 1) {
        var _url = window.location.href;
        if (_url.indexOf("?") != -1) {
            _url = _url.substring(0, _url.indexOf("?"));
        }
        var returnUrl = location.href.getQueryString("returnUrl");

        if (returnUrl == null || returnUrl == undefined)
            returnUrl = "";
        //        else
        //            returnUrl = decodeURIComponent(location.href.getQueryString("returnUrl"));
        var id = location.href.getQueryString("id");
        var curUrl = _url + "?id=" + id + "&m=" + menuPos + "&l=" + listPos + "&returnUrl=" + returnUrl;


        if (menuArray[menuPos].listArray[listPos].adr.indexOf("?") != -1)
            location.href = menuArray[menuPos].listArray[listPos].adr + "&returnUrl=" + encodeURIComponent(curUrl);
        else
            location.href = menuArray[menuPos].listArray[listPos].adr + "?returnUrl=" + encodeURIComponent(curUrl);

    }
    else if (area == 2) {
        if (btnPos == 0) {
            if (menuArray[menuPos].listArray.length > 0) {
                pageUp();
            }
        }
        else if (btnPos == 1) {
            if (menuArray[menuPos].listArray.length > 0) {
                pageDown();
            }
        }
        else if (btnPos == 2) {
            goback();
        }
        else if (btnPos == 3) {
            location.href = HOMEURL;
        }
    }
}

//初始加载
function init() {
    if (location.href.getQueryString("m") == null || location.href.getQueryString("m") == undefined) {
        menuPos = 0;
    }
    else {
        menuPos = parseInt(location.href.getQueryString("m"));
    }
    if (location.href.getQueryString("l") == null || location.href.getQueryString("l") == undefined) {
        listPos = 0;
    }
    else {
        listPos = parseInt(location.href.getQueryString("l"));
    }
    if (menuPos >= menuArray.length) {
        menuPos = 0;
        listPos = 0;
    }
    if (listPos > menuArray[menuPos].listArray.length) {
        if (menuArray[menuPos].listArray.length < 1) {
            area = 0;
        }
        else {
            area = 1;
            listPos = 0;
        }
    }
    if (menuArray[menuPos].listArray.length < 1) {
        area = 0;
    }
    else {
        area = 1;
        listFocus(0);
    }
    for (i = 0; i < btnArray.length; i++) {
        $("button" + i).innerText = btnArray[i];
    }
    showMenu();
    menuFocus(0);
    if (menuArray[menuPos].listArray.length < 1) {
        area = 0;
        $("focus1").style.opacity = "0";
    }

}




//键盘响应
document.onkeydown = grabEvent;
document.onkeypress = grabEvent;
document.onsystemevent = grabEvent;
document.sonirkeypres = grabEvent;

function grabEvent() {
    var key_code = event.which != undefined ? event.which : event.keyCode;
    if (key_code >= 49 && key_code <= 55)//1~7
    {
        var curIndex = 0;
        curIndex = key_code - 48;
        var newlistPos = parseInt(listPos / listSize) * listSize + curIndex - 1
        if (newlistPos < menuArray[menuPos].listArray.length) {
            if (area == 0) {
                listFocus(newlistPos - listPos);
                area = 1;
            }
            else if (area == 1)
                listFocus(newlistPos - listPos);
            else if (area == 2) {
                listFocus(newlistPos - listPos);
                $("button" + btnPos).style.background = "url('images/icon.png')";
                area = 1;
            }
        }
        return 0;
    }
    switch (key_code) {
        //up     
        case 1:
        case 28:
        case 269:
        case 38:
            if (area == 1) {
                if (listPos % 7 == 0) {
                    area = 0;
                    menuFocus(0);
                    $("focus1").style.opacity = "0";
                }
                else {
                    listFocus(-1);
                }
            }
            else if (area == 2) {
                if (menuArray[menuPos].listArray.length < 1) {
                    area = 0;
                    menuFocus(0);
                    $("button" + btnPos).style.background = "url('images/icon.png')";
                }
                else {
                    area = 1;
                    btnFocus(-1);
                    pagelast();
                    listFocus(0);
                    $("button" + btnPos).style.background = "url('images/icon.png')";
                }
            }
            return 0;
            break;
        //down     
        case 2:
        case 40:
        case 31:
        case 270:
            if (area == 0) {
                if (menuArray[menuPos].listArray.length < 1) {
                    area = 2;
                    btnFocus(0);
                }
                else {
                    area = 1;
                    listFocus(0);
                }
            }
            else if (area == 1) {
                listFocus(1);
            }

            return 0;
            break;
        case 3: //left
        case 37:
        case 29:
        case 271:
            if (area == 0) {
                listPos = 0;
                menuFocus(-1);
            }
            else if (area == 1) {
                area = 0;
                $("focus1").style.opacity = "0";
                listPos = 0;
                menuFocus(-1);
            }
            else if (area == 2) {
                btnFocus(-1);
            }
            return 0;
            break;
        //right     
        case 4:
        case 30:
        case 272:
        case 39:
            if (area == 0) {
                listPos = 0;
                menuFocus(1);
            }
            else if (area == 1) {
                area = 0;
                $("focus1").style.opacity = "0";
                listPos = 0;
                menuFocus(1);
            }
            else if (area == 2) {
                btnFocus(1);
            }
            return 0;
            break;

        case 33:
        case 372: //上一页
            if (area == 1)
                if (menuArray[menuPos].listArray.length > 0) {
                    pageUp();
                }
            return 0;
            break;

        case 34:
        case 373:

            if (area == 1) {
                pageDown();
                if (listPos > menuArray[menuPos].listArray.length - 1) {
                    listPos = menuArray[menuPos].listArray.length - 1;
                }
                if (menuArray[menuPos].listArray.length > 0) {
                    listFocus(0);
                }
            }
            return 0;
            break;

        case 13: //enter
            doselect();
            return 0;
            break;
        case 340: //back
            goback();
            return 0;
            break;

    }
}
/////键盘响应

