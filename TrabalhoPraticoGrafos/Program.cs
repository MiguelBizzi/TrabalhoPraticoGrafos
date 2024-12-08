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

    static Grafo LerGrafo(string caminhoArquivo)
    {
        try
        {
            string[] linhas = File.ReadAllLines(caminhoArquivo);

            Grafo grafo = new Grafo();

            string[] primeiraLinha = linhas[0].Split(' ');
            int numeroVertices = int.Parse(primeiraLinha[0]);
            int numeroArestas = int.Parse(primeiraLinha[1]);

            for (int i = 1; i <= numeroVertices; i++)
            {
                grafo.AdicionarVertice(i);
            }

            for (int i = 1; i < linhas.Length; i++)
            {
                var partes = linhas[i].Split(' ');
                int origem = int.Parse(partes[0]);
                int destino = int.Parse(partes[1]);
                double peso = double.Parse(partes[2]);

                grafo.AdicionarAresta(origem, destino, peso);
            }

            grafo.ImprimirListaAdjacencia();

            return grafo;

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message, "Shape processing failed.");
            throw;
        }
    }

    static void Main(string[] args)
    {
        bool encerrarApp = false;
        int[,] matrizAdjacencia = null;
        int[,] matrizIncidencia = null;
        Dictionary<int, List<(int, int)>> listaAdjacencia = null;
        Grafo? grafoLido = null;

        while (!encerrarApp)
        {
            Console.WriteLine("==== Menu Principal ====");
            Console.WriteLine("1. Construir Grafo");
            Console.WriteLine("2. Ler grafo");
            if (grafoLido != null)
            {
                Console.WriteLine("3. Imprimir todas as arestas adjacentes de uma aresta");
                Console.WriteLine("4. Imprimir todas as arestas adjacentes de um vértice");
                Console.WriteLine("5. Imprimir todas as arestas incidentes de uma aresta");
                Console.WriteLine("6. Imprimir todas as arestas incidentes de um vértice");
                Console.WriteLine("7. Imprimir o grau de um vértice");
                Console.WriteLine("8. Determinar se dois vértices são adjacentes");
                Console.WriteLine("9. Substituir o peso de uma aresta");
                Console.WriteLine("10. Trocar dois vértices");
                Console.WriteLine("11. Busca em Largura");
                Console.WriteLine("12. Busca em Profundidade");
                Console.WriteLine("13. Algoritmo de Dijkstra");
                Console.WriteLine("14. Algoritmo de Floyd Warshall");
            }

            Console.WriteLine("15. Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    ConstruirGrafo();
                    break;
                case "2":
                    grafoLido = LerGrafo("../../../grafo.txt");
                    Console.Clear();
                    break;
                case "3":
                    if (grafoLido == null)
                    {
                        Console.WriteLine("Grafo não lido!");
                        break;
                    }

                    Console.WriteLine("Informe a origem e o destino da aresta (ex: 0 1):");
                    var entradaAresta = Console.ReadLine().Split(' ');
                    grafoLido.ImprimirArestasAdjacentes(int.Parse(entradaAresta[0]), int.Parse(entradaAresta[1]));
                    break;
                case "4":
                    if (grafoLido == null)
                    {
                        Console.WriteLine("Grafo não lido!");
                        break;
                    }

                    Console.WriteLine("Informe o vértice:");
                    grafoLido.ImprimirVerticesAdjacentes(int.Parse(Console.ReadLine()));
                    break;
                case "5":
                    if (grafoLido == null)
                    {
                        Console.WriteLine("Grafo não lido!");
                        break;
                    }

                    Console.WriteLine("Informe o vértice:");
                    grafoLido.ImprimirArestasIncidentes(int.Parse(Console.ReadLine()));
                    break;
                case "6":
                    if (grafoLido == null)
                    {
                        Console.WriteLine("Grafo não lido!");
                        break;
                    }

                    Console.WriteLine("Informe a origem e o destino da aresta (ex: 0 1):");
                    var entradaVertice = Console.ReadLine().Split(' ');
                    grafoLido.ImprimirVerticesIncidentes(int.Parse(entradaVertice[0]), int.Parse(entradaVertice[1]));
                    break;
                case "7":
                    if (grafoLido == null)
                    {
                        Console.WriteLine("Grafo não lido!");
                        break;
                    }

                    Console.WriteLine("Informe o vértice:");
                    grafoLido.ImprimirGrauVertice(int.Parse(Console.ReadLine()));
                    break;
                case "8":
                    if (grafoLido == null)
                    {
                        Console.WriteLine("Grafo não lido!");
                        break;
                    }

                    Console.WriteLine("Informe a origem, destino e o novo peso da aresta (ex: 0 1 10.5):");
                    var dadosAresta = Console.ReadLine().Split(' ');
                    int origem = int.Parse(dadosAresta[0]);
                    int destino = int.Parse(dadosAresta[1]);
                    double novoPeso = double.Parse(dadosAresta[2]);

                    grafoLido.SubstituirPesoAresta(origem, destino, novoPeso);
                    break;
                case "15":
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