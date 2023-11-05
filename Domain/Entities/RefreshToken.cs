using SportComplexResourceOptimizationApi.Domain.Common;

namespace SportComplexResourceOptimizationApi.Domain.Entities;

public class RefreshToken : EntityBase
{
   public string Token { get; set; }     

   public DateTime ExpiryDateUTC { get; set; }
}