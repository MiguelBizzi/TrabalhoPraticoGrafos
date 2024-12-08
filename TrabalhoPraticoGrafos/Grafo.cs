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

    public void AdicionarAresta(int saida, int entrada, double peso)
    {
        Vertice? verticeSaida = vertices.Find(v => v.Indice == saida);
        Vertice? verticeEntrada = vertices.Find(v => v.Indice == entrada);

        if (verticeSaida == null || verticeEntrada == null)
        {
            Console.WriteLine("Os vértices informados não existem!");
            return;
        }

        arestas.Add(new Aresta(verticeSaida, verticeEntrada, peso));
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

        foreach (var aresta in arestas)
        {
            matriz[aresta.VerticeSaida.Indice, aresta.VerticeEntrada.Indice] = aresta.Peso;
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
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
        int grauEntrada = arestas.Count(a => a.VerticeEntrada.Indice == vertice);
        int grauSaida = arestas.Count(a => a.VerticeSaida.Indice == vertice);
        Console.WriteLine($"Grau do vértice {vertice}: Entrada = {grauEntrada}, Saída = {grauSaida}, Total = {grauEntrada + grauSaida}");
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
}