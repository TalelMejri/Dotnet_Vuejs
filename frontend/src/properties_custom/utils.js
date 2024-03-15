
import { toRaw } from "vue";

export function createElement(type, properties, bpmnFactory) {
    const element = bpmnFactory.create(type, properties);
    return element;
}

export function AddElementComposer(
    element,
    bpmnFactory,
    TypeChild,
    TypeChildOfChild,
    ValuesChild,
    typeName,
    name, value
) {

    const businessObject = toRaw(element.value[3]);
    let ItemParent = businessObject.get("extensionElements");

    if (!ItemParent) {
        ItemParent = createElement("bpmn:ExtensionElements", {}, bpmnFactory);
        businessObject.set("extensionElements", ItemParent);
    }

    let ChildItem = ItemParent.get('values').find(e => e.$type === TypeChild);

    if (!ChildItem) {
        ChildItem = createElement(TypeChild, {}, bpmnFactory);
        ItemParent.get('values').push(ChildItem);
    }

    if (typeName == 'key') {
        var ChildItemValue = createElement(TypeChildOfChild, {
            key: name, value: value
        }, bpmnFactory);
    } else if (typeName == 'name') {
        var ChildItemValue = createElement(TypeChildOfChild, {
            name: name, value: value
        }, bpmnFactory);
    } else if(typeName=="source") {
        var ChildItemValue = createElement(TypeChildOfChild, {
            source: name, target: value
        }, bpmnFactory);
    }else{
        var ChildItemValue = createElement(TypeChildOfChild, {
            IdUser: name, comment: value
        }, bpmnFactory); 
    }

    ChildItem.get(ValuesChild).push(ChildItemValue);
}


export function DeleteElement(
    element,
    TypeChild,
    bpmnFactory,
    valueChild,
    properties,
    TypeChildOfChild,
    typeName
) {
    const businessObject = toRaw(element.value[3]);
    let extensionElements = businessObject.get('extensionElements');

    var baseElement = extensionElements.get('values').find(e => e.$type === TypeChild);

    if (valueChild == "properties") {
        baseElement.properties = [];
    } else if (valueChild == "values") {
        baseElement.values = [];
    } else if(valueChild == "inputParameters") {
        baseElement.inputParameters = [];
    }else if(valueChild == "outputParameters"){
        baseElement.outputParameters = [];
    }else{
        baseElement.comments = [];
    }

    if (!extensionElements) {
        extensionElements = createElement('bpmn:ExtensionElements', {}, bpmnFactory);
        businessObject.set('extensionElements', extensionElements);
    }

    if (properties.length >= 1) {
        if (!baseElement) {
            baseElement = createElement(TypeChild, {}, bpmnFactory);
            extensionElements.get('values').push(baseElement);
        }
        if (typeName == "key") {
            for (let i = 0; i < properties.length; i++) {
                var baseElementValue = createElement(TypeChildOfChild, {
                    key: properties[i].key, value: properties[i].value
                }, bpmnFactory);
                baseElement.get(valueChild).push(baseElementValue);
            }
        } else if (typeName == "name") {
            for (let i = 0; i < properties.length; i++) {
                var baseElementValue = createElement(TypeChildOfChild, {
                    name: properties[i].name, value: properties[i].value
                }, bpmnFactory);
                baseElement.get(valueChild).push(baseElementValue);
            }
        } else if(typeName=="source") {
            for (let i = 0; i < properties.length; i++) {
                var baseElementValue = createElement(TypeChildOfChild, {
                    source: properties[i].name, target: properties[i].value
                }, bpmnFactory);
                baseElement.get(valueChild).push(baseElementValue);
            }
        }else{
            for (let i = 0; i < properties.length; i++) {
                var baseElementValue = createElement(TypeChildOfChild, {
                    IdUser: properties[i].IdUser, comment: properties[i].comment
                }, bpmnFactory);
                baseElement.get(valueChild).push(baseElementValue);
            }
        }
    }
}


export function UpdateElement(element, TypeChild, ValuesChild, TypeName, name, value, newName, newValue) {
    
    const businessObject = toRaw(element.value[3]);
    let extensionElements = businessObject.get('extensionElements');
    let BaseElement = extensionElements.get('values').find(e => e.$type === TypeChild);

    if (TypeName == "name") {
        var res = BaseElement.get(ValuesChild).find(e => e.name === name && e.value === value);
    } else if (TypeName == "value") {
        var res = BaseElement.get(ValuesChild).find(e => e.key === name && e.value === value);
    } else {
        var res = BaseElement.get(ValuesChild).find(e => e.source === name && e.target === value);
    }

    if (res) {
        if (TypeName == "name") {
            res.name = newName;
            res.value = newValue;
        } else if (TypeName == "key") {
            res.key = newName;
            res.value = newValue;
        } else {
            res.source = newName;
            res.target = newValue;
        }
    } else {
        console.log('Not found');
    }
}
