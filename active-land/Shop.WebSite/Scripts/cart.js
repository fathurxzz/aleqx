﻿var Shop =
{
    addToCart: function (id) {
        $.get("/Cart/Add/" + id, function (data) {
            $("#cart-items-count").html(data);
            $("#product-add-to-cart").addClass("hidden");
            $("#product-in-cart-text").removeClass("hidden");
            $("#cart-items-count").addClass("show");

            //alert("Товар добавлен в корзину");
        });
    },

    updateCart: function (id,quantity) {
        //alert(id+" "+quantity);
        $.get("/Cart/Update/?id=" + id + "&quantity=" + quantity);
    }
};



$(function() {
    $(".arrow-inc").click(function () {

        var wrapper = $(this).closest(".cart-item")[0];
        var priceByItem = $(wrapper).find(".hidden-price")[0].innerHTML.trim();

        var quantityControl = $(wrapper).find(".quantity-amount");
        var quantity = quantityControl[0].innerHTML.trim();
        var newQuantity = Number(quantity) + 1;
        quantityControl[0].innerHTML = newQuantity;

        var priceTotal = $(wrapper).find(".price")[0];
        var totalCostByPosition = priceByItem * newQuantity;
        
        var totalByItemControl = $(wrapper).find(".hidden-total-by-item-price");
        totalByItemControl[0].innerHTML = totalCostByPosition;

        priceTotal.innerHTML = totalCostByPosition.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ") + " грн";

        var totalCost = 0;

        var orderItemId = $(wrapper).find(".hidden-order-item-id")[0].innerHTML.trim();
        Shop.updateCart(orderItemId, newQuantity);

        $(".hidden-total-by-item-price").each(function () {
            totalCost += Number(this.innerHTML.trim());
        });



        $("#totalPrice")[0].innerHTML = totalCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");

    });

    $(".arrow-dec").click(function () {
        var wrapper = $(this).closest(".cart-item")[0];
        var priceByItem = $(wrapper).find(".hidden-price")[0].innerHTML.trim();

        var quantityControl = $(wrapper).find(".quantity-amount");
        var quantity = quantityControl[0].innerHTML.trim();
        var newQuantity = Number(quantity) - 1;
        

        if (newQuantity > 0) {
            quantityControl[0].innerHTML = newQuantity;
            var priceTotal = $(wrapper).find(".price")[0];
            var totalCostByPosition = priceByItem * newQuantity;
            var totalByItemControl = $(wrapper).find(".hidden-total-by-item-price");
            totalByItemControl[0].innerHTML = totalCostByPosition;

            priceTotal.innerHTML = totalCostByPosition.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ") + " грн";

        }

        var totalCost = 0;



        $(".hidden-total-by-item-price").each(function () {
            var orderItemId = $(wrapper).find(".hidden-order-item-id")[0].innerHTML.trim();
            Shop.updateCart(orderItemId, newQuantity);
            totalCost += Number(this.innerHTML.trim());
            $("#totalPrice")[0].innerHTML = totalCost.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
        });

        
    });


})