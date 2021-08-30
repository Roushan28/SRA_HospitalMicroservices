using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinesRegister.API.Dtos;
using VaccinesRegister.API.Entites;
using VaccinesRegister.API.Repositories;

namespace VaccinesRegister.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("ClientIdPolicy")]
    public class VaccinesRegisterController : Controller
    {

        private readonly IVaccinesRegisterRepository repository;

        public VaccinesRegisterController(IVaccinesRegisterRepository repository)
        {
            this.repository = repository;

        }

     
        [HttpGet]
        public async Task<IEnumerable<RegisteredDetailDto>> GetRegisteredDetailsAsync(string name = null)
        {
            var details = (await repository.GetRegisteredDetailsAsync())
                        .Select(item => item.AsDto());
            if (!string.IsNullOrWhiteSpace(name))
            {
                details = details.Where(item => item.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            return details;
        }

      
        [HttpGet("GetRegisteredDetail/{id}", Name = "GetRegisteredDetail")]
        public async Task<ActionResult<RegisteredDetailDto>> GetRegisteredDetailAsync(Guid id)
        {
            var detail = await repository.GetRegisteredDetailAsync(id);

            if (detail is null)
            {
                return NotFound();
            }

            return detail.AsDto();
        }

       
        [HttpPost]
        public async Task<ActionResult<RegisteredDetailDto>> CreateDetailAsync(CreateDetailDto itemDto)
        {
            VaccineRegister vaccineRegister = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                IdentityNumber = itemDto.IdentityNumber,
                NoOfDepedents = itemDto.NoOfDepedents,
                AvailableDate = itemDto.AvailableDate
            };

            await repository.CreateDetailAsync(vaccineRegister);

            //return CreatedAtAction(nameof(GetRegisteredDetailAsync), new { id = vaccineRegister.Id }, vaccineRegister.AsDto());
            return CreatedAtRoute("GetRegisteredDetail", new { controller = "VaccinesRegister",  id = vaccineRegister.Id}, vaccineRegister.AsDto());

        }


    [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDetailAsync(Guid id, UpdateDetailDto itemDto)
        {
            var existingdetail = await repository.GetRegisteredDetailAsync(id);

            if (existingdetail is null)
            {
                return NotFound();
            }

            existingdetail.Name = itemDto.Name;
            existingdetail.IdentityNumber = itemDto.IdentityNumber;
            existingdetail.NoOfDepedents = itemDto.NoOfDepedents;
            existingdetail.AvailableDate = itemDto.AvailableDate;

            await repository.UpdateDetailAsync(existingdetail);

            return Ok();

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDetailAsync(Guid id)
        {
            var existingdetail = await repository.GetRegisteredDetailAsync(id);

            if (existingdetail is null)
            {
                return NotFound();
            }

            await repository.DeleteDetailAsync(id);

            return Ok();
        }
    }

}
