using System;
using System.Linq;
using Framework.Utility;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace WebApi6_0.Filter
{
    public class ValidationFailedResultModel : TResponse<object>
    {
        public ValidationFailedResultModel(ModelStateDictionary modelState)
        { 
            Message = "参数不合法";
            Data = modelState.Keys
                        .SelectMany(key => modelState[key]!.Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                        .ToList();
            Code = (int)ReturnStatus.Fail;
        }
    }
     
    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }
        public string Message { get; }
        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null!;
            Message = message;
        }
    }
    public enum ReturnStatus
    {
        Success = 1,
        Fail = 0,
        ConfirmIsContinue = 2,
        Error = 3
    }
}
