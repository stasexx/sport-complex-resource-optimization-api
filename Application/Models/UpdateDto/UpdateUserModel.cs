using SportComplexResourceOptimizationApi.Application.Models.Dtos;
using SportComplexResourceOptimizationApi.Application.Models.Identity;

namespace SportComplexResourceOptimizationApi.Application.Models.UpdateDto;

public class UpdateUserModel
{
    public TokensModel Tokens { get; set; }

    public UserUpdateDto User { get; set; }
}
