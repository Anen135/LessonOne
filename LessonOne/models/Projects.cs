using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne.models
{
    public class Projects
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<Experiment> Experiment { get; set; } = new List<Experiment>();
        public ICollection<Reports> Reports { get; set; } = new List<Reports>();
        
        //сюда эксперементы и репорты
    }
}
