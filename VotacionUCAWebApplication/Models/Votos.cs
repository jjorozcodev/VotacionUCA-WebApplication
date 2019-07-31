using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VotacionUCAWebApplication.Models
{
    public class Votos
    {
        public int Id { get; set; }
        public int IdEstudiante { get; set; }
        public int IdVotacion { get; set; }
    }
}