using System;

namespace VaccinesRegister.API.Dtos
{
    public record RegisteredDetailDto(Guid Id, string Name, string IdentityNumber, string NoOfDepedents, DateTime AvailableDate);
    public record CreateDetailDto(string Name, string IdentityNumber, string NoOfDepedents, DateTime AvailableDate);
    public record UpdateDetailDto(string Name, string IdentityNumber, string NoOfDepedents, DateTime AvailableDate);

}