var contentFocus = 0;
var contentSize = 0;
var contentPageMaxSize = 8;
var contentMaxSize = 8;
var contentPos = 0;
var contentPageSize = 0;
var topFocus = 0;
var topPos = 0;
var topSize = 2;
var bottomFocus = 0;
var bottomPos = 0;
var bottomSize = 3;
var pageNum = 0;
var page = 0;
var wordSize = 23;

//contentView
var topvalue = 0;
var totalHeight = 0;
var pageHeight = 450;
var moveHeight = 450;
var scrollBarMaxLength = 0;
var scrollBarLength = 0;
var scrollBarLengthScale = 0;
var scrollBarScale = 0;
var jsonArray = [];


function scrollPage(_num) {
    if (_num > 0) {
        if (topvalue < 0) {
            topvalue += moveHeight;
            scrollBarLength -= scrollBarLengthScale;
            document.getElementById("inner").style.top = topvalue + "px";
            $("scrollBar1").style.height = scrollBarLength + "px"; //(-topvalue / moveHeight + 1) + "/" + ((totalHeight - pageHeight) <= 0 ? 1 : Math.ceil((totalHeight - pageHeight) / moveHeight) + 1);
        }
    }
    else{
        if (-topvalue + pageHeight < totalHeight) {
            topvalue -= moveHeight;
            scrollBarLength += scrollBarLengthScale;
            document.getElementById("inner").style.top = topvalue + "px";
            $("scrollBar1").style.height = scrollBarLength + "px"; //(-topvalue / moveHeight + 1) + "/" + ((totalHeight - pageHeight) <= 0 ? 1 : Math.ceil((totalHeight - pageHeight) / moveHeight) + 1);
        }
    }
}

var menuJson = {
    "data": [
    { "img": "1", "title": "测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试", "date": "2015-07-15", "content": "123" },
{ "img": "1", "title": "test1", "date": "2015-07-15", "content": "1234" },
{ "img": "0", "title": "test2", "date": "2015-07-15", "content": "12345" },
{ "img": "1", "title": "test3", "date": "2015-07-15", "content": "1234567" },
{ "img": "0", "title": "test4", "date": "2015-07-15", "content": "123123" },
{ "img": "1", "title": "test0", "date": "2015-07-15", "content": "7" },
{ "img": "1", "title": "test3", "date": "2015-07-15", "content": "121test233" },
{ "img": "0", "title": "test4", "date": "2015-07-15", "content": "12qwe3" }
    ],
    "totalCount": 8,
    "message": "",
    "page": 0,
    "top": 0
};

var menuList = [
{ "img": "1", "title": "测试测试测试测试测试测试测试测试测试测试测试测试测试测试测试", "date": "2015-07-15", "content":"123" },
{ "img": "1", "title": "test1", "date": "2015-07-15", "content": "1234" },
{ "img": "0", "title": "test2", "date": "2015-07-15", "content": "12345" },
{ "img": "1", "title": "test3", "date": "2015-07-15", "content": "1234567" },
{ "img": "0", "title": "test4", "date": "2015-07-15", "content": "123123" },
{ "img": "1", "title": "test0", "date": "2015-07-15", "content": "7" },
{ "img": "1", "title": "test1", "date": "2015-07-15", "content": "1239" },
{ "img": "0", "title": "test2", "date": "2015-07-15", "content": "123asdasd" },
{ "img": "1", "title": "test3", "date": "2015-07-15", "content": "123" },
{ "img": "0", "title": "test4", "date": "2015-07-15", "content": "123" },
{ "img": "1", "title": "test0", "date": "2015-07-15", "content": "123" },
{ "img": "1", "title": "test1", "date": "2015-07-15", "content": "123" },
{ "img": "0", "title": "test2", "date": "2015-07-15", "content": "12grfsd3" },
{ "img": "1", "title": "test3", "date": "2015-07-15", "content": "121test233" },
{ "img": "0", "title": "test4", "date": "2015-07-15", "content": "12qwe3"}];

var area = 0;

function $(id) {
    return document.getElementById(id);
}
function initFocus() {
    $("bottomFocus").className = "bottomFocus";
    $("contentFocus").className = "contentFocus";
    $("topFocus").className = "topFocus";
}

//内容栏
function contentFocusImg(__num) {
    if (contentPos + __num < contentSize && contentPos + __num >= 0) {
        contentPos += __num;
        $("contentFocus").className = "contentFocus" + contentPos;
    }
    else if (contentPos + __num < 0) {
        initFocus();
        area = 0;
        topFocusImg(0);
    } 
    else if (contentPos + __num >= contentSize) {
        initFocus();
        area = 2;
        bottomFocusImg(0);
    }
}
//下边栏
function bottomFocusImg(__num) {
    if (bottomPos + __num < bottomSize && bottomPos + __num >= 0) {
        bottomPos += __num;
        $("bottomFocus").className = "bottomFocus" + bottomPos;
    }
}
//上边栏
function topFocusImg(__num) {
    if (topPos + __num < topSize && topPos + __num >= 0) {
        topPos += __num;
        $("topFocus").className = "topFocus" + topPos;
    }
}
function getWordSize(__num) {//判定字数多少
    if (__num.length > wordSize) {
        return __num.slice(0, wordSize - 1) + "...";
    }
    else {
        return __num;
    }
}
function initContent(_jsonArray) {
    contentSize = _jsonArray.length;

    for (var i = 0; i < contentSize; i++) {
        var single = _jsonArray[i];
        $("item" + i).className = "item0";
        $("icon" + i).className = "icon" + single.img;
        $("title" + i).innerHTML = getWordSize(single.title);
        $("itemDate" + i).innerHTML = "(" + single.date + ")";
    }
}
function contentMenuClear() {//右边清理
    for (var i = 0; i < contentPageMaxSize; i++) {
        $("item" + i).className = "item";
    }
}

