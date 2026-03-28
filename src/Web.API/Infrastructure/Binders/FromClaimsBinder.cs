using System.ComponentModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web.API.Infrastructure.Binders;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property)]
internal class FromClaimsAttribute : ModelBinderAttribute<FromClaimAttributeModelBinder>
{
    public FromClaimsAttribute(string claimType)
    {
        Name = claimType;
    }
}

internal sealed class FromClaimAttributeModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var identity = bindingContext.HttpContext.User.Identity as ClaimsIdentity;
        var claim = identity?.FindFirst(bindingContext.FieldName);

        if (string.IsNullOrEmpty(claim?.Value))
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var targetType = Nullable.GetUnderlyingType(bindingContext.ModelType) ?? bindingContext.ModelType;
        var converter = TypeDescriptor.GetConverter(targetType);

        if (!converter.CanConvertFrom(typeof(string)))
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        try
        {
            var value = converter.ConvertFromInvariantString(claim.Value);
            bindingContext.Result = ModelBindingResult.Success(value);
        }
        catch
        {
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}