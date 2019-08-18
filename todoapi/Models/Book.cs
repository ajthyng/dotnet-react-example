using System;
using System.Collections.Generic;

namespace todos
{
  class Book
  {
    public delegate void GradeAddedDelegate(object sender, GradeAddedEventArgs args);

    private List<double> grades;
    private const string category = "Science";
    public double Average { get; private set; } = 0;
    public double Min { get; private set; } = double.MaxValue;
    public double Max { get; private set; } = double.MinValue;

    public Book()
    {
      grades = new List<double>();
    }

    private void UpdateAverage()
    {
      var result = 0.0;
      foreach (double grade in grades)
      {
        result += grade;
      }
      Average = result / grades.Count;
    }

    private void UpdateMinGrade(double grade)
    {
      Min = Math.Min(grade, Min);
    }

    private void UpdateMaxGrade(double grade)
    {
      Max = Math.Max(grade, Max);
    }

    public void AddGrade(double grade)
    {
      grades.Add(grade);
      if (GradeAdded != null)
      {
        var args = new GradeAddedEventArgs();
        args.Grade = grade;
        GradeAdded(this, args);
      }
      UpdateMaxGrade(grade);
      UpdateMinGrade(grade);
      UpdateAverage();
    }

    public event GradeAddedDelegate GradeAdded;

    public override string ToString()
    {
      return $"\n{category} Grades\nAverage Grade: {Average:N1}\nMin Grade: {Min}\nMax Grade: {Max}\n";
    }
  }

  public class GradeAddedEventArgs : EventArgs
  {
    public double Grade { get; set; }
  }
}