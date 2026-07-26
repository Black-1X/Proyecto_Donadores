using Donantes.API.Dtos.Common;
using Donantes.API.Dtos.Donadores;

namespace Donantes.API.Services.Donadores
{
    public interface IDonadorService
    {
        Task<ResponseDto<List<ResponseDonadorDto>>> GetAllAsync();

        Task<ResponseDto<ResponseDonadorDto>> GetByIdAsync(string id);

        Task<ResponseDto<ResponseDonadorDto>> CreateAsync(
            CreateDonadorDto dto
        );

        Task<ResponseDto<ResponseDonadorDto>> UpdateAsync(
            string id,
            UpdateDonadorDto dto
        );

        Task<ResponseDto<bool>> DeleteAsync(string id);
    }
}