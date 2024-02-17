
export function openDiagram(modeler, diagramXml) {
  fetch(diagramXml)
    .then(response => response.text())
    .then(diagram => {
      modeler.importXML(diagram.toString(), error => {
        if (error) {
          console.error('Could not import BPMN diagram', error);
        } else {
          modeler.get('canvas').zoom('fit-viewport');
          var transactionBoundaries = modeler.get('transactionBoundaries');
          transactionBoundaries.show();
        }
      });
    })
    .catch(err => console.error('Error loading BPMN diagram', err));
}

export function resetDiagramToBlank(modeler) {
  openDiagram(modeler, './InitDiagram.bpmn');
}

export function saveDiagram(modeler) {
  modeler.saveXML({ format: true }, function (err, xml) {
    if (err) {
      console.log('Error saving XML', err);
    } else {
      const blob = new Blob([xml], { type: 'application/bpmn20-xml;charset=utf-8' });
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      link.download = 'diagram.bpmn';
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
      window.URL.revokeObjectURL(url);
    }
  })
}

export function SaveSvg(modeler) {
  modeler.saveSVG({ format: true }, function (err, svg) {
    if (err) {
      console.error('Error saving SVG: ', err);
    } else {
      const svgBlob = new Blob([svg], { type: 'image/svg+xml;charset=utf-8' });
      const svgUrl = URL.createObjectURL(svgBlob);
      const downloadLink = document.createElement('a');
      downloadLink.href = svgUrl;
      downloadLink.download = 'diagram.svg';
      document.body.appendChild(downloadLink);
      downloadLink.click();
      document.body.removeChild(downloadLink);
    }
  });
}

