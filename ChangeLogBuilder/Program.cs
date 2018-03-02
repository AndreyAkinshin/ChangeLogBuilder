using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ChangeLogBuilder
{
    public static class Program
    {
        public static async Task Main()
        {
            try
            {
                if (!File.Exists(Config.MainConfigFileName))
                {
                    Config.CreateBlank().WriteFile(Config.MainConfigFileName);
                    Console.WriteLine(Config.MainConfigFileName + " is created. Please, fill it.");
                    return;
                }

                var config = Config.ReadFile(Config.MainConfigFileName);
                var releaseNotes = await MarkdownBuilder.Build(config);
                Console.WriteLine(releaseNotes);
                File.WriteAllText("release-notes.md", releaseNotes);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Demystify().ToString());
            }
        }
    }
}