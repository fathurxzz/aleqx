var Shop =
{
    addToCart: function (id) {
        $.get("/Cart/Add/" + id, function (data) {
            $("#cart-items-count").html(data);
            $("#product-add-to-cart").addClass("hidden");
            $("#product-in-cart-text").removeClass("hidden");
            //alert("Товар добавлен в корзину");
        });
    }
};

