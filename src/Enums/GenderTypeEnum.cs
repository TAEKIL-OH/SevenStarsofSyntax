using System.ComponentModel.DataAnnotations;
using System;

namespace ContosoCrafts.WebSite.Enums
{
    /// <summary>
    /// Define enums for different genders
    /// </summary>
    public enum GenderTypeEnum
    {

        // Adding categories for Gender list displayed on create and update page for user to choose from
        [Display(Name = "Undefined")] Undefined = 0,
        [Display(Name = "Male")] Male = 1,
        [Display(Name = "Female")] Female = 2,
    }
}