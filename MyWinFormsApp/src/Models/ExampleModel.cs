using System;

namespace MyWinFormsApp.Models
{
    public class ExampleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ExampleModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id}: {Name}";
        }
    }
}