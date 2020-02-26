function inputSearch(name, url, backName) {
    $("#" + name).autocomplete(url, {
        minChars: 0,
        max: 10,
        width: 200,
        autoFill: false,
        scroll: false,
        scrollHeight: 500,
        parse: function (data) {
            return $.map(eval(data), function (row) {
                return {
                    data: row,
                    value: row.name,
                    result: row.name
                }
            });
        },
        formatItem: function (data, i, total) {
            return data.name
        },
        formatMatch: function (data, i, total) {
            return data.name;
        },
        formatResult: function (data, value) {
            return data.name;
        }
    }).result(function (event, data, formatted) {
        if (backName != "") {
            $("#" + backName).val(data.to);
        }
    });
}