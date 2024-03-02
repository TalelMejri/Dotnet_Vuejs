
import {
  getComments,
} from './util';


export default function Comments(eventBus) {
  function renderComments(element) {
      var comments = getComments(element);
  }
  eventBus.on('shape.added', function(event) {
    var element = event.element;
    defer(function() {
      renderComments(element);
    });

  });
}

function defer(fn) {
  setTimeout(fn, 0);
}

