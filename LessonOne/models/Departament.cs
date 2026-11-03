using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne.models
{
    public class Departament
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int employers { get; set; } = 0;
        public string materials { get; set; } = string.Empty;
        public string reports { get; set; } = string.Empty;

    }
}
