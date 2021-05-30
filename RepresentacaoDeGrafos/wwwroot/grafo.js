function desenharGrafo(vertices, arestas) {
    vertices = eval(vertices);
    arestas = eval(arestas);

    var dadosDosVertices = new Array();
    var dadosDasArestas = new Array();

    for (var i = 0; i < vertices.length; i++) {
        var vertice = {
            id: vertices[i].Codigo,
            label: vertices[i].Identificador,
            color: "#3B413F",
            font: { color: "white" }
        };
        dadosDosVertices.push(vertice);
    }

    for (var i = 0; i < arestas.length; i++) {
        var ehOrientado = arestas[i].EhOrientado;
        var aresta = {
            id: i,
            label: arestas[i].Identificador + ": " + arestas[i].Custo,
            from: arestas[i].Antecessor.Codigo,
            to: arestas[i].Sucessor.Codigo,
            color: {
                color: arestas[i].Cor
            },
            background: {
                enabled: arestas[i].Cor == "#808988" || ehOrientado ? false : true,
                size: 5, 
                color: "#F2AC29"
            },
            arrows: {
                to: {
                    enabled: ehOrientado,
                    type: "normal"
                }
            }
        };
        dadosDasArestas.push(aresta);
    }

    var nodes = new vis.DataSet(dadosDosVertices);
    var edges = new vis.DataSet(dadosDasArestas);

    // create a network
    var container = document.getElementById("grafo");
    var data = {
        nodes: nodes,
        edges: edges,
    };
    var options = {
        physics: {
            enabled: false,
        },
        nodes: {
            shape: "circle",
            size: 40,
            borderWidth: 2,
            shadow: true
        },
        edges: {
            smooth: {
                type: "continuous"
            }
        }
    };
    var network = new vis.Network(container, data, options);
}

function desenharWelsh(cidades, distancias) {
    cidades = eval(cidades);
    distancias = eval(distancias);

    var dadosDasCidades = new Array();
    var dadosDasDistancias = new Array();

    for (var i = 0; i < cidades.length; i++) {
        var cidade = {
            id: cidades[i].Codigo,
            label: cidades[i].Identificador,
            color: cidades[i].Cor,
            font: { color: "black" }
        };
        dadosDasCidades.push(cidade);
    }

    for (var i = 0; i < distancias.length; i++) {
        var distancia = {
            id: i,
            label: distancias[i].Valor,
            from: distancias[i].Origem.Codigo,
            to: distancias[i].Destino.Codigo,
            color: {
                color: distancias[i].Cor
            },
            arrows: {
                to: {
                    enabled: false,
                    type: "normal"
                }
            }
        };
        dadosDasDistancias.push(distancia);
    }

    var nodes = new vis.DataSet(dadosDasCidades);
    var edges = new vis.DataSet(dadosDasDistancias);

    var container = document.getElementById("grafo");
    var data = {
        nodes: nodes,
        edges: edges,
    };
    var options = {
        physics: {
            enabled: false,
        },
        nodes: {
            shape: "circle",
            size: 40,
            borderWidth: 2,
            shadow: true
        },
        edges: {
            smooth: {
                type: "continuous"
            }
        }
    };
    var network = new vis.Network(container, data, options);
}

function desenharCaminhosMinimos(cidades, distancias) {
    cidades = eval(cidades);
    distancias = eval(distancias);

    var dadosDasCidades = new Array();
    var dadosDasDistancias = new Array();

    for (var i = 0; i < cidades.length; i++) {
        var cidade = {
            id: cidades[i].Codigo,
            label: cidades[i].Identificador,
            color: cidades[i].Cor,
            font: { color: "black" }
        };
        dadosDasCidades.push(cidade);
    }

    for (var i = 0; i < distancias.length; i++) {
        console.log(distancias[i].PesoDaDistancia);

        var distancia = {
            id: i,
            label: distancias[i].PesoDaDistancia.toString(),
            from: distancias[i].Origem.Codigo,
            to: distancias[i].Destino.Codigo,
            color: {
                color: distancias[i].Cor
            },
            background: {
                enabled: distancias[i].Cor != "#808988",
                size: 5,
                color: distancias[i].Cor
            },
            arrows: {
                to: {
                    enabled: false,
                    type: "normal"
                }
            }
        };
        dadosDasDistancias.push(distancia);
    }

    var nodes = new vis.DataSet(dadosDasCidades);
    var edges = new vis.DataSet(dadosDasDistancias);

    var container = document.getElementById("grafo");
    var data = {
        nodes: nodes,
        edges: edges,
    };
    var options = {
        physics: {
            enabled: false,
        },
        nodes: {
            shape: "circle",
            size: 40,
            borderWidth: 2,
            shadow: true
        },
        edges: {
            smooth: {
                type: "continuous"
            }
        }
    };
    var network = new vis.Network(container, data, options);
}