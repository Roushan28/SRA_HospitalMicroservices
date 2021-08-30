using VaccinesRegister.API.Dtos;
using VaccinesRegister.API.Entites;

namespace VaccinesRegister.API
{
    public static class Extensions
    {
        public static RegisteredDetailDto AsDto(this VaccineRegister register)
        {
            return new RegisteredDetailDto(register.Id, register.Name, register.IdentityNumber, register.NoOfDepedents, register.AvailableDate);
        }
    }
}