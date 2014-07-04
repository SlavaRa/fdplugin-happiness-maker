using PluginCore;
using PluginCore.Helpers;
using PluginCore.Managers;
using PluginCore.Utilities;
using System;
using System.ComponentModel;
using System.IO;

namespace HappinessMaker
{
    public class HappinessMaker : IPlugin
    {
        private string pluginName = "HappinessMaker";
        private string pluginGuid = "b96691e9-8d89-4c8b-a21e-7c6d57607610";
        private string pluginHelp = "";
        private string pluginDesc = "";
        private string pluginAuth = "s.buynov";
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
        public Object Settings
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
        public void HandleEvent(Object sender, NotifyEvent e, HandlingPriority prority)
        {
            TraceManager.Add(string.Format("{0}:{1}", "e", e));
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
            EventManager.AddEventHandler(this, EventType.Keys);
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
