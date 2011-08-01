function flash(selector) {

    $(selector).stop()
        .css('opacity', 0)
            .animate({ backgroundColor: "khaki", opacity: 1 }, 300)
            .animate({ backgroundColor: '#ffffff' }, 200);
}

function resetForm() {

    clear_form_elements($('form'));
    clearValidationSummary();
}

function clearValidationSummary() {

    var container = $('form').find('[data-valmsg-summary="true"]');
    var list = container.find('ul');

    if (list && list.length) {
        list.empty();
        container.addClass('validation-summary-valid').removeClass('validation-summary-errors');
    }
}


function clear_form_elements(ele) {

    $(ele).find(':input').each(function () {
        switch (this.type) {
            case 'password':
            case 'select-multiple':
            case 'select-one':
            case 'text':
            case 'textarea':
                $(this).val('');
                break;
            case 'checkbox':
            case 'radio':
                this.checked = false;
        }
    });

}