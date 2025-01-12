using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Timesheets.Web.Models.Shared.Molecules
{
    [ModelBinder<TextInputModelBinder>]
    public class TextInputModel
    {
        public string Name { get; init; }

        public string Value { get; init; }

        public TextInputModel(string name)
        {
            Name = name;
            Value = string.Empty;
        }

        public TextInputModel(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    public class TextInputModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ArgumentNullException.ThrowIfNull(bindingContext);
    
            var modelName = bindingContext.ModelName;
    
            // try to fetch the value of the argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }
            var value = valueProviderResult.FirstValue ?? string.Empty;
    
            // create the object and update binding-context 
            var model = new TextInputModel(modelName, value);
            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            bindingContext.Result = ModelBindingResult.Success(model);
        
            return Task.CompletedTask;
        }
    }
}