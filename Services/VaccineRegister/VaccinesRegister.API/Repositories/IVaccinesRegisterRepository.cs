using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaccinesRegister.API.Entites;

namespace VaccinesRegister.API.Repositories
{

    public interface IVaccinesRegisterRepository
    {
        Task<VaccineRegister> GetRegisteredDetailAsync(Guid id);
        Task<IEnumerable<VaccineRegister>> GetRegisteredDetailsAsync();
        Task CreateDetailAsync(VaccineRegister vaccineRegister);
        Task UpdateDetailAsync(VaccineRegister vaccineRegister);
        Task DeleteDetailAsync(Guid id);

    }
}

