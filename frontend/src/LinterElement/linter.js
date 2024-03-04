import $ from 'jquery';

const colorImageSvg = `
<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" class="feather feather-alert-circle">
    <circle cx="12" cy="12" r="10"></circle>
    <line x1="12" y1="8" x2="12" y2="12"></line>
    <line x1="12" y1="16" x2="12" y2="16"></line>
</svg>
`;
const CorrectIcon = `
<svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" class="feather feather-check-circle">
  <circle cx="12" cy="12" r="10"></circle>
  <path d="M9 12l2 2 4-4"></path>
</svg>

`;
import { addError, removeError } from "./util"
import { ELEMENT_CHANGED_EVENT, PLAY_SIMULATION_EVENT } from '@/bpmn-js-token-simulation/lib/util/EventHelper';

export default function Linter(eventBus, overlays) {

  function createIcon(element) {
    var $overlay = $(colorImageSvg);
    $overlay.click(function (e) {
      console.log("error");
    });
    overlays.add(element, 'icons', {
      position: {
        bottom: 10,
        right: 10
      },
      html: $overlay
    });
  }

  function createIconCorrect(element) {
    var $overlay = $(CorrectIcon);
    $overlay.click(function (e) {
      console.log("error");
    });
    overlays.add(element, 'icons', {
      position: {
        bottom: 10,
        right: 10
      },
      html: $overlay
    });
  }

  eventBus.on(ELEMENT_CHANGED_EVENT, function (event) {
    var element = event.element;
    if (element.labelTarget ||
      !element.businessObject.$instanceOf('bpmn:FlowNode')) {
      return;
    }
    if (element.businessObject.
      $attrs.status==0) {
        createIcon(element);
    }else{
        createIconCorrect(element);
    }
  });

  eventBus.on(['shape.changed', 'shape.added'], function (event) {
    var element = event.element;
    if (element.labelTarget ||
      !element.businessObject.$instanceOf('bpmn:FlowNode')) {
      return;
    }
    defer(function () {
      if (element.businessObject.$instanceOf('bpmn:StartEvent')) {
        if (element.businessObject.eventDefinitions) {
          if (element.businessObject.eventDefinitions[0]?.$type == "bpmn:TimerEventDefinition") {
            if (element.businessObject.extensionElements) {
              removeError(element.id);
              return;
            } else {
              addError("Start event must have a timer definition", element.id);
              createIcon(element);
            }
          }
        } else {
          return
        }
      } else if (element.businessObject.$instanceOf('bpmn:ScriptTask')) {
        if (element.businessObject.extensionElements) {
          if (element.businessObject.extensionElements.values[0]['code'] != "") {
            removeError(element.id);
            return;
          }
        } else {
          addError("Task must have a timer definition", element.id);
          createIcon(element);
        }
      }
    });
  });
}

function defer(fn) {
  setTimeout(fn, 0);
}
