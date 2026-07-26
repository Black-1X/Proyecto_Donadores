using Donantes.API.Database;
using Donantes.API.Dtos.Common;
using Donantes.API.Dtos.Donadores;

namespace Donantes.API.Services.Donadores
{
    public class DonadorService : IDonadorService
    {
        private readonly BloodDonationDbContext _context;

        public DonadorService(BloodDonationDbContext context)
        {
            _context = context;
        }

        public Task<ResponseDto<List<ResponseDonadorDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ResponseDonadorDto>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ResponseDonadorDto>> CreateAsync(
            CreateDonadorDto dto
        )
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ResponseDonadorDto>> UpdateAsync(
            string id,
            UpdateDonadorDto dto
        )
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<bool>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}