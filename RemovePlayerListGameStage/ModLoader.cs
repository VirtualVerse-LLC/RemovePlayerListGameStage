using System.Reflection;
using System.Xml.Linq;
using System.IO;
using HarmonyLib;

namespace BaseMod 
{
    public class ModLoader : IModApi
    {
        public static bool IsEnabled { get; set; }
        public static bool IsDebugging { get; set; }
        public static string Version = "v1.0";
        public static string MessagePrefix = "[Remove Scoreboard Game Stage]";


        public void InitMod(Mod _modInstance)
        {
            LoadConfig();
            var harmony = new Harmony(_modInstance.Name);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            Log.Out($"Started Remove Scoreboard Game Stage {Version}");
        }
        public static void LoadConfig()
        {
            var configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.xml");
            if (File.Exists(configPath))
            {
                var configXml = XDocument.Load(configPath);
                // load enabled
                var enabledElement = configXml.Root.Element("enabled");
                if (enabledElement != null && bool.TryParse(enabledElement.Value, out bool isEnabled))
                {
                    IsEnabled = isEnabled;
                }
                // load debugging
                var debuggingElement = configXml.Root.Element("debug");
                if (debuggingElement != null && bool.TryParse(debuggingElement.Value, out bool isDebugging))
                {
                    IsDebugging = isDebugging;
                }
            }
        }

        public static void SaveConfig()
        {
            var configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.xml");
            var configXml = new XDocument(
                new XElement("config",
                    new XElement("enabled", IsEnabled),
                    new XElement("debug", IsDebugging)
                )
            );
            configXml.Save(configPath);
        }
    }
}
