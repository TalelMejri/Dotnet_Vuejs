import {
  domify,
  classes as domClasses,
  event as domEvent,
  query as domQuery
} from 'min-dom';

import { errors } from "../../../../../LinterElement/util.js"

import {
  TOGGLE_MODE_EVENT
} from '../../../util/EventHelper';

import {
  ToggleOffIcon,
  ToggleOnIcon
} from '../../../icons';


export default function ToggleMode(
  eventBus, canvas, selection,
  contextPad) {

  this._eventBus = eventBus;
  this._canvas = canvas;
  this._selection = selection;
  this._contextPad = contextPad;

  this._active = false;

  eventBus.on('import.parse.start', () => {

    if (this._active) {
      this.toggleMode(false);

      eventBus.once('import.done', () => {
        this.toggleMode(true);
      });
    }
  });

  eventBus.on('diagram.init', () => {
    this._canvasParent = this._canvas.getContainer().parentNode;
    this._palette = domQuery('.djs-palette', this._canvas.getContainer());
    this._init();
  });
}

ToggleMode.prototype._init = function () {
  this._container = domify(`
    <div class="bts-toggle-mode">
      Start Process  <span class="bts-toggle">${ToggleOffIcon()}</span>
    </div>
  `);

  domEvent.bind(this._container, 'click', () => this.toggleMode());

  this._canvas.getContainer().appendChild(this._container);
};

ToggleMode.prototype.toggleMode = function (active = !this._active) {

  if (active === this._active) {
    return;
  }
  let btn = document.querySelector(".button-container");

  if (active) {
    if (errors.length > 0) {
      console.log(errors);
      alert("There are errors in the process. Please fix them before starting the simulation.")
      return;
    } else {
      this._container.innerHTML = `Start Process <span class="bts-toggle">${ToggleOnIcon()}</span>`;
      domClasses(this._canvasParent).add('simulation');
      domClasses(this._palette).add('hidden');
      btn.classList.add("hidden")
    }
  } else {
   this._container.innerHTML = `Start Process <span class="bts-toggle">${ToggleOffIcon()}</span>`;
    domClasses(this._canvasParent).remove('simulation');
    domClasses(this._palette).remove('hidden');
    btn.classList.remove("hidden")
    const elements = this._selection.get();

    if (elements.length === 1) {
      this._contextPad.open(elements[0]);
    }
  }

  this._eventBus.fire(TOGGLE_MODE_EVENT, {
    active
  });

  this._active = active;
};

ToggleMode.$inject = [
  'eventBus',
  'canvas',
  'selection',
  'contextPad'
];