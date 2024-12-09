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

        // foreach (var aresta in arestas)
        // {
        //     matriz[aresta.VerticeSaida.Indice, aresta.VerticeEntrada.Indice] = aresta.Peso;
        // }

        for (int i = 0; i < matriz.GetLength(0); i++)
        {
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                foreach (var a in arestas)
                {
                    if (j + 1 == a.VerticeEntrada.Indice && i + 1 == a.VerticeSaida.Indice)
                    {
                        matriz[i,j] = matriz[i,j] == 0? a.Peso : +a.Peso;
                        // if (matriz[i, j] == 0)
                        // {
                        //     matriz[i, j] = a.Peso;
                        // }
                        // else if (matriz[i, j] != 0)
                        // {
                        //     matriz[i, j] += a.Peso;
                        // }
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
        // int grauEntrada = arestas.Count(a => a.VerticeEntrada.Indice == vertice);
        // int grauSaida = arestas.Count(a => a.VerticeSaida.Indice == vertice);
        // Console.WriteLine($"Grau do vértice {vertice}: Entrada = {grauEntrada}, Saída = {grauSaida}, Total = {grauEntrada + grauSaida}");

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


    //Busca largura


    public void IniciarBuscaLargura(int v)
    {
        int t = 0;
        Queue<Vertice> fila = new();

        if (!vertices.Any(ve => ve.Indice == v))
        {
            Console.WriteLine("O vértice fornecido não foi encontrado.");
            return;
        }

        Vertice primeiroV = vertices.Find(a => a.Indice == v);

        List<Vertice> vetorModificado = vertices;

        vetorModificado.Remove(primeiroV);
        vetorModificado.Insert(0, primeiroV);

        foreach (Vertice vertice in vetorModificado)
        {
            if (vertice.L == 0)
            {
                t += 1;
                vertice.L = t;
                fila.Enqueue(vertice);
                BuscaLarguraDirecionada(arestas, t, fila);
            }
        }
    }
    public static void BuscaLarguraDirecionada(List<Aresta> arestas, int t, Queue<Vertice> fila)
    {
        while (fila.Count != 0)
        {
            Vertice v = fila.First();
            fila.Dequeue();
            var arestasOrdenadas = arestas.OrderBy(a => a.VerticeEntrada.Indice);
            foreach (Aresta a in arestasOrdenadas)
            {
                if (a.VerticeSaida == v)
                {

                    if (a.VerticeEntrada.L == 0)
                    {
                        t += 1;
                        a.VerticeEntrada.L = t;
                        a.VerticeEntrada.pai = v;
                        a.VerticeEntrada.nivel = v.nivel + 1;
                        fila.Enqueue(a.VerticeEntrada);
                        Console.WriteLine($"Aresta árvore de {v.Indice} para {a.VerticeEntrada.Indice}. Nível de {v.Indice}: {v.nivel}. Predecessor: {(v.pai != null ? v.pai.Indice : "-")}");
                    }
                    else if (v.pai != a.VerticeEntrada && v.nivel > a.VerticeEntrada.nivel)
                    {
                        Console.WriteLine($"Aresta de retorno de {v.Indice} para {a.VerticeEntrada.Indice}. Nível de {v.Indice}: {v.nivel}. Predecessor: {v.pai.Indice}.");
                    }
                    else
                    {
                        Console.WriteLine($"Aresta de cruzamento de {v.Indice} para {a.VerticeEntrada.Indice}. Nível de {v.Indice}: {v.nivel}. Predecessor: {v.pai.Indice}.");
                    }
                }
            }
        }
    }


    //Profundidade

    public void IniciarBuscaProfundidade()
    {
        int t = 0;
        foreach (var vertice in vertices)
        {
            if (vertice.tempoDescoberta == 0)
            {
                BuscaProfundidade(t, vertice);
            }
        }
    }
    public void BuscaProfundidade(int t, Vertice v)
    {
        t += 1;
        v.tempoDescoberta = t;

        foreach (Aresta a in arestas.Where(a => a.VerticeSaida == v))
        {
            if (a.VerticeEntrada.tempoDescoberta == 0)
            {
                a.VerticeEntrada.pai = v;
                Console.WriteLine($"Aresta de árvore de {v.Indice} para {a.VerticeEntrada.Indice}. Tempo de descoberta de {v.Indice}: {v.tempoDescoberta}.");
                BuscaProfundidade(t, a.VerticeEntrada);
            }
            else
            {
                if (a.VerticeEntrada.tempoTermino == 0)
                {
                    Console.WriteLine($"Aresta de retorno de {v.Indice} para {a.VerticeEntrada.Indice}.");
                }
                else if (v.tempoDescoberta < a.VerticeEntrada.tempoDescoberta)
                {
                    Console.WriteLine($"Aresta de avanço de {v.Indice} para {a.VerticeEntrada.Indice}.");
                }
                else
                {
                    Console.WriteLine($"Aresta de cruzamento de {v.Indice} para {a.VerticeEntrada.Indice}.");
                }
            }
            t += 1;
            v.tempoTermino = t;

        }

        Console.WriteLine("Tempo de término: " + v.tempoTermino);
    }


    // Dijkstra

    public void Dijkstra(int origem, int destino)
    {
        List<Aresta> caminho = [];
        List<Aresta> explorados = [];

        Vertice primeiroV = vertices.Find(a => a.Indice == origem);

        Queue<Vertice> fila = [];
        fila.Enqueue(primeiroV);
        while (fila.Count > 0)
        {
            // List<Aresta> tempList = [];
            // foreach (var a in arestas)
            // {
            //     if (a.VerticeSaida == v)
            //     {
            //         tempList.Add(a);
            //     }
            // }

            double menor = double.MaxValue;
            Aresta escolhida = null;
            foreach (var a in arestas)
            {
                if (a.VerticeSaida.Indice == fila.Peek().Indice)
                {
                    if (a.Peso < menor)
                    {
                        escolhida = a;
                        menor = a.Peso;
                    }

                }

            }

            fila.Dequeue();

            if (escolhida != null)
            {
                escolhida.VerticeEntrada.distancia = escolhida.VerticeSaida.distancia + escolhida.Peso;
                escolhida.VerticeEntrada.pai = escolhida.VerticeSaida;
                caminho.Add(escolhida);
            }

            fila.Enqueue(escolhida.VerticeEntrada);
        }

        foreach (var a in caminho)
        {
            Console.WriteLine($"Vértice {a.VerticeSaida.Indice} para {a.VerticeEntrada.Indice} com peso {a.Peso}.");
        }
    }
}