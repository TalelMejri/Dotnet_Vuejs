// function convertToXml(modelData) {
//   let xml = `<?xml version="1.0" encoding="UTF-8"?>\n`;
//   xml += `<definitions id="${modelData.$.id}" xmlns="${modelData.$['xmlns']}" xmlns:xsi="${modelData.$['xmlns:xsi']}" xmlns:bpmn="${modelData.$['xmlns:bpmn']}" xmlns:bpmndi="${modelData.$['xmlns:bpmndi']}" xmlns:omgdc="${modelData.$['xmlns:omgdc']}" xmlns:omgdi="${modelData.$['xmlns:omgdi']}" xmlns:camunda="${modelData.$['xmlns:camunda']}" targetNamespace="${modelData.$.targetNamespace}">\n`;

//   // Exporter information
//   xml += `<exporter>${modelData.$.exporter}</exporter>\n`;
//   xml += `<exporterVersion>${modelData.$.exporterVersion}</exporterVersion>\n`;

//   // BPMNDiagram
//   xml += `<bpmndi:BPMNDiagram id="${modelData.bpmndi.BPMNDiagram[0].$.id}">\n`;
//   // You would need to iterate through BPMNPlane and BPMNShape here
//   xml += `</bpmndi:BPMNDiagram>\n`;

//   // Process
//   modelData.process.forEach(process => {
//     xml += `<bpmn:process id="${process.$.id}" isExecutable="${process.$.isExecutable}">\n`;
//     // You would need to iterate through each element inside the process here
//     xml += `</bpmn:process>\n`;
//   });

//   xml += `</definitions>`;

//   return xml;
// }

const test=`
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

 export  function openDiagram(modeler, diagramXml)  {
      modeler.importXML(diagramXml==null ? test : diagramXml, error => {
        if (error) {
          console.error('Could not import BPMN diagram', error);
        } else {
          modeler.get('canvas').zoom('fit-viewport');
          var transactionBoundaries = modeler.get('transactionBoundaries');
          transactionBoundaries.show();
        }
      });
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

export function saveDiagramToLocal(DiagXml) {
  window.localStorage.setItem('savedDiagram', DiagXml);
}
