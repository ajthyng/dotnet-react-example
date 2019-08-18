using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace todos.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TodosController : ControllerBase
  {
    [HttpGet("list")]
    async public Task<IActionResult> Get()
    {
      try
      {
        var result = await TodosClient.GetTodos();
        return Ok(result);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }

    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(MongoTodo todo)
    {
      try
      {
        var savedTodo = await todo.Save();
        return Ok(savedTodo);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }
  }
}
