/**
* 提供函数用于将普通浮动div
* @author lvll
*
* 主要方法: 
* createMoveLayer(elementName, elementTitle, elementTop, elementLeft, elementWidth, elementHeight);
* appendFloatLayer(elementName, elementTop, elementLeft, elementWidth, elementHeight);
* appendControlBar(elementName);
* appendTitle(elementName, elementTitle);
* appendContorlBox(elementName);
* showFloatLayer(elementName);
* hideFloatLayer(elementName);
* removeFloatLayer(elementName);
*/

//是否提示错误
var floatLayerDebug = false;
//控制条的背景图片
var controlBoxBackgroundImage = "url(/boss/image/header_bg.png)";

/**
* 根据名称获取页面元素
* @param elementName 元素名称
* @return 元素对象
*/
function $(elementName) {
	return document.getElementById(elementName);
}

/**
* 将div浮动成默认样式, 包括控制条, 标题和关闭按钮
* 拖动控制条可移动div, 双击控制条可缩小div, 点击关闭隐藏div
*
* @param elementName 待浮动的div的id (必要参数)
* @param elementTitle 标题
* @param elementLeft 顶端距离
* @param elementLeft 左边距离
* @param elementWidth 宽度
* @param elementHeight 高度
* @return 创建后的浮动div对象
*/
function createMoveLayer(elementName, elementTitle, elementTop, elementLeft, elementWidth, elementHeight) {
	var floatLayer = appendFloatLayer(elementName, elementTop, elementLeft, elementWidth, elementHeight);
	appendControlBar(elementName);
	appendTitle(elementName, elementTitle);
	appendControlBox(elementName);
	return floatLayer;
}

/**
* 将div设置为浮动状态
*
* @param elementName 待浮动的div的id (必要参数)
* @param elementLeft 顶端距离
* @param elementLeft 左边距离
* @param elementWidth 宽度
* @param elementHeight 高度
*/
function appendFloatLayer(elementName, elementTop, elementLeft, elementWidth, elementHeight) {
	var srcElement = $(elementName);
	if(srcElement == null) {
		if(floatLayerDebug) {
			alert(elementName + "不存在.");
		}
		return;
	}
	
	var floatLayer = $(elementName + "FloatLayer");
	if(floatLayer != null) {
		srcElement.style.display = "block";
		floatLayer.style.display = "block";
		appendLayerMask(elementName);
		return floatLayer;
	}
	
	var bodyWidth = document.body.scrollWidth;
	var bodyHeight = document.body.scrollHeight;
	var layerWidth//浮动层宽度

	with(srcElement) {
		style.display = "block";
		if(elementWidth == null) {
			layerWidth = offsetWidth;
			if(style.marginLeft != null) {
				layerWidth += Number(style.marginLeft.replace(/px/, ""));
			}
			if(style.marginRight != null) {
				layerWidth += Number(style.marginRight.replace(/px/, ""));
			}
		} else {
			layerWidth = elementWidth;
		}
	}
	
	var layerTop;//顶部距离;
	if(elementTop == null) {
		layerTop = 200;
	} else {
		layerTop = elementTop;
	}

	//浮动层
	floatLayer = document.createElement("div");
	with(floatLayer) {
		setAttribute("id", elementName + "FloatLayer");
		
		var documentScrollTop = 0;
		if(window.document.documentElement && window.document.documentElement.scrollTop) {
			documentScrollTop = window.document.documentElement.scrollTop;
		} else if(window.document.body.scrollTop) {
			documentScrollTop = window.document.body.scrollTop;
		}
		
		//顶部距离
		style.top = documentScrollTop + layerTop + "px";
		//左边距离
		if(elementLeft != null)
			style.left = elementLeft + "px";
		else
			style.left = (bodyWidth - layerWidth) / 2 + "px";
		//元素宽度

		style.width = layerWidth + "px";
		//元素高度
		if(elementHeight != null) {
			srcElement.style.height = elementHeight + "px";
		}
		
		style.position = "absolute";
		style.zIndex = 1000;
		style.backgroundColor = "#fff";

		appendChild(srcElement);
	}
	
	document.body.appendChild(floatLayer);
	appendLayerMask(elementName);
	return floatLayer;
}