function init() {
    menuList = menuJson.data;
    contentMaxSize = menuJson.totalCount;
    if (contentPageMaxSize < contentMaxSize) {
        contentSize = contentPageMaxSize;
    } else {
        contentSize = contentMaxSize;
    }
    jsonArray = menuList.slice(0, contentSize);
    initContent(jsonArray);
    initFocus();
    topFocusImg(0);
}

function menu_prev() {
    if (contentPageSize - contentPageMaxSize >= 0) {
        $("loading").style.visibility = "visible";
        contentPageSize -= contentPageMaxSize;
        //Ajax(contentPageSize, contentPageSize + contentPageMaxSize,0);
        jsonArray = menuList.slice(contentPageSize, [contentPageSize + contentPageMaxSize]);
        contentMenuClear();
        initContent(jsonArray);
    }
    else {
    }
}
function menu_next() {
    if (contentPageSize + contentPageMaxSize < contentMaxSize) {
        $("loading").style.visibility = "visible";
        contentPageSize += contentPageMaxSize;
        //Ajax(contentPageSize, contentPageSize + contentPageMaxSize, 0);
        jsonArray = menuList.slice(contentPageSize, [contentPageSize + contentPageMaxSize]);
        contentMenuClear();
        initContent(jsonArray);
    }
    else {
    }
}

function pageTurn(__num) {
    if (__num > 0) {//up
        menu_prev();
    } else {//down
        menu_next();
    }
}
function windows_href() {

}
function doSelect(_jsonArray) {
    area = 3;
    $("contentView").style.visibility = "visible";
    $("contentText").style.visibility = "visible";
    if (_jsonArray != null) {
        $("inner").innerHTML = _jsonArray[contentPos].content;
    }
    totalHeight = $("inner").clientHeight;
    scrollBarMaxLength = $("scrollBar0").clientHeight;
    scrollBarScale = ((totalHeight - pageHeight) <= 0 ? 0 : Math.ceil((totalHeight - pageHeight) / moveHeight)) + 1;
    scrollBarLength = scrollBarMaxLength / scrollBarScale;
    scrollBarLengthScale = scrollBarMaxLength / scrollBarScale;
    $("scrollBar1").style.height = scrollBarLength + "px";
}

function goBack() {
    area = 1;
    $("contentView").style.visibility = "hidden";
    $("contentText").style.visibility = "hidden";
}



//document.onsystemevent = grabEvent;
//document.onkeypress = grabEvent;
//document.onirkeypress=grabEvent;
document.onkeydown = function () {
    var key_code = event.keyCode;
    //alert(key_code);
    switch (key_code) {
        case 1: //up
        case 38:
            if (area == 0) {

            }
            else if (area == 1) {
                contentFocusImg(-1);
            } else if (area == 2) {
                area = 1;
                initFocus();
                contentFocusImg(0);
            } else if (area == 3) {
                scrollPage(1);
            }
            return 0;
            break;
        case 2: //down
        case 40:
            if (area == 0) {
                area = 1;
                initFocus();
                contentFocusImg(0);
            }
            else if (area == 1) {
                contentFocusImg(1);
            } else if (area == 2) {

            } else if (area == 3) {
                scrollPage(0);
            }
            return 0;
            break;
        case 3: //left
        case 37:
            if (area == 0) {
                topFocusImg(-1);
            }
            else if (area == 1) {

            } else if (area == 2) {
                bottomFocusImg(-1);
            } else if (area == 3) {
                goBack();
            }
            return 0;
            break;
        case 4: //right
        case 39:
            if (area == 0) {
                topFocusImg(1);
            }
            else if (area == 1) {

            } else if (area == 2) {
                bottomFocusImg(1);
            } else if (area == 3) {

            }
            return 0;
            break;
        case 13: //enter
            //iPanel.Media.videoControl('stop');
            if (area == 0) {
                pageTurn(topPos);
            }
            else if (area == 1) {
                doSelect(jsonArray);
            } else if (area == 2) {
                windows_href();
            } else if (area == 3) {

            }

            return 0;
            break;
        case 340: //back
            if (returnUrl == null || returnUrl == "") {
                location.href = "search_Employs.aspx?id=" + Key;
            }
            else if (area == 3) {
                goBack();
            }
            else {
                location.href = decodeURIComponent(use_returnUrl);
            }
            return 0;
            break;
        case 372: //上
            on_btn0();
            return 0;
            break;
        case 373: //下
            on_btn1();
            return 0;
            break;
        case 283:
            if (returnUrl == null || returnUrl == "") {
                location.href = "search_Employs.aspx?id=" + Key;
            }
            else {
                location.href = decodeURIComponent(use_returnUrl);
            }
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
    function grabEvent() {
        var key_code = event.which;
        alert(key_code);
    }
}