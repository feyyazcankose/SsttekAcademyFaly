setTimeout(() => {
    const searchInput = $("<input>").attr({
        name: "searchInput",
        id: "searchInput",
        placeholder: "🔎 Search",
        type: "text",
        style: "width: calc(100% - 10px);padding: 5px;margin: 10px 5px;border: 1px solid #4e4e4e;border-radius: 5px;"
    });
    searchInput.insertBefore($("[id='sl-toc-/']").parent().parent());
    searchInput.on('click', function () {
        searchInput.focus();
    });

    searchInput.on('keyup', function () {
        const search = searchInput.val().toLowerCase();
        console.log(search);
        $("[id='sl-toc-/']").parent().parent().children().each(function () {
            const text = $(this).text().toLowerCase();
            if (text.includes(search)) {
                $(this).show();
            } else {
                $(this).hide();
            }
            $(this).children().each(function () {
                const text = $(this).text().toLowerCase();
                if (text.includes(search)) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
                $(this).children().each(function () {
                    const text = $(this).text().toLowerCase();
                    if (text.includes(search)) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }
                });
            });
        });
    });
}, 100);