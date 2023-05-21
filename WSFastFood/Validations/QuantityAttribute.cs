using System.ComponentModel.DataAnnotations;

namespace WSFastFood.Validations
{
    public class QuantityAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if ( value == null || string.IsNullOrEmpty(value.ToString()) )
            {
                return ValidationResult.Success;
            }

            int quantity = int.Parse(value.ToString()!);

            if (quantity == 0)
            {
                return new ValidationResult("Cantidad Inválida"); 
            }

            return ValidationResult.Success;
        }
    }
}
