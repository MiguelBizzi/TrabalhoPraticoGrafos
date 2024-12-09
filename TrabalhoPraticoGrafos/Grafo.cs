class Grafo
{
    private List<Vertice> vertices;
    private List<Aresta> arestas;

    public Grafo()
    {
        vertices = new List<Vertice>();
        arestas = new List<Aresta>();
    }

    public void AdicionarVertice(int indice)
    {
        vertices.Add(new Vertice(indice));
    }

    public bool AdicionarAresta(int saida, int entrada, double peso)
    {
        Vertice? verticeSaida = vertices.Find(v => v.Indice == saida);
        Vertice? verticeEntrada = vertices.Find(v => v.Indice == entrada);

        if (verticeSaida == null || verticeEntrada == null)
        {
            Console.WriteLine("Os vértices informados não existem!");
            return false;
        }

        arestas.Add(new Aresta(verticeSaida, verticeEntrada, peso));
        return true;
    }

    public double CalcularDensidade()
    {
        int maxArestas = vertices.Count * (vertices.Count - 1);
        return (double)arestas.Count / maxArestas;
    }

    public void ImprimirListaAdjacencia()
    {
        Console.WriteLine("==== Lista de Adjacência ====");
        Dictionary<int, List<(int, double)>> adjacencia = new Dictionary<int, List<(int, double)>>();

        foreach (Vertice vertice in vertices)
        {
            adjacencia[vertice.Indice] = new List<(int, double)>();
        }

        foreach (Aresta aresta in arestas)
        {
            adjacencia[aresta.VerticeSaida.Indice].Add((aresta.VerticeEntrada.Indice, aresta.Peso));
        }

        foreach (var item in adjacencia)
        {
            Console.Write($"{item.Key}: ");
            foreach (var (destino, peso) in item.Value)
            {
                Console.Write($"({destino}, peso: {peso}) ");
            }
            Console.WriteLine();
        }
    }

    public void ImprimirMatrizAdjacencia()
    {
        Console.WriteLine("==== Matriz de Adjacência ====");
        int n = vertices.Count;
        double[,] matriz = new double[n, n];

        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                foreach (var a in arestas)
                {
                    if (j + 1 == a.VerticeEntrada.Indice && i + 1 == a.VerticeSaida.Indice)
                    {
                        matriz[i, j] = matriz[i, j] == 0 ? a.Peso : +a.Peso;
                    }
                }
                Console.Write($"{matriz[i, j]} ");
            }

            Console.WriteLine();
        }
    }

    public void ImprimirArestasAdjacentes(int origem, int destino)
    {
        var aresta = arestas.Find(a => a.VerticeSaida.Indice == origem && a.VerticeEntrada.Indice == destino);
        if (aresta == null)
        {
            Console.WriteLine("Aresta informada não encontrada!");
            return;
        }

        Console.WriteLine($"Arestas adjacentes à aresta ({origem} -> {destino}):");
        foreach (var adj in arestas)
        {
            if (adj.VerticeSaida.Indice == aresta.VerticeEntrada.Indice || adj.VerticeEntrada.Indice == aresta.VerticeSaida.Indice)
            {
                Console.WriteLine($"({adj.VerticeSaida.Indice} -> {adj.VerticeEntrada.Indice}), peso: {adj.Peso}");
            }
        }
    }

    public void ImprimirVerticesAdjacentes(int vertice)
    {
        Console.WriteLine($"Vértices adjacentes ao vértice {vertice}:");
        foreach (var aresta in arestas)
        {
            if (aresta.VerticeSaida.Indice == vertice)
            {
                Console.WriteLine(aresta.VerticeEntrada.Indice);
            }
        }
    }

    public void ImprimirArestasIncidentes(int vertice)
    {
        Console.WriteLine($"Arestas incidentes no vértice {vertice}:");
        foreach (var aresta in arestas)
        {
            if (aresta.VerticeEntrada.Indice == vertice)
            {
                Console.WriteLine($"({aresta.VerticeSaida.Indice} -> {aresta.VerticeEntrada.Indice}), peso: {aresta.Peso}");
            }
        }
    }

    public void ImprimirVerticesIncidentes(int origem, int destino)
    {
        var aresta = arestas.Find(a => a.VerticeSaida.Indice == origem && a.VerticeEntrada.Indice == destino);
        if (aresta == null)
        {
            Console.WriteLine("Aresta informada não encontrada!");
            return;
        }

        Console.WriteLine($"Vértices incidentes à aresta ({origem} -> {destino}):");
        Console.WriteLine($"Origem: {aresta.VerticeSaida.Indice}, Destino: {aresta.VerticeEntrada.Indice}");
    }

    public void ImprimirGrauVertice(int vertice)
    {
        double somaPeso = 0;

        foreach (var aresta in arestas)
        {
            if (aresta.VerticeEntrada.Indice == vertice)
            {
                somaPeso += aresta.Peso;
            }
        }

        Console.WriteLine($"O grau do vértice {vertice} é {somaPeso}.");
    }

    public void SubstituirPesoAresta(int origem, int destino, double novoPeso)
    {
        var aresta = arestas.Find(a => a.VerticeSaida.Indice == origem && a.VerticeEntrada.Indice == destino);

        if (aresta != null)
        {
            aresta.Peso = novoPeso;
            Console.WriteLine($"O peso da aresta ({origem} -> {destino}) foi alterado para {novoPeso}.");
        }
        else
        {
            Console.WriteLine("Aresta não encontrada!");
        }
    }

    public void TrocarVertices(int vertice1, int vertice2)
    {

        var v1 = vertices.Find(a => a.Indice == vertice1);
        var v2 = vertices.Find(a => a.Indice == vertice2);

        if (v1 is null || v2 is null)
        {
            Console.WriteLine("Um dos vértices não foi encontrado.");
            return;
        }

        vertices = vertices.Select(v => v == v1 ? (v = v2) : v == v2 ? (v = v1) : v).ToList();

        arestas = arestas.Select(a =>
        {
            if (a.VerticeSaida.Indice == v1.Indice && a.VerticeEntrada.Indice == v2.Indice)
            {
                a.VerticeSaida = v2;
                a.VerticeEntrada = v1;
            }
            else if (a.VerticeSaida.Indice == v2.Indice && a.VerticeEntrada.Indice == v1.Indice)
            {
                a.VerticeSaida = v1;
                a.VerticeEntrada = v2;
            }
            else if (a.VerticeSaida.Indice == v1.Indice)
            {
                a.VerticeSaida = v2;
            }
            else if (a.VerticeEntrada.Indice == v1.Indice)
            {
                a.VerticeEntrada = v2;
            }
            else if (a.VerticeSaida.Indice == v2.Indice)
            {
                a.VerticeSaida = v1;
            }
            else if (a.VerticeEntrada.Indice == v2.Indice)
            {
                a.VerticeEntrada = v1;
            }
            return a;
        }
        ).ToList();

        ImprimirListaAdjacencia();
    }
    public void MostrarSeDoisSaoAdjacentes(int v1, int v2)
    {
        bool eAdj = false;

        bool v1Existe = arestas.Any(a => a.VerticeSaida.Indice == v1 || a.VerticeEntrada.Indice == v1);
        bool v2Existe = arestas.Any(a => a.VerticeSaida.Indice == v2 || a.VerticeEntrada.Indice == v2);

        if (v1Existe && v2Existe)
        {
            foreach (var a in arestas)
            {
                if (a.VerticeEntrada.Indice == v1 && a.VerticeSaida.Indice == v2 || a.VerticeEntrada.Indice == v2 && a.VerticeSaida.Indice == v1)
                {
                    eAdj = true;
                }
            }

            if (eAdj)
            {
                Console.WriteLine($"O vértice {v1} é adjacente de {v2}.");
            }
            else
            {
                Console.WriteLine($"O vértice {v1} não é adjacente de {v2}.");
            }
        }
        else
        {
            Console.WriteLine("Um dos vértices fornecidos não existe");
        }

    }

    public void IniciarBuscaLargura(int verticeInicial)
    {
        if (verticeInicial <= 0 || verticeInicial >= vertices.Count)
        {
            Console.WriteLine("Vértice inicial inválido.");
            return;
        }

        var niveis = new Dictionary<int, int>();
        var predecessores = new Dictionary<int, int?>();
        var visitados = new HashSet<int>();
        var fila = new Queue<int>();

        niveis[verticeInicial] = 0;
        predecessores[verticeInicial] = null;
        fila.Enqueue(verticeInicial);
        visitados.Add(verticeInicial);

        Console.WriteLine($"Iniciando a busca em largura a partir do vértice {verticeInicial}.");

        while (fila.Count > 0)
        {
            int atual = fila.Dequeue();
            Console.WriteLine($"\nExplorando vértice {atual} no nível {niveis[atual]}.");

            var adjacentes = arestas
                .Where(a => a.VerticeSaida.Indice == atual)
                .Select(a => a.VerticeEntrada.Indice)
                .OrderBy(v => v);

            foreach (int vizinho in adjacentes)
            {
                if (!visitados.Contains(vizinho))
                {
                    visitados.Add(vizinho);
                    niveis[vizinho] = niveis[atual] + 1;
                    predecessores[vizinho] = atual;
                    fila.Enqueue(vizinho);
                    Console.WriteLine($"Vértice {vizinho} descoberto no nível {niveis[vizinho]} (predecessor: {atual}).");
                }
            }
        }

        Console.WriteLine("\n--- Resultados da busca em largura ---");
        Console.WriteLine("Vértice | Nível | Predecessor");
        foreach (var vertice in vertices)
        {
            int v = vertice.Indice;
            string predecessor = predecessores.ContainsKey(v) && predecessores[v].HasValue
                ? predecessores[v].Value.ToString()
                : "Nenhum";
            Console.WriteLine($"{v,7} | {niveis.GetValueOrDefault(v, -1),5} | {predecessor,11}");
        }
    }

    public void IniciarBuscaProfundidade(int verticeInicial)
    {
        if (verticeInicial <= 0 || verticeInicial > vertices.Count)
        {
            Console.WriteLine("Vértice inicial inválido.");
            return;
        }

        var descoberto = new Dictionary<int, int>();
        var finalizado = new Dictionary<int, int>();
        var predecessores = new Dictionary<int, int?>();
        var visitados = new HashSet<int>();
        int tempo = 0;

        foreach (var vertice in vertices)
        {
            predecessores[vertice.Indice] = null;
        }

        Console.WriteLine($"Iniciando busca em profundidade a partir do vértice {verticeInicial}.");
        ExecutarBusca(verticeInicial, ref tempo, ref descoberto, ref finalizado, ref predecessores, ref visitados);

        Console.WriteLine("\n--- Resultados da busca em profundidade ---");
        Console.WriteLine("Vértice | Descoberta | Finalização | Predecessor");
        foreach (var vertice in vertices)
        {
            int v = vertice.Indice;
            string predecessor = predecessores[v].HasValue ? predecessores[v].Value.ToString() : "Nenhum";
            Console.WriteLine($"{v,7} | {descoberto.GetValueOrDefault(v, -1),10} | {finalizado.GetValueOrDefault(v, -1),12} | {predecessor}");
        }
    }

    public void ExecutarBusca(int verticeAtual, ref int tempo,
                    ref Dictionary<int, int> descoberto,
                    ref Dictionary<int, int> finalizado,
                    ref Dictionary<int, int?> predecessores,
                    ref HashSet<int> visitados)
    {
        visitados.Add(verticeAtual);

        tempo++;
        descoberto[verticeAtual] = tempo;

        var adjacentes = arestas
            .Where(a => a.VerticeSaida.Indice == verticeAtual)
            .Select(a => a.VerticeEntrada.Indice)
            .OrderBy(v => v);

        foreach (int vizinho in adjacentes)
        {
            if (!visitados.Contains(vizinho))
            {
                predecessores[vizinho] = verticeAtual;
                ExecutarBusca(vizinho, ref tempo, ref descoberto, ref finalizado, ref predecessores, ref visitados);
            }
        }

        tempo++;
        finalizado[verticeAtual] = tempo;
    }

    public void Dijkstra(int origem, int destino)
    {
        Vertice primeiro = vertices.Find(v => v.Indice == origem);
        Vertice d = vertices.Find(v => v.Indice == destino);

        if (primeiro == null || d == null)
        {
            Console.WriteLine("O vértice de origem ou de destino fornecido não existe.");
            return;
        }

        List<Aresta> explorados = [];
        Queue<Vertice> fila = [];
        fila.Enqueue(primeiro);
        List<Vertice> caminho = [];
        double menorCaminho = 0;

        primeiro.distancia = 0;

        while (fila.Count > 0)
        {
            var v = fila.Dequeue();

            var arestasAdj = arestas.Where(a => a.VerticeSaida == v).ToList();

            foreach (Aresta a in arestasAdj)
            {
                if (!explorados.Contains(a))
                {
                    if (v.distancia + a.Peso < a.VerticeEntrada.distancia)
                    {
                        a.VerticeEntrada.distancia = v.distancia + a.Peso;
                        a.VerticeEntrada.pai = v;
                        a.VerticeEntrada.pesoMenorAresta = a.Peso;
                    }

                    if (!fila.Contains(a.VerticeEntrada))
                    {
                        fila.Enqueue(a.VerticeEntrada);
                    }

                    explorados.Add(a);

                    if (a.VerticeEntrada.Indice == destino)
                    {
                        menorCaminho = a.VerticeEntrada.distancia;
                    }
                }
            }
        }

        Vertice vDeRetorno = d;

        while (vDeRetorno != null)
        {
            caminho.Insert(0, vDeRetorno);
            vDeRetorno = vDeRetorno.pai;
        }

        foreach (var v in caminho)
        {
            Console.WriteLine($"({v.Indice}, peso {v.pesoMenorAresta})");
        }

    }

    public void Floyd()
    {
        double[,] matrizDist = new double[vertices.Count, vertices.Count];

        for (int i = 0; i < matrizDist.GetLength(0); i++)
        {
            for (int j = 0; j < matrizDist.GetLength(1); j++)
            {
                if (j != i)
                {
                    matrizDist[i, j] = double.MaxValue;
                }
                else
                {
                    matrizDist[i, j] = 0;
                }
            }
        }

        for (int i = 0; i < matrizDist.GetLength(0); i++)
        {
            for (int j = 0; j < matrizDist.GetLength(1); j++)
            {
                foreach (var a in arestas)
                {
                    if (j + 1 == a.VerticeEntrada.Indice && i + 1 == a.VerticeSaida.Indice)
                    {
                        matrizDist[i, j] = matrizDist[i, j] == 0 ? a.Peso : +a.Peso;
                    }
                }
            }
        }

        for (int k = 0; k < matrizDist.GetLength(0); k++)
        {
            for (int i = 0; i < matrizDist.GetLength(1); i++)
            {
                for (int j = 0; j < vertices.Count; j++)
                {
                    if (matrizDist[i, j] > matrizDist[i, k] + matrizDist[k, j])
                    {
                        matrizDist[i, j] = matrizDist[i, k] + matrizDist[k, j];
                    }
                }

            }
        }

        for (int i = 0; i < matrizDist.GetLength(0); i++)
        {
            for (int j = 0; j < matrizDist.GetLength(1); j++)
            {
                Console.Write($"{matrizDist[i, j]} ");
            }
            Console.WriteLine();
        }
    }

}