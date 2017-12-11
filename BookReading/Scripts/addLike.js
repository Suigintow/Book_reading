$('.like-btn').click(function () {
    var reviewId = $(this).data('review');
    var $this = $(this).children('span');
    $.post({
        'url': '/Review/IncrementAndGetLikes',
        'data': { reviewId: reviewId },
        'success': function (Like) {
            $this.text('Like: ' + Like);
        },
        'error': function (_, error) {
            alert('error: ' + error)
        }
    });
});