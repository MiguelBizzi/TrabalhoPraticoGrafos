class Program
{
    static void ConstruirGrafo()
    {
        Console.Clear();
        Console.WriteLine("==== Construir Grafo ====");

        Grafo grafo = new Grafo();
        Console.Write("Digite a quantidade de vértices: ");
        int qtdVertices = int.Parse(Console.ReadLine());

        for (int i = 1; i <= qtdVertices; i++)
        {
            grafo.AdicionarVertice(i);
        }

        Console.Write("Digite a quantidade de arestas: ");
        int qtdArestas = int.Parse(Console.ReadLine());

        for (int i = 0; i < qtdArestas; i++)
        {
            Console.WriteLine($"Aresta {i + 1}:");
            Console.Write("Vértice de saída: ");
            int saida = int.Parse(Console.ReadLine());
            Console.Write("Vértice de entrada: ");
            int entrada = int.Parse(Console.ReadLine());
            Console.Write("Peso: ");
            double peso = double.Parse(Console.ReadLine());

            grafo.AdicionarAresta(saida, entrada, peso);
        }

        Console.WriteLine();
        Console.WriteLine("----------------");
        double densidade = grafo.CalcularDensidade();
        Console.WriteLine($"Densidade do grafo: {densidade:F2}");

        if (densidade > 0.5)
        {
            Console.WriteLine("Representação escolhida: Matriz de Adjacência");
            grafo.ImprimirMatrizAdjacencia();
        }
        else
        {
            Console.WriteLine("Representação escolhida: Lista de Adjacência");
            grafo.ImprimirListaAdjacencia();
        }

        Console.WriteLine("----------------");
        Console.WriteLine();
    }

    static void LerGrafo()
    {

    }

    static void Main(string[] args)
    {
        bool encerrarApp = false;
        int[,] matrizAdjacencia = null;
        int[,] matrizIncidencia = null;
        Dictionary<int, List<(int, int)>> listaAdjacencia = null;

        while (!encerrarApp)
        {
            Console.WriteLine("==== Menu Principal ====");
            Console.WriteLine("1. Construir Grafo");
            Console.WriteLine("2. Ler grafo");
            Console.WriteLine("10. Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ConstruirGrafo();
                    break;
                case "2":
                    LerGrafo();
                    Console.Clear();
                    break;
                case "10":
                    Console.WriteLine("Saindo da aplicação");
                    return;
                default:
                    Console.WriteLine("Opção inválida. Pressione Enter para continuar.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}