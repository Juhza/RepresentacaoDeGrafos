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
            smooth: {
                continuous
            },
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
/*        nodes: {
            shape: "circle",
            size: 30,
            font: {
                size: 32,
            },
            borderWidth: 2,
            shadow: true
        },
        edges: {
            width: 2,
            shadow: true,
        },
        layout: {
            hierarchical: {
                direction: ud,
            },
        },*/
    };
    var network = new vis.Network(container, data, options);
}

function teste() {
    var nodes = new vis.DataSet([
        { id: 1, label: "Node 1" },
        { id: 2, label: "Node 2" },
        { id: 3, label: "Node 3" },
        { id: 4, label: "Node 4" },
        { id: 5, label: "Node 5" },
    ]);

    // create an array with edges
    var edges = new vis.DataSet([
        { from: 1, to: 3 },
        { from: 1, to: 2 },
        { from: 2, to: 4 },
        { from: 2, to: 5 },
        { from: 3, to: 3 },
    ]);

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
            size: 30,
            font: {
                size: 32,
            },
            borderWidth: 2,
            shadow: true
        },
        edges: {
            width: 2,
            shadow: true,
        },
        layout: {
            hierarchical: {
                direction: ud,
            },
        },
    };
    var network = new vis.Network(container, data, options);
}