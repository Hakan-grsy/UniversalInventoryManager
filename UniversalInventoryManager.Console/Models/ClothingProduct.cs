namespace UniversalInventoryManager.Console.Models
{
    public class ClothingProduct : Product
    {
        public string Size { get; set; } // S, M, L, XL

        public override decimal CalculateTax()
        {
            return Price * 0.10m; // 10% Tax for clothing
        }
    }
}