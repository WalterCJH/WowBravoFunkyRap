$(function () {
    $('body').on('click', ".js-all-check", function () {
        var _this = $(this);
        var checkVal = _this.prop("checked");
        _this.closest(".js-div-check").find(".js-check").prop("checked", checkVal);
    });
});