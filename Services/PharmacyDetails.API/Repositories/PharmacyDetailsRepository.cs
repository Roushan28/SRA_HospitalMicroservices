using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PharmacyDetails.API.Data;
using PharmacyDetails.API.Dtos;
using PharmacyDetails.API.Entities;
using PharmacyDetails.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PharmacyDetails.API.Repositories
{
    public class PharmacyDetailsRepository : IPharmacyDetailsRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public PharmacyDetailsRepository(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetDetailDto>>> GetPharmacyDetails()
        {
            ServiceResponse<List<GetDetailDto>> serviceResponse = new();
            try
            {
                List<PharmacyDetail> dbpharmacydetails = await _context.pharmacyDetails.ToListAsync();
                serviceResponse.Data = (dbpharmacydetails.Select(c => _mapper.Map<GetDetailDto>(c))).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetDetailDto>> GetPharmacyDetail(string name)
        {
            ServiceResponse<GetDetailDto> serviceResponse = new();
            try
            {
                PharmacyDetail dbpharmacydetails = await _context.pharmacyDetails.FirstOrDefaultAsync(c => c.Name == name);
                if (dbpharmacydetails != null)
                {
                    serviceResponse.Data = _mapper.Map<GetDetailDto>(dbpharmacydetails);
                }
                else
                {
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Search result not fund";
                }


            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetDetailDto>>> CreateProduct(AddDetailDto addDetailDto)
        {
            ServiceResponse<List<GetDetailDto>> serviceResponse = new();
            try
            {
                PharmacyDetail detail = _mapper.Map<PharmacyDetail>(addDetailDto);

                await _context.pharmacyDetails.AddAsync(detail);
                await _context.SaveChangesAsync();
                serviceResponse.Data = (_context.pharmacyDetails.Select(c => _mapper.Map<GetDetailDto>(c))).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetDetailDto>>> DeletePharmacyDetail(int id)
        {
            ServiceResponse<List<GetDetailDto>> serviceResponse = new();
            try
            {
                PharmacyDetail detail = await _context.pharmacyDetails
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (detail != null)
                {
                    _context.pharmacyDetails.Remove(detail);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = (_context.pharmacyDetails
                        .Select(c => _mapper.Map<GetDetailDto>(c))).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Detail not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }



        public async Task<ServiceResponse<GetDetailDto>> UpdatePharmacyDetail(UpdateDetailDto pharmacyDetailupdate)
        {
            ServiceResponse<GetDetailDto> serviceResponse = new();
            try
            {
                PharmacyDetail detail = await _context.pharmacyDetails.FirstOrDefaultAsync(c => c.Id == pharmacyDetailupdate.Id);

                detail.Name = pharmacyDetailupdate.Name;
                detail.Cost = pharmacyDetailupdate.Cost;
                detail.Category = pharmacyDetailupdate.Category;
                detail.ExpiryDate = pharmacyDetailupdate.ExpiryDate;
                detail.ManufacturedBy = pharmacyDetailupdate.ManufacturedBy;

                _context.pharmacyDetails.Update(detail);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetDetailDto>(detail);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
