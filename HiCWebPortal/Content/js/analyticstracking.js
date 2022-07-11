/* GA */
(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
})(window,document,'script','https://www.google-analytics.com/analytics.js','ga');


/* GA */
ga('create', 'UA-138262819-1', 'auto');
ga('send', 'pageview');


/* Facebook Pixel*/
/*
!function(f,b,e,v,n,t,s){if(f.fbq)return;n=f.fbq=function(){n.callMethod?
n.callMethod.apply(n,arguments):n.queue.push(arguments)};if(!f._fbq)f._fbq=n;
n.push=n;n.loaded=!0;n.version='2.0';n.queue=[];t=b.createElement(e);t.async=!0;
t.src=v;s=b.getElementsByTagName(e)[0];s.parentNode.insertBefore(t,s)}(window,
document,'script','https://connect.facebook.net/en_US/fbevents.js');
// Insert Your Custom Audience Pixel ID below. 
fbq('init', '964517867049481');
fbq('track', 'PageView');

function callFBPixel(type){
	if(fbq != undefined){
		fbq('track', type);  
	}
}
*/


function callGaEvent(cat, action, label, value, obj){
	if(ga != undefined){
		ga('send', 'event', cat, action, label, value, obj);

	}
}


function callStaticFL(type, cat) {

	var source = "0000000";
	//var axel = Math.random() + "";
	//var num = axel * 1000000000000000000;
	var tag_url = new Image();
	//tag_url.src = "https://ad.doubleclick.net/ddm/activity/src=" + source + ";type=" + type + ";cat=" + cat + ";ord=1;num="+num+"?";
	tag_url.src = "https://ad.doubleclick.net/ddm/activity/src="+source+";type="+type+";cat="+cat+";dc_lat=;dc_rdid=;tag_for_child_directed_treatment=;ord=1?";

	gtag('event', 'conversion', {
	    'allow_custom_scripts': true,
	    'send_to': 'DC-'+source+'/'+type+'/'+cat+'+standard'
	});

}



function TriggerGA(evt){
	//evt.preventDefault();
	var cat = $(this).data("cat");
	var action = $(this).data("action");
	callGaEvent(cat, action);

	/*
	var gtype = $(this).data("gtype");
	var gcat = $(this).data("gcat");
	callStaticFL(gtype, gcat);
	
	var PixelType = $(this).data("pixel");
	if(PixelType != undefined)
		callFBPixel(PixelType);
	*/
}


function TriggerGA_manual(cat, action, gtype, gcat){
	//evt.preventDefault();
	//var cat = $(this).data("cat");
	//var action = $(this).data("action");
	callGaEvent(cat, action);

	//var gtype = $(this).data("gtype");
	//var gcat = $(this).data("gcat");
	/*
	if(gtype != null && gcat != null)
		callStaticFL(gtype, gcat);
	*/

	//var PixelType = $(this).data("pixel");
	/*
	if(PixelType != undefined)
		callFBPixel(PixelType);
	*/
}


$(document).ready(function(){
	$(".btn-ga").click(TriggerGA);
})
