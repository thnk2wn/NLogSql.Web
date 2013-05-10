using System;
using System.IO;
using System.Linq;

namespace NLogSql.Web.Infrastructure.Diagnostics.Info
{
    public class AssemblyInfo : DiagnosticInfoBase
    {
        protected override void GenerateReport()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().OrderBy(x=>x.FullName);
            
            StartTable();

            foreach (var asm in assemblies)
            {
                var name = asm.GetName().Name;
                var ver = asm.GetName().Version.ToString();

                var lastModified = "N/A";
                var size = "N/A";
                var location = "N/A";

                if (!asm.IsDynamic)
                {
                    var fi = new FileInfo(asm.Location);
                    lastModified = fi.LastWriteTime.ToString("G");
                    size = (fi.Length/1024).ToString("##0") + " KB";
                    location = asm.Location;
                }

                //TODO: better formatting of assembly info
                AppendRow(name, string.Format("Version: {0}. Last modified: {1}. Size: {2}. Location: {3}", 
                    ver, lastModified, size, location));
            }

            EndTable();
        }
    }
}