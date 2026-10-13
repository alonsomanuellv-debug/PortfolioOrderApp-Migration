using System.Diagnostics;
using System.Web;

namespace PortfolioOrderApp.Infrastructure
{
 
    public class ComplianceTraceModule : IHttpModule
    {
        private const string StopwatchKey = "__RequestStopwatch";

        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
            context.EndRequest += OnEndRequest;
        }

        private void OnBeginRequest(object sender, System.EventArgs e)
        {
            HttpContext.Current.Items[StopwatchKey] = Stopwatch.StartNew();
        }

        private void OnEndRequest(object sender, System.EventArgs e)
        {
            var sw = HttpContext.Current.Items[StopwatchKey] as Stopwatch;
            if (sw != null)
            {
                sw.Stop();
                Trace.WriteLine(string.Format(
                    "{0} tomo {1} ms",
                    HttpContext.Current.Request.Path,
                    sw.ElapsedMilliseconds));
            }
        }

        public void Dispose()
        {
        }
    }
}
