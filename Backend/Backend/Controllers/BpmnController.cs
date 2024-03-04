using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BpmnController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> UploadBpmn(IFormFile file)
        {
           
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "fileBpmn");
            var filePath = Path.Combine(directoryPath, file.FileName);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            // Load and execute the workflow
            /* var workflow = await LoadBpmnWorkflow(filePath);
             await ExecuteWorkflow(workflow);*/

            return Ok();
        }
    }
}
