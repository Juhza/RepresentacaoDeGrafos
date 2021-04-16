function desenharGrafo(vertices, arestas) {
    vertices = eval(vertices);
    arestas = eval(arestas);

    var dadosDosVertices = new Array();
    var dadosDasArestas = new Array();

    for (var i = 0; i < vertices.length; i++) {
        var vertice = {
            id: vertices[i].Codigo,
            label: vertices[i].Identificador,
            color: "#FFA807"
        };
        dadosDosVertices.push(vertice);
    }

    for (var i = 0; i < arestas.length; i++) {
        var ehOrdenado = arestas[i].EhOrdenado;
        var aresta = {
            id: i,
            label: arestas[i].Identificador + ": " + arestas[i].Custo,
            from: arestas[i].Antecessor.Codigo,
            to: arestas[i].Sucessor.Codigo,
            arrows: {
                to: {
                    enabled: ehOrdenado,
                    type: "normal"
                }
            }
        };
        dadosDasArestas.push(aresta);
    }

    console.log(dadosDosVertices);
    console.log(dadosDasArestas);

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
                type: "horizontal"
            }
        }
    };
    var network = new vis.Network(container, data, options);
}