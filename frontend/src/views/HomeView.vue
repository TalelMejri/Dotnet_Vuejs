<template>
  <div>
    <div class="titre" data-aos="zoom-out">
      <h2>Neoledge Bpmn</h2>
    </div>
    <div class="bpmn_content">
      <div class="img">
        <img src="../assets/images/neoledge.png">
      </div>
      <div class="flow-container">
        <div ref="content" class="containers">
          <div id="canvas" ref="canvas" class="canvas"></div>
          <custome_panel v-if="element" @UpdateValue="UpdateValue" @AddTimer="AddTimer"
            @updatePropertyHeader="updatePropertyHeader" @SetHeaders="SetHeaders" @DeleteOutput="DeleteOutput"
            @AddCodePython="AddCodePython" @AddOutputs="AddOutputs" @UpdatePropertyInput="UpdatePropertyInput"
            @UpdatePropertyOutput="UpdatePropertyOutput" @DeleteInput="DeleteInput" @AddInputs="AddInputs"
            :element="element" @setValue="setValue" @updateActivityName="updateActivityName" @addFn="addFn"
            @updateProperties="updateProperties" @deleteHeader="deleteHeader" class="properties">
          </custome_panel>
        </div>
      </div>
      <div class="button-container">
        <button type="button" @click="downloadDiagramXml()">
          <i class="fa fa-download"></i>
        </button>
        <button type="button" @click="resetDiagram()">
          <i class="fa fa-refresh"></i>
        </button>
        <button type="button" @click="downloadDiagramSvg()">
          <i class="fa fa-file-image-o"></i>
        </button>
        <button type="button" @click="importDiagram">
          <i class="fa fa-upload"></i>
        </button>
        <button type="button" @click="zoomIn">
          +
        </button>
        <button type="button" @click="zoomOut">
          -
        </button>
        <button type="button" id="simulationToggleButton" @click="ToggleSimulation()">Start</button>
        <button type="button" data-bs-toggle="modal" data-bs-target="#exampleModal">
          <i class="fa fa-keyboard-o"></i>
        </button>
        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Keyboard shortcuts</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
              </div>
              <div class="modal-body">
                <div class="keyboard-shortcuts">
                  <ul>
                    <li>Undo Ctrl + Z</li>
                    <li>Redo Ctrl + ⇧ + Z</li>
                    <li>Select All Ctrl + A</li>
                    <li>Hand Tool H</li>
                    <li>Direct Editing E</li>
                    <li>Lasso Tool L</li>
                    <li>Create Milestone ⇧ + M</li>
                    <li>Scrolling (Vertical) Ctrl + Scrolling</li>
                    <li>Scrolling (Horizontal) Ctrl + ⇧ + Scrolling</li>
                    <li>Attention Grabber A</li>
                    <li>Space Tool S</li>
                    <li>Search BPMN Symbol Ctrl + F</li>
                  </ul>
                </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
              </div>
            </div>
          </div>
        </div>
        <button type="button" @click="SaveDiagram()">
          <i class="fa fa-save"></i>
        </button>
        <input type="file" accept=".bpmn" @change="handleFileImport" ref="fileInput" style="display: none" />
      </div>
    </div>
    <div>

    </div>
  </div>
</template>

<script>
import { ref, onMounted, toRaw } from 'vue';
import Modeler from "../Modeler/CustomBpmnModeler.js";
import NeoledgeDescriptor from "../descriptor/NeoledgeDescriptor.json"
import gridModule from 'diagram-js-grid';
import CommentModule from "../comment_custom/index"
import TokenSimulationModule from '../bpmn-js-token-simulation/lib/modeler.js';
import ColorsBpm from "../colors/index";
import transactionBoundariesModule from 'camunda-transaction-boundaries';
import custome_panel from "@/components/custome_panel.vue";
import { createElement, AddElementComposer, DeleteElement, UpdateElement } from "../properties_custom/utils.js";
import { openLocalDiagram, saveDiagram, SaveSvg, saveDiagramToLocal, ResetDiagramLocal } from "../Utils/diagram_util.js";
import Linter from "../LinterElement/index.js"
import {toggleMode} from "../SimulationNeo/util.js"
import WorkfloService  from "../service/WorkfloService.js"
import { errors } from "../LinterElement/util.js"
export default {
  components: { custome_panel },
  setup() {
    const canvas = ref(null);
    const element = ref(null);
    const _active = ref(true);
    const error = ref(errors);
    const propertiesVisible = ref(false);
    const fileInput = ref(null);
    const test = ref(null);
    const keyboardShortcutsVisible = ref(false);
    let zoomLevel = ref(1);
    let modeler;
    let bpmnElementRegistry;
    let bpmnElementfactory;

    onMounted(() => {

      initializeModeler();
    });

    const initializeModeler = () => {
      modeler = new Modeler({
        container: canvas.value,
        keyboard: { bindTo: window },
        additionalModules: [
          ColorsBpm,
          gridModule,
          TokenSimulationModule,
          CommentModule,
          transactionBoundariesModule,
          Linter
        ],
        moddleExtensions: { neo: NeoledgeDescriptor }
      });

      bpmnElementRegistry = modeler.get('elementRegistry');
      bpmnElementfactory = modeler.get('bpmnFactory');


      bindModelerEvents();
      openLocalDiagram(modeler, null);
    };

    const bindModelerEvents = () => {
      modeler.on('selection.changed', handleSelectionChange);
      modeler.on('element.changed', handleElementChange);
    };

    const handleSelectionChange = (event) => {
      const selectedElement = event.newSelection[0];
      if (selectedElement !== undefined) {
        // const overlays = modeler.get('overlays');
        element.value = [selectedElement.id, selectedElement.labels, selectedElement.type, selectedElement.businessObject];
        // createIcon(selectedElement, overlays);
        CheckStatus(element.value);
      } else {
        element.value = null;
      }
    };

    const CheckStatus = (element) => {
      if (element[3]["status"] == undefined) {
        const elementNew = bpmnElementRegistry.get(element[3]["id"]);
        modeler.get('modeling').updateProperties(elementNew, { status: 0 });
      } else {
        console.log('null');
      }
    }

    const zoomIn = () => {
      if (zoomLevel.value < 3) {
        zoomLevel.value += 0.1;
        modeler.get('canvas').zoom(zoomLevel.value);
      }
    };

    const ToggleSimulation = () => {
      modeler.saveXML({ format: true }, function (err, updatedXml) {
        if (err) {
          console.error(err);
          return;
        }
        const blob = new Blob([updatedXml], { type: 'application/bpmn20-xml;charset=utf-8' });
        WorkfloService.UploadFile(blob).then((res)=>{
        const eventBus = modeler.get('eventBus');
         const active = _active.value;
        _active.value = !active;
        const canvas = modeler.get('canvas');
        const selection = modeler.get('selection');
        const contextPad = modeler.get('contextPad');
        toggleMode(active,eventBus,canvas,selection,contextPad);
      })
      });
    }

    const zoomOut = () => {
      if (zoomLevel.value > 0.2) {
        zoomLevel.value -= 0.1;
        modeler.get('canvas').zoom(zoomLevel.value);
      }
    };

    const handleElementChange = (event) => {
      const changedElement = event.element;
      if (changedElement !== undefined) {
        element.value = [changedElement.id, changedElement.labels, changedElement.type, changedElement.businessObject];
      } else {
        element.value = null;
      }
    };

    const setValue = (name, value) => {
      AddElementComposer(
        element,
        bpmnElementfactory,
        "neo:Properties",
        "neo:Property",
        "properties",
        "name",
        name,
        value
      );
    }

    const AddInputs = (name, value) => {
      AddElementComposer(
        element,
        bpmnElementfactory,
        "neo:IoMapping",
        "neo:Input",
        "inputParameters",
        "source",
        name,
        value
      );
    }

    const SetHeaders = (key, value) => {
      AddElementComposer(
        element,
        bpmnElementfactory,
        "neo:TaskHeaders",
        "neo:Header",
        "values",
        "key",
        key,
        value);
    }

    const AddOutputs = (name, value) => {
      AddElementComposer(
        element,
        bpmnElementfactory,
        "neo:IoMapping",
        "neo:Output",
        "outputParameters",
        "source",
        name,
        value
      );
    }

    const addFn = (type, retries) => {
      const businessObject = toRaw(element.value[3]);
      let extensionElements = businessObject.get('extensionElements');

      if (!extensionElements) {
        extensionElements = createElement('bpmn:ExtensionElements', {}, bpmnElementfactory);
        businessObject.set('extensionElements', extensionElements);
      }

      let task_def = extensionElements.get('values').find(e => e.$type === 'neo:TaskDefinition');

      if (!task_def) {
        task_def = createElement('neo:TaskDefinition', { type: type, retries: retries }, bpmnElementfactory);
        extensionElements.get('values').push(task_def);
      } else {
        task_def.type = type;
        task_def.retries = retries;
      }
    };

    const AddCodePython = (code) => {
      const businessObject = toRaw(element.value[3]);
      let extensionElements = businessObject.get('extensionElements');

      if (!extensionElements) {
        extensionElements = createElement('bpmn:ExtensionElements', {}, bpmnElementfactory);
        businessObject.set('extensionElements', extensionElements);
      }

      let code_python = extensionElements.get('values').find(e => e.$type === 'neo:PythonCode');

      if (!code_python) {
        code_python = createElement('neo:PythonCode', { code: code }, bpmnElementfactory);
        extensionElements.get('values').push(code_python);
      }
      RefreshDiagram();
    }

    const AddTimer = (timer) => {
      const businessObject = toRaw(element.value[3]);
      let extensionElements = businessObject.get('extensionElements');
      if (!extensionElements) {
        extensionElements = createElement('bpmn:ExtensionElements', {}, bpmnElementfactory);
        businessObject.set('extensionElements', extensionElements);
      }
      let timerCycle = extensionElements.get('values').find(e => e.$type === 'neo:TimerCycle');
      if (!timerCycle) {
        timerCycle = createElement('neo:TimerCycle', { time: timer }, bpmnElementfactory);
        extensionElements.get('values').push(timerCycle);
      }
      RefreshDiagram();
    }

    const RefreshDiagram = () => {
      modeler.saveXML({ format: true }, function (err, updatedXml) {
        if (err) {
          console.error(err);
          return;
        }
        modeler.importXML(updatedXml, function (err) {
          if (err) {
            console.error(err);
          }
        });
      });
    }

    const DeleteInput = (properties) => {
      DeleteElement(
        element,
        'neo:IoMapping',
        bpmnElementfactory,
        "inputParameters",
        properties,
        'neo:Input',
        "source"
      );
    }

    const DeleteOutput = (properties) => {
      DeleteElement(
        element,
        'neo:IoMapping',
        bpmnElementfactory,
        "outputParameters",
        properties,
        'neo:Output',
        "source"
      );
    }

    const updateProperties = (properties) => {
      DeleteElement(
        element,
        'neo:Properties',
        bpmnElementfactory,
        "properties",
        properties,
        'neo:Property',
        "name"
      );
    };

    const deleteHeader = (properties) => {
      DeleteElement(
        element,
        'neo:TaskHeaders',
        bpmnElementfactory,
        "values",
        properties,
        'neo:Header',
        "key"
      );
    }

    const updateActivityName = newName => {
      if (element && element.value) {
        const elementNew = bpmnElementRegistry.get(element.value[3]["id"]);
        modeler.get('modeling').updateProperties(elementNew, { name: newName });
      } else {
        console.log('null');
      }
    };

    const UpdateValue = (newName, NewValue, name, value) => {
      UpdateElement(
        element,
        'neo:Properties',
        "properties",
        "name",
        name,
        value,
        newName,
        NewValue
      );
    };

    const updatePropertyHeader = (newName, NewValue, name, value) => {
      UpdateElement(
        element,
        'neo:TaskHeaders',
        "values",
        "key",
        name,
        value,
        newName,
        NewValue
      );
    };

    const UpdatePropertyInput = (newName, NewValue, name, value) => {
      UpdateElement(
        element,
        'neo:IoMapping',
        "inputParameters",
        "source",
        name,
        value,
        newName,
        NewValue
      );
    }

    const UpdatePropertyOutput = (newName, NewValue, name, value) => {
      UpdateElement(
        element,
        'neo:IoMapping',
        "outputParameters",
        "source",
        name,
        value,
        newName,
        NewValue
      );
    }

    const resetDiagram = () => {
      modeler.destroy();
      ResetDiagramLocal();
      initializeModeler();
    };

    const importDiagram = () => {
      fileInput.value.click();
    };

    const handleFileImport = (event) => {
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = (e) => {
        modeler.destroy();
        modeler = new Modeler({
          container: canvas.value,
          keyboard: { bindTo: window },
          additionalModules: [
          ColorsBpm,
          gridModule,
          TokenSimulationModule,
          CommentModule,
          transactionBoundariesModule,
          Linter
        ],
          moddleExtensions: { neo: NeoledgeDescriptor }
        });

        bpmnElementRegistry = modeler.get('elementRegistry');
        bpmnElementfactory = modeler.get('bpmnFactory');

        bindModelerEvents();
        openLocalDiagram(modeler, e.target.result);
      };
      reader.readAsBinaryString(file);
    };

    const downloadDiagramXml = () => {
      saveDiagram(toRaw(modeler))
    };

    const downloadDiagramSvg = async () => {
      SaveSvg(modeler);
    };

    const toggleProperties = () => {
      propertiesVisible.value = !propertiesVisible.value;
    };

    const SaveDiagram = () => {
      saveDiagramToLocal(modeler);
    }

    const toggleKeyboardShortcutsVisibility = () => {
      keyboardShortcutsVisible.value = !keyboardShortcutsVisible.value;
    };

    return {
      canvas,
      element,
      propertiesVisible,
      fileInput,
      error,
      test,
      addFn,
      AddOutputs,
      DeleteOutput,
      setValue,
      UpdateValue,
      AddInputs,
      SetHeaders,
      updateProperties,
      AddCodePython,
      DeleteInput,
      AddTimer,
      UpdatePropertyInput,
      deleteHeader,
      updateActivityName,
      resetDiagram,
      UpdatePropertyOutput,
      updatePropertyHeader,
      importDiagram,
      keyboardShortcutsVisible,
      toggleKeyboardShortcutsVisibility,
      zoomIn,
      zoomOut,
      ToggleSimulation,
      handleFileImport,
      downloadDiagramXml,
      downloadDiagramSvg,
      toggleProperties,
      SaveDiagram
    };
  }
}
</script>

<style lang="scss">
@import '~bpmn-js/dist/assets/diagram-js.css';
@import url("../assets/style/comments.css");
@import '~bpmn-js/dist/assets/bpmn-font/css/bpmn-codes.css';
@import '~bpmn-js/dist/assets/bpmn-font/css/bpmn-embedded.css';
@import 'https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css';
@import url("../assets/style/bpmn-js-token-simulation.css");

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
  position: fixed;
  border: 1px solid #ccc;
  border-radius: 5px;
  overflow-y: scroll;
  top: 0;
  width: 320px;
  right: 0;
  height: 100%;
  padding: 10px;
  background-color: #f9f9f9;
}

.button-container {
  position: absolute;
  bottom: 0;
  display: flex;
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
  display: none;
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

.button-container.hidden {
  display: none;
}

.titre {
  z-index: 9;
  position: absolute;
  padding-top: 10px;
  color: black;
  left: 40%;
  transform: translateX(-100%);
  display: flex;
  justify-content: center;
}

.keyboard-shortcuts {
  padding: 10px;
  background-color: #fff;
  border: 1px solid #ccc;
  border-radius: 5px;
  z-index: 1000;
}

.keyboard-shortcuts ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

.keyboard-shortcuts li {
  margin-bottom: 5px;
}

.icon-invalid {
  position: absolute;
  top: -10px;
  right: -10px;
  width: 20px;
  height: 20px;
  background-color: red;
  color: white;
  font-size: 12px;
  border-radius: 50%;
  text-align: center;
  line-height: 20px;
  cursor: pointer;
}
</style>
../SimulationNeoledge/toggleMode.js