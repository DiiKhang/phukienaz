$(function () {
    $('body').on('click', '.add-to-wishlist', function (e) {
        var btn = ($(this));
        var status = btn.find('span');
        var id = $(this).parents('.product-body').attr('id');
        $.ajax({
            type: "POST",
            url: "/Customer/Product/Like",
            data: { productId: id, like: (status.text() == 'Yêu thích') },
            success: function (data) {
                if (data.endsWith('!')) {
                    alert(data);
                } else {
                    var result = data.split('.');
                    btn.parent().siblings('.product-rating').find('b').html(result[0]);
                    if (result[1] == 'True') {
                        alert('Đã thích');
                        status.html('Bỏ thích');
                        btn.find('i').attr('class', 'fa fa-heart');
                    } else {
                        alert('Đã bỏ thích');
                        status.html('Yêu thích');
                        btn.find('i').attr('class', 'fa fa-heart-o');
                    }
                }
            }
        });
    });
})

$(function () {
    $('body').on('click', '.add-to-cart-btn', function () {
        if ($(this).attr('name') == null) {
            addToCart($(this).val(), 1);
        } else {
            addToCart($(this).val(), $('#quantity').val());
        }
    });
});

function addToCart(productId, quantity) {
    $.post({
        url: "/Customer/Cart/UseCart",
        data: {
            productId: productId,
            quantity: quantity,
            isAdd: true
        },
        success: function (data) {
            if (data.endsWith('!')) {
                alert(data);
            } else {
                alert("Thêm vào giỏ hàng thành công");
                $('#total-cart-item').html(data);
            }
        }
    });
}