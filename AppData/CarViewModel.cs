namespace Core
{
    public class CarViewModel
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string? Class { get; set; }
        public string? Drive { get; set; }
        public string? Transmission { get; set; }
        public string? Fuel_Type { get; set; }
        public string? City_Mpg { get; set; }
        public string? Combination_Mpg { get; set; }
        public string? Highway_Mpg { get; set; }
        public int? Cylinders { get; set; }
        public double? Displacement { get; set; }
        public string? ImageUrl => $"/images/{Make.ToLower()}.jpg";
    }
}