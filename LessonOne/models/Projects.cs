using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne.models
{
    internal class Projects
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Name {  get; set; }
        public string reports { get; set; }
        public string experements { get; set; }

    }
}
