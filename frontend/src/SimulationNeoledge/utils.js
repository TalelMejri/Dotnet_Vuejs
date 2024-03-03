import {
    domify,
    classes as domClasses,
    event as domEvent,
    query as domQuery
} from 'min-dom';

import { errors } from '../LinterElement/util.js';

export function toggleMode(activeNew, eventBus, canvas, selection, contextPad) {
    const TOGGLE_MODE_EVENT = 'tokenSimulation.toggleMode';
    let active = false;
    if (activeNew === false) {
        return;
    }

    let btn = document.querySelector(".button-container");

    if (activeNew) {
        if (errors.length > 0) {
            console.log(errors);
            alert("There are errors in the process. Please fix them before starting the simulation.");
            return;
        } else {
            domClasses(canvas.getContainer().parentNode).add('simulation');
            //domClasses(domQuery('.djs-palette', canvas.getContainer())).add('hidden');
            btn.classList.add("hidden");
        }
    } else {
        domClasses(canvas.getContainer().parentNode).remove('simulation');
        //domClasses(domQuery('.djs-palette', canvas.getContainer())).remove('hidden');
        btn.classList.remove("hidden");

        const elements = selection.get();

        if (elements.length === 1) {
            contextPad.open(elements[0]);
        }
        eventBus.fire(TOGGLE_MODE_EVENT, { active });
      
    }

  
}
