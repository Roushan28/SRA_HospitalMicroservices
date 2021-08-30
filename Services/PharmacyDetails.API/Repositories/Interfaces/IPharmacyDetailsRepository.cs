using PharmacyDetails.API.Dtos;
using PharmacyDetails.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmacyDetails.API.Repositories.Interfaces
{
    public interface IPharmacyDetailsRepository
    {

        Task<ServiceResponse<List<GetDetailDto>>> GetPharmacyDetails();
        Task<ServiceResponse<GetDetailDto>> GetPharmacyDetail(string name);
        Task<ServiceResponse<List<GetDetailDto>>> CreateProduct(AddDetailDto product);
        Task<ServiceResponse<GetDetailDto>> UpdatePharmacyDetail(UpdateDetailDto updatedCharacter);
        Task<ServiceResponse<List<GetDetailDto>>> DeletePharmacyDetail(int id);
    }
}
