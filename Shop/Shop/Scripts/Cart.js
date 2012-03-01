var Shop =
{
    addToCart: function (id) {
        $.get("/Cart/Add/" + id, function (data) {
            $("#smallShoppingCartItemsCount").html(data);
            alert("Продукт добавлен");
        });

    }
};

