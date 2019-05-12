$(document).ready(function () {

    var cartTotal = $('#grand-total').val();


    if (cartTotal > 0) {
        $('#shoppingcartitems').show();
        $('#checkoutbtn').show();
        $('#carttotal').show();

        $('#shoppingcartempty').hide();
    }
    else {
        $('#shoppingcartitems').hide();
        $('#checkoutbtn').hide();
        $('#carttotal').hide();

        $('#shoppingcartempty').show();
    }

});