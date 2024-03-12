$(function () {
    DisabledItems();
    ReadonlyItems();

    $('body').on('click', '.js-search-clear', function () {
        ClearColumn($(this), true);
    });

    $('body').on('click', '.js-search-clear-no-submit', function () {
        ClearColumn($(this));
    });

    $('body').on('click', 'button.js-delete-confirm', function () {
        if (!confirm('刪除後無法復原，確定要刪除嗎?')) {
            return false;
        }
    })

    if ($("input[type='file']")) {
        $("input[type='file']:first").closest("form").attr("enctype", "multipart/form-data");
    }
});

function ClearColumn(_this, isSubmit) {
    var row = _this.closest(".row");
    row.find("input[type='text']").val("");
    row.find("input[type='date']").val("");
    row.find("input[type='checkbox']").prop("checked", false);
    row.find("select", row).val("");
    if (isSubmit) row.closest("form").submit();
}

function DisabledItems() {
    $(".js-disabled").each(function () {
        $("input[type='text']", $(this)).prop('disabled', true)
        $("input[type='password']", $(this)).prop('disabled', true)
        $("input[type='number']", $(this)).prop('disabled', true)
        $("input[type='email']", $(this)).prop('disabled', true)
        $("input[type='checkbox']", $(this)).prop('disabled', true)
        $("input[type='radio']", $(this)).prop('disabled', true)
        $("input[type='date']", $(this)).prop('disabled', 'disabled')
        $("input[type='datetime']", $(this)).prop('disabled', 'disabled')
        $("input[type='datetime-local']", $(this)).prop('disabled', 'disabled')
        $("select", $(this)).prop('disabled', true)
        $("select[name='PageSize']", $(this)).removeAttr('disabled')
        $("textarea", $(this)).prop('disabled', true)
        $("p", $(this)).prop('disabled', true)
    });
}
function ReadonlyItems() {
    $(".js-readonly").each(function () {
        $("input[type='text']", $(this)).prop('readonly', 'readonly')
        $("input[type='password']", $(this)).prop('readonly', 'readonly')
        $("input[type='number']", $(this)).prop('readonly', 'readonly')
        $("input[type='email']", $(this)).prop('readonly', 'readonly')
        $("input[type='checkbox']", $(this)).prop('readonly', 'readonly')
        $("input[type='radio']", $(this)).prop('readonly', 'readonly')
        $("input[type='date']", $(this)).prop('readonly', 'readonly')
        $("input[type='datetime']", $(this)).prop('readonly', 'readonly')
        $("input[type='datetime-local']", $(this)).prop('disabled', 'disabled')
        $("select", $(this)).attr('disabled', 'disabled')
        $("select[name='PageSize']", $(this)).removeAttr('disabled')
        $("textarea", $(this)).prop('readonly', 'readonly')
        $("p", $(this)).prop('readonly', 'readonly')
    });

    $("input[type='checkbox'][readonly]").click(function () {
        return false;
    });
    $("input[type='radio'][readonly]").click(function () {
        return false;
    });
    $("select[readonly='readonly']").click(function () {
        $(this).closest("div").focus();
    });
}