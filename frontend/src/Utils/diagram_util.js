
const InitialDiagram = `
<?xml version="1.0" encoding="UTF-8"?>
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL" xmlns:bpmndi="http://www.omg.org/spec/BPMN/20100524/DI" xmlns:omgdc="http://www.omg.org/spec/DD/20100524/DC" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" id="sid-38422fae-e03e-43a3-bef4-bd33b32041b2" targetNamespace="http://bpmn.io/bpmn" exporter="bpmn-js (https://demo.bpmn.io)" exporterVersion="16.4.0">
  <process id="Process_0" isExecutable="false">
    <startEvent id="StartEvent_1y45yut" name="Start" />
  </process>
  <bpmndi:BPMNDiagram id="BpmnDiagram_1">
    <bpmndi:BPMNPlane id="BpmnPlane_1" bpmnElement="Process_0">
      <bpmndi:BPMNShape id="StartEvent_1y45yut_di" bpmnElement="StartEvent_1y45yut">
        <omgdc:Bounds x="152" y="102" width="36" height="36" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x="159" y="145" width="25" height="14" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
    </bpmndi:BPMNPlane>
  </bpmndi:BPMNDiagram>
</definitions>
`

export function openDiagram(modeler, diagramXml) {
  modeler.importXML(diagramXml, error => {
    if (error) {
      console.error('Could not import BPMN diagram', error);
    } else {
      modeler.get('canvas').zoom('fit-viewport');
      var transactionBoundaries = modeler.get('transactionBoundaries');
      transactionBoundaries.show();
    }
  });
}

export function openLocalDiagram(modeler) {
  let localDiagram = localStorage.getItem("savedDiagram");
  let diagram = localDiagram ? localDiagram : InitialDiagram;
  return openDiagram(modeler, diagram);
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

export function saveDiagramToLocal(modeler) {
  modeler.saveXML({ format: true }, function (err, xml) {
    localStorage.setItem("savedDiagram", xml);
  });
}

export function ResetDiagramLocal(){
  localStorage.removeItem("savedDiagram");
}

