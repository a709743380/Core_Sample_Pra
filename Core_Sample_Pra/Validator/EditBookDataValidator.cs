using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Core_Sample_Pra.Models;
using Core_Sample_Pra.ViewModel;
using FluentValidation;

namespace Core_Sample_Pra.Validator
{

    /// <summary>
    /// Card Parameter 的驗證器
    /// </summary>
    public class EditBookDataValidator : AbstractValidator<EditBookData>
    {
        /// <summary>
        /// 驗證器的建構式: 在這裡註冊我們要驗證的規則
        /// </summary>
        public EditBookDataValidator()
        {
            RuleFor(book => book.BOOK_NAME)
                .NotNull().WithMessage("{PropertyName} 不能為 null")
                .WithName("書名");

            //RuleFor(book => book.BOOK_AMOUNT)
            //    .NotEmpty().WithMessage("{PropertyName} 不能為空白")
            //    .NotNull().WithMessage("{PropertyName} 不能為 null")
            //    .WithName("金額")
            //;

            RuleFor(book => book.BOOK_AUTHOR)
                .NotNull().WithMessage("{PropertyName} 不能為 null")
                .WithName("作者")
                ;
            RuleFor(book => book.BOOK_PUBLISHER)
                .NotNull().WithMessage("{PropertyName} 不能為 null")
                .WithName("出版商")
                ;

            RuleFor(book => book.BOOK_NOTE)
                .NotNull().WithMessage("{PropertyName} 不能為 null")
                .WithName("內容簡介")
                ;
            RuleFor(book => book.BOOK_STATUS)
                .NotNull().WithMessage("{PropertyName} 不能為 null")
                .When(n =>n.IsAdd==false)
                .WithName("借閱狀態")
                ;
            
            RuleFor(book => book.BOOK_KEEPER)
                .NotNull().WithMessage("{PropertyName} 不能為 null")
                .When(n => n.IsAdd == false)
                .WithName("借閱人")
                ;

            RuleFor(book => book.BOOK_BOUGHT_DATE)
                .GreaterThan(new DateTime(1500, 1, 1))
                .WithMessage("{PropertyName} 必須大於 1500 年")
                .WithName("購書日期")
                ;

            RuleFor(book => book.BOOK_CLASS_ID)
                .NotEmpty().WithMessage("{PropertyName} 不能為空白")
                .NotNull().WithMessage("{PropertyName} 不能為 null")
                .WithName("圖書類別")
                ;


        }
    }
}

