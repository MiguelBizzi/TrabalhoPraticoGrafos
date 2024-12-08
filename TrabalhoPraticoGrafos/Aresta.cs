class Aresta
{
    public Vertice VerticeSaida { get; set; }
    public Vertice VerticeEntrada { get; set; }
    public double Peso { get; set; }

    public Aresta(Vertice verticeSaida, Vertice verticeEntrada, double peso)
    {
        VerticeSaida = verticeSaida;
        VerticeEntrada = verticeEntrada;
        Peso = peso;
    }
}