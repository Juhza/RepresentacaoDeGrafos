using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepresentacaoDeGrafos.Models
{
    public class Economia
    {
        public Aresta ArestaJK { get; set; }

        public Aresta ArestaKI { get; set; }

        public Aresta ArestaIJ { get; set; }

        public int Valor => ArestaJK.Custo + ArestaKI.Custo - ArestaIJ.Custo;
    }
}
