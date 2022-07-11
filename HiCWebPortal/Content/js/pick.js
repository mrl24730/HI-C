$(function() {

  var classifyCat = $('#classify');
  var classifyType = $('form input[type="checkbox"]');

  init();

  function init(){
    
    $("audio").on("play", function () {
      $("audio").not(this).each(function (index, audio) {
        audio.pause();
      });
    });

    classifyType.click(function () {
      filterLogic();
    });

    classifyCat.on('change', function () {
      if (classifyCat.val() == 'random') {
        window.location.href = "/home/form?random=true&id=" + data[Math.floor(Math.random() * data.length)].id;
      }
      filterLogic();
    });

    filterLogic();
  }

  function filterLogic() {

    if (classifyCat.val() == 'all') {
      var textContent = $('.media-content.text-wrapper');
      var mediaContent = $('.media-content.audio-wrapper');
    } else {
      $('.media-content:not(.' + classifyCat.val() + ')').addClass("d-none");
      var textContent = $('.media-content.text-wrapper.' + classifyCat.val());
      var mediaContent = $('.media-content.audio-wrapper.' + classifyCat.val());
    }

    var checked = [];
    var unchecked = [];
    
    classifyType.each(function (e) {
      if ($(this).is(":checked")) {
        checked.push($(this).val());
      } else {
        unchecked.push($(this).val());
      }
    });

    for (var i = 0; i < checked.length; i++) {
      if (checked[i] == "audio") {
        textContent.addClass('d-none');
      } else if (checked[i] == "text") {
        mediaContent.addClass('d-none');
      }
    }

    for (var i = 0; i < unchecked.length; i++) {
      if (unchecked[i] == "audio") {
        textContent.removeClass('d-none');
      } else if (unchecked[i] == "text") {
        mediaContent.removeClass('d-none');
      }
    }

    if (unchecked.length == 0) {
      textContent.removeClass('d-none');
      mediaContent.removeClass('d-none');
    }
  }
})
