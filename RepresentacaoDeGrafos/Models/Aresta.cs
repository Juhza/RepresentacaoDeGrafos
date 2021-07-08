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

        public string Identificador { get; set; }

        public Vertice Antecessor { get; set; }

        public Vertice Sucessor { get; set; }

        [Range(1, 1000)]
        public int Custo { get; set; }

        public bool EhOrientado { get; set; }

        public string Cor { get; set; } = "#808988";
    }
}
