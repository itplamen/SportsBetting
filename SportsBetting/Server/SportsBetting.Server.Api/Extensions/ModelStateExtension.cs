namespace SportsBetting.Server.Api.Extensions
{
    using System.Collections.Generic;
    using System.Web.Http.ModelBinding;

    using SportsBetting.Common.Results;

    public static class ModelStateExtension
    {
        public static void AddModelErrors(this ModelStateDictionary dictionary, IEnumerable<ValidationResult> validations)
        {
            foreach (var validation in validations)
            {
                dictionary.AddModelError(validation.ErrorKey, validation.ErrorMessage);
            }
        }
    }
}