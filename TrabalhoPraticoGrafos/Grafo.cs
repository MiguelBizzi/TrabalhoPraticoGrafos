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
        var verticeSaida = vertices.Find(v => v.Indice == saida);
        var verticeEntrada = vertices.Find(v => v.Indice == entrada);

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
        var adjacencia = new Dictionary<int, List<(int, double)>>();

        foreach (var vertice in vertices)
        {
            adjacencia[vertice.Indice] = new List<(int, double)>();
        }

        foreach (var aresta in arestas)
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
}