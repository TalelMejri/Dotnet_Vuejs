import Modeler from "bpmn-js/lib/Modeler";

import CustomModule from "../custom";

Modeler.prototype._modules = [].concat(Modeler.prototype._modules, [
  CustomModule
]);

console.log(Modeler.prototype._modules);

export default Modeler;
