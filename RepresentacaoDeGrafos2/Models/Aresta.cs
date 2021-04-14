using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepresentacaoDeGrafos2.Models
{
    public class Aresta
    {
        public int Codigo { get; set; }

        public string Identificador { get; set; }

        public Vertice Antecessor { get; set; }

        public Vertice Sucessor { get; set; }

        public int Custo { get; set; }

        public bool EhOrdenado { get; set; }

        public bool FoiVisitada { get; set; }
    }
}