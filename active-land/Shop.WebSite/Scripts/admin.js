$(function() {
    $("#categoryId").change(function () {
        var id = $(this).val();
        var select = document.getElementById('productsId');

        function removeOptions(selectbox) {
            var i;
            for (i = selectbox.options.length - 1; i >= 0; i--) {
                selectbox.remove(i);
            }
        }
        removeOptions(select);
        var opt = document.createElement('option');
        opt.value = '';
        opt.innerHTML = 'Загрузка товаров...';
        select.appendChild(opt);

        //var length = select.options.length;
        //for (var i = 0; i < length; i++) {
        //    select.options[i] = null;
        //}

        $.get("/Home/GetProducts/" + id, function (data) {
            removeOptions(select);
            var obj = JSON.parse(data);
            obj.forEach(function (entry) {
                var opt = document.createElement('option');
                opt.value = entry.id;
                opt.innerHTML = entry.title;
                select.appendChild(opt);
            });
        });
    });
});