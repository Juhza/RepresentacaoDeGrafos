using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepresentacaoDeGrafos.Models
{
    public class TesteDeGrafo
    {
        public List<Vertice> Vertices { get; set; }

        public List<Aresta> Arestas { get; set; }

        public TesteDeGrafo()
        {
            GerarGrafo();
        }

        public void GerarGrafo()
        {
            Vertices = new List<Vertice>();
            Arestas = new List<Aresta>();

            var vertice0 = new Vertice()
            {
                Codigo = 0,
                Identificador = "0"
            };

            var vertice1 = new Vertice()
            {
                Codigo = 1,
                Identificador = "1"
            };

            var vertice2 = new Vertice()
            {
                Codigo = 2,
                Identificador = "2"
            };

            var vertice3 = new Vertice()
            {
                Codigo = 3,
                Identificador = "3"
            };

            var vertice4 = new Vertice()
            {
                Codigo = 4,
                Identificador = "4"
            };

            var vertice5 = new Vertice()
            {
                Codigo = 5,
                Identificador = "5"
            };

            var vertice6 = new Vertice()
            {
                Codigo = 6,
                Identificador = "6"
            };

            Vertices.Add(vertice0);
            Vertices.Add(vertice1);
            Vertices.Add(vertice2);
            Vertices.Add(vertice3);
            Vertices.Add(vertice4);
            Vertices.Add(vertice5);
            Vertices.Add(vertice6);

            var aresta1 = new Aresta()
            {
                Codigo = 0,
                Identificador = "a",
                Antecessor = vertice1,
                Sucessor = vertice2
            };

            var aresta2 = new Aresta()
            {
                Codigo = 1,
                Identificador = "b",
                Antecessor = vertice1,
                Sucessor = vertice6
            };

            var aresta3 = new Aresta()
            {
                Codigo = 2,
                Identificador = "c",
                Antecessor = vertice6,
                Sucessor = vertice4
            };

            var aresta4 = new Aresta()
            {
                Codigo = 3,
                Identificador = "d",
                Antecessor = vertice4,
                Sucessor = vertice5
            };

            var aresta5 = new Aresta()
            {
                Codigo = 4,
                Identificador = "e",
                Antecessor = vertice5,
                Sucessor = vertice0
            };

            var aresta6 = new Aresta()
            {
                Codigo = 5,
                Identificador = "f",
                Antecessor = vertice0,
                Sucessor = vertice1
            };

            var aresta7 = new Aresta()
            {
                Codigo = 6,
                Identificador = "g",
                Antecessor = vertice6,
                Sucessor = vertice3
            };
            
            Arestas.Add(aresta1);
            Arestas.Add(aresta2);
            Arestas.Add(aresta3);
            Arestas.Add(aresta4);
            Arestas.Add(aresta5);
            Arestas.Add(aresta6);
            Arestas.Add(aresta7);
        }
    }
}
