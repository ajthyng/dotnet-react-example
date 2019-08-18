using System;
using System.Threading.Tasks;

namespace todos
{
  public abstract class Todo
  {
    public DateTime DueDate { get; set; }
    public string Text { get; set; }
    public string ID { get; set; }

    public Todo(DateTime due, string text)
    {
      DueDate = due;
      Text = text;
    }

    public Todo(DateTime due, string text, string id)
    {
      DueDate = due;
      Text = text;
      ID = id;
    }

    public abstract Task<Todo> Save();
  }
}