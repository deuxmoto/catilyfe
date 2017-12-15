using CatiLyfe.Common.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatiLyfe.Backend.Web.Core.Code.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.ModelState.IsValid == false)
            {
                var dict =  new Dictionary<string, List<string>>();

                foreach(var issue in context.ModelState)
                {
                    if(false == dict.ContainsKey(issue.Key))
                    {
                        dict[issue.Key] = new List<string>();
                    }

                    foreach(var error in issue.Value.Errors)
                    {
                        dict[issue.Key].Add(string.IsNullOrWhiteSpace(error.ErrorMessage) ? error.Exception?.Message : error.ErrorMessage);
                    }
                }

                throw new ModelValidationException(dict);
            }
        }
    }
}
