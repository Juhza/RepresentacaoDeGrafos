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
            Vertices = new List<Vertice>();
            Arestas = new List<Aresta>();
        }

        public void GerarGrafoParaBuscas(bool ehOrientado)
        {
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
                Sucessor = vertice2,
                Custo = 2,
                EhOrientado = ehOrientado
            };

            var aresta2 = new Aresta()
            {
                Codigo = 1,
                Identificador = "b",
                Antecessor = vertice1,
                Sucessor = vertice6,
                Custo = 6,
                EhOrientado = ehOrientado
            };

            var aresta3 = new Aresta()
            {
                Codigo = 2,
                Identificador = "c",
                Antecessor = vertice6,
                Sucessor = vertice4,
                Custo = 3,
                EhOrientado = ehOrientado
            };

            var aresta4 = new Aresta()
            {
                Codigo = 3,
                Identificador = "d",
                Antecessor = vertice4,
                Sucessor = vertice5,
                Custo = 7,
                EhOrientado = ehOrientado
            };

            var aresta5 = new Aresta()
            {
                Codigo = 4,
                Identificador = "e",
                Antecessor = vertice5,
                Sucessor = vertice0,
                Custo = 1,
                EhOrientado = ehOrientado
            };

            var aresta6 = new Aresta()
            {
                Codigo = 5,
                Identificador = "f",
                Antecessor = vertice0,
                Sucessor = vertice1,
                Custo = 5,
                EhOrientado = ehOrientado
            };

            var aresta7 = new Aresta()
            {
                Codigo = 6,
                Identificador = "g",
                Antecessor = vertice6,
                Sucessor = vertice3,
                Custo = 3,
                EhOrientado = ehOrientado
            };
            
            Arestas.Add(aresta1);
            Arestas.Add(aresta2);
            Arestas.Add(aresta3);
            Arestas.Add(aresta4);
            Arestas.Add(aresta5);
            Arestas.Add(aresta6);
            Arestas.Add(aresta7);
        }

        public void GerarGrafoParaPrim()
        {
            var verticeA = new Vertice()
            {
                Codigo = 0,
                Identificador = "A"
            };

            var verticeB = new Vertice()
            {
                Codigo = 1,
                Identificador = "B"
            };

            var verticeC = new Vertice()
            {
                Codigo = 2,
                Identificador = "C"
            };

            var verticeD = new Vertice()
            {
                Codigo = 3,
                Identificador = "D"
            };

            var verticeE = new Vertice()
            {
                Codigo = 4,
                Identificador = "E"
            };

            Vertices.Add(verticeA);
            Vertices.Add(verticeB);
            Vertices.Add(verticeC);
            Vertices.Add(verticeD);
            Vertices.Add(verticeE);

            var aresta1 = new Aresta()
            {
                Codigo = 0,
                Identificador = "ab",
                Antecessor = verticeA,
                Sucessor = verticeB,
                Custo = 9,
                EhOrientado = false
            };

            var aresta2 = new Aresta()
            {
                Codigo = 1,
                Identificador = "be",
                Antecessor = verticeB,
                Sucessor = verticeE,
                Custo = 5,
                EhOrientado = false
            };

            var aresta3 = new Aresta()
            {
                Codigo = 2,
                Identificador = "ec",
                Antecessor = verticeE,
                Sucessor = verticeC,
                Custo = 5,
                EhOrientado = false
            };

            var aresta4 = new Aresta()
            {
                Codigo = 3,
                Identificador = "ca",
                Antecessor = verticeC,
                Sucessor = verticeA,
                Custo = 5,
                EhOrientado = false
            };

            var aresta5 = new Aresta()
            {
                Codigo = 4,
                Identificador = "ad",
                Antecessor = verticeA,
                Sucessor = verticeD,
                Custo = 2,
                EhOrientado = false
            };

            var aresta6 = new Aresta()
            {
                Codigo = 5,
                Identificador = "db",
                Antecessor = verticeD,
                Sucessor = verticeB,
                Custo = 6,
                EhOrientado = false
            };

            var aresta7 = new Aresta()
            {
                Codigo = 6,
                Identificador = "de",
                Antecessor = verticeD,
                Sucessor = verticeE,
                Custo = 3,
                EhOrientado = false
            };

            var aresta8 = new Aresta()
            {
                Codigo = 7,
                Identificador = "dc",
                Antecessor = verticeD,
                Sucessor = verticeC,
                Custo = 4,
                EhOrientado = false
            };

            Arestas.Add(aresta1);
            Arestas.Add(aresta2);
            Arestas.Add(aresta3);
            Arestas.Add(aresta4);
            Arestas.Add(aresta5);
            Arestas.Add(aresta6);
            Arestas.Add(aresta7);
            Arestas.Add(aresta8);
        }
    }
}
