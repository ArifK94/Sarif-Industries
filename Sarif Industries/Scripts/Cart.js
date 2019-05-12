var AddItem = function (ProductId) {

    var e = document.getElementById("colour-selection");
    var selectedColour = e.options[e.selectedIndex].text;

    $.ajax({
        type: "POST",
        url: "/Carts/AddToCart",
        data: {

            ProductId: ProductId,
            Colour: selectedColour,
            Quantity: $("#quantity").val(),
            Price: $("#price").val()
        },
        success: function (result) {

            if (result) {

                //$("#header-cart").load(location.href + " #header-cart>*", "");

                location.reload();

            }
        }
    });
}

var DeleteItem = function (ProductId) {

    $.ajax({
        type: "POST",
        url: "/Carts/DeleteConfirmed",
        data: { id: ProductId },
        success: function (result) {

            if (result) {
                //swal("Item removed from cart!", "success");

                //$("#header-cart").load(location.href + " #header-cart>*", "");

                location.reload();

            }
        }
    });
}