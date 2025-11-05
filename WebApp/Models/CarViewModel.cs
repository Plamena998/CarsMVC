namespace WebApp.Models
{
    public class CarViewModel
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }

        public string? Class { get; set; }
        public string? Drive { get; set; }
        public string? Transmission { get; set; }
        public string? Fuel_Type { get; set; }   // забележи: с долна черта, както в JSON

        // Премиум полета – оставяме ги като string
        public string? City_Mpg { get; set; }
        public string? Combination_Mpg { get; set; }
        public string? Highway_Mpg { get; set; }
        public int? Cylinders { get; set; }
        public double? Displacement { get; set; }

        public string ImageUrl => $"https://img.linemedia.com/img/s/hatchback-Nissan-Micra-City---1726126261768230580_big--24091210310139250000.jpg{Make}+car";
    }
}