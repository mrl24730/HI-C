var isCalling = false;

var AjaxManager = {
    that: this,
    url: null,
    postData: null,
    callback: null,
    errCallback: null,
    timeout: 180000,

    fire: function(url, postData, successCallBack, _timeout, _method, _errCallBack) {

        if (!isCalling) {
            isCalling = true;

            $(document).trigger("ajaxRequestStart");

            if (_timeout !== null) AjaxManager.timeout = _timeout;

            _method = (_method == undefined) ? "POST" : _method;

            if (successCallBack != undefined) AjaxManager.callback = successCallBack;
            if (_errCallBack != undefined) AjaxManager.errCallback = _errCallBack;

            AjaxManager.url = url;
            AjaxManager.postData = postData;

            jQuery.ajax({
                    timeout: AjaxManager.timeout,
                    method: _method,
                    url: url + "?v=" + Math.random(),
                    dataType: "json",
                    data: postData
                })
                .fail(function(x, t, m) {
                    AjaxManager.error(x, t, m);
                }).done(function(result) {
                    AjaxManager.success(result);
                }).always(function() {
                    $(document).trigger("ajaxRequestEnd");
                });

        }
    },

    error: function(x, t, m) {
        isCalling = false;
        if (AjaxManager.errCallback) AjaxManager.errCallback(x, t, m);
        if (t === "timeout") {

            //alert("Connection timeout, please try again."); //Connection timeout, please try again

        } else {
            //alert("Connection error, please try again later.\n"+m); //Connection error, please try again later.
        }

        this.reset();
    },

    success: function(result) {
        isCalling = false;
        if (AjaxManager.callback) AjaxManager.callback(result);
        this.reset();
    },

    reset: function() {

    }
}


var KitFormValidator = (function() {

    var cls = function() {

        var that = this;

        that.isEmpty = function(s) {
            s = s.trim();
            return ((s == null) || (s.length == 0))
        }

        that.isEmail = function(emailAddr) {
            if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(emailAddr))
                return (true);
        }

        that.isNumber = function(str) {
            // body...
            charset = "0123456789";
            var result = true;

            for (var i = 0; i < str.length; i++) {
                if (charset.indexOf(str.substr(i, 1)) < 0) {
                    result = false;
                    break;
                }
            }

            return result;
        }

        that.isPhone = function(str) {
            return (/^([2,3,5,6,9])([0-9]{7})$/).test(str);
        }
    }

    return cls;

})();


var KitUIHelper = (function() {

    var cls = function() {

        var that = this;

        that.scrollToElm = function(elm, time, offset) {
            //var x = $(elm).offset().top - 100; // 100 provides buffer in viewport
            var x = $(elm).offset().top;
            if (time == undefined) {
                time = 500;
            }

            if (offset != undefined) {
                x += offset;
            }

            $('html,body').animate({ scrollTop: x }, time);
        }

        that.scrollToX = function(x, time) {
            if (time == undefined)
                time = 500;
            $('html,body').animate({ scrollTop: x }, time);
        }


        that.getQueryString = function() {
            var vars = [],
                hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }

        that.getHashByKey = function() {
            var vars = [],
                hash;
            var hashes = window.location.hash.slice(window.location.hash.indexOf('#') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }
        
    }

    return cls;

})();
