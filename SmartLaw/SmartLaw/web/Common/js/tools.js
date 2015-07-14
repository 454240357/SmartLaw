String.prototype.getQueryString = function (name) {
    var reg = new RegExp("(^|&|\\?)" + name + "=([^&]*)(&|$)"), r;
    if (r = this.match(reg))
        return r[2];
        //return unescape(r[2]);
    return null;
};