/**
* 为浮动div添加控制条
* 如果div未浮动则将其置为浮动
*
* @param elementName div的id
* @return 控制条对象
*/
function appendControlBar(elementName) {
	if($(elementName) == null) {
		if(floatLayerDebug) {
			alert(elementName + "不存在.");
		}
		return;
	}
	if($(elementName + "FloatLayerControlBar") != null) {
		if(floatLayerDebug) {
			alert(elementName + "FloatLayerControlBar" + "已存在.");
		}
		return $(elementName + "FloatLayerControlBar");
	}
	
	var floatLayer = appendFloatLayer(elementName);

	//控制条
	var floatLayerControlBar = document.createElement("div");
	with(floatLayerControlBar) {
		setAttribute("id", elementName + "FloatLayerControlBar");
		setAttribute("moveTarget", elementName + "FloatLayer");
		style.backgroundImage = controlBoxBackgroundImage;
		style.height = "22px";
		style.lineHeight = "22px";
		style.fontSize = "12px";
		style.cursor = "hand";
	}
	
	floatLayerControlBar.ondblclick = function() {
		if($(elementName).style.display == "none") {
			$(elementName).style.display = "block";
		} else {
			$(elementName).style.display = "none";
		}
		return false;
	}

	with(floatLayer) {
		style.padding = "1px";
		style.border = "1px solid #666";
		appendChild(floatLayerControlBar);
		appendChild($(elementName));
	}
	return floatLayerControlBar;
}

/**
* 为浮动div的控制条添加控制条
* 如果div控制条不存在则添加控制条
*
* @param elementName div的id
* @param elementTitle 标题
* @return 标题对象
*/
function appendTitle(elementName, elementTitle) {
	if($(elementName) == null) {
		if(floatLayerDebug) {
			alert(elementName + "不存在.");
		}
		return;
	}
	
	if($(elementName + "FloatLayerTitle") != null) {
		if(floatLayerDebug) {
			alert(elementName + "FloatLayerTitle" + "已存在.");
		}
		return $(elementName + "FloatLayerTitle");
	}
	
	var floatLayerControlBar = appendControlBar(elementName);
	
	//标题
	var floatLayerTitle = document.createElement("div");
	with(floatLayerTitle) {
		setAttribute("id", elementName + "FloatLayerTitle");
		setAttribute("moveTarget", elementName + "FloatLayer");
		style.styleFloat = "left";
		style.cssFloat = "left";
		style.width = floatLayerControlBar.style.width;
		style.height = "18px";
		style.fontWeight = "bold";
		style.paddingTop = "2px";
		style.paddingLeft = "3px";
		style.color = "#000";
		style.lineHeight = "18px";
		style.textOverflow = "ellipsis";
		style.overflow = "hidden";
		if(elementTitle != null) {
			innerHTML = elementTitle;
		}
	}
	floatLayerControlBar.appendChild(floatLayerTitle);
	return floatLayerTitle;
}

/**
* 为浮动div的控制条添加关闭按钮
* 如果div控制条不存在则添加控制条
*
* @param elementName div的id
* @return 关闭按钮对象
*/
function appendControlBox(elementName) {
	if($(elementName) == null) {
		if(floatLayerDebug) {
			alert(elementName + "不存在.");
		}
		return;
	}
	
	if($(elementName + "FloatLayerControlBox") != null) {
		if(floatLayerDebug) {
			alert(elementName + "FloatLayerControlBox" + "已存在.");
		}
		return $(elementName + "FloatLayerControlBox");
	}
	
	var floatLayerControlBar = appendControlBar(elementName);
	
	//控制框
	var floatLayerControlBox = document.createElement("div");
	with(floatLayerControlBox) {
		setAttribute("id", elementName + "FloatLayerControlBox");
		style.styleFloat = "right";
		style.cssFloat = "right";
		style.paddingTop = "3px";
		style.width = "18px";
		style.color = "#000";
		style.fontSize = "14px";
		style.fontWeight = "bold";
		innerHTML = "×";
	}
	
	//增加点击事件
	floatLayerControlBox.onclick = function() {
		hideFloatLayer(elementName);
	}
	
	floatLayerControlBar.appendChild(floatLayerControlBox);
	
	//减小标题宽度
	var floatLayerTitle = $(elementName + "FloatLayerTitle");
	if(floatLayerTitle != null) {
		floatLayerTitle.style.width = $(elementName + "FloatLayerControlBar").offsetWidth - floatLayerControlBox.offsetWidth - 4 + "px";
	}
	return floatLayerControlBox;
}
/**
* 显示浮动层
* @param elementName 元素名称
* @return 浮动层元素
*/
function showFloatLayer(elementName) {
	var srcElement = $(elementName);
	if(srcElement == null) {
		if(floatLayerDebug) {
			alert(elementName + "不存在.");
		}
		return;
	} else {
		srcElement.style.display = "block";
	}
	
	var floatLayer = $(elementName + "FloatLayer");
	if(floatLayer == null) {
		if(floatLayerDebug) {
			alert(elementName + "FloatLayer不存在.");
		}
		return;
	} else {
		floatLayer.style.display = "block";
	}
	
	var layerMask = $(elementName + "LayerMask");
	if(layerMask != null) {
		layerMask.style.display = "block";
	}
	return floatLayer;
}
/**
* 隐藏浮动层
* @param elementName 元素名称
* @return 浮动层元素
*/
function hideFloatLayer(elementName) {
	var srcElement = $(elementName);
	if(srcElement == null) {
		if(floatLayerDebug) {
			alert(elementName + "不存在.");
		}
		return;
	} else {
		srcElement.style.display = "none";
	}
	
	var floatLayer = $(elementName + "FloatLayer");
	if(floatLayer == null) {
		if(floatLayerDebug) {
			alert(elementName + "FloatLayer不存在.");
		}
		return;
	} else {
		floatLayer.style.display = "none";
	}
	
	var layerMask = $(elementName + "LayerMask");
	if(layerMask != null) {
		layerMask.style.display = "none";
	}
	return floatLayer;
}
/**
* @param elementName 元素名称
*/
function removeFloatLayer(elementName) {
	var floatLayer = $(elementName + "FloatLayer");
	if(floatLayer == null) {
		if(floatLayerDebug) {
			alert(elementName + "FloatLayer不存在.");
		}
	} else {
		document.body.removeChild(floatLayer);
		floatLayer = null;
	}
	
	var layerMask = $(elementName + "LayerMask");
	if(layerMask != null) {
		document.body.removeChild(layerMask);
		layerMask = null;
	}
}
//创建iframe覆盖
function appendLayerMask(elementName) {
	if(document.all != null) {
		var floatLayer = $(elementName + "FloatLayer");
		if(floatLayer == null) {
			if(floatLayerDebug) {
				alert(elementName + "FloatLayer不存在.");
			}
			return;
		}
		
		var layerMask = $(elementName + "LayerMask");
		//iframe不存在则创建
		if(layerMask == null) {
			layerMask = document.createElement("iframe");
			with(layerMask) {
				setAttribute("id", elementName + "LayerMask");
				setAttribute("src","");
				setAttribute("scrolling","no");
				setAttribute("frameborder","0");
				style.position = "absolute";
				style.zIndex = floatLayer.style.zIndex - 1;
				style.filter = "Alpha(Opacity=0)";
				style.opacity = "0";
			}
			
			//当层属性改变时同时更改iframe的位置和大小
			floatLayer.onpropertychange = function() {
				layerMask.style.top = floatLayer.style.top;
				layerMask.style.left = floatLayer.style.left;
				layerMask.style.width = floatLayer.offsetWidth + "px";
				layerMask.style.height = floatLayer.offsetHeight + "px";
			}
			
			document.body.appendChild(layerMask);
		} else {
			layerMask.style.display = "block";
		}
	
		//iframe与浮动层的位置和大小相同
		layerMask.style.top = floatLayer.style.top;
		layerMask.style.left = floatLayer.style.left;
		layerMask.style.width = floatLayer.offsetWidth + "px";
		layerMask.style.height = floatLayer.offsetHeight + "px";

		return layerMask;
	}
}