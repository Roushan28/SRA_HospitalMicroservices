using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyDetails.API.Dtos;
using PharmacyDetails.API.Entities;
using PharmacyDetails.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PharmacyDetails.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("ClientIdPolicy")]
    public class PharmacyDetailsController : Controller
    {
        private readonly IPharmacyDetailsRepository _pharmacyDetailsRepository;
        private readonly ILogger<PharmacyDetailsController> _logger;

        public PharmacyDetailsController(IPharmacyDetailsRepository pharmacyDetailsRepository, ILogger<PharmacyDetailsController> logger)
        {
            _pharmacyDetailsRepository = pharmacyDetailsRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("PharmacyDetailsController Get Method");
            return Ok(await _pharmacyDetailsRepository.GetPharmacyDetails());
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPharmacyDetail(string name)
        {
            _logger.LogInformation("PharmacyDetailsController GetPharmacyDetail Method");
            return Ok(await _pharmacyDetailsRepository.GetPharmacyDetail(name));
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(AddDetailDto addDetailDto)
        {
            _logger.LogInformation("PharmacyDetailsController AddProduct Method");
            return Ok(await _pharmacyDetailsRepository.CreateProduct(addDetailDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateDetailDto updateDetailDto)
        {
            _logger.LogInformation("PharmacyDetailsController UpdateProduct Method");
            ServiceResponse<GetDetailDto> response = await _pharmacyDetailsRepository.UpdatePharmacyDetail(updateDetailDto);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("PharmacyDetailsController Delete Method");
            ServiceResponse<List<GetDetailDto>> response = await _pharmacyDetailsRepository.DeletePharmacyDetail(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
