using PluginCore;
using PluginCore.Helpers;
using PluginCore.Managers;
using PluginCore.Utilities;
using ScintillaNet;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace HappinessMaker
{
    public class HappinessMaker : IPlugin
    {
        private string pluginName = "HappinessMaker";
        private string pluginGuid = "b96691e9-8d89-4c8b-a21e-7c6d57607610";
        private string pluginHelp = "";
        private string pluginDesc = "Tried one - you will nevver be the same";
        private string pluginAuth = "Slavara";
        private string settingFilename;
        private Settings settings;

        #region Required Properties

        /// <summary>
        /// Api level of the plugin
        /// </summary>
        public int Api
        {
            get { return 1; }
        }

        /// <summary>
        /// Name of the plugin
        /// </summary> 
        public string Name
        {
            get { return pluginName; }
        }

        /// <summary>
        /// GUID of the plugin
        /// </summary>
        public string Guid
        {
            get { return pluginGuid; }
        }

        /// <summary>
        /// Author of the plugin
        /// </summary> 
        public string Author
        {
            get { return pluginAuth; }
        }

        /// <summary>
        /// Description of the plugin
        /// </summary> 
        public string Description
        {
            get { return pluginDesc; }
        }

        /// <summary>
        /// Web address for help
        /// </summary> 
        public string Help
        {
            get { return pluginHelp; }
        }

        /// <summary>
        /// Object that contains the settings
        /// </summary>
        [Browsable(false)]
        public object Settings
        {
            get { return settings; }
        }

        #endregion

        #region Required Methods

        /// <summary>
        /// Initializes the plugin
        /// </summary>
        public void Initialize()
        {
            InitBasics();
            LoadSettings();
            AddEventHandlers();
        }

        /// <summary>
        /// Disposes the plugin
        /// </summary>
        public void Dispose()
        {
            SaveSettings();
        }

        /// <summary>
        /// Handles the incoming events
        /// </summary>
        public void HandleEvent(object sender, NotifyEvent e, HandlingPriority prority)
        {
            switch (e.Type)
            {
                case EventType.Keys:
                    bool handled = ((KeyEvent)e).Value == (Keys.Shift | Keys.Return);
                    e.Handled = handled;
                    if (handled)
                    {
                        ScintillaControl sci = PluginBase.MainForm.CurrentDocument.SciControl;
                        if (sci == null) return;
                        sci.LineEnd();
                        sci.NewLine();
                    }
                    return;
            }
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Initializes important variables
        /// </summary>
        private void InitBasics()
        {
            string path = Path.Combine(PathHelper.DataDir, pluginName);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            settingFilename = Path.Combine(path, "Settings.fdb");
        }

        /// <summary>
        /// Loads the plugin settings
        /// </summary>
        private void LoadSettings()
        {
            settings = new Settings();
            if (!File.Exists(settingFilename)) SaveSettings();
            else settings = (Settings)ObjectSerializer.Deserialize(settingFilename, settings);
        }

        /// <summary>
        /// Adds the required event handlers
        /// </summary> 
        private void AddEventHandlers()
        {
            PluginBase.MainForm.IgnoredKeys.Add(System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Enter);
            EventManager.AddEventHandler(this, EventType.Keys, HandlingPriority.High);
        }

        /// <summary>
        /// Saves the plugin settings
        /// </summary>
        private void SaveSettings()
        {
            ObjectSerializer.Serialize(settingFilename, settings);
        }

        #endregion
    }
}
