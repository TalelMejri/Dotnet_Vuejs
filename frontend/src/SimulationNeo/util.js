import {
    domify,
    classes as domClasses,
    event as domEvent,
    query as domQuery
} from 'min-dom';

import { errors } from '../LinterElement/util.js';

import {
    TOGGLE_MODE_EVENT
} from "../bpmn-js-token-simulation/lib/util/EventHelper.js"

export function toggleMode(active, eventBus, canvas, selection, contextPad) {
    let btn = document.querySelector(".button-container");
    if (errors.length > 0) {
        console.log(errors);
        alert("There are errors in the process. Please fix them before starting the simulation.")
        return;
    } else {
        eventBus.fire(TOGGLE_MODE_EVENT, { active });
        document.getElementById('simulationToggleButton').innerText = active ? 'Start' : 'Stop';
        if (active) {
            btn.classList.add("hidden");
            domClasses(canvas.getContainer().parentNode).add('simulation');
        } else {
            btn.classList.remove("hidden");
            domClasses(canvas.getContainer().parentNode).remove('simulation');
        }

    }
}