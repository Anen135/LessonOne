using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LessonOne.models
{
    public class Materials
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int count { get; set; } = 0;

        public int DepartmentId { get; set; }
        public Departament Department { get; set; }


        //[Indexed]
        //public int DepartmentId { get; set; }
    }
}
