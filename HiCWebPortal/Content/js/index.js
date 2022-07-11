$(function () {
  var Validator = new KitFormValidator();
  var audioWrapper = $('.audio-wrapper');

  init();

  function init(){
    $('#btn-recipient').click(recipientClickHandler);
    $('#btn-submit').click(submitClickHandler);
  }

  function recipientClickHandler(evt){
    evt.preventDefault();
    $(".recipient-step1").addClass("d-none");

    $(".recipient-step2").removeClass("d-none");
    $(".recipient-step2").hide();
    $(".recipient-step2").fadeIn();
  }

  function submitClickHandler(evt){
    evt.preventDefault();
    var passcode = $('#passcode').val();

    if (Validator.isEmpty(passcode)) {
      $('#passcode').addClass('is-error');
      $('.error-hint').removeClass('d-none');
    } else {
      var endpoint = '/api/message/' + passcode;
      AjaxManager.fire(endpoint, {}, function (res) {
        if (res.Data == null) {
          $('#passcode').addClass('is-error');
          $('.error-hint').removeClass('d-none');
        } else {
          $('html,body').animate({scrollTop:0},0);
          $('body').addClass('sent');
          $('.bubble-middle .form').html(res.Data.Msg.Sender);
          $('.bubble-middle .to').html(res.Data.Msg.Receiver);
          $('header').removeClass('d-none').addClass('d-flex');
          $('.bubble-wrapper').removeClass('d-none');
          $('.index-page').addClass('d-none');
          $('.sent-page').removeClass('d-none');

          res.Data.Msg.MsgId = $.trim(res.Data.Msg.MsgId);

          data.forEach(function (media) {
            if (res.Data.Msg.MsgId == media.id) {
              if (media.type == 'audio') {
                audioWrapper.find('audio').prop('src', media.src);
                audioWrapper.find('.audio-desc').html(media.text);
                audioWrapper.find('.audio-author').text(media.author);
              } else if (media.type == 'text') {
                audioWrapper.find('.audio-outer').remove();
                audioWrapper.find('.audio-desc').html(media.text);
                audioWrapper.find('.audio-desc').addClass('pt-4');
                audioWrapper.find('.audio-author').remove();
              }
            }
          });;
        }
      }, 180000, "GET");
    }
  }

});
