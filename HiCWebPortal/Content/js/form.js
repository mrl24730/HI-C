$(function () {

  var Helper = new KitUIHelper();
  var Validator = new KitFormValidator();

  var msgid = Helper.getQueryString().id || null;
  var isRandom = Helper.getQueryString().random || false;
  var audioWrapper = $('.audio-wrapper');
  var $msgform = $('#msgform');

  var isPassCheck = false;
  var isConfirmed = false;
  var isRetry = false;

  var rcmdLabel = $('[name="recommendation"] ~ label');
  var rcmd = $('[name="recommendation"]');
  var passcode = $('[name="passcode"]');

  rcmd.on('change', function() {
    passcode.val($(this).siblings().text().trim());
  });

  passcode.keyup(function() {
    rcmd.prop('checked', false);
  });

  $('#refresh').click(function() {
    window.location.href = "/home/form?random=true&id=" + data[Math.floor(Math.random() * data.length)].id;
  });

  $("#sender").val($.cookie("sender"));
  $("#receiver").val($.cookie("receiver"));
  $("#passcode").val($.cookie("passcode"));

  data.forEach(function (media) {
    if (msgid == media.id) {
      if (media.type == 'audio') {
        audioWrapper.find('audio').prop('src', media.src);
        audioWrapper.find('.audio-desc').html(media.text);
        audioWrapper.find('.audio-author span').text(media.author);
        if (isRandom) {
          audioWrapper
            .find('.audio-author')
            .removeClass('justify-content-start')
            .addClass('justify-content-between');
          audioWrapper.find('.audio-author img').removeClass('d-none');
        }
      } else if (media.type == 'text') {
        audioWrapper.find('.audio-outer').remove();
        audioWrapper.find('.audio-desc').html(media.text);
        audioWrapper.find('.audio-desc').addClass('pt-4');
        audioWrapper.find('.audio-author span').text("");
        if (isRandom) {
          audioWrapper
            .find('.audio-author')
            .removeClass('justify-content-start')
            .addClass('justify-content-between');
          audioWrapper.find('.audio-author img').removeClass('d-none');
        }
      }
    }
  });

  $('#submit').on('click', function() {
    $msgform.submit();
  });

  $('#confirm').on('click', function() {
    var isError = false;
    /*
    
    var yy = $("#yy").val();
    var mm = $("#mm").val();
    var dd = $("#dd").val();

    var dob = moment({year:yy, month:(mm-1), day:dd});
    if(dob._isValid){
      $(".error-hint.dob").addClass("d-none");
      $("#year").val(yy);
      $("#month").val(mm);
      $("#day").val(dd);
    }else{
      $(".error-hint.dob").removeClass("d-none");
      $("#year").val();
      $("#month").val();
      $("#day").val();
      isError = true;
    }
    */

    if (!$('#agree').is(":checked")) {
      isError = true;
      $('#agree').addClass('is-invalid');
    }else{
      $('#agree').removeClass('is-invalid');
    }

    if(!isError){
      $msgform.submit();
    }
  })

  $('#retry').on('click', function () {
    $msgform.submit();
  })

  $msgform.on('submit', function (e) {
    e.preventDefault();
    $('.error-hint').addClass('d-none');

    var hasError = false;
    var req = e.target.elements;

    var sender = req.sender;
    var receiver = req.receiver;
    var passcode = req.passcode;

    var postData = {
      msgid: msgid,
      passcode: passcode.value,
      sender: sender.value,
      receiver: receiver.value,
      year: req.year.value,
      month: req.month.value,
      day: req.day.value,
    };
    
    for (var i = 0; i < filterList.length; i++) {
      if (passcode.value.toLowerCase().indexOf(filterList[i]) > -1) {
        $('.passcode-3').removeClass('d-none');
        $('.passcode-3 .word').text(filterList[i]);
        passcode.focus();
        hasError = true;
        break;
      }
      if (receiver.value.toLowerCase().indexOf(filterList[i]) > -1) {
        $('.receiver-2').removeClass('d-none');
        $('.receiver-2 .word').text(filterList[i]);
        receiver.focus();
        hasError = true;
        break;
      }
      if (sender.value.toLowerCase().indexOf(filterList[i]) > -1) {
        $('.sender-2').removeClass('d-none');
        $('.sender-2 .word').text(filterList[i]);
        sender.focus();
        hasError = true;
        break;
      }
    }

    if (postData.msgid == null) {
      window.location.href = "/home/pick";
    }

    if (Validator.isEmpty(postData.sender)) {
      sender.classList.add('is-error');
      sender.focus();
      $('.error-hint.sender').removeClass('d-none');
      hasError = true;
    } else {
      sender.classList.remove('is-error');
      $('.error-hint.sender').addClass('d-none');
      $.cookie("sender", postData.sender);
    }

    if (Validator.isEmpty(postData.receiver)) {
      receiver.classList.add('is-error');
      receiver.focus();
      $('.error-hint.receiver').removeClass('d-none');
      hasError = true;
    } else {
      receiver.classList.remove('is-error');
      $('.error-hint.receiver').addClass('d-none');
      $.cookie("receiver", postData.receiver);
    }

    if (Validator.isEmpty(postData.passcode) && isRetry == false) {
      passcode.classList.add('is-error');
      passcode.focus();
      $('.error-hint.passcode').removeClass('d-none');
      hasError = true;
    } else {
      passcode.classList.remove('is-error');
      $('.error-hint.passcode').addClass('d-none');
      isPassCheck = true;
      $.cookie("passcode", postData.passcode);
    }

    if (isPassCheck && !(/^[a-zA-Z0-9]{3,20}$/.test(postData.passcode))) {
      passcode.classList.add('is-error');
      passcode.focus();
      $('.error-hint.custom').removeClass('d-none');
      hasError = true;
    } else {
      passcode.classList.remove('is-error');
      $('.error-hint.custom').addClass('d-none');
    }

    if (hasError === false && isConfirmed === true) {

      AjaxManager.fire('/api/message', postData, function (res) {
        if (res.Code == 0) {
          $('.ready-page').removeClass('d-none');
          $('.pass-wrapper .from').text(postData.sender);
          $('.pass-wrapper .to').text(postData.receiver);
          $('.pass-wrapper .passcode').text(postData.passcode);
          $('.form-page').addClass('d-none');
          $(".header-step").removeClass("active");
          $(".header-step.step4").addClass("active");
          $(".ani-passcode").text($.cookie("passcode"));
          setTimeout(function(){
            $(".ani-passcode").removeClass("infinite ");
          },100);

          $.removeCookie("passcode");
        } else if (res.Code == -98) {
          $.each(rcmdLabel, function () {
            $(this).text(Math.random().toString(36).substring(5));
          });

          isRetry = true;
          passcode.disabled = false;
          passcode.classList.add('is-error');
          passcode.focus();

          $('.rcmd-wrapper').removeClass('d-none');
          $('.error-hint.passcode-2').removeClass('d-none');
          $('.confirm-wrapper').addClass('d-none');
        } else if (res.Code == -99) {
          alert('Something went wrong, please try it later');
        } else if (res.Code == -97){
          alert('對不起, 你未符合今次活動參加資格。');
        }else {
          alert('Connection error, please try it later');
        }
      });
      isPassCheck = false;
    }

    if (!hasError && !isRetry) {
      isConfirmed = true;
      $('.action-container').addClass('d-none');
      $('.confirm-wrapper').removeClass('d-none');
      sender.disabled = true;
      receiver.disabled = true;
      passcode.disabled = true;
      $(".header-step").removeClass("active");
      $(".header-step.step3").addClass("active");

    }

  });
});
