using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace hahn.Domain.ValueObject
{
    public class Adress
    {
        [Required]
        [NotNull]
        public string country { get; set; }  = string.Empty;

        [Required]
        [NotNull]
        public string city { get; set; }  = string.Empty;

        [Required]
        [NotNull]
        public string adress { get; set; }  = string.Empty;
        public Adress() { }
        public Adress(string country, string city,string adress)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country is required", nameof(country));
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("City is required", nameof(city));
            if (string.IsNullOrWhiteSpace(adress))
                throw new ArgumentException("adress is required", nameof(adress));

            this.country = country;
            this.city = city;
            this.adress = adress;
        }
     

    }
}