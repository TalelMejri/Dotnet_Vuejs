// const ExampleModule = {
//     __init__: [
//       [ 'eventBus', 'bpmnjs', 'toggleMode', function(eventBus, bpmnjs, toggleMode) {
  
//         if (persistent) {
//           eventBus.on('commandStack.changed', function() {
//             bpmnjs.saveXML().then(result => {
//               localStorage['diagram-xml'] = result.xml;
//             });
//           });
//         }
  
//         if ('history' in window) {
//           eventBus.on('tokenSimulation.toggleMode', event => {
  
//             if (event.active) {
//               url.searchParams.set('e', '1');
//             } else {
//               url.searchParams.delete('e');
//             }
  
//             history.replaceState({}, document.title, url.toString());
//           });
//         }
  
//         eventBus.on('diagram.init', 500, () => {
//           toggleMode.toggleMode(active);
//         });
//       } ]
//     ]
//   };