namespace UniversalInventoryManager.Console.Models
{
    public class TechProduct : Product
    {
        public bool HasBattery { get; set; }

        public override decimal CalculateTax()
        {
            return Price * 0.20m; // 20% Tax for tech products
        }

        public override string GetInfo()
        {
            // Using ternary operator for battery status
            string batteryStatus = HasBattery ? "(With Battery)" : "(No Battery)";
            return $"{base.GetInfo()} {batteryStatus}";
        }
    }
}