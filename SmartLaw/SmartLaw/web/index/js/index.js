// JScript source code
var menuPos = 0;
function $(id) {
    return document.getElementById(id);
}

function showMenu() {
    for (i = 0; i < menuArray.length; i++) {
        $("menu" + i).style.background = menuArray[i].smallpic;
    }
}

function menuFocus(__num) {
    
        if (menuPos + __num < menuArray.length && menuPos + __num >= 0) {
        menuPos += __num;
        switch (menuPos) {
        case 0:
        case 1:
        case 2:
        case 3:
        case 4:
        case 5:


    }


//        if (menuPos == 0) {
//            $("menu0").style.top = 155 + "px";
//            $("menu0").style.width = 360 + "px";
//            $("menu0").style.height = 300 + "px";
//            $("menu0").style.left = 110 + "px";
//            $("menu0").style.background = menuArray[0].bigpic;
//            $("menu" + 0).style.zIndex = "2";
//        } else {
//            $("menu0").style.width =339 + "px";
//            $("menu0").style.height = 182 + "px";
//            $("menu0").style.left = 116 + "px";
//            $("menu0").style.top = 165 + "px";
//            $("menu0").style.background = menuArray[0].smallpic;
//            $("menu" + 0).style.zIndex = "-1";
//        }

//        if (menuPos == 1) {
//            $("menu1").style.width = 465 + "px";
//            $("menu1").style.height = 300 + "px";
//            $("menu1").style.left = 456 + "px";
//            $("menu1").style.top = 155 + "px";
//            $("menu1").style.background = menuArray[1].bigpic;
//            $("menu1").style.zIndex = "2";

//        } else {
//            $("menu1").style.width = 426 + "px";
//            $("menu1").style.height = 182 + "px";
//            $("menu1").style.left = 476 + "px";
//            $("menu1").style.top = 165 + "px";
//            $("menu1").style.background = menuArray[1].smallpic;
//            $("menu" + 1).style.zIndex = "-1";
//        }
//        if (menuPos == 2) {
//            $("menu2").style.width = 285 + "px";
//            $("menu2").style.height = 300 + "px";
//            $("menu2").style.left = 910 + "px";
//            $("menu2").style.top = 155 + "px";
//            $("menu2").style.background = menuArray[2].bigpic;
//            $("menu2").style.zIndex = "2";

//        } else {
//            $("menu2").style.width = 260 + "px";
//            $("menu2").style.height = 182 + "px";
//            $("menu2").style.left = 925 + "px";
//            $("menu2").style.top = 165 + "px";
//            $("menu2").style.background = menuArray[2].smallpic;
//            $("menu" + 2).style.zIndex = "-1";
//        }

//        if (menuPos == 3) {
//            $("menu3").style.width = 570 + "px";
//            $("menu3").style.height = 220 + "px";
//            $("menu3").style.left = 110+ "px";
//            $("menu3").style.top = 445 + "px";
//            $("menu3").style.background = menuArray[3].bigpic;
//            $("menu3").style.zIndex = "2";

//        } else {
//            $("menu3").style.width = 510+ "px";
//            $("menu3").style.height = 202+ "px";
//            $("menu3").style.left = 116 + "px";
//            $("menu3").style.top = 456 + "px";
//            $("menu3").style.background = menuArray[3].smallpic;
//            $("menu" + 3).style.zIndex = "-1";
//        }

    
 }

 function doSelect() {
     var curUrl = location.href;
     if (menuArray[menuPos].href.indexOf("?") != -1)
         location.href = menuArray[menuPos].href + "&returnUrl=" + encodeURIComponent(curUrl);
     else
         location.href = menuArray[menuPos].href + "?returnUrl=" + encodeURIComponent(curUrl);
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

document.onkeydown = grabEvent;
//document.onkeypress = grabEvent;
document.onsystemevent = grabEvent;
document.sonirkeypres = grabEvent;

function grabEvent() {
    var key_code = event.which != undefined ? event.which : event.keyCode;
    switch (key_code) {
        //up    
        case 1:
        case 28:
        case 269:
        case 38:
            if (menuPos == 2 || menuPos == 3) {
                menuFocus(-menuPos);
            } else { menuFocus(-3); }
            return 0;
            break;
        //down    
        case 2:
        case 40:
        case 31:
        case 270:
            if (menuPos == 0) {
                menuFocus(2);
            } else { menuFocus(3); }
            return 0;
            break;
        case 3: //left
        case 37:
        case 29:
        case 271:
            menuFocus(-1);
            return 0;
            break;
        //right    
        case 4:
        case 30:
        case 272:
        case 39:
            menuFocus(1);
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
    }
}


function init() {
   

    showMenu();
    menuFocus(0);
    
    
    
}
