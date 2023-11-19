using System.ComponentModel.DataAnnotations;
using System;
namespace ContosoCrafts.WebSite.Enums
{
    public enum GenderTypeEnum
    {
        [Display(Name = "Undefined")] Undefined = 0,
        [Display(Name = "Male")] Male = 1,
        [Display(Name = "Female")] Female = 2,
    }
}
