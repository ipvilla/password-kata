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
    }

    public class PasswordValidator
    {
        public ValidationResult Validate(string password)
        {
            return new ValidationResult(false, "Password must be at least 8 characters");
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