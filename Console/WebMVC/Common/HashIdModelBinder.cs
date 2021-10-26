using HashidsNet;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace WebMVC.Common
{
    public class HashIdModelBinder : IModelBinder
    {
        Hashids hashids = new Hashids("kay");//加盐

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            var str = valueProviderResult.FirstValue;

            bindingContext.Result = ModelBindingResult.Success(hashids.Decode(str)[0]);

            return Task.CompletedTask;
        }
    }
}
