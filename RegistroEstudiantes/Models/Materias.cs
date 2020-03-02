using System;
using System.Collections.Generic;

namespace RegistroEstudiantes.Models
{
    public partial class Materias
    {
        public Materias()
        {
            Estudiantes = new HashSet<Estudiantes>();
        }

        public int IdMateria { get; set; }
        public string Nombre { get; set; }
        public int? Nivel { get; set; }

        public ICollection<Estudiantes> Estudiantes { get; set; }
    }
}
