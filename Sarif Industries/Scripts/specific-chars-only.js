$('.alphaonly').bind('keyup blur', function () {
    var node = $(this);
    node.val(node.val().replace(/[^a-z ]/g, ''));
}
);

$('.numbersonly').bind('keyup blur', function () {
    var node = $(this);
    node.val(node.val().replace(/[^0-9 ]/g, ''));
}
);
