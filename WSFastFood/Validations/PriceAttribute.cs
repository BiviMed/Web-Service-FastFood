using System.ComponentModel.DataAnnotations;

namespace WSFastFood.Validations
{
    public class PriceAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if ( value == null || string.IsNullOrEmpty(value.ToString()) )
            {
                return ValidationResult.Success;
            }

            decimal price = (Decimal)value;

            if (price == 0)
            {
                return new ValidationResult("Precio Inválido");
            }

            return ValidationResult.Success;
        }
    }
}
