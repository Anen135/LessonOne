using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne.models
{
    public class Experiment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int count { get; set; } = 0;

        public int ProjectsId { get; set; }
        public Projects Progects { get; set; }
    }
}
