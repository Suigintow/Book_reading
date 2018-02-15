function myFunction(need) {
    var person = prompt("Please, incorrect comment", need);
    return person;
}
$('.edit-btn').click(function () {
    var reviewId = $(this).data('review');
    var $this = $(this).children('span');
    var need = $(this).children('span').html();
    var text = myFunction(need);
    $.post({
        'url': '/Review/EditText',
        'data': { reviewId: reviewId, newText: text},
        'success': function (Text) {
            $this.text(Text);
        },
        'error': function (_, error) {
            alert('error: ' + error)
        }
    });
});
