using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Security.Principal;

namespace Client.Logic.Security
{
    class Admin
    {
        static internal bool IsVistaOrHigher() {
            return Environment.OSVersion.Version.Major >= 6;
        }

        /// <summary>
        /// Checks if the process is elevated
        /// </summary>
        /// <returns>If is elevated</returns>
        static internal bool IsAdmin() {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal p = new WindowsPrincipal(id);
            return p.IsInRole(WindowsBuiltInRole.Administrator);
        }

        static internal Process StartProcessElevated(string processPath, string arguments) {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = processPath;
            startInfo.Verb = "runas";
            startInfo.Arguments = arguments;

            Process process = null;

            try {
                process = Process.Start(startInfo);
            } catch (System.ComponentModel.Win32Exception) {
            }

            return process;
        }
    }
}
