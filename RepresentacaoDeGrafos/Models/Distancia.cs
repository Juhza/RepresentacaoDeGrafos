using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepresentacaoDeGrafos.Models
{
    public class Distancia
    {
        public Cidade Origem { get; set; }

        public Cidade Destino { get; set; }

        public double Custo { get; set; }

        public string Cor { get; set; } = "#808988";
    }
}
