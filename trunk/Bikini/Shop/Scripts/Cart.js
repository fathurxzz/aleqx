var Shop =
{
    addToCart: function (id) {
        $.get("/Cart/Add/" + id, function (data) {
            //$("#smallShoppingCartItemsCount").html(data);
            alert("Товар добавлен в корзину");
        });

    }
};

