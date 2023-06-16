using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnnotationStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            Author author = new Author();
            author.FirstName = "Yuna";
            author.LastName = "Sim";
            author.PhoneNumber = "01000000000";
            author.Email = "s921119@hanmail.net";

            // 유효성 검사
            ValidationContext context = new ValidationContext(author, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(author, context, validationResults, true);
            if (!valid)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    Console.WriteLine("{0}", validationResult.ErrorMessage);
                }
            }

            Author author2 = new Author();
            //author2.FirstName = "A";
            author2.LastName = "";
            author2.PhoneNumber = "";
            author2.Email = "";

            // 유효성 검사
            context = new ValidationContext(author2, null, null);
            validationResults = new List<ValidationResult>();
            valid = Validator.TryValidateObject(author2, context, validationResults, true);
            if (!valid)
            {
                foreach (ValidationResult validationResult in validationResults)
                {
                    Console.WriteLine("{0}", validationResult.ErrorMessage);
                }
            }
        }
    }

    public class Author
    {
        [Required(ErrorMessage = "{0}는 필수 값 입니다.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First Name의 문자 길이는 최소:3, 최대:50 입니다.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0}는 필수 값 입니다.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last Name의 문자 길이는 최소:3, 최대:50 입니다.")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [IsEmpty(ErrorMessage = "빈 값일 수 없습니다.")]
        public string strCusValue { get; set; }
    }

    // 사용자 지정 유효성 검사 클래스 만들기
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class IsEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputValue = value as string;
            return !string.IsNullOrEmpty(inputValue);
        }
    }
}
