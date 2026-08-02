using Donantes.API.Dtos.Common;
using Donantes.API.Dtos.Donadores;
using Donantes.API.Dtos.SolicitudesSangre;


namespace Donantes.API.Services.SolicitudesSangre
{
    public interface ISolicitudSangreService
    {
        Task<ResponseDto<PageDto<List<ResponseSolicitudSangreDto>>>>
            GetPageAsync(
                string searchTerm = "",
                int page = 1,
                int pageSize = 10
            );

        Task<ResponseDto<CreateSolicitudResponseDto>>
            GetByIdAsync(string id);

        Task<ResponseDto<CreateSolicitudResponseDto>>
            CreateAsync(CreateSolicitudSangreDto dto);

        Task<ResponseDto<ResponseSolicitudSangreDto>>
            UpdateAsync(
                string id,
                UpdateSolicitudSangreDto dto
            );

        Task<ResponseDto<bool>> DeleteAsync(string id);

        Task<ResponseDto<List<ResponseDonadorDto>>>
            GetDonantesDisponiblesAsync(string solicitudId);

        Task<ResponseDto<ResponseSolicitudSangreDto>>
            AsignarDonadorAsync(
                string solicitudId,
                string donadorId
            );
    }
}