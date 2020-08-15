using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace controllerbattery.UPower
{
    public class UPowerWrapper
    {
        private Process ChildProcess { get; }

        public UPowerWrapper(string[] args)
        {
            ChildProcess = ConfigureProcess(args);
        }

        public ICollection<string> GetOutput()
        {
            ChildProcess.Start();
            var output = ChildProcess.StandardOutput.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
            return output.ToList();
        }

        private Process ConfigureProcess(string[] args = null)
        {
            var proc = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/usr/bin/upower",
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                }
            };

            proc.StartInfo.Arguments = string.Join(" ", args);

            return proc;
        }
    }
}