$(function () {
    WorkForce.init();
});
var WorkForce = {
    init: function () {
        this.loader.init();
    },
    loader: {
        init: function () {
            $(window).bind('load', function () {
                WorkForce.loader.hide(500);
            });
        },
        show: function (t) {
            t = t ? t : 500;
            $('.loader-wrap').fadeIn(t);
        },
        hide: function (t) {
            t = t ? t : 500;
            $('.loader-wrap').fadeOut(t);
        }
    }
}