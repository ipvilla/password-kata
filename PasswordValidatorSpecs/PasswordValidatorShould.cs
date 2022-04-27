using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace PasswordValidatorSpecs
{
    public class PasswordValidatorShould
    {
        private PasswordValidator passwordValidator;

        [SetUp]
        public void Setup()
        {
            passwordValidator = new PasswordValidator();
        }

        [Test]
        public void return_validation_error_when_password_is_shorter_than_8_characters()
        {
            var password = "a";

            var validationResult = passwordValidator.Validate(password);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.ErrorMessage.Contains("Password must be at least 8 characters"));
        }

        [Test]
        public void return_validation_error_when_password_does_not_contain_at_least_2_numbers()
        {
            var password = "abcdefghi";

            var validationResult = passwordValidator.Validate(password);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.ErrorMessage.Contains("Password must contain at least 2 numbers"));
        }

        [Test]
        public void return_two_error_messages_when_password_is_shorter_than_8_characters_and_does_not_contain_at_least_2_numbers()
        {
            var password = "abci";

            var validationResult = passwordValidator.Validate(password);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.ErrorMessage.Contains("Password must be at least 8 characters\nPassword must contain at least 2 numbers"));
        }

        [Test]
        public void return_error_when_password_does_not_contain_at_least_one_capital_letter()
        {
            var password = "abci";

            var validationResult = passwordValidator.Validate(password);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(validationResult.ErrorMessage.Contains("Password must contain at least one capital letter"));
        }

        [Test]
        public void return_error_when_password_does_not_contain_any_special_character()
        {
            var password = "abci";

            var validationResult = passwordValidator.Validate(password);

            Assert.IsFalse(validationResult.IsValid);
            Assert.IsTrue(
                validationResult.ErrorMessage.Contains("Password must contain at least one special character"));
        }

        [Test]
        public void return_ok_when_password_has_valid_format()
        {
            var password = "ThisIsAValidPassword_13";

            var validationResult = passwordValidator.Validate(password);

            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(string.Empty, validationResult.ErrorMessage);
        }
    }

    public class PasswordValidator
    {
        public ValidationResult Validate(string password)
        {
            var errorMessages = new List<string>();
            if (password.IsAlphanumeric())
            {
                errorMessages.Add("Password must contain at least one special character");
            }
            if (password.HasNoCapitalLetters())
            {
                errorMessages.Add("Password must contain at least one capital letter");
            }
            if (password.Length < 8)
            {
                errorMessages.Add("Password must be at least 8 characters");
            }
            if (password.GetCountOfDigits() < 2)
            {
                errorMessages.Add("Password must contain at least 2 numbers");
            }

            var isValid = !errorMessages.Any();

            return new ValidationResult(isValid, string.Join('\n', errorMessages));
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

    public static class StringExtensions
    {
        public static bool IsAlphanumeric(this string text)
        {
            var regex = new Regex("^[a-zA-Z0-9 ]*$");
            return regex.IsMatch(text);
        }

        public static bool HasNoCapitalLetters(this string text)
        {
            return text.Equals(text.ToLower());
        }

        public static int GetCountOfDigits(this string text)
        {
            return text.AsEnumerable().Count(char.IsDigit);
        }
    }
}