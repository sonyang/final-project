/// <reference path="http://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js" />
$(document).ready(function () {

  $('#LoginButton').on('click', function () {
    var btn = $(this);
    var spin = $(this).siblings('i');
    var err = $(this).siblings('span');

    var url = $(this).closest('form').attr('action');
    var data = $(this).closest('form').serialize();

    btn.hide();
    err.hide();
    spin.show();
  
    var post = $.post(url, data);

    post.done(function () {
      window.location.reload(true);
    });

    post.fail(function () {
      spin.hide();
      btn.show();
      err.show();
    });

    return false;
  });
});