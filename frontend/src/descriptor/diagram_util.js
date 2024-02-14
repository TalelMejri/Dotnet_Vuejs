import { decompress, compress } from "lz-string";
import blankDiagram from "../../public/diagram.bpmn";

export function openDiagram(modeler, diagramXml) {
  console.log(blankDiagram);
  modeler.importXML(blankDiagram, function(err) {
    if (!err) {
      console.log("success!");
      modeler.get("canvas").zoom("fit-viewport");
    } else {
      console.log("something went wrong:", err);
    }
  });
  console.log(modeler.get("canvas"));
  modeler.get("canvas").zoom("fit-viewport");
}

export function openLocalDiagram(modeler) {
  /*let localDiagram = sessionStorage.getItem("diagram");
  let diagram = localDiagram ? decompress(localDiagram) : blankDiagram;*/
  
  return openDiagram(modeler, blankDiagram);
}

export function resetDiagramToBlank(modeler) {
  return openDiagram(modeler, blankDiagram);
}

export function saveDiagram(modeler) {
  return new Promise(resolve => {
    modeler.saveXML({ format: true }, function(err, xml) {
      sessionStorage.setItem("diagram", compress(xml));
      resolve(getDownloadUrl(xml));
    });
  });
}

export function getDownloadUrl(xml) {
  return `data:application/bpmn20-xml;charset=UTF-8,${encodeURIComponent(xml)}`;
}

export function getBlankDiagramDownloadUrl() {
  return getDownloadUrl(blankDiagram);
}

export function parseBPMNJson({ rootElements }) {
  const steps = rootElements
    .filter(el => el.$type === "bpmn:Process")
    .reduce((acc, curr) => curr.flowElements);
}
