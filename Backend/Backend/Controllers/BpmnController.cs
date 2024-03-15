using Backend.Activities;
using Backend.Models;
using Backend.Service;
using Backend.Worflows;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Elsa.Workflows.Contracts;
using Timer = Elsa.Scheduling.Activities.Timer;
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
            foreach (ElementType element in elements)
            {
                if (element.ExtensionElements != null)
                {
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
                var workflow = await BmnWorkflow(data);
            /*
               var currentTime = DateTime.UtcNow;
               var nextMidnight = currentTime.Date.AddDays(1); 
               var delay = nextMidnight - currentTime;
            */
           /*
              var currentTime = DateTime.UtcNow;
              var nextTargetTime = currentTime.Date.AddHours(23).AddMinutes(53);
              if (currentTime > nextTargetTime)
              {
                 nextTargetTime = nextTargetTime.AddDays(1);
              }
              var delay = nextTargetTime - currentTime;
              _timer = new System.Timers.Timer(delay.TotalMilliseconds);
            */
                _timer = new System.Timers.Timer(TimeSpan.FromMinutes(replay).TotalMilliseconds);
                _timer.Elapsed += async (sender, e) =>
                {
                     workflow=await BmnWorkflow(data);
                };
                _timer.AutoReset = true; // to make it executed many time
                _timer.Start();
                //_timer.Stop();
                return Ok(workflow);
 
            /*var elements = JsonConvert.DeserializeObject<List<ElementType>>(data);
           
             var replay = 0;
          foreach (ElementType element in elements)
             {
                 if (element.ExtensionElements != null)
                 {
                   if(element.ExtensionElements.Any(ev => ev.Time != null))
                   {
                     var extensionValue = element.ExtensionElements.FirstOrDefault(ev => ev.Time != null);
                         if (int.TryParse(extensionValue.Time, out int timeValue))
                         {
                             replay = timeValue;
                         }
                         else
                         {
                             replay = 0;
                         }
                   }
                 }
             }*/

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
                              await Task.Delay(TimeSpan.FromSeconds(2));
                              await _workflowRunner.RunAsync(new ScriptTaskWorkflow(code));
                             // list.Add(element.Id);
                           }
                        break;
                    case "bpmn:SendTask":
                        //list.Add(element.Id);
                        break;
                    default:
                        break;
                }
            }
            return list;
        }
      

    }
}
/*   if (replay == 0)
            {
               await BmnWorkflow(data);
            }
            else
            {
                StartWorkflowTimer(replay, data);
            }
         */
/*   public void StartWorkflowTimer(int replay,string data)
       {
           timer = new System.Threading.Timer(ExecuteWorkflow, data, TimeSpan.Zero, TimeSpan.FromMinutes(replay));
       }

       private void ExecuteWorkflow(object state)
       {
           string jsonData = (string)state;
           BmnWorkflow(jsonData);
       }*/

//  await ExecuteWorkflow(workflow);
// return file
// var fileContent = await LoadBpmnWorkflow(filePath);
// return File(fileContent, "application/octet-stream", randomFileName);
//return file 
/* 
 * private async Task<byte[]> LoadBpmnWorkflow(string filePath)
 {
     using (var stream = new FileStream(filePath, FileMode.Open))
     {
         using (var memoryStream = new MemoryStream())
         {
             await stream.CopyToAsync(memoryStream);
             return memoryStream.ToArray();
         }
     }
 }
*/
/*  var workflow = new Employe(JsonConvert.SerializeObject(elements));
    var executionContext = await _workflowRunner.RunAsync(workflow); *
/*await _fileService.SaveFileName(randomFileName);
var workflow = await BmnWorkflow(data);*/