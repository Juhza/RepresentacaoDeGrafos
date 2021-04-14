using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepresentacaoDeGrafos2.Models
{
    public class Vertice
    {
        public int Codigo { get; set; }

        public string Identificador { get; set; }

        public bool FoiVisitado { get; set; }
    }
}