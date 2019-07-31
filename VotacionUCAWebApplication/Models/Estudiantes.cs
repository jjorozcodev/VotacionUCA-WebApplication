using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VotacionUCAWebApplication.Models
{
    public class Estudiantes
    {
        public int Id { get; set; }
        public string CodGrupo { get; set; }
        public string NombreCompleto { get; set; }
        public string NumCarnet { get; set; }

        public List<Votos> Votos { get; set; }
    }
}