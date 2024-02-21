<template>
  <div>
    <!-- <div class="titre"  data-aos="zoom-out" >
       <h2>Neoledge Bpmn</h2>
    </div> -->
    <div class="bpmn_content">
      <div class="img">
        <img src="../assets/images/neoledge.png">
      </div>
      <div class="flow-container">
        <div ref="content" class="containers">
          <div id="canvas" ref="canvas" class="canvas"></div>
          <!-- <div id="properties" :class="propertiesVisible ? 'properties-visible' : 'properties'"></div> -->
        </div>
      </div>
      <div class="button-container">
        <button type="button" @click="DownloadDiagramXml()">
          <i class="fa fa-download"></i>
        </button>
        <button type="button" @click="ResetDiagram()">
          <i class="fa fa-refresh"></i>
        </button>
        <button type="button" @click="DownloadDiagramSvg()">
          <i class="fa fa-file-image-o"></i>
        </button>
        <button type="button" @click="ShowComments()">
          <i :class="!showComments ? 'fa fa-comments' : 'fa fa-times-circle'"></i>
        </button>
        <button type="button" @click="ToggleProperties">
          Affiche Prop
        </button>
        <button type="button" @click="ImportDiagram">
          <i class="fa fa-upload"></i>
        </button>
        <input type="file" accept=".bpmn" @change="HandleFileImport" ref="fileInput" style="display: none" />
      </div>
    </div>
  </div>
</template>

<script>
import KeyboardModule from "bpmn-js/lib/features/keyboard/index.js";
import CopyPasteModule from 'bpmn-js/lib/features/copy-paste';
import { onMounted, ref, toRaw } from 'vue';
import propertiesPanelModule from 'bpmn-js-properties-panel';
import propertiesProviderModule from 'bpmn-js-properties-panel/lib/provider/camunda';
// import camundaModdleDescriptor from 'camunda-bpmn-moddle/resources/camunda';
import camundaModdleDescriptor from "../descriptor/camundaDescriptor.json"
import Modeler from "../Modeler/CustomBpmnModeler.js";
import gridModule from 'diagram-js-grid';
import CommentModule from "../comment_custom/index"
import TokenSimulationModule from 'bpmn-js-token-simulation';
import ColorsBpm from "../colors/index";
import transactionBoundariesModule from 'camunda-transaction-boundaries';
import { openDiagram, saveDiagram, SaveSvg, saveDiagramToLocal } from "../Utils/diagram_util.js";
import PriorityAwareModeler from "bpmn-js-task-priorities/lib/priorities"
import "bpmn-js-task-priorities/assets/priorities.css"
import KeyboardMoveModule from 'diagram-js/lib/navigation/keyboard-move';
import MoveCanvasModule from 'diagram-js/lib/navigation/movecanvas';
import ZoomScrollModule from 'diagram-js/lib/navigation/zoomscroll';
// import bpmnlintrc from '../../.bpmnlintrc';
// import lintModule from 'bpmn-js-bpmnlint';
// import 'bpmn-js-bpmnlint/dist/assets/css/bpmn-js-bpmnlint.css';
export default {
  setup() {

    const canvas = ref(null);
    const test = ref(null);
    const showComments = ref(false)
    const fileInput = ref(null);
    const propertiesVisible = ref(false);


    onMounted(() => {
      const modeler = new Modeler({
        container: canvas.value,
        // propertiesPanel: {
        //   parent: '#properties',
        // },
        // linting: {
        //   bpmnlint: bpmnlintrc
        // },
        keyboard: {
          bindTo: window
        },
        additionalModules: [
          // propertiesPanelModule,
          // propertiesProviderModule,
          ColorsBpm,
          gridModule,
          TokenSimulationModule,
          CommentModule,
          transactionBoundariesModule,
          PriorityAwareModeler,
          KeyboardModule,
          // lintModule
        ],
        moddleExtensions: {
          camunda: camundaModdleDescriptor,
        }
      });
      test.value = modeler;
      modeler.on('selection.changed', (e) => {
        console.log(e.newSelection[0]);
       //this.setElement(e.newSelection[0]);
      });
       modeler.on('element.changed', (e) => {
        console.log(e.element);
        //  this.setElement(e.element);
      });
      openDiagram(modeler);
    });


    const ResetDiagram = () => {
      test.value.destroy();
      const modeler = new Modeler({
        container: canvas.value,
        propertiesPanel: {
          parent: '#properties',
        },
        // linting: {
        //   bpmnlint: bpmnlintrc
        // },
        keyboard: {
          bindTo: window
        },
        additionalModules: [
          //propertiesPanelModule,
          //propertiesProviderModule,
          ColorsBpm,
          gridModule,
          TokenSimulationModule,
          CommentModule,
          transactionBoundariesModule,
          PriorityAwareModeler
          // lintModule
        ],
        moddleExtensions: {
          camunda: camundaModdleDescriptor,
        }
      });
      test.value = modeler;
      modeler.on('selection.changed', (e) => {
        console.log(e.newSelection[0]);
       //this.setElement(e.newSelection[0]);
      });
       modeler.on('element.changed', (e) => {
        console.log(e.element);
        //  this.setElement(e.element);
      });
      openDiagram(modeler);
    };

    const ImportDiagram = () => {
      fileInput.value.click();
    };

    const HandleFileImport = (event) => {
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = (e) => {
        test.value.destroy();
        const modeler = new Modeler({
          container: canvas.value,
          propertiesPanel: {
            parent: '#properties',
          },
          // linting: {
          //   bpmnlint: bpmnlintrc
          // },
          keyboard: {
            bindTo: window
          },
          additionalModules: [
            // propertiesPanelModule,
            // propertiesProviderModule,
            ColorsBpm,
            gridModule,
            TokenSimulationModule,
            CommentModule,
            transactionBoundariesModule,
            PriorityAwareModeler
            // lintModule
          ],
          moddleExtensions: {
            camunda: camundaModdleDescriptor,
          }
        });
        modeler.on('selection.changed', (e) => {
        console.log(e.newSelection[0]);
       //this.setElement(e.newSelection[0]);
      });
       modeler.on('element.changed', (e) => {
        console.log(e.element);
        //  this.setElement(e.element);
      });
        test.value = modeler;
        openDiagram(modeler, e.target.result);
      };
      reader.readAsBinaryString(file);
    };

    const DownloadDiagramXml = () => {
      saveDiagram(toRaw(test.value))
    };

    const DownloadDiagramSvg = async () => {
      SaveSvg(test.value);
    };

    const ShowComments = () => {
      showComments.value = !showComments.value;
      const ListComments = document.querySelectorAll('.comments-overlay');
      ListComments.forEach(element => {
        if (showComments.value) {
          element.classList.add('expanded');
        } else {
          element.classList.remove('expanded');
        }
      });
    }

    const ToggleProperties = () => {
      propertiesVisible.value = !propertiesVisible.value;
    };


    return {
      canvas, DownloadDiagramXml, ResetDiagram,
      DownloadDiagramSvg, ShowComments, showComments,
      ImportDiagram, HandleFileImport, fileInput, ToggleProperties, propertiesVisible, test
    };
  },
};
</script>

<style  lang="scss">
@import '~bpmn-js/dist/assets/diagram-js.css';
@import url("../assets/style/comments.css");
@import '~bpmn-js/dist/assets/bpmn-font/css/bpmn-codes.css';
@import '~bpmn-js/dist/assets/bpmn-font/css/bpmn-embedded.css';
@import '~bpmn-js-properties-panel/dist/assets/bpmn-js-properties-panel.css';
@import 'https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css';
@import '~bpmn-js-token-simulation/assets/css/bpmn-js-token-simulation.css';

.djs-popup.color-picker .djs-popup-body .entry {
  margin: 2px;
}

.djs-popup.color-picker .djs-popup-body {
  display: grid;
  grid: auto-flow / 1fr 1fr 1fr;
}

.flow-container {
  display: flex;
}

.containers {
  position: absolute;
  background-color: #ffffff;
  width: 100%;
  height: 100%;
}

.canvas {
  width: 100%;
  height: 100%;
}

.bjs-powered-by {
  display: none;
}

.properties {
  position: absolute;
  right: 0;
  top: 0;
  width: 300px;
  height: 100%;
  overflow-y: scroll;
}

.button-container {
  position: absolute;
  bottom: 0;
  // left: 50%;
  // transform: translateX(-50%);
  display: flex;

  // justify-content: center;
  gap: 20px;
  padding: 0 0 5px 10px;

  button {
    color: #000;
    border: none;
    border-radius: 25px;
    padding: 10px 25px;
  }

  button:hover {
    color: green;
    transform: scale(1.1);
    transition: all 0.5 ease-in-out;
  }
}

.properties-visible {
  display: none;
  transition: all 0.5 ease-in-out;
}

.img {
  position: absolute;
  z-index: 98999999;
  bottom: 0;

  img {
    width: 50px;
    height: 50px;
    border-radius: 30px;
  }

  right: 25px;
  bottom : 20px;
  animation: ColorLogo 4s infinite;
}


@keyframes ColorLogo {
  0% {
    filter: grayscale(0);
  }

  100% {
    filter: grayscale(15000);
  }
}

// .titre{
//   z-index: 9;
//   position: absolute;
//   padding-top: 10px;
//   color: black;
//   left: 40%;
//   transform: translateX(-100%);
//   display: flex;
//   justify-content: center;
// }
</style> 

