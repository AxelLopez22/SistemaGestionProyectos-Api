using System.ComponentModel.DataAnnotations;

namespace Api_ProjectManagement.Common.Validaciones
{
    public class PesoArchivoValidacion : ValidationAttribute
    {
        private readonly int pesoMaximo;
        public PesoArchivoValidacion(int PesoMaximo)
        {
            pesoMaximo = PesoMaximo;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile? formFile = value as IFormFile;
            if (formFile == null)
            {
                return ValidationResult.Success;
            }
            if (formFile.Length > pesoMaximo * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {pesoMaximo} MB");
            }
            return ValidationResult.Success;
        }
    }
}
