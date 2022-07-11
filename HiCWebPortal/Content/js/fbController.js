
var FB_APP_ID = 633633717049819;

var FBController = (function() {
    var cls = function() {
        var that = this;
        var isConnected = false;
        var isShare = false;
        that.fbname = "";
        that.fbpic = "";
        init();

        function init() {
            $(".btn-FbShare").click(function(evt){
                evt.preventDefault();
                var url = window.location.href;

                if($(this).data("url") != undefined && $(this).data("url") != ""){
                    url = $(this).data("url");
                }
                that.Share(url, null);
            });
        }

        that.Share = function(url, _callback) {
            FB.ui({
                method: 'share',
                href: url,
            }, function(response) {

                if (response !== undefined) {
                    if (_callback)
                        _callback(true);
                } else {
                    if (_callback)
                        _callback(false);
                }
            });
        } //end function

    } //end cls

    return cls;

})();


(function(d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s);
    js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));


var fbCtr = new FBController();

window.fbAsyncInit = function() {
    FB.init({
        appId: FB_APP_ID,
        cookie: true,
        xfbml: true,
        version: 'v3.0'
    });

    FB.AppEvents.logPageView();

};