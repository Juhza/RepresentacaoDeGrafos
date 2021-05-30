using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RepresentacaoDeGrafos.Models
{
    public class Cidade : Vertice
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
        
        public int Grau { get; set; }

        public double DistanciaManhattan { get; set; }

        public string Cor { get; set; } = "#808988";
    }
}
