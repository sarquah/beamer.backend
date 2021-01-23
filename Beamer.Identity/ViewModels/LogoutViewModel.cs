using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Beamer.Identity.ViewModels
{
    public class LogoutViewModel
    {
        [BindNever]
        public string RequestId { get; set; }
    }
}
