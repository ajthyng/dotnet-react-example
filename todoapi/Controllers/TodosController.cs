using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

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
        var result = await MongoCRUD.LoadRecords<TodoModel>("todos");
        return Ok(result);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    [HttpGet("getOne")]
    async public Task<IActionResult> Get(string id)
    {
      try 
      {
        var result = await MongoCRUD.LoadRecordById<TodoModel>("todos", new ObjectId(id));
        return Ok(result);
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(TodoModel todo)
    {
      try
      {
        await MongoCRUD.InsertRecord("todos", todo);
        return Ok();
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }

    [HttpPost("update")]
    public async  Task<IActionResult> Update(TodoModel todo)
    {
      try
      {
        var updateInstructions = Builders<TodoModel>.Update.Set("done", todo.done);
        await MongoCRUD.UpdateRecord("todos", todo.Id, updateInstructions);
        return Ok();
      }
      catch (Exception e)
      {
        return StatusCode(500, e.Message);
      }
    }
  }
}
