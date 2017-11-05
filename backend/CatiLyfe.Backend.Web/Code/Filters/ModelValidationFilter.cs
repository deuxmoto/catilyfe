namespace CatiLyfe.Backend.Web.Code.Filters
{
    using System.Linq;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    using CatiLyfe.Common.Exceptions;

    /// <summary>
    /// Validates the model state.
    /// </summary>
    public class ModelValidationFilter : ActionFilterAttribute
    {
        /// <summary>
        /// The on action executing.
        /// </summary>
        /// <param name="actionContext">
        /// The action context.
        /// </param>
        /// <exception cref="InvalidArgumentException">
        /// </exception>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelstate = actionContext.ModelState;
            if (false == modelstate.IsValid)
            {
                throw new InvalidArgumentException(string.Join(", ", modelstate.Select(state => $"{state.Key} : {state.Value.Errors.FirstOrDefault()?.Exception.Message}")));
            }

            base.OnActionExecuting(actionContext);
        }

    }
}