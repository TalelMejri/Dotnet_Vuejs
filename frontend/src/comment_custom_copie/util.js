import { toRaw } from "vue";

export function _getCommentsElement(element, create) {

  var bo = toRaw(element[3]);
  var docs = bo['documentation'];

  var comments;

  if(docs!=undefined){
    docs.some(function(d) {
      return d.textFormat === 'text/x-comments' && (comments = d);
    });
  }

  if (!comments && create) {
    comments = bo.$model.create('bpmn:Documentation', { textFormat: 'text/x-comments' });
    docs.push(comments);
  }

  return comments;
}

export function getComments(element) {
  var doc = _getCommentsElement(element);

  if (!doc || !doc.text) {
    return [];
  } else {
    return doc.text.split(/;\r?\n;/).map(function(str) {
      return str.split(/:/, 2);
    });
  }
}

export function setComments(element, comments) {
  var doc = _getCommentsElement(element, true);

  var str = comments.map(function(c) {
    return c.join(':');
  }).join(';\n;');

  doc.text = str;
}

export function addComment(element, author, str) {
  var comments = getComments(element);

  comments.push([ author, str ]);

  setComments(element, comments);
}

export function removeComment(element, comment) {
  var comments = getComments(element);

  var idx = -1;

  comments.some(function(c, i) {

    var matches = c[0] === comment[0] && c[1] === comment[1];

    if (matches) {
      idx = i;
    }

    return matches;
  });


  if (idx !== -1) {
    comments.splice(idx, 1);
  }
  
  setComments(element, comments);

}