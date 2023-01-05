using System.ComponentModel.DataAnnotations;

namespace ApiContracts
{
    public record CurrencyRequest(
        [Required]
        string Name,
        string Description);

}
