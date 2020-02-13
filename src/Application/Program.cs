using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Application
{
    public static class Program
    {
        public static void Main(string[] _)
        {
            var padding = Padding();
            Console.WriteLine("{0} Git {0}", padding);
            Console.WriteLine("{0,-10} {1}", "Commit", ThisAssembly.Git.Commit);
            Console.WriteLine("{0,-10} {1}", "Commit SHA", ThisAssembly.Git.Sha);
            Console.WriteLine("{0,-10} {1}", "Branch", ThisAssembly.Git.Branch);
            Console.WriteLine("{0,-10} {1}", "Tag", ThisAssembly.Git.Tag);
            Console.WriteLine("{0,-10} {1}", "Commits", ThisAssembly.Git.Commits);

            Console.WriteLine("{0} Assembly {0}", padding);
            Console.WriteLine("{0,-20} {1}", "Version", typeof(Program).Assembly.GetName().Version?.ToString(3));
            Console.WriteLine("{0,-20} {1}", "Assembly",
                typeof(Program).Assembly.GetCustomAttribute<AssemblyVersionAttribute>()?.Version ?? "Not found: AssemblyVersionAttribute");
            Console.WriteLine("{0,-20} {1}", "Informational",
                typeof(Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                    ?.InformationalVersion);
            Console.WriteLine("{0,-20} {1}", "File",
                typeof(Program).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()?.Version);

            if (Debugger.IsAttached) Console.ReadKey();
        }

        private static string Padding()
        {
            return string.Join(string.Empty, Enumerable.Range(0, 10).Select(_ => "=").ToArray());
        }
    }
}
