using SportComplexResourceOptimizationApi.Application.Models.CreateDto;
using SportComplexResourceOptimizationApi.Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.Models.Identity;
using SportComplexResourceOptimizationApi.Application.Models.UpdateDto;
using SportComplexResourceOptimizationApi.Application.Paging;

namespace SportComplexResourceOptimizationApi.Application.IServices;

public interface IUserService
{
    Task AddUserAsync(UserCreateDto dto, CancellationToken cancellationToken);

    Task<UserDto> GetUserAsync(string id, CancellationToken cancellationToken);

    Task<PagedList<UserDto>> GetUsersPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task<UpdateUserModel> UpdateAsync(UserUpdateDto userDto, CancellationToken cancellationToken);

    Task<TokensModel> LoginAsync(LoginUserDto login, CancellationToken cancellationToken);
}
