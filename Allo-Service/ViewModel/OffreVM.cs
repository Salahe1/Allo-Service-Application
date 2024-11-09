using System.ComponentModel.DataAnnotations;

namespace Allo_Service.ViewModel
{
    public class OffreVM
    {
        [Required(ErrorMessage = "Le titre est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le titre ne peut pas dépasser 100 caractères.")]
        public string Titre { get; set; }

        [Required(ErrorMessage = "La photo est obligatoire.")]
        [DataType(DataType.Upload)]
        public IFormFile Photo { get; set; }

        [Required(ErrorMessage = "La description est obligatoire.")]
        [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La date de début est obligatoire.")]
        [DataType(DataType.Date)]
        public DateTime DateDebut { get; set; }

        [Required(ErrorMessage = "La date de fin est obligatoire.")]
        [DataType(DataType.Date)]
        [DateGreaterThan("DateDebut", ErrorMessage = "La date de fin doit être postérieure à la date de début.")]
        public DateTime DateFin { get; set; }

        [Required(ErrorMessage = "Le prix est obligatoire.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Le prix doit être supérieur à zéro.")]
        public decimal Prix { get; set; }


    }

    // Custom validation attribute to ensure that the end date is after the start date
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public DateGreaterThanAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var startDate = (DateTime)validationContext.ObjectType
                .GetProperty(_startDatePropertyName)
                .GetValue(validationContext.ObjectInstance);

            var endDate = (DateTime)value;

            if (endDate <= startDate)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }

}

