using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonOne.models
{
    public class Departament
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int employers { get; set; } = 0;

        public ICollection<Materials> Materials { get; set; } = new List<Materials>();

        //Сюда материалы
    }
}
