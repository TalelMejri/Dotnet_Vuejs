 <template>
  <div>
  <div class="diagram-container">
    <div ref="canvas" id="canvas" class="canvas"></div>
    <div id="properties" class="properties"></div>
  </div>
  <button type="button" @click="DownloadDiagramXml()">Download</button>
  <button type="button" @click="ResetDiagram()">Reset</button>
</div>
</template>

<script>
import { onMounted, ref ,toRaw} from 'vue';
import propertiesPanelModule from 'bpmn-js-properties-panel';
import propertiesProviderModule from 'bpmn-js-properties-panel/lib/provider/camunda';
import camundaModdleDescriptor from '../descriptor/camundaDescriptor.json';
import Modeler from "../assets/CustomBpmnModeler.js"
import { openDiagram,saveDiagram,resetDiagramToBlank } from "../descriptor/diagram_util.js";
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
        additionalModules: [propertiesPanelModule, propertiesProviderModule],
        moddleExtensions: {
          camunda: camundaModdleDescriptor,
        },
      });
      test.value=modeler;

      openDiagram(modeler,'./diagram.bpmn');
   
    });
      
    const DownloadDiagramXml = () => {
        saveDiagram(toRaw(test.value))
    };

    const ResetDiagram = () =>{
      resetDiagramToBlank(test.value)
    }

    return {
      canvas,DownloadDiagramXml,ResetDiagram
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

