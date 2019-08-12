using System;
using System.Collections.Generic;
using System.Linq;
namespace VotacionUCAWebApplication.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public bool Gestiona { get; set; }
    }
}