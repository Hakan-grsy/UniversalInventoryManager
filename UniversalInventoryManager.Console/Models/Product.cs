namespace UniversalInventoryManager.Console.Models
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Abstract method for tax calculation, must be overridden by derived classes
        public abstract decimal CalculateTax();

        // Virtual method to get basic product info
        public virtual string GetInfo()
        {
            return $"[{Id}] {Name} : ${Price}";
        }
    }
}