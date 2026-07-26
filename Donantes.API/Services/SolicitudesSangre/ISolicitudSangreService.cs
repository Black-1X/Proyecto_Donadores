using Donantes.API.Dtos.Common;
using Donantes.API.Dtos.SolicitudesSangre;

namespace Donantes.API.Services.SolicitudesSangre
{
    public interface ISolicitudSangreService
    {
        Task<ResponseDto<List<ResponseSolicitudSangreDto>>> GetAllAsync();

        Task<ResponseDto<ResponseSolicitudSangreDto>> GetByIdAsync(
            string id
        );

        Task<ResponseDto<ResponseSolicitudSangreDto>> CreateAsync(
            CreateSolicitudSangreDto dto
        );

        Task<ResponseDto<ResponseSolicitudSangreDto>> UpdateAsync(
            string id,
            UpdateSolicitudSangreDto dto
        );

        Task<ResponseDto<bool>> DeleteAsync(string id);
    }
}