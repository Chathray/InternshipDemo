// INITIALIZATION OF QUILLJS EDITOR
// =======================================================
var quill = $.HSCore.components.HSQuill.init('.js-quill');

// INITIALIZATION OF SELECT2
// =======================================================
$('.js-select2-custom').each(function () {
    $.HSCore.components.HSSelect2.init($(this));
});