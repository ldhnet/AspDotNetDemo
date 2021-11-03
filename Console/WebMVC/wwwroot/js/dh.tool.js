
(function ($) {
    $.dh = $.dh || { version: 1.0 };
})(jQuery);


(function ($) {

    //辅助工具类
    $.dh = {

        //判断是否IE，兼容到IE11
        isIe: ("ActiveXObject" in window),

        isChrome: !!window.chrome && !!window.chrome.webstore,

        isFirefox: typeof InstallTrigger !== 'undefined',

        getChromeVer: function () {
            var agent = navigator.userAgent.toLowerCase();
            if (agent.indexOf("chrome") > 0) {
                var code = agent.match(/chrome\/[\d.]+/gi);
                if (code.length > 0) {
                    return code[0].replace("chrome/", "").split(".")[0];
                }
            }
            return undefined;
        },

        //Cookie操作
        cookies: {
            get: function (name) {
                var value = "";
                var search = name + "=";
                if (document.cookie.length == 0) {
                    return value;
                }
                var offset = document.cookie.indexOf(search);
                if (offset == -1) {
                    return value;
                }
                offset += search.length;
                var end = document.cookie.indexOf(";", offset);
                if (end == -1) {
                    end = document.cookie.length;
                }
                value = decodeURIComponent(document.cookie.substring(offset, end));
                return value;
            },
            set: function (name, value, expireDays) {
                var expires;
                if (expireDays == null) {
                    expireDays = 1;
                }
                expires = new Date((new Date()).getTime() + expireDays * 86400000);
                expires = ";expires=" + expires.toGMTString();
                document.cookie = name + "=" + encodeURIComponent(value) + ";path=/" + expires;
            },
            remove: function (name) {
                var expires;
                expires = new Date(new Date().getTime() - 1);
                expires = ";expires=" + expires.toGMTString();
                document.cookie = name + "=" + escape("") + ";path=/" + expires;
            }
        },
        //localstorage操作
        localstorage: {
            get: function (name) {
                var timeout = name + "_timeout";
                var value = window.localStorage.getItem(name);
                if (!value) {
                    return "";
                }

                var datetime_timeout = parseInt(window.localStorage.getItem(timeout));

                var datetime_now = new Date().getTime();

                if (datetime_now > datetime_timeout) {
                    this.remove(name);
                    return "";
                } else {
                    return value;
                }
            },
            set: function (name, value, expireDays) {
                var timeout = name + "_timeout";
                var expires;
                if (expireDays == null) {
                    expireDays = 1;
                }
                expires = new Date((new Date()).getTime() + expireDays * 86400000);
                window.localStorage.setItem(name, value);
                window.localStorage.setItem(timeout, expires.toGMTString());
            },
            remove: function (name) {
                var timeout = name + "_timeout";
                window.localStorage.removeItem(name);
                window.localStorage.removeItem(timeout);
            },
            clear: function () {
                window.localStorage.clear();
            },
        },
        //sessionstorage操作
        sessionstorage: {
            get: function (name) {

            },
            set: function (name, value, expireDays) {

            },
            remove: function (name) {
            },
            clear: function () {

            },
        },

        shortkeysearch: function (fn, $dom) {
            //ENTER  快捷键
            ($dom || $(document)).on("keydown", function () {
                var e = window.event || arguments.callee.caller.arguments[0];
                var code = e.keyCode || e.which || e.charCode;
                if (e && code == 13) { // enter 键
                    if (fn && typeof fn == "function") {
                        fn();
                    }
                    return false;
                }
            });
        },
        //追加Query参数
        appendQueryString: function (url, parameters) {
            url += "?";
            for (var key in parameters) {
                if (parameters[key] === null || parameters[key] === undefined || parameters[key] === "") {
                    continue;
                }
                url += key + "=" + encodeURIComponent(parameters[key]) + "&";
            }
            if (url.indexOf("rnd=") == -1)
                url += "rnd=" + Math.random();
            if (url.endWith("?")) url = url.substr(0, url.length - 1);
            return url;
        },

        //URL编码与解码
        url: {
            encode: function (url) {
                return encodeURIComponent(url);
            },
            decode: function (url) {
                return decodeURIComponent(url);
            }
        },
        //数据处理
        data: {
            valueToText: function (value, array, defaultText) {
                var text = defaultText == null ? value : defaultText;
                $.each(array, function () {
                    if (this.id != undefined && this.id == value) {
                        text = this.text;
                        return false;
                    }
                    if (this.id != undefined && this.id == value) {
                        text = this.text;
                        return false;
                    }
                    return true;
                });
                return text;
            }
        },
        //集合操作
        array: {
            expandAndToString: function (array, separator) {
                var result = "";
                if (!separator) {
                    separator = ";";
                }
                $.each(array, function (index, item) {
                    result = result + item.toString() + separator;
                });
                return result.substring(0, result.length - separator.length);
            }
        },
        //时间格式化
        formatDate: function (value, format) {
            if (!value) {
                return "";
            }
            format = format || "yyyy-MM-dd hh:mm:ss";
            return (new Date(parseInt(value.substring(value.indexOf('(') + 1, value.indexOf(')'))))).format(format);
        },

        //时间格式化
        formatDateTime: function (value, format) {
            if (!value) {
                return "";
            }
            format = format || "yyyy-MM-dd hh:mm:ss";
            return (new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10))).Format(format);
        },

        //返回上一页
        backPage: function () {
            //window.history.back(-1);
            if (window.history.state != null) {
                window.history.go(-1);
            }
        },

        //跳转页面
        directePage: function (url) {
            window.location.href = url;
        },

        //替换页面
        replacePage: function (url) {
            window.location.replace(url);
        },
          
        /**
         * 将json格式的日期(例如:"\/Date(1522512000000)\/")转为可读日期
         * @param {string} jsonTime json格式的日期
         */
        formatDate: function (jsonTime) {
            var dt = new Date(parseInt(jsonTime.slice(6, 19)));
            var year = dt.getFullYear();
            var month = dt.getMonth() + 1;
            var monthText = month < 10 ? "0" + month : "" + month;
            var date = dt.getDate();
            var dateText = date < 10 ? "0" + date : "" + date;
            return year + "/" + monthText + "/" + dateText;
        },

        download: function (url, fileName) {
            var request = new XMLHttpRequest();
            request.responseType = "blob";
            request.open("GET", url);
            request.onload = function () {
                var url = window.URL.createObjectURL(this.response);
                var a = document.createElement("a");
                document.body.appendChild(a);
                a.href = url;
                a.download = fileName
                a.click();
                $.vxi.loading.point.stop();
            }
            request.send();
            $.vxi.loading.point.start();
        }
    };
})(jQuery);


(function ($) {
    $.fn.serializeJson = function () {
        var serializeObj = {};
        var array = this.serializeArray();

        $(array).each(function () {
            if (serializeObj[this.name]) {
                if ($.isArray(serializeObj[this.name])) {
                    serializeObj[this.name].push(this.value);
                } else {
                    serializeObj[this.name] = [serializeObj[this.name], this.value];
                }
            } else {
                if (this.name.indexOf(".") < 0) {
                    serializeObj[this.name] = this.value;
                } else {
                    var subNameArr = this.name.split(".");
                    var length = subNameArr.length;

                    var obj = serializeObj[subNameArr[0]];
                    if (!obj) {
                        serializeObj[subNameArr[0]] = {};
                        obj = serializeObj[subNameArr[0]];
                    }

                    for (var i = 1; i < length; i++) {
                        if (i == length - 1)
                            obj[subNameArr[i]] = this.value;
                        else {
                            if (!obj[subNameArr[i]]) {
                                obj[subNameArr[i]] = {};
                            }
                            obj = obj[subNameArr[i]];
                        }
                    }
                }
            }
        });
        return serializeObj;
    };

    //fix sendAsBinary for chrome
    try {
        if (typeof XMLHttpRequest.prototype.sendAsBinary == 'undefined') {
            XMLHttpRequest.prototype.sendAsBinary = function (text) {
                var data = new ArrayBuffer(text.length);
                var ui8a = new Uint8Array(data, 0);
                for (var i = 0; i < text.length; i++) ui8a[i] = (text.charCodeAt(i) & 0xff);
                this.send(ui8a);
            };
        }
    } catch (e) {
    }
})(jQuery);


String.prototype.startWith = function (str) {
    if (str == null || str == "" || this.length == 0 || str.length > this.length)
        return false;
    if (this.substr(0, str.length) == str)
        return true;
    else
        return false;
    //return true;
};

String.prototype.endWith = function (str) {
    if (str == null || str == "" || this.length == 0 || str.length > this.length)
        return false;
    if (this.substring(this.length - str.length) == str)
        return true;
    else
        return false;
    //return true;
};

Date.prototype.Format = function (fmt) { //author: meizz   
    var o = {
        "M+": this.getMonth() + 1,                 //月份   
        "d+": this.getDate(),                    //日   
        "h+": this.getHours(),                   //小时   
        "m+": this.getMinutes(),                 //分   
        "s+": this.getSeconds(),                 //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds()             //毫秒   
    };
    if (/(y+)/.test(fmt))
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt))
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}