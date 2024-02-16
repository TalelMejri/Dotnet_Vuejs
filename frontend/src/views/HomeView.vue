<template>
  <div class="bpmn_content">
    <div class="flow-container">
    <div ref="content" class="containers">
      <div id="canvas" ref="canvas" class="canvas" ></div>
      <div id="properties" class="properties" ></div>
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
    </div>
  </div>
</template>

<script>
import { onMounted, ref, toRaw } from 'vue';
import propertiesPanelModule from 'bpmn-js-properties-panel';
import propertiesProviderModule from 'bpmn-js-properties-panel/lib/provider/camunda';
// import camundaModdleDescriptor from 'camunda-bpmn-moddle/resources/camunda';
import camundaModdleDescriptor from "../descriptor/camundaDescriptor.json"
import Modeler from "../Modeler/CustomBpmnModeler.js";
import ColorsBpm from "../colors/index";
import { openDiagram, saveDiagram, resetDiagramToBlank, SaveSvg } from "../Utils/diagram_util.js";
export default {
  setup() {

    const canvas = ref(null);
    const test = ref(null);

    onMounted(() => {
      const modeler = new Modeler({
        container: canvas.value,
        propertiesPanel: {
          parent: '#properties',
        },
        keyboard: {
          bindTo: window
        },
        additionalModules: [
          propertiesPanelModule,
          propertiesProviderModule,
          ColorsBpm
        ],
        moddleExtensions: {
          camunda: camundaModdleDescriptor,
        },
      });
      
      test.value = modeler;

      openDiagram(modeler, './diagram.bpmn');

    });

    const DownloadDiagramXml = () => {
      saveDiagram(toRaw(test.value))
    };

    const ResetDiagram = () => {
      resetDiagramToBlank(test.value)
    }

    const DownloadDiagramSvg = async () => {
      SaveSvg(test.value);
    };

    return {
      canvas, DownloadDiagramXml, ResetDiagram, DownloadDiagramSvg
    };
  },
};
</script>

<style  lang="scss">
@import '~bpmn-js/dist/assets/diagram-js.css';
@import '~bpmn-js/dist/assets/bpmn-font/css/bpmn-codes.css';
@import '~bpmn-js/dist/assets/bpmn-font/css/bpmn-embedded.css';
@import '~bpmn-js-properties-panel/dist/assets/bpmn-js-properties-panel.css';
@import 'https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css';

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

  .containers{
    position: absolute;
    background-color: #ffffff;
    width: 100%;
    height: 100%;
  }

  .canvas{
    width: 100%;
    height: 100%;
    background-image: url("../assets/images/back.png");
    background-repeat: no-repeat;
    background-size: cover;
  }


  .properties{
    position: absolute;
    right: 0;
    top: 0;
    width: 300px;
    height:100%;
    z-index: 999999999;
    overflow-y:scroll;
  }

  .button-container{
    position: absolute;
    bottom: 0;
    left: 50%; 
    transform: translateX(-50%); 
    display: flex;
    justify-content: center; 
    gap: 20px;
    padding-bottom: 10px;
    button{
      color: #000;
      border: none;
      border-radius: 25px;
      padding: 10px 25px;
    }

    button:hover{
      color: green;
      transform: scale(1.1);
      transition: all 0.5 ease-in-out;
    }
  }


</style> 

