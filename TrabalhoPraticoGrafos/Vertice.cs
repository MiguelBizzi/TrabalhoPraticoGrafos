class Vertice
{
    public int Indice { get; set; }
    public Vertice pai = null;
    public int L = 0;
    public int nivel = 0;
    public int tempoDescoberta = 0;
    public int tempoTermino = 0;
    public double distancia = double.MaxValue;

    public Vertice(int indice)
    {
        Indice = indice;
    }
}