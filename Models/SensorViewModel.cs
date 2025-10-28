namespace RealTimeMonitoringUTS.Models
{
    public class SensorViewModel
    {
        public double TemperatureC { get; set; }
        public double Humidity { get; set; }
        public double MethaneGas { get; set; }
        public double HydrogenGas { get; set; }
        public double Smoke { get; set; }
        public double LpgGas { get; set; }
        public double AlcohonGas { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public DateTime AddAt { get; set; }
    }
}
