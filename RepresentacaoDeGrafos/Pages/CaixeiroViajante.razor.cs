using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RepresentacaoDeGrafos.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepresentacaoDeGrafos.Pages
{
    public partial class CaixeiroViajante : ComponentBase
    {
        protected int codigoAtual = 0;

        public Vertice Vertice { get; set; }

        public Aresta Aresta { get; set; }

        public string MensagemDeErro { get; set; }

        public List<Vertice> Vertices { get; set; }

        public List<Aresta> Arestas { get; set; }

        public List<Economia> Economias { get; set; }

        public List<Cor> Cores { get; set; }

        public string Antecessor { get; set; }

        public string Sucessor { get; set; }

        public int CustoTotal { get; set; }

        protected override void OnInitialized()
        {
            Arestas = new List<Aresta>();
            Vertices = new List<Vertice>();
            Vertice = new Vertice();
            Aresta = new Aresta();
            Economias = new List<Economia>();

            InicializarCores();
            GerarDadosParaTeste();

            base.OnInitialized();
        }

        private void GerarDadosParaTeste()
        {
            var mesaDeTestes = new TesteDeGrafo();
            mesaDeTestes.GerarGrafoParaProblemaDoCaixeiroViajante();

            Vertices = mesaDeTestes.Vertices;
            Arestas = mesaDeTestes.Arestas;
        }

        private void InicializarCores()
        {
            Cores = new List<Cor>();

            var cinza = new Cor()
            {
                Nome = "Padrão",
                Hexadecimal = "#808988"
            };

            var vermelho = new Cor()
            {
                Nome = "Vermelho",
                Hexadecimal = "#b50c00"
            };

            var verde = new Cor()
            {
                Nome = "Verde",
                Hexadecimal = "#0dba3b"
            };

            var azul = new Cor()
            {
                Nome = "Azul",
                Hexadecimal = "#2267d6"
            };

            var amarelo = new Cor()
            {
                Nome = "Amarelo",
                Hexadecimal = "#fcba03"
            };

            var laranja = new Cor()
            {
                Nome = "Laranja",
                Hexadecimal = "#ed6d11"
            };

            var rosa = new Cor()
            {
                Nome = "Rosa",
                Hexadecimal = "#ed1199"
            };

            var roxo = new Cor()
            {
                Nome = "Roxo",
                Hexadecimal = "#8132a8"
            };

            Cores.Add(cinza);
            Cores.Add(vermelho);
            Cores.Add(verde);
            Cores.Add(azul);
            Cores.Add(amarelo);
            Cores.Add(laranja);
            Cores.Add(rosa);
            Cores.Add(roxo);
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            Renderizar();
            return base.OnAfterRenderAsync(firstRender);
        }

        private void Renderizar()
        {
            var verticesJson = JsonSerializer.Serialize(Vertices);
            var arestasJson = JsonSerializer.Serialize(Arestas);
            js.InvokeVoidAsync("desenharAlgoritmoDasEconomias", verticesJson, arestasJson);
        }

        public void AdicionarVertice()
        {
            MensagemDeErro = string.Empty;

            if (Vertices.Any(v => v.Identificador == Vertice.Identificador))
            {
                MensagemDeErro = "Existe um vertice já cadastrado com esse identificador.";
                return;
            }

            Vertice.Codigo = Vertices.Any() ? Vertices.Last().Codigo++ : 0;
            Vertices.Add(Vertice);

            Vertice = new Vertice();
        }
         
        public void AdicionarAresta()
        {
            MensagemDeErro = string.Empty;

            if (Arestas.Any(a => (a.Antecessor.Identificador == Antecessor && a.Sucessor.Identificador == Sucessor)
                || (a.Sucessor.Identificador == Antecessor && a.Antecessor.Identificador == Sucessor)))
            {
                MensagemDeErro = "Os vertices selecionados já estão ligados entre si.";
                return;
            }
            else if (Antecessor == Sucessor)
            {
                MensagemDeErro = "Não é possível fazer essa ligação.";
                return;
            }

            Aresta.Antecessor = Vertices.First(v => v.Identificador == Antecessor);
            Aresta.Sucessor = Vertices.First(v => v.Identificador == Sucessor);
            Aresta.Codigo = Arestas.Any() ? Arestas.Last().Codigo++ : 0;
            Arestas.Add(Aresta);

            Aresta = new Aresta();
        }

        public void RemoverVertice(Vertice vertice)
        {
            Arestas.RemoveAll(a => a.Antecessor == vertice || a.Sucessor == vertice);
            Vertices.Remove(vertice);
        }

        public void RemoverAresta(Aresta aresta)
        {
            Arestas.Remove(aresta);
        }

        public void PintarArestasDeIdaEVolta()
        {
            var arestasDaOrigem = Arestas.Where(a => a.Antecessor.Identificador == "Sede").ToList();
            var corLaranja = Cores.First(c => c.Nome == "Laranja");

            foreach (var aresta in arestasDaOrigem)
            {
                aresta.Cor = corLaranja.Hexadecimal;

                var arestaNova = new Aresta()
                {
                    Codigo = Arestas.Last().Codigo + 1,
                    Custo = aresta.Custo,
                    Antecessor = aresta.Sucessor,
                    Sucessor = aresta.Antecessor,
                    Cor = corLaranja.Hexadecimal
                };

                Arestas.Add(arestaNova);
            }
        }

        public void AplicarAlgoritmoDasEconomias()
        {
            var sede = Vertices.First(v => v.Identificador == "Sede");
            var corAtual = Cores.First(c => c.Nome == "Vermelho");

            const int distanciaMaxima = 30;
            var maiorCustoDeArestaParaASede = Arestas.Where(a => a.Antecessor == sede).OrderByDescending(a => a.Custo).First().Custo;

            var economias = ObterListaDeEconomias();
            var verticesPassados = new List<Vertice>();

            while (economias.Any())
            {
                var economiaAtual = economias.First();
                var valorTotalIndoEVoltandoParaSede = economiaAtual.ArestaJK.Custo * 2 + economiaAtual.ArestaKI.Custo * 2;
                var valorPegandoAtalho = economiaAtual.ArestaIJ.Custo + economiaAtual.ArestaJK.Custo + economiaAtual.ArestaKI.Custo;

                if (valorTotalIndoEVoltandoParaSede > valorPegandoAtalho)
                {
                    CustoTotal += economiaAtual.ArestaIJ.Custo;

                    economiaAtual.ArestaIJ.Cor = corAtual.Hexadecimal;

                    if (verticesPassados.Contains(economiaAtual.ArestaIJ.Antecessor) || verticesPassados.Contains(economiaAtual.ArestaIJ.Sucessor))
                    {
                        var verticeReferencia = verticesPassados.Contains(economiaAtual.ArestaIJ.Antecessor) ? economiaAtual.ArestaIJ.Antecessor : economiaAtual.ArestaIJ.Sucessor;
                        var arestasASeremRemovidas = Arestas.Where(a => a.Antecessor == verticeReferencia || a.Sucessor == verticeReferencia);
                        economias.RemoveAll(a => arestasASeremRemovidas.Contains(a.ArestaIJ));
                    }

                    verticesPassados.Add(economiaAtual.ArestaIJ.Antecessor);
                    verticesPassados.Add(economiaAtual.ArestaIJ.Sucessor);
                }
                else
                {
                    CustoTotal += valorTotalIndoEVoltandoParaSede;

                    economiaAtual.ArestaJK.Cor = corAtual.Hexadecimal;
                    economiaAtual.ArestaKI.Cor = corAtual.Hexadecimal;

                    var arestaJKContraria = Arestas.First(a => a.Antecessor == economiaAtual.ArestaJK.Sucessor && a.Sucessor == economiaAtual.ArestaJK.Antecessor);
                    arestaJKContraria.Cor = corAtual.Hexadecimal;

                    var arestaKIContraria = Arestas.First(a => a.Antecessor == economiaAtual.ArestaKI.Sucessor && a.Sucessor == economiaAtual.ArestaKI.Antecessor);
                    arestaKIContraria.Cor = corAtual.Hexadecimal;
                }

                Economias.Add(economiaAtual);
                economias.Remove(economiaAtual);
            }
        }

        private List<Economia> ObterListaDeEconomias()
        {
            var economias = new List<Economia>();
            var sede = Vertices.First(v => v.Identificador == "Sede");
            var arestasLigadasASede = Arestas.Where(a => a.Antecessor == sede || a.Sucessor == sede);

            foreach (var aresta in Arestas)
            {
                var i = aresta.Antecessor;
                var j = aresta.Sucessor;
                var arestasAPartirDaSede = arestasLigadasASede.Where(a => a.Antecessor == i || a.Sucessor == i || a.Antecessor == j || a.Sucessor == j);

                var economia = new Economia()
                {
                    ArestaIJ = aresta,
                    ArestaJK = arestasAPartirDaSede.First(a => a.Antecessor == j || a.Sucessor == j),
                    ArestaKI = arestasAPartirDaSede.First(a => a.Antecessor == i || a.Sucessor == i)
                };

                economias.Add(economia);
            }

            return economias.OrderByDescending(e => e.Valor).ToList();
        }
    }
}
