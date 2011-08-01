$(function () {
    var jqxhr = $.ajax({ url: "/LivingHelp/Index" })
    .success(function (data) {
        
        processLh(data);
    });
});

function processLh(data) {
    $(data).insertAfter($("footer"));

    $('#lh').find('div.lh-scope').each(function () {
        var popupDiv = $(this);
        var scopeType = this.attributes['data-lh-scope-type'].nodeValue;
        var scopeId = this.attributes['data-lh-scope-id'].nodeValue;

        var icon = null;

        switch (scopeType) {
            case 'FormSubmit':
                var form = $('#' + scopeId);
                var submit = form.find('input[type="submit"]');

                var icon = $('<img class="lh-help-icon" src="/Content/info_button.png" />');
                icon.insertAfter(submit);

                break;
            case 'Click':
                var button = $('#' + scopeId);

                var icon = $('<img class="lh-help-icon" src="/Content/info_button.png" />');
                icon.insertAfter(button);

                break;
            case 'Navigate':
                var header = $('header');

                var icon = $('<img class="lh-help-icon" src="/Content/info_button.png" />');
                header.append(icon);

                break;
        }

        if (icon != null) {
            icon.mouseover(function (event) {
                popupDiv.fadeIn();
                positionTooltip(event, popupDiv);
            });

            icon.mouseout(function () {
                popupDiv.fadeOut();
            })

            popupDiv.corner();
        }
    });

}

function positionTooltip(event, popup) {
    var winHeight = $(window).height();
    var popupHeight = popup.height();
    var tPosX = event.pageX + 10;
    var tPosY = event.pageY - (popupHeight / 3);
    if (tPosY < 0)
        tPosY = 0;
    else if (tPosY + popupHeight > winHeight) {
        tPosY = winHeight - popupHeight - 40;
    }
    popup.css({ top: tPosY, left: tPosX });
};
