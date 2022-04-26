using NUnit.Framework;

namespace PasswordValidatorSpecs
{
    public class Tests
    {
        private PasswordValidator passwordValidator;

        [SetUp]
        public void Setup()
        {
            passwordValidator = new PasswordValidator();
        }

        [Test]
        public void should_return_validation_error_when_password_is_shorter_than_8_characters()
        {
            var password = "a";

            var validationResult = passwordValidator.Validate(password);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.ErrorMessage.Contains("Password must be at least 8 characters"));
        }

        [Test]
        public void should_return_validation_error_when_password_does_not_contain_at_least_2_numbers()
        {
            var password = "abcdefghi";

            var validationResult = passwordValidator.Validate(password);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.ErrorMessage.Contains("Password must contain at least 2 numbers"));
        }
    }

    public class PasswordValidator
    {
        public ValidationResult Validate(string password)
        {
            if (password.Length < 8)
            {
                return new ValidationResult(false, "Password must be at least 8 characters");
            }

            return new ValidationResult(false, "Password must contain at least 2 numbers");
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }

        public ValidationResult(bool isValid, string errorMessage)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }
    }
}