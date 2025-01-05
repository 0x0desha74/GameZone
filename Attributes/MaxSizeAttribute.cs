namespace GameZone.Attributes
{
    public class MaxSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;
        public MaxSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file is not null)
            {
              if(file.Length > _maxSize)
                {
                    return new ValidationResult($"Maximum Allowed Size is {FileSettings.MaxFileSizeInMB}MB");
                }

            }
            return ValidationResult.Success;
        }
    }
}
