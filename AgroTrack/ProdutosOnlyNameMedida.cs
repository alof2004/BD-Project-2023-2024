public class ProdutosOnlyNameMedida
{
    public int Id_Produto { get; set; }
    public string Produto { get; set; }
    public string Unidade_medida { get; set; }
    public int Quantidade { get; set; }

    // Constructor
    public ProdutosOnlyNameMedida(int id_produto, string produto, string unidade_medida, int quantidade)
    {
        Id_Produto = id_produto;
        Produto = produto;
        Unidade_medida = unidade_medida;
        Quantidade = quantidade;
    }
    public ProdutosOnlyNameMedida()
    {
    }
    // Override ToString() to return a string representation containing all relevant information
    public override string ToString()
    {
        return $"{Id_Produto} - {Produto} - Quantidade Disponível: {Quantidade} - Unidade de medida: {Unidade_medida})"; // Example: "Product Name (10 kg)"
    }
}
