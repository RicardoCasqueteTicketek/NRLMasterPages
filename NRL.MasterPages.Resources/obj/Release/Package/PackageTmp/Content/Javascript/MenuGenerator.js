$(document).ready(function () {

    function addMenuItems(menu, items) {
        console.log('start MenuRender');
        $(items).each(function () {
            console.log('MenuItem');
            console.log(this);

            var li = $('<li/>')
                .appendTo(menu);

            var anchor = $('<a/>')
                .appendTo(li)
                .attr('href', $(this).attr('link'));

            var em = $('<em/>')
                .addClass('nrl-menu-item')
                .appendTo(anchor)
                .text($(this).attr('label'));
        });
    };

    // handle nav
    var clubMenu = $('.clubMenu');
    addMenuItems(clubMenu, menuItems.clubMenu.Links);

    $('#nrl-logo a').attr('href', menuItems.LogoLink);
    $('#nrl-sponsor a').attr('href', menuItems.SponsorLink);
    $('#nrl-AdLeaderboard a').attr('href', menuItems.AdLink);
    $('.home a').attr('href', menuItems.HomeLink);
});
