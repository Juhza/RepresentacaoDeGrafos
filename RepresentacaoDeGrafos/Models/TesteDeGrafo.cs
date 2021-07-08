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

        public List<Distancia> Distancias { get; set; }

        public List<Cidade> Cidades { get; set; }

        public TesteDeGrafo()
        {
            Vertices = new List<Vertice>();
            Arestas = new List<Aresta>();
            Cidades = new List<Cidade>();
            Distancias = new List<Distancia>();
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

        public void GerarGrafoEstadoDoParana()
        {
            var curitiba = new Cidade()
            {
                Codigo = 0,
                Identificador = "Curitiba",
                Latitude = -25.4284,
                Longitude = -49.2733
            };

            var paranagua = new Cidade()
            {
                Codigo = 1,
                Identificador = "Paranaguá",
                Latitude = -25.5205,
                Longitude = -48.5095
            };

            var pontaGrossa = new Cidade()
            {
                Codigo = 2,
                Identificador = "Ponta Grossa",
                Latitude = -25.0945,
                Longitude = -50.1633
            };

            var londrina = new Cidade()
            {
                Codigo = 3,
                Identificador = "Londrina",
                Latitude = -23.2927,
                Longitude = -51.1732
            };

            var maringa = new Cidade()
            {
                Codigo = 4,
                Identificador = "Maringá",
                Latitude = -23.4273,
                Longitude = -51.9375
            };

            var umuarama = new Cidade()
            {
                Codigo = 5,
                Identificador = "Umuarama",
                Latitude = -23.7641,
                Longitude = -53.3184
            };

            var toledo = new Cidade()
            {
                Codigo = 6,
                Identificador = "Toledo",
                Latitude = -24.7199,
                Longitude = -53.7433
            };

            var cascavel = new Cidade()
            {
                Codigo = 7,
                Identificador = "Cascavel",
                Latitude = -24.9555,
                Longitude = -53.4552
            };

            var fozDoIguacu = new Cidade()
            {
                Codigo = 8,
                Identificador = "Foz do Iguaçu",
                Latitude = -25.5469,
                Longitude = -54.5882
            };

            var guarapuava = new Cidade()
            {
                Codigo = 9,
                Identificador = "Guarapuava",
                Latitude = -25.3935,
                Longitude = -51.4562
            };

            var franciscoBeltrao = new Cidade()
            {
                Codigo = 10,
                Identificador = "Francisco Beltrão",
                Latitude = -26.0783,
                Longitude = -53.0531
            };

            var saoMateusDoSul = new Cidade()
            {
                Codigo = 11,
                Identificador = "São Mateus do Sul",
                Latitude = -25.8767,
                Longitude = -50.3842
            };

            Cidades.Add(curitiba);
            Cidades.Add(paranagua);
            Cidades.Add(pontaGrossa);
            Cidades.Add(londrina);
            Cidades.Add(maringa);
            Cidades.Add(umuarama);
            Cidades.Add(toledo);
            Cidades.Add(cascavel);
            Cidades.Add(fozDoIguacu);
            Cidades.Add(guarapuava);
            Cidades.Add(franciscoBeltrao);
            Cidades.Add(saoMateusDoSul);

            var ligacao0 = new Distancia()
            {
                Origem = curitiba,
                Destino = paranagua
            };

            var ligacao1 = new Distancia()
            {
                Origem = curitiba,
                Destino = pontaGrossa
            };

            var ligacao2 = new Distancia()
            {
                Origem = pontaGrossa,
                Destino = londrina
            };

            var ligacao3 = new Distancia()
            {
                Origem = pontaGrossa,
                Destino = maringa
            };

            var ligacao4 = new Distancia()
            {
                Origem = pontaGrossa,
                Destino = guarapuava
            };

            var ligacao5 = new Distancia()
            {
                Origem = londrina,
                Destino = maringa
            };

            var ligacao6 = new Distancia()
            {
                Origem = maringa,
                Destino = umuarama
            };

            var ligacao7 = new Distancia()
            {
                Origem = umuarama,
                Destino = toledo
            };

            var ligacao8 = new Distancia()
            {
                Origem = toledo,
                Destino = cascavel
            };

            var ligacao9 = new Distancia()
            {
                Origem = cascavel,
                Destino = fozDoIguacu
            };


            var ligacao10 = new Distancia()
            {
                Origem = cascavel,
                Destino = guarapuava
            };


            var ligacao11 = new Distancia()
            {
                Origem = cascavel,
                Destino = franciscoBeltrao
            };

            var ligacao12 = new Distancia()
            {
                Origem = franciscoBeltrao,
                Destino = saoMateusDoSul
            };

            var ligacao13 = new Distancia()
            {
                Origem = saoMateusDoSul,
                Destino = curitiba
            };

            Distancias.Add(ligacao0);
            Distancias.Add(ligacao1);
            Distancias.Add(ligacao2);
            Distancias.Add(ligacao3);
            Distancias.Add(ligacao4);
            Distancias.Add(ligacao5);
            Distancias.Add(ligacao6);
            Distancias.Add(ligacao7);
            Distancias.Add(ligacao8);
            Distancias.Add(ligacao9);
            Distancias.Add(ligacao10);
            Distancias.Add(ligacao11);
            Distancias.Add(ligacao12);
            Distancias.Add(ligacao13);
        }

        public void GerarGrafoParaProblemaDoCaixeiroViajante()
        {
            var origem = new Vertice()
            {
                Codigo = 0,
                Identificador = "Sede"
            };

            var clienteA = new Vertice()
            {
                Codigo = 1,
                Identificador = "Cliente A"
            };

            var clienteB = new Vertice()
            {
                Codigo = 2,
                Identificador = "Cliente B"
            };

            var clienteC = new Vertice()
            {
                Codigo = 3,
                Identificador = "Cliente C"
            };

            var clienteD = new Vertice()
            {
                Codigo = 4,
                Identificador = "Cliente D"
            };

            var clienteE = new Vertice()
            {
                Codigo = 5,
                Identificador = "Cliente E"
            };

            Vertices.Add(origem);
            Vertices.Add(clienteA);
            Vertices.Add(clienteB);
            Vertices.Add(clienteC);
            Vertices.Add(clienteD);
            Vertices.Add(clienteE);

            var aresta0 = new Aresta()
            {
                Codigo = 0,
                Custo = 10,
                Antecessor = clienteA,
                Sucessor = clienteB
            };

            var aresta1 = new Aresta()
            {
                Codigo = 1,
                Custo = 7,
                Antecessor = clienteB,
                Sucessor = clienteC
            };

            var aresta2 = new Aresta()
            {
                Codigo = 2,
                Custo = 8,
                Antecessor = clienteC,
                Sucessor = clienteD
            };

            var aresta3 = new Aresta()
            {
                Codigo = 3,
                Custo = 10,
                Antecessor = clienteD,
                Sucessor = clienteE
            };

            var aresta4 = new Aresta()
            {
                Codigo = 4,
                Custo = 10,
                Antecessor = clienteE,
                Sucessor = clienteA
            };

            var aresta5 = new Aresta()
            {
                Codigo = 5,
                Custo = 18,
                Antecessor = clienteA,
                Sucessor = clienteD
            };

            var aresta6 = new Aresta()
            {
                Codigo = 6,
                Custo = 12,
                Antecessor = clienteA,
                Sucessor = clienteC
            };

            var aresta7 = new Aresta()
            {
                Codigo = 7,
                Custo = 12,
                Antecessor = clienteB,
                Sucessor = clienteE
            };

            var aresta8 = new Aresta()
            {
                Codigo = 8,
                Custo = 12,
                Antecessor = clienteB,
                Sucessor = clienteD
            };

            var aresta9 = new Aresta()
            {
                Codigo = 9,
                Custo = 12,
                Antecessor = clienteC,
                Sucessor = clienteE
            };

            var aresta10 = new Aresta()
            {
                Codigo = 10,
                Custo = 10,
                Antecessor = origem,
                Sucessor = clienteA
            };

            var aresta11 = new Aresta()
            {
                Codigo = 11,
                Custo = 5,
                Antecessor = origem,
                Sucessor = clienteB
            };

            var aresta12 = new Aresta()
            {
                Codigo = 12,
                Custo = 5,
                Antecessor = origem,
                Sucessor = clienteC
            };

            var aresta13 = new Aresta()
            {
                Codigo = 13,
                Custo = 10,
                Antecessor = origem,
                Sucessor = clienteD
            };

            var aresta14 = new Aresta()
            {
                Codigo = 14,
                Custo = 10,
                Antecessor = origem,
                Sucessor = clienteE
            };

            Arestas.Add(aresta0);
            Arestas.Add(aresta1);
            Arestas.Add(aresta2);
            Arestas.Add(aresta3);
            Arestas.Add(aresta4);
            Arestas.Add(aresta5);
            Arestas.Add(aresta6);
            Arestas.Add(aresta7);
            Arestas.Add(aresta8);
            Arestas.Add(aresta9);
            Arestas.Add(aresta10);
            Arestas.Add(aresta11);
            Arestas.Add(aresta12);
            Arestas.Add(aresta13);
            Arestas.Add(aresta14);
        }
    }
}
