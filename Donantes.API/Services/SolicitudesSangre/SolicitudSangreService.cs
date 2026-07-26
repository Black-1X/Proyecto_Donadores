using Donantes.API.Database;
using Donantes.API.Dtos.Common;
using Donantes.API.Dtos.SolicitudesSangre;

namespace Donantes.API.Services.SolicitudesSangre
{
    public class SolicitudSangreService : ISolicitudSangreService
    {
        private readonly BloodDonationDbContext _context;

        public SolicitudSangreService(
            BloodDonationDbContext context
        )
        {
            _context = context;
        }

        public Task<ResponseDto<List<ResponseSolicitudSangreDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ResponseSolicitudSangreDto>> GetByIdAsync(
            string id
        )
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ResponseSolicitudSangreDto>> CreateAsync(
            CreateSolicitudSangreDto dto
        )
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<ResponseSolicitudSangreDto>> UpdateAsync(
            string id,
            UpdateSolicitudSangreDto dto
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