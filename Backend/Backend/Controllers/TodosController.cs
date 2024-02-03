using Backend.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {

        /* DbContextClasse dbContext =new DbContextClasse();
         public TodosController() { 

         }
         // GET: api/todos
         [HttpGet]
         public IActionResult Get()
         {
             var todos=this.dbContext.Todos.ToList();
             if (todos == null)
             {
                 return NotFound(new { message = "todo Updated" });
             }
             else
             {
                 return Ok(todos);
             }
         }

         // GET api/<TodosController>/5
         [HttpGet("{id}")]
         public IActionResult Get(int id)
         {
             var todo=this.dbContext.Todos.Find(id);
             return Ok(todo);
         }

         // POST api/<TodosController>
         [HttpPost]
         public IActionResult Post([FromBody] Todo todo)
         {
             try
             {
                 todo.status = false;
                 dbContext.Add(todo);
                 dbContext.SaveChanges();
                 return Ok("Todo Added");
             }catch(Exception ex)
             {
                 return BadRequest(ex.Message);
             }
         }

         // PUT api/<TodosController>/5
         [HttpPut("{id}")]
         public IActionResult Put(int id, [FromBody] Todo value)
         {
             var todoUpdated=dbContext.Todos.Find(id);
             if(todoUpdated == null)
             {
                 todoUpdated.title = value.title;
                 todoUpdated.description = value.description;
                 dbContext.SaveChanges();
                 return Ok("Updated with success");
             }
             else
             {
                 return NotFound();
             }
         }

         // DELETE api/<TodosController>/5
         [HttpDelete("{id}")]
         public IActionResult Delete(int id)
         {
             Todo todo = dbContext.Todos.Find(id);

             if( todo == null) {
                 return NotFound();
             }
             else
             {
                 dbContext.Remove(todo);
                 dbContext.SaveChanges();
                 return Ok("Todo Deleted");
             }

         }


         [HttpPut("changerStatus")]
         public IActionResult ChangerStatus(int id)
         {
             var todo=dbContext.Todos.Find(id);
             if( todo == null) {
                 return NotFound();
             }
             else
             {
                 todo.status=!todo.status;
                 dbContext.SaveChanges();
                 return Ok(
                     new { message = "todo Updated" }
                  );
             }
         }

         [HttpGet("countTodos")]
         public IActionResult CountTodos()
         {
             List<Todo> todos=dbContext.Todos.ToList();
             return Ok(todos.Count());
         }
        */

    }
}
