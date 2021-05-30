using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RepresentacaoDeGrafos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RepresentacaoDeGrafos.Pages
{
    public partial class Coloracao : ComponentBase
    {
        protected int count = 0;
        protected Cidade cidadeInicial;
        protected Cidade cidadeFinal;
        protected Cidade cidade;
        protected Distancia ligacao;

        public string MensagemDeErro { get; set; }

        public string Resultado { get; set; }

        public List<Cidade> Cidades { get; set; }

        public List<Distancia> Distancias { get; set; }

        public string AntecessorSelecionado
        {
            get => ligacao.Origem?.Identificador;
            set => ligacao.Origem = Cidades.Single(v => v.Identificador == value);
        }

        public string SucessorSelecionado
        {
            get => ligacao.Destino?.Identificador;
            set => ligacao.Destino = Cidades.Single(v => v.Identificador == value);
        }

        public string CidadeInicial
        {
            get => cidadeInicial.Identificador;
            set => cidadeInicial = Cidades.Single(v => v.Identificador == value);
        }

        public string CidadeFinal
        {
            get => cidadeFinal.Identificador;
            set => cidadeFinal = Cidades.Single(v => v.Identificador == value);
        }

        public List<Cor> Cores { get; set; }

        public bool EhCaminhosMinimos { get; set; }

        protected override void OnInitialized()
        {
            Distancias = new List<Distancia>();
            Cidades = new List<Cidade>();

            ligacao = new Distancia();
            cidade = new Cidade();
            cidadeInicial = new Cidade();
            cidadeFinal = new Cidade();

            // remover para iniciar zerado
            GerarDadosParaTeste();
            InicializarCores();

            base.OnInitialized();
        }

        private void GerarDadosParaTeste()
        {
            var mesaDeTestes = new TesteDeGrafo();
            mesaDeTestes.GerarGrafoEstadoDoParana();

            Cidades = mesaDeTestes.Cidades;
            Distancias = mesaDeTestes.Distancias;
        }

        private void InicializarCores()
        {
            Cores = new List<Cor>();

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
            if (EhCaminhosMinimos)
                RenderizarCaminhosMinimos();
            else
                RenderizarWelsh();

            EhCaminhosMinimos = false;
        }

        private void RenderizarWelsh()
        {
            var cidadesJson = JsonSerializer.Serialize(Cidades);
            var distanciasJson = JsonSerializer.Serialize(Distancias);
            js.InvokeVoidAsync("desenharWelsh", cidadesJson, distanciasJson);
        }

        private void RenderizarCaminhosMinimos()
        {
            var cidadesJson = JsonSerializer.Serialize(Cidades);
            var distanciasJson = JsonSerializer.Serialize(Distancias);
            js.InvokeVoidAsync("desenharCaminhosMinimos", cidadesJson, distanciasJson);
        }

        public void AdicionarCidade()
        {
            MensagemDeErro = string.Empty;

            if (Cidades.Any(v => v.Identificador == cidade.Identificador))
            {
                MensagemDeErro = "Existe uma cidade já cadastrada com esse nome.";
                return;
            }

            cidade.Codigo = count;
            Cidades.Add(cidade);
            cidade = new Cidade();

            count++;
        }

        public void ExcluirCidade(Cidade cidade)
        {
            Distancias.RemoveAll(d => d.Origem.Identificador == cidade.Identificador || d.Destino.Identificador == cidade.Identificador);
            Cidades.Remove(cidade);
        }

        public void AdicionarLigacaoEntreCidades()
        {
            MensagemDeErro = string.Empty;

            if (Distancias.Any(d => (d.Origem.Identificador == AntecessorSelecionado && d.Destino.Identificador == SucessorSelecionado) 
                || (d.Origem.Identificador == SucessorSelecionado && d.Destino.Identificador == AntecessorSelecionado)))
            {
                MensagemDeErro = "As cidades selecionadas já estão ligadas entre si.";
                return;
            }
            else if (AntecessorSelecionado == SucessorSelecionado)
            {
                MensagemDeErro = "Não é possível fazer essa ligação.";
                return;
            }

            Distancias.Add(ligacao);
            ligacao = new Distancia();
        }

        public void ExcluirLigacaoEntreCidades(Distancia ligacao) 
            => Distancias.Remove(ligacao);

        public void RecalcularDistanciasManhattan()
        {
            Cidades.ForEach(cidade =>
                cidade.DistanciaManhattan = Math.Abs(cidadeFinal.Latitude - cidade.Latitude) + Math.Abs(cidadeFinal.Longitude - cidade.Longitude));
        }

        public void RecalcularGrausDosVertices()
        {
            Cidades.ForEach(cidade => 
                cidade.Grau = Distancias.Count(d => d.Origem.Identificador == cidade.Identificador || d.Destino.Identificador == cidade.Identificador));
        }

        public void ExecutarAlgoritmoDeWelshPowell()
        {
            EhCaminhosMinimos = false;

            RecalcularGrausDosVertices();
            ResetarCoresDoGrafo();

            var verticesReorganizadosPorGrau = Cidades.OrderByDescending(cidade => cidade.Grau);
            var corAtual = Cores.First();
            var cinzaPadrao = "#808988";

            while (Cidades.Any(cidade => cidade.Cor == cinzaPadrao))
            {
                foreach (var cidade in Cidades)
                {
                    var adjacentes = BuscarCidadesVizinhas(cidade);
                    if (cidade.Cor == cinzaPadrao && !adjacentes.Any(a => a.Cor == corAtual.Hexadecimal))
                    {
                        cidade.Cor = corAtual.Hexadecimal;
                    }
                }

                corAtual = Cores.First(cor => !Cidades.Any(cidade => cidade.Cor == cor.Hexadecimal));
            }
        }

        private void ResetarCoresDoGrafo()
        {
            Cidades.ForEach(cidade => cidade.Cor = "#808988");
            Distancias.ForEach(distancia => distancia.Cor = "#808988");
        }

        private List<Cidade> BuscarCidadesVizinhas(Cidade cidade)
        {
            var arestas = BuscarLigacoes(cidade);
            var cidades = arestas.Select(a => a.Origem.Identificador == cidade.Identificador ? a.Destino : a.Origem);
            return cidades.ToList();
        }

        private List<Distancia> BuscarLigacoes(Cidade cidade) 
            => Distancias.Where(d => d.Destino.Identificador == cidade.Identificador || d.Origem.Identificador == cidade.Identificador).ToList();

        public void ExecutarAlgoritmoAStarParaCaminhosMinimos()
        {
            if (string.IsNullOrEmpty(cidadeInicial.Identificador) || string.IsNullOrEmpty(cidadeFinal.Identificador)) return;

            Resultado = string.Empty;
            ResetarCoresDoGrafo();
            RecalcularGrausDosVertices();
            RecalcularDistanciasManhattan();

            var cidadeAtual = cidadeInicial;
            var corSelecionada = Cores.FirstOrDefault(cor => cor.Nome == "Laranja");
            var resultado = new StringBuilder();
            var teste = 0;
            var testeCusto = 0.0;
            var distanciaTotalParaACidadeInicial = 0.0;

            Cidades.ForEach(cidade => Console.WriteLine($"{cidade.Identificador} grau {cidade.Grau}"));

            while (cidadeFinal.Cor != corSelecionada.Hexadecimal && teste < 15)
            {
                resultado.Append($"{cidadeAtual.Identificador} => ");
                var ligacoesDaCidadeAtual = Distancias
                    .Where(distancia => (distancia.Origem == cidadeAtual || distancia.Destino == cidadeAtual) && distancia.Cor != corSelecionada.Hexadecimal)
                    .ToList();

                foreach (var ligacao in ligacoesDaCidadeAtual)
                {
                    var cidadeParaCalcular = ObterCidadeOpostaNaLigacao(ligacao, cidadeAtual);
                    var distanciaParaACidadeAnterior = Math.Abs(cidadeAtual.Latitude - cidadeParaCalcular.Latitude) + Math.Abs(cidadeInicial.Longitude - cidadeParaCalcular.Longitude);
                    var distanciaParaACidadeInicial = distanciaParaACidadeAnterior + distanciaTotalParaACidadeInicial;
                    ligacao.Custo = cidadeParaCalcular.DistanciaManhattan + distanciaParaACidadeInicial;
                }

                var teste3 = resultado.ToString();
                var ligacaoDeMenorCusto = ligacoesDaCidadeAtual.OrderByDescending(ligacao => ligacao.Custo).First();
                var proximaCidade = ObterCidadeOpostaNaLigacao(ligacaoDeMenorCusto, cidadeAtual);

                while (((proximaCidade.Grau == 1 && proximaCidade.Identificador != cidadeFinal.Identificador) || proximaCidade.Cor == corSelecionada.Hexadecimal) 
                    && ligacoesDaCidadeAtual.Count > 1)
                {
                    ligacoesDaCidadeAtual.Remove(ligacaoDeMenorCusto);
                    var bla = ligacoesDaCidadeAtual.OrderByDescending(ligacao => ligacao.Custo);

                    if (bla.First() == null)
                    {
                        Console.WriteLine($"{ligacaoDeMenorCusto.Origem} para {ligacaoDeMenorCusto.Destino}"); 
                        return;
                    }

                    ligacaoDeMenorCusto = bla.First();
                    proximaCidade = ObterCidadeOpostaNaLigacao(ligacaoDeMenorCusto, cidadeAtual);
                }

                distanciaTotalParaACidadeInicial += ligacaoDeMenorCusto.Custo - proximaCidade.DistanciaManhattan;
                ligacaoDeMenorCusto.Cor = corSelecionada.Hexadecimal;
                cidadeAtual.Cor = corSelecionada.Hexadecimal;
                cidadeAtual = proximaCidade;
                testeCusto += ligacaoDeMenorCusto.Custo;
                teste++;
            }

            resultado.Append(cidadeAtual.Identificador);
            Resultado += $"{resultado.ToString()} {teste} custo {testeCusto}";
            EhCaminhosMinimos = false;
        }

        private Cidade ObterCidadeOpostaNaLigacao(Distancia ligacao, Cidade cidadeReferencia) 
            => ligacao.Origem == cidadeReferencia ? ligacao.Destino : ligacao.Origem;
    }
}
