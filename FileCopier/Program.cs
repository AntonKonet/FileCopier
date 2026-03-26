using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.Json;

namespace FileCopier
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string configFilePath = Path.Combine(AppContext.BaseDirectory, "config.json");
            string json = File.ReadAllText(configFilePath);

            Config config = JsonSerializer.Deserialize<Config>(json);

            string sourceFilePath = Path.Combine(config.SourceFolder, config.SourceFileName);
            string targetFileName = BuildTargetFileName(config.Template, config.Prefix, config.Id, config.TargetFolder);
            string targetFilePath = Path.Combine(config.TargetFolder, targetFileName);
            File.Copy(sourceFilePath, targetFilePath);
        }

        private static string BuildTargetFileName(string template, string prefix, string id, string targetFolder)
        {
            string fileName = template;

            fileName = fileName.Replace("%prefix%", prefix);

            int idLength = id.Length;
            int freeId = int.Parse(id);

            string[] filesNames = Directory.GetFiles(targetFolder, $"{prefix}_*");
            int[] takenIds = filesNames.Select(
                s => int.Parse(Path.GetFileName(s).Substring(prefix.Length + 1, idLength))
            ).ToArray();

            while (takenIds.Contains(freeId) && freeId < 9999)
                freeId += 1;

            fileName = fileName.Replace("%id%", freeId.ToString($"D{idLength}"));

            fileName = fileName.Replace("%timestamp%", DateTime.Now.ToString("yyyyMMddHHmmss"));

            return fileName;
        }
    }
}
