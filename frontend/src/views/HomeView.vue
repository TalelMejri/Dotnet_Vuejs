<!-- <template>
  <div class="modeler">
      <div id="canvas"></div>
      <div id="properties"></div>
  </div>
</template>

<script setup>
import blankBpmn from "../../public/diagram.bpmn"
import BpmnModeler from 'bpmn-js/lib/Modeler';
import propertiesPanelModule from 'bpmn-js-properties-panel'
import propertiesProviderModule from 'bpmn-js-properties-panel/lib/provider/camunda'
import { ref, onMounted } from 'vue';

const bpmnDig=ref(`<?xml version="1.0" encoding="UTF-8"?>
<definitions xmlns="http://www.omg.org/spec/BPMN/20100524/MODEL" xmlns:bpmndi="http://www.omg.org/spec/BPMN/20100524/DI" xmlns:omgdi="http://www.omg.org/spec/DD/20100524/DI" xmlns:omgdc="http://www.omg.org/spec/DD/20100524/DC" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" id="sid-38422fae-e03e-43a3-bef4-bd33b32041b2" targetNamespace="http://bpmn.io/bpmn" exporter="bpmn-js (https://demo.bpmn.io)" exporterVersion="16.4.0">
  <process id="Process_1" isExecutable="false">
    <startEvent id="StartEvent_1y45yut" name="Start">
      <outgoing>SequenceFlow_0h21x7r</outgoing>
    </startEvent>
    <sequenceFlow id="SequenceFlow_0h21x7r" sourceRef="StartEvent_1y45yut" targetRef="Task_1hcentk" />
    <serviceTask id="Task_1hcentk" name="4">
      <incoming>SequenceFlow_0h21x7r</incoming>
      <outgoing>Flow_0nz5t1y</outgoing>
    </serviceTask>
    <endEvent id="Event_12o1npa">
      <incoming>Flow_0nz5t1y</incoming>
    </endEvent>
    <sequenceFlow id="Flow_0nz5t1y" sourceRef="Task_1hcentk" targetRef="Event_12o1npa" />
  </process>
  <bpmndi:BPMNDiagram id="BpmnDiagram_1">
    <bpmndi:BPMNPlane id="BpmnPlane_1" bpmnElement="Process_1">
      <bpmndi:BPMNShape id="StartEvent_1y45yut_di" bpmnElement="StartEvent_1y45yut">
        <omgdc:Bounds x="152" y="102" width="36" height="36" />
        <bpmndi:BPMNLabel>
          <omgdc:Bounds x="159" y="145" width="25" height="14" />
        </bpmndi:BPMNLabel>
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id="Activity_19my0z6_di" bpmnElement="Task_1hcentk">
        <omgdc:Bounds x="300" y="80" width="100" height="80" />
        <bpmndi:BPMNLabel />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNShape id="Event_12o1npa_di" bpmnElement="Event_12o1npa">
        <omgdc:Bounds x="522" y="102" width="36" height="36" />
      </bpmndi:BPMNShape>
      <bpmndi:BPMNEdge id="SequenceFlow_0h21x7r_di" bpmnElement="SequenceFlow_0h21x7r">
        <omgdi:waypoint x="188" y="120" />
        <omgdi:waypoint x="300" y="120" />
      </bpmndi:BPMNEdge>
      <bpmndi:BPMNEdge id="Flow_0nz5t1y_di" bpmnElement="Flow_0nz5t1y">
        <omgdi:waypoint x="400" y="120" />
        <omgdi:waypoint x="522" y="120" />
      </bpmndi:BPMNEdge>
    </bpmndi:BPMNPlane>
  </bpmndi:BPMNDiagram>
</definitions>
`);
const modeler = ref(null);

onMounted(() => {
  modeler.value = new BpmnModeler({
    container: '#canvas',
    propertiesPanel: {
      parent: '#properties',
    },
    additionalModules: [
    propertiesPanelModule,
    propertiesProviderModule
    ],
  });
  modeler.value.importXML(bpmnDig.value, function(err, warnings) {
  if (err) {
    console.error('Error importing XML:', err);
  } else {
    console.log('Warnings:', warnings);
  }
});
});
</script> -->


<!-- 
<template>
  <div class="diagrame_contianer">
     <vue-bpmn
        url="./diagram.bpmn"
        :options="options"
        v-on:error="handleError"
        v-on:shown="handleShown"
        v-on:loading="handleLoading"
     ></vue-bpmn>
     <div id="properties" class="properties" />
    </div>
</template>

<script>
  import Modeler from 'bpmn-js/lib/Modeler'
  import propertiesPanelModule from 'bpmn-js-properties-panel'
  import propertiesProviderModule from 'bpmn-js-properties-panel/lib/provider/camunda'
  import camundaModdleDescriptor from '../descriptor/camundaDescriptor.json'
  import VueBpmn from '../../node_modules/vue-bpmn/src';
  export default {
    components: {
      VueBpmn
    },
    data() {
      return {
        modeler:null,
        options: {
          propertiesPanel: {
            parent: '#properties'
         },
          additionalModules: [
           
          ],

        }
      }
    },
    mounted() {
     this.$nextTick(() => {
      
     });
    },
    methods: {
      handleError: function(err) {
        console.error('failed to show diagram', err);
      },
      handleShown: function() {
        console.log('diagram shown');
      },
      handleLoading: function() {
        console.log('diagram loading');
      }
    }
  };
</script>

<style>
.diagrame_contianer{
  height: 100vh;
}
h1,
h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}

.canvas {
  width: 100%;
  height: 70vh;
}
</style> -->


 <template>
  <div class="diagram-container">
    <div ref="canvas" id="canvas" class="canvas"></div>
    <div id="properties" class="properties"></div>
  </div>
</template>

<script>
import { onMounted, ref } from 'vue';
import Modeler from 'bpmn-js/lib/Modeler';
import propertiesPanelModule from 'bpmn-js-properties-panel';
import propertiesProviderModule from 'bpmn-js-properties-panel/lib/provider/camunda';
import camundaModdleDescriptor from '../descriptor/camundaDescriptor.json';

export default {
  setup() {
    const canvas = ref(null);
    
    onMounted(() => {
      const modeler = new Modeler({
        container: canvas.value,
        propertiesPanel: {
          parent: '#properties',
        },
        additionalModules: [propertiesPanelModule, propertiesProviderModule],
        moddleExtensions: {
          camunda: camundaModdleDescriptor,
        },
      });

      fetch('./diagram.bpmn')
        .then(response => response.text())
        .then(diagram => {
          modeler.importXML(diagram, error => {
            if (error) {
              console.error('Could not import BPMN diagram', error);
            } else {
              modeler.get('canvas').zoom('fit-viewport');
            }
          });
        })
        .catch(err => console.error('Error loading BPMN diagram', err));
    });

    return {
      canvas,
    };
  },
};
</script>

<style>
  @import '~bpmn-js/dist/assets/diagram-js.css' ;
  @import '~bpmn-js/dist/assets/bpmn-font/css/bpmn-codes.css';
  @import '~bpmn-js/dist/assets/bpmn-font/css/bpmn-embedded.css';
  @import '~bpmn-js-properties-panel/dist/assets/bpmn-js-properties-panel.css';
.diagram-container {
  display: flex;
  height: 100vh;
}
.canvas {
  width: 80%;
  height: 100%;
}
.properties {
  width: 20%;
  height: 100%;
  overflow: auto;
}
</style> 


<!-- 
<template>
  <div class="flow-container">
    <div ref="content" class="containers">
      <div id="canvas" ref="canvas" class="canvas"></div>
      <div id="properties" class="properties" ></div>
    </div>
  </div>
</template>

<script>
import Modeler from 'bpmn-js/lib/Modeler'
import propertiesPanelModule from 'bpmn-js-properties-panel'
import propertiesProviderModule from 'bpmn-js-properties-panel/lib/provider/camunda'
import camundaModdleDescriptor from '../descriptor/camundaDescriptor.json'
export default {
  name: 'Panel',
  data() {
    return {
      modeler: null
    }
  },
  mounted() {
    this.init()
  },
  methods: {
    init() {
      const canvas = this.$refs.canvas
      this.modeler = new Modeler({
        container: canvas,
        propertiesPanel: {
          parent: '#properties'
        },
        additionalModules: [
          propertiesPanelModule,
          propertiesProviderModule
        ],
        moddleExtensions: {
          camunda: camundaModdleDescriptor
        }
      })
    }
  }
}
</script>

<style scoped>
  @import '~bpmn-js/dist/assets/diagram-js.css' ;
  @import '~bpmn-js/dist/assets/bpmn-font/css/bpmn-codes.css';
  @import '~bpmn-js/dist/assets/bpmn-font/css/bpmn-embedded.css';
  @import '~bpmn-js-properties-panel/dist/assets/bpmn-js-properties-panel.css';
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
  }
  .properties{
    position: absolute;
    right: 0;
    top: 0;
    width: 300px;
    height:100%;
    overflow-y:scroll;
  }
</style>  -->



<!-- 
<template>
  <div class="hello">
    <div class="content" id="js-drop-zone">
  </div>

    <div class="navigation">
      <ul class="buttons">
        <li>
          <a href v-on:click="resetDiagram">Reset Diagram</a>
        </li>
        <li>
          <a id="js-download-diagram" :href="diagramUrl" v-bind:download="diagramName" title="download BPMN diagram">
            Download as BPMN diagram
          </a>
        </li>
        <li>
          <a id="js-download-svg" :href="svgUrl" v-bind:download="svgName"  title="download as SVG image">
            Donwload as SVG image
          </a>
        </li>
        <li>
          <a href v-on:click="centerDiagram">Center Diagram</a>
        </li>
      </ul>
    </div>

    <div class="canvas" id="canvas"></div>
    <div id="properties"></div>
  </div>
</template>

<script>
import BpmnModeler from "bpmn-js/lib/Modeler";
import propertiesProviderModule from 'bpmn-js-properties-panel/lib/provider/camunda';
import camundaModdleDescriptor from 'camunda-bpmn-moddle/resources/camunda';
import propertiesPanelModule from 'bpmn-js-properties-panel';
import {
  parseBPMNJson,
  getBlankDiagramDownloadUrl,
  openLocalDiagram,
  resetDiagramToBlank,
  saveDiagram
} from "../descriptor/diagram_util.js";
import style from "bpmn-js/dist/assets/diagram-js.css";
import icons from "bpmn-font/dist/css/bpmn-embedded.css"; 
import CustomModeler from "../assets/CustomBpmnModeler.js";

export default {
  name: "ModelEditor",
  data() {
    return {
      msg: "Welcome to Your Vue.js App",
      modeler: null,
      diagramUrl: getBlankDiagramDownloadUrl(),
      diagramName: "diagram.bpmn",
      svgUrl: "data:application/bpmn20-xml;charset=UTF-8,hello",
      svgName: "diagram.svg"
    };
  },
  methods: {
    resetDiagram(ev) {
      ev.preventDefault();
      resetDiagramToBlank(this.modeler);
    },
    centerDiagram(ev) {
      ev.preventDefault();
      const self = this;
      self.modeler.get("canvas").zoom("fit-viewport");
      saveDiagram(self.modeler).then(url => {
        self.diagramUrl = url;
        console.log("Saved");
      });
    }
  },
  mounted() {
  
      this.modeler = new CustomModeler({
        x: 100,
        container: "#canvas"
      });

    //  let modeler = this.modeler;
    //   // let modeler={}
    //   // Object.defineProperty(modeler, "prop", {
    //   //     value: this.modeler,
    //   //     writable: false,
    //   //     configurable: false,
    //   // });

    //   modeler.on("element.changed", function(event) {
    //     let element = event.element;
    //     if (event.gfx) {
    //     }
    //   });

    //   let self = this;
    //   modeler.on("commandStack.changed", () => {
    //     var definitions = modeler.get("canvas").getRootElement().businessObject
    //       .$parent;
    //     parseBPMNJson(definitions);
    //     saveDiagram(modeler).then(url => {
    //       self.diagramUrl = url;
    //     });
    //     modeler.saveSVG(function(err, svg) {
    //       self.svgUrl = `data:application/bpmn20-xml;charset=UTF-8,${encodeURIComponent(
    //         svg
    //       )}`;
    //     });
    //   });
     
      openLocalDiagram(this.modeler);
   
  }
};
</script>

<style scoped>
h1,
h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}

.canvas {
  width: 100%;
  height: 70vh;
}
</style> 
 -->
