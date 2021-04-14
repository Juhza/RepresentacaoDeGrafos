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

        public List<Vertice> Vertices { get; set; }

        public List<Aresta> Arestas { get; set; }

        public bool EhGrafoOrdenado => Arestas.Count > 0 && Arestas.Any(a => a.EhOrdenado);

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

            var teste = new TesteDeGrafo();
            Vertices = teste.Vertices;
            Arestas = teste.Arestas;

            base.OnInitialized();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            Renderizar();
            return base.OnAfterRenderAsync(firstRender);
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
                EhOrdenado = EhGrafoOrdenado
            };
            contagemDeArestas++;
        }

        public void Renderizar()
        {
            var verticesJson = JsonSerializer.Serialize(Vertices);
            var arestasJson = JsonSerializer.Serialize(Arestas);
            js.InvokeVoidAsync("desenharGrafo", verticesJson, arestasJson);
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
                MensagemDeErro = "Nenhum vertice selecionado.";
                return;
            }

            var buscaEmLargura = RealizarBuscaEmLargura(verticeInicial);
            var resultado = buscaEmLargura.Select(v => v.Identificador).ToArray();
            Resultado = $"Busca em largura finalizada. Resultado: {string.Join(", ", resultado)}";
        }

        public List<Vertice> RealizarBuscaEmLargura(Vertice verticeAtual)
        {
            var marcados = new List<Vertice>();
            var fila = new List<Vertice>();

            fila.Add(verticeAtual);
            marcados.Add(verticeAtual);
            // para cada vertice
            // buscar todos adjacentes e adicionar na fila
            // proximo vertice = primeiro da fila

            // 0 1 5 2 6 4 3

            var count = 0;

            while (fila.Count > 0)
            {
                fila.Remove(verticeAtual);
                var adjacentes = ObterAdjacentes(verticeAtual);

                foreach (var vertice in adjacentes)
                {
                    count++;
                    if (!marcados.Contains(vertice))
                    {
                        //explorar verticeInicial vertice
                        fila.Add(vertice);
                        marcados.Add(vertice);
                    }
                    else
                    {
                        // se vw não explorada, entao explorar vw
                        /*var aresta = Arestas.Where(a => a.Antecessor == verticeAtual && a.Sucessor == vertice).FirstOrDefault();
                        if (!aresta.FoiVisitada)
                        {
                            verticeAtual = aresta.Sucessor;
                        }*/
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
            var ehOrdenado = todasAsArestas
                .FirstOrDefault()
                .EhOrdenado;

            if (ehOrdenado)
            {
                var codigosDosOrdenados = Arestas.Where(a => a.Antecessor.Codigo == vertice.Codigo).Select(a => a.Sucessor.Codigo).ToArray();
                var adjacentesOrdenados = Vertices.Where(v => codigosDosOrdenados.Contains(v.Codigo)).ToArray();
                return adjacentesOrdenados;
            }
             
            var antecessores = Arestas.Where(a => a.Sucessor.Codigo == vertice.Codigo).Select(a => a.Antecessor.Codigo).ToArray();
            var sucessores = Arestas.Where(a => a.Antecessor.Codigo == vertice.Codigo).Select(a => a.Sucessor.Codigo).ToArray();
            var codigosDosNaoOrdenados = antecessores.Concat(sucessores).ToArray();
            var adjacentesNaoOrdenados = Vertices.Where(v => codigosDosNaoOrdenados.Contains(v.Codigo) && v.Codigo != vertice.Codigo).ToArray();

            return adjacentesNaoOrdenados;
        }

        public void BuscarEmProfundidade()
        {
            MensagemDeErro = string.Empty;
            var verticeInicial = Vertices.FirstOrDefault(v => v.Identificador == VerticeInicialSelecionado);

            if (verticeInicial == null)
            {
                MensagemDeErro = "Nenhum vertice selecionado.";
                return;
            }

            var marcados = new List<Vertice>();
            var buscaEmProfundidade = RealizarBuscaEmProfundidade(verticeInicial, marcados);
            var resultado = buscaEmProfundidade.Select(v => v.Identificador).ToArray();
            Resultado = $"Busca em profundidade finalizada. Resultado: {string.Join(", ", resultado)}";
        }

        public List<Vertice> RealizarBuscaEmProfundidade(Vertice verticeAtual, List<Vertice> marcados)
        {
            marcados.Add(verticeAtual);
            var adjacentes = ObterAdjacentes(verticeAtual);

            foreach (var vertice in adjacentes)
            {
                if (!marcados.Contains(vertice))
                {
                    marcados = RealizarBuscaEmProfundidade(vertice, marcados);
                }
            }

            return marcados;
        }

        public void AplicarAlgoritmoDeRoy(Vertice verticeInicial)
        {

        }
    }
}
