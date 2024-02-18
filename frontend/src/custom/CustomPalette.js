const assign = Object.assign;

export default function PaletteProvider(
  palette,
  create,
  elementFactory,
  spaceTool
) {
  this._create = create;
  this._elementFactory = elementFactory;
  this._spaceTool = spaceTool;

  palette.registerProvider(this);
}

PaletteProvider.$inject = ["palette", "create", "elementFactory", "spaceTool"];

PaletteProvider.prototype.getPaletteEntries = function(element) {
  var actions = {},
    create = this._create,
    elementFactory = this._elementFactory,
    spaceTool = this._spaceTool;

  function createAction(type, group, className, title, options) {
    function createListener(event) {
      var shape = elementFactory.createShape(assign({ type: type }, options));

      if (options) {
        shape.businessObject.di.isExpanded = options.isExpanded;
      }

      create.start(event, shape);
    }

    var shortType = type.replace(/^bpmn:/, "");

    return {
      group: group,
      className: className,
      title: title || "Create " + shortType,
      action: {
        dragstart: createListener,
        click: createListener
      }
    };
  }

  function createParticipant(event, collapsed) {
    create.start(event, elementFactory.createParticipantShape(collapsed));
  }

  assign(actions, {
    "create.start-event": createAction(
      "bpmn:StartEvent",
      "event",
      "bpmn-icon-start-event-none"
    ),
    "create.intermediate-event": createAction(
      "bpmn:IntermediateThrowEvent",
      "event",
      "bpmn-icon-intermediate-event-none"
    ),
    "create.end-event": createAction(
      "bpmn:EndEvent",
      "event",
      "bpmn-icon-end-event-none"
    ),
    "create.exclusive-gateway": createAction(
      "bpmn:ExclusiveGateway",
      "gateway",
      "bpmn-icon-gateway-xor"
    ),
    "create.task": createAction("bpmn:Task", "activity", "bpmn-icon-task"),
    "create.participant-expanded": {
      group: "collaboration",
      className: "bpmn-icon-participant",
      title: "Create Pool/Participant",
      action: {
        dragstart: createParticipant,
        click: createParticipant
      }
    },
  });

  return actions;
};
