using System.Collections.Generic;
using System.Linq;
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

        [Test]
        public void should_return_two_error_messages_when_password_is_shorter_than_8_characters_and_does_not_contain_at_least_2_numbers()
        {
            var password = "abci";

            var validationResult = passwordValidator.Validate(password);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.ErrorMessage.Contains("Password must be at least 8 characters\nPassword must contain at least 2 numbers"));
        }

        [Test]
        public void should_return_error_when_password_does_not_contain_at_least_one_capital_letter()
        {
            var password = "abci";

            var validationResult = passwordValidator.Validate(password);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.ErrorMessage.Contains("Password must contain at least one capital letter"));
        }
    }

    public class PasswordValidator
    {
        public ValidationResult Validate(string password)
        {
            var errorMessages = new List<string>();
            if (password.Length < 8)
            {
                errorMessages.Add("Password must be at least 8 characters");
            }
            errorMessages.Add("Password must contain at least 2 numbers");

            return new ValidationResult(false, string.Join('\n', errorMessages));
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