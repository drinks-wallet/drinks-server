using System.Linq;
using System.Text;

// ReSharper disable once CheckNamespace
namespace System.Web.Mvc
{
    public static class HtmlHelpers
    {
        static readonly string _ValidationSummaryFormatString = string.Concat(
                "<div class=\"alert alert-dismissable alert-danger\">", Environment.NewLine,
                    "<button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>", Environment.NewLine,    
                    "<h4>Error</h4>", Environment.NewLine,
                "{0}</div>");

        public static MvcHtmlString BootstrapValidationSummary(this HtmlHelper helper)
        {
            return helper.ViewData.ModelState.IsValid
                ? MvcHtmlString.Empty
                : MvcHtmlString.Create(string.Format(_ValidationSummaryFormatString, GenerateErrorHtmlString(helper.ViewData.ModelState)));
        }

        public static string GenerateErrorHtmlString(ModelStateDictionary modelState)
        {
            var sb = new StringBuilder();
            foreach (var error in modelState.Keys.SelectMany(key => modelState[key].Errors))
            {
                sb.Append("<p>");
                sb.Append(error.ErrorMessage);
                sb.Append("</p>");
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}