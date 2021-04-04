using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepresentacaoDeGrafos.Models
{
    public class Aresta
    {
        public int Codigo { get; set; }

        [Required]
        public string Identificador { get; set; }

        [Required]
        public Vertice Antecessor { get; set; }

        [Required]
        public Vertice Sucessor { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Custo { get; set; }

        [Required]
        public bool EhOrdenado { get; set; }
    }
}
