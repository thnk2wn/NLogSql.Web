using System.Linq;
using System.Web.Mvc;
using NLogSql.Web.Infrastructure.Extensions.System_Web_Mvc_;

namespace NLogSql.Web.Infrastructure.Diagnostics.Info
{
    public class ViewDataInfo : DiagnosticInfoBase
    {
        private readonly ControllerBase _controller;

        public ViewDataInfo(ControllerBase controller)
        {
            _controller = controller;
        }

        protected override void GenerateReport()
        {
            var viewData = _controller.ViewData;
            if (null == viewData) return;
            if (null == viewData.ModelState) return;

            StartTable();

            AppendRow("ModelState.IsValid", viewData.ModelState.IsValid);

            var modelErrors = viewData.ModelState.GetModelErrors();

            if (null != modelErrors && modelErrors.Any())
            {
                foreach (var error in modelErrors)
                {
                    AppendRow("Error", error);
                }
            }

            EndTable();
        }
    }
}