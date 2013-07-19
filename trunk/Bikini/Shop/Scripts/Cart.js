var Shop =
{
    addToCart: function (id) {
        $.get("/Cart/Add/" + id, function (data) {
            $("#smallShoppingCartItemsCount").html(data);
            $(".emptyCart").css("display", "none");
            $(".smallShoppingCartItemsCount").css("display", "block");
            alert("Товар добавлен в корзину");
        });

    }
};

