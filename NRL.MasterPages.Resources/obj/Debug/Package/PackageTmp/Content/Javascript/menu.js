$(function () {

    if ($(window).width() < 700) {

        var menu = new SlideMenu({
            menuText: '',
            closeText: '',
            backText: '',
            icons: true,
            button: $('#icon-menu'),
            menu: $('.nrl-nav'),
            submenu: $('.nrl-nav ul ul'),
            hide: $('#nrl-content, footer, #nrl-sponsor, #nrl-adLeaderboard')
        });

    }
    
    if ($(window).width() < 753) {
        
        $('.phoneNumber').wrap("<a href='#' onclick='goog_report_conversion();'>");
    }

});

SlideMenu = function(params) {
    this.menuText = 'Menu';
    this.closeText = 'Close';
    this.backText = 'Back';
    this.icons = false;
    for (name in params) {
        this[name] = params[name];
    }
    this.button.text(this.menuText).fadeIn();
    this.init();
};

SlideMenu.prototype = {

    state: 1,

    timer: null,

    init: function() {
        var that = this;
        this.button.off('click');
		this.menu.off('click');
        this.left = $(window).width() + 'px';
        this.width = $(window).width();
        this.submenu.each(function () {
            $(this).css({
                position: 'absolute',
                width: that.width - parseInt($(this).css('margin-left')),
                left: '100%',
                top: '0'
            }).hide();
        });
        this.menu.css({left: this.left, width: this.width}).hide();
        this.button.toggle(
            function () {
                $(this).data('open', true);
            },
            function () {
                $(this).data('open', false);
            }
        );
        this.button.on('click', function(){
            if (that.pre) {
                that.pre();
            }
            that.slide();
        });
        this.menu.on('click', 'a', function(e){
            var ul = $(this).parent().has('ul');
            if (ul.length) {
                e.preventDefault();
                that.showSubnav(ul.find('ul').first());
            }
        });

        window.onorientationchange = function(){
            clearTimeout(that.timer);
            that.timer = setTimeout(function(){
                that.init();
                that.state = 2;
				that.slide();
				that.button.removeClass('icon-menu-back');
            }, 100);
        };

    },

    showSubnav: function(subnav) {
        var that = this;
        subnav.show(1, function(){
            that.menu.css('left', '-' + (that.width) + 'px');
            that.button.text(that.backText);
            if (that.icons) {
                that.button.removeClass('icon-menu-close');
                that.button.addClass('icon-menu-back');
            }
            that.state = 3;
            subnav.css({
                left: that.width + 'px'
            });
        });
    },

    slide: function() {

        var that = this;

        switch (this.state) {

            // menu button clicked
            case 1:
                that.hide.hide();
                that.menu.css('left', 0).fadeIn();
                that.button.text(that.closeText);
                if (that.icons) {
                    that.button.removeClass('icon-menu');
                    that.button.addClass('icon-menu-close');
                }
                that.state = 2;
                break;

            // close button clicked
            case 2:
                that.menu.css('left', this.width);
                that.state = 1;
                that.button.text(that.menuText);
                if (that.icons) {
                    that.button.removeClass('icon-menu-close');
                    that.button.addClass('icon-menu');
                }
                that.menu.hide();
                that.hide.fadeIn();
                break;

            // back button clicked
            case 3:
                that.menu.css('left', 0);
                that.button.text(that.closeText);
                if (that.icons) {
                    that.button.removeClass('icon-menu-back');
                    that.button.addClass('icon-menu-close');
                }
                that.state = 2;
                that.submenu.fadeOut(500);
                break;
        }
        return false;
    }

};
