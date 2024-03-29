using Backend.Activities;
using Backend.Models;
using Backend.Service;
using Backend.Worflows;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Elsa.Workflows.Contracts;
using Timer = Elsa.Scheduling.Activities.Timer;
using Esprima.Ast;
using Python.Runtime;
using IronPython.Runtime;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BpmnController : ControllerBase
    {
        private System.Threading.Timer timer;
        FileService _fileService = new FileService();
        private DateTime _lastExecutionTime = DateTime.MinValue;
        private System.Timers.Timer _timer;
        private readonly IWorkflowRunner _workflowRunner;

        public BpmnController(IWorkflowRunner workflowRunner)
        {
            _workflowRunner = workflowRunner;
        }

        [HttpPost]
        public async Task<IActionResult> UploadBpmn([FromForm] IFormFile file, [FromForm] string data)
        {
          
            var list = new List<String>();

              var elements = JsonConvert.DeserializeObject<List<ElementType>>(data);
              var replay = 0;
              var path = "";
              foreach (ElementType element in elements)
              {
                  if (element.ExtensionElements != null)
                  {
                    var extensionPath = element.ExtensionElements.FirstOrDefault(ev => ev.Path != null);
                    if (extensionPath != null && extensionPath is ExtensionElement extensionElement)
                    {
                        path = extensionElement.Path; 
                    }
                 
                    if (element.ExtensionElements.Any(ev => ev.Time != null))
                      {
                          var extensionValue = element.ExtensionElements.FirstOrDefault(ev => ev.Time != null);
                          if (int.TryParse(extensionValue.Time, out int replayMinutes))
                          {
                              replay = replayMinutes;
                          }
                          else
                          {
                              replay = 0;
                          }
                      }
                  }
              }
       
           
              var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "fileBpmn");
                  if (!Directory.Exists(directoryPath))
                      Directory.CreateDirectory(directoryPath);

                  var randomFileName = Guid.NewGuid().ToString() + ".bpmn";
                  var filePath = Path.Combine(directoryPath, randomFileName);

                  using (var stream = new FileStream(filePath, FileMode.Create))
                  {
                      await file.CopyToAsync(stream);
                  }
            
            await _fileService.SaveFileName(randomFileName);
            var listWorflows = new List<String>();
            if (path=="" && replay == 0)
            {
                listWorflows = await BmnWorkflow(data);
                return Ok(listWorflows);
            }else if (path!="" && replay==0)
            {
                 string directoryToWatch = path;
                 FileSystemWatcher watcher = new FileSystemWatcher();
                 watcher.Path = directoryToWatch;
                 watcher.NotifyFilter = NotifyFilters.FileName;
                 // watcher.Filter = "*.bpmn"
                 watcher.Filter = "*.*";
                 watcher.Created += async (sender, e) =>
                 {
                     listWorflows = await BmnWorkflow(data);
                 };
                watcher.EnableRaisingEvents = true;
                return Ok(listWorflows);
            }
            else
            {
                  listWorflows = await BmnWorkflow(data);
                 _timer = new System.Timers.Timer(TimeSpan.FromMinutes(replay).TotalMilliseconds);
                 _timer.Elapsed += async (sender, e) =>
                 {
                      listWorflows = await BmnWorkflow(data);
                 };
                 _timer.AutoReset = true; // to make it executed many time
                 _timer.Start();
                 //_timer.Stop();
                 return Ok(listWorflows);
            }
        }
    
            private async Task<List<String>> BmnWorkflow(string data)
        { 
            var elements = JsonConvert.DeserializeObject<List<ElementType>>(data);
            var list = new List<String>();

            foreach (var element in elements)
            {
                switch (element.Type)
                {
                    case "bpmn:StartEvent":
                        list.Add(element.Id);
                        break;
                    case "bpmn:ScriptTask":
                         var scriptValue = element.ExtensionElements.FirstOrDefault(ev => ev.Code != null);
                          if (scriptValue != null)
                          {
                              var code = scriptValue.Code;
                              //await Task.Delay(TimeSpan.FromSeconds(2));
                              await _workflowRunner.RunAsync(new ScriptTaskWorkflow(code));
                              list.Add(element.Id);
                           }
                        break;
                    case "bpmn:BusinessRuleTask":
                        var connection = element.ExtensionElements.FirstOrDefault(ev => ev.ConnectionString != null);
                        var requete = element.ExtensionElements.FirstOrDefault(ev => ev.Requete != null);
                        var type = element.ExtensionElements.FirstOrDefault(ev => ev.TypeSgbd != null);

                        if (connection != null && requete!=null && type!=null)
                        {
                            var connectionTest = connection.ConnectionString;
                            var requeteTest = requete.Requete;
                            var typesgbd = type.TypeSgbd;
                            await _workflowRunner.RunAsync(new BDConnectionWorkflow(connectionTest,requeteTest, typesgbd));
                            list.Add(element.Id);
                        }
                        break;
                    case "bpmn:SendTask":
                        list.Add(element.Id);
                        break;
                    default:
                        break;
                }
            }
            return list;
        }
      

    }
}
