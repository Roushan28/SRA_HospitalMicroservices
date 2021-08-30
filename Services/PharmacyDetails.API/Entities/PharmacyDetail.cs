using System;

namespace PharmacyDetails.API.Entities
{
    public class PharmacyDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string Category { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ManufacturedBy { get; set; }
    }
}
