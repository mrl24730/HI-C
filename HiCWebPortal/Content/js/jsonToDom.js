$(function() {
  var _audioDom = $('#media-content-audio').clone();
  $('#media-content-audio').remove();
  _audioDom.removeAttr('id').removeClass('d-none');
  var _textDom = $('#media-content-text').clone();
  $('#media-content-text').remove();
  _textDom.removeAttr('id').removeClass('d-none');
  var divider = $('#divider');

  data.forEach(function (media) {
    if (media.type == 'audio') {
      audioDom = _audioDom.clone();
      audioDom.addClass(media.cat)
      audioDom.find('audio').prop('src', media.src);
      audioDom.find('.audio-desc').html(media.text);
      audioDom.find('.audio-author').text(media.author);
      audioDom.find('a').prop('href', "/home/form?id=" + media.id);
      audioDom.insertAfter(divider);
    } else if (media.type == 'text') {
      textDom = _textDom.clone();
      textDom.addClass(media.cat)
      textDom.find('.text-desc').html(media.text);
      textDom.find('a').prop('href', "/home/form?id=" + media.id);
      textDom.insertAfter(divider);
    }
  });
  
});
