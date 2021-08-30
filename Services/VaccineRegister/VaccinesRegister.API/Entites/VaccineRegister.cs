using System;

namespace VaccinesRegister.API.Entites
{
    public class VaccineRegister
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public string NoOfDepedents { get; set; }
        public DateTime AvailableDate { get; set; }
    }
}
