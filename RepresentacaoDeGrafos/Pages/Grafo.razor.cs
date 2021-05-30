using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;
using RepresentacaoDeGrafos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepresentacaoDeGrafos.Pages
{
    public partial class Grafo : ComponentBase
    {
        protected Vertice verticeInicial;
        protected Vertice vertice;
        protected Aresta aresta;

        protected int contagemDeVertices = 0;
        protected int contagemDeArestas = 0;

        public string MensagemDeErro { get; set; }

        public string Resultado { get; set; }

        public string VerticesPassados { get; set; }

        public List<Vertice> Vertices { get; set; }

        public List<Aresta> Arestas { get; set; }

        public bool EhGrafoOrientado => Arestas.Count > 0 && Arestas.Any(a => a.EhOrientado);

        public string AntecessorSelecionado
        {
            get => aresta.Antecessor?.Identificador;
            set => aresta.Antecessor = Vertices.Single(v => v.Identificador == value);
        }

        public string SucessorSelecionado
        {
            get => aresta.Sucessor?.Identificador;
            set => aresta.Sucessor = Vertices.Single(v => v.Identificador == value);
        }

        public string VerticeInicialSelecionado
        {
            get => verticeInicial.Identificador; 
            set => verticeInicial = Vertices.Single(v => v.Identificador == value);
        }

        protected override void OnInitialized()
        {
            Arestas = new List<Aresta>();
            Vertices = new List<Vertice>();

            aresta = new Aresta();
            vertice = new Vertice();
            verticeInicial = new Vertice();

            // remover para iniciar zerado
            GerarDadosParaTeste();

            base.OnInitialized();
        }

        private void GerarDadosParaTeste()
        {
            var mesaDeTestes = new TesteDeGrafo();

            //mesaDeTestes.GerarGrafoParaBuscas(true);
            //mesaDeTestes.GerarGrafoParaBuscas(false);
            mesaDeTestes.GerarGrafoParaPrim();

            Vertices = mesaDeTestes.Vertices;
            Arestas = mesaDeTestes.Arestas;
        }
        
        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            Renderizar();
            return base.OnAfterRenderAsync(firstRender);
        }

        public void Renderizar()
        {
            var verticesJson = JsonSerializer.Serialize(Vertices);
            var arestasJson = JsonSerializer.Serialize(Arestas);
            js.InvokeVoidAsync("desenharGrafo", verticesJson, arestasJson);
        }

        public void AdicionarVertice()
        {
            MensagemDeErro = string.Empty;

            if (Vertices.Any(v => v.Identificador == vertice.Identificador))
            {
                MensagemDeErro = "Identificador já utilizado em outro vértice.";
                return;
            }

            vertice.Codigo = contagemDeVertices;
            Vertices.Add(vertice);
            vertice = new Vertice();
            contagemDeVertices++;
        }

        public void AdicionarAresta()
        {
            MensagemDeErro = string.Empty;

            if (Arestas.Any(a => a.Identificador == aresta.Identificador))
            {
                MensagemDeErro = "Identificador já utilizado em outra aresta.";
                return;
            }

            aresta.Codigo = contagemDeArestas;
            Arestas.Add(aresta);
            aresta = new Aresta()
            {
                EhOrientado = EhGrafoOrientado
            };
            contagemDeArestas++;
        }

        public void ExcluirAresta(Aresta aresta)
        {
            Arestas.Remove(aresta);
        }

        public void ExcluirVertice(Vertice vertice)
        {
            Arestas.RemoveAll(a => a.Antecessor == vertice || a.Sucessor == vertice);
            Vertices.Remove(vertice);
        }

        private void BuscarEmLargura()
        {
            MensagemDeErro = string.Empty;
            var verticeInicial = Vertices.FirstOrDefault(v => v.Identificador == VerticeInicialSelecionado);

            if (verticeInicial == null)
            {
                MensagemDeErro = "Nenhum vértice selecionado.";
                return;
            }

            var buscaEmLargura = AplicarBuscaEmLargura(verticeInicial);
            var resultado = buscaEmLargura.Select(v => v.Identificador).ToArray();
            Resultado = $"Busca em largura finalizada. Resultado: {string.Join(", ", resultado)}";
            ColorirArestas();
        }

        public List<Vertice> AplicarBuscaEmLargura(Vertice verticeAtual)
        {
            var marcados = new List<Vertice>();
            var fila = new List<Vertice>();

            fila.Add(verticeAtual);
            marcados.Add(verticeAtual);

            VerticesPassados = $"\r\nCaminho: {verticeAtual.Identificador}";

            while (fila.Count > 0)
            {
                fila.Remove(verticeAtual);
                var adjacentes = ObterAdjacentes(verticeAtual);

                foreach (var vertice in adjacentes)
                {
                    VerticesPassados += $" => {vertice.Identificador}";

                    if (!marcados.Contains(vertice))
                    {
                        fila.Add(vertice);
                        marcados.Add(vertice);
                    }
                }

                verticeAtual = fila.FirstOrDefault();
            }

            return marcados;
        }

        private Vertice[] ObterAdjacentes(Vertice vertice)
        {
            var todasAsArestas = Arestas
                .Where(a => a.Antecessor.Codigo == vertice.Codigo || a.Sucessor.Codigo == vertice.Codigo);

            if (EhGrafoOrientado)
            {
                var codigosDosVerticesOrientados = Arestas.Where(a => a.Antecessor.Codigo == vertice.Codigo).Select(a => a.Sucessor.Codigo).ToArray();
                var verticesAdjacentesOrientados = Vertices.Where(v => codigosDosVerticesOrientados.Contains(v.Codigo)).ToArray();
                return verticesAdjacentesOrientados;
            }
             
            var antecessores = Arestas.Where(a => a.Sucessor.Codigo == vertice.Codigo).Select(a => a.Antecessor.Codigo).ToArray();
            var sucessores = Arestas.Where(a => a.Antecessor.Codigo == vertice.Codigo).Select(a => a.Sucessor.Codigo).ToArray();
            var codigosDosNaoOrientados = antecessores.Concat(sucessores).ToArray();
            var adjacentesNaoOrientados = Vertices.Where(v => codigosDosNaoOrientados.Contains(v.Codigo) && v.Codigo != vertice.Codigo).ToArray();

            return adjacentesNaoOrientados;
        }

        public void BuscarEmProfundidade()
        {
            MensagemDeErro = string.Empty;
            var verticeInicial = Vertices.FirstOrDefault(v => v.Identificador == VerticeInicialSelecionado);

            if (verticeInicial == null)
            {
                MensagemDeErro = "Nenhum vértice selecionado.";
                return;
            }

            var marcados = new List<Vertice>();
            var buscaEmProfundidade = AplicarBuscaEmProfundidade(verticeInicial, marcados);
            var resultado = buscaEmProfundidade.Select(v => v.Identificador).ToArray();
            Resultado = $"Busca em profundidade finalizada. Resultado: {string.Join(", ", resultado)}";
            ColorirArestas();
        }

        public List<Vertice> AplicarBuscaEmProfundidade(Vertice verticeAtual, List<Vertice> marcados)
        {
            marcados.Add(verticeAtual);
            var adjacentes = ObterAdjacentes(verticeAtual);

            VerticesPassados = $"\r\nCaminho: {verticeAtual.Identificador}";

            foreach (var vertice in adjacentes)
            {
                VerticesPassados += $" => {vertice.Identificador}";

                if (!marcados.Contains(vertice))
                {
                    marcados = AplicarBuscaEmProfundidade(vertice, marcados);
                }
            }

            return marcados;
        }

        public void PRIM()
        {
            MensagemDeErro = string.Empty;
            VerticesPassados = string.Empty;

            if (EhGrafoOrientado)
            {
                MensagemDeErro = "Impossível aplicar o algoritmo de PRIM em grafos orientados.";
                return;
            }

            var verticeInicial = string.IsNullOrEmpty(VerticeInicialSelecionado) 
                ? Vertices.FirstOrDefault()
                : Vertices.FirstOrDefault(v => v.Identificador == VerticeInicialSelecionado);

            var resultado = AplicarPRIM(verticeInicial);
            var nomes = resultado.Select(v => v.Identificador).ToArray();
            var custoTotal = 0;

            foreach (var aresta in resultado)
            {
                custoTotal += aresta.Custo;
            }

            Resultado = $"Algoritmo de PRIM finalizado. Custo total: {custoTotal}";
            ColorirArestas(resultado);
        }

        private List<Aresta> AplicarPRIM(Vertice verticeInicial)
        {
            var resultado = new List<Aresta>();
            var arestasAdjacentes = ObterArestasAdjacentes(verticeInicial);
            var arestaDeMenorCusto = arestasAdjacentes.First();
            resultado.Add(arestaDeMenorCusto);

            foreach (var vertice in Vertices)
            {
                if (!vertice.Codigo.Equals(verticeInicial.Codigo))
                {
                    arestasAdjacentes = ObterArestasAdjacentes(vertice);
                    arestaDeMenorCusto = arestasAdjacentes.First();

                    if (!resultado.Contains(arestaDeMenorCusto))
                        resultado.Add(arestaDeMenorCusto);
                }
            }

            return resultado;
        }

        private List<Aresta> ObterArestasAdjacentes(Vertice vertice)
        {
            return Arestas
                .Where(a => a.Sucessor == vertice || a.Antecessor == vertice)
                .OrderBy(a => a.Custo)
                .ToList();
        }

        private void ColorirArestas(List<Aresta> arestas = null)
        {
            Arestas.ForEach(a => a.Cor = "#808988");

            if (arestas == null) return;

            foreach (var arestaColorida in arestas)
            {
                Arestas.Find(a => a.Codigo == arestaColorida.Codigo).Cor = "#F2AC29";
            }
        }
    }
}
