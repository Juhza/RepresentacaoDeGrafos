using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepresentacaoDeGrafos.Models
{
    public class Vertice
    {
        public int Codigo { get; set; }

        [Required]
        [MinLength(1)]
        public string Identificador { get; set; }

        public bool FoiVisitado { get; set; }
    }
}
