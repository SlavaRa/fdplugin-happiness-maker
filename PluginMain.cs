using PluginCore;
using PluginCore.Managers;
using ScintillaNet;
using System.ComponentModel;
using System.Windows.Forms;

namespace HappinessMaker
{
    public class HappinessMaker : IPlugin
    {
        #region Required Properties

        /// <summary>
        /// Api level of the plugin
        /// </summary>
        public int Api { get { return 1; }}

        /// <summary>
        /// Name of the plugin
        /// </summary> 
        public string Name { get { return "HappinessMaker"; }}

        /// <summary>
        /// GUID of the plugin
        /// </summary>
        public string Guid { get { return "b96691e9-8d89-4c8b-a21e-7c6d57607610"; }}

        /// <summary>
        /// Author of the plugin
        /// </summary> 
        public string Author { get { return "SlavaRa"; }}

        /// <summary>
        /// Description of the plugin
        /// </summary> 
        public string Description { get { return "Tried one - you will never be the same"; }}

        /// <summary>
        /// Web address for help
        /// </summary> 
        public string Help { get { return ""; }}

        /// <summary>
        /// Object that contains the settings
        /// </summary>
        [Browsable(false)]
        public object Settings { get { return null; }}

        #endregion

        #region Required Methods

        /// <summary>
        /// Initializes the plugin
        /// </summary>
        public void Initialize()
        {
            AddEventHandlers();
        }

        /// <summary>
        /// Disposes the plugin
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Handles the incoming events
        /// </summary>
        public void HandleEvent(object sender, NotifyEvent e, HandlingPriority prority)
        {
            if (e.Type != EventType.Keys) return;
            bool handled = ((KeyEvent) e).Value == (Keys.Shift | Keys.Return);
            e.Handled = handled;
            if (!handled) return;
            ScintillaControl sci = PluginBase.MainForm.CurrentDocument.SciControl;
            if (sci == null) return;
            sci.LineEnd();
            sci.NewLine();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Adds the required event handlers
        /// </summary> 
        private void AddEventHandlers()
        {
            PluginBase.MainForm.IgnoredKeys.Add(Keys.Shift | Keys.Enter);
            EventManager.AddEventHandler(this, EventType.Keys, HandlingPriority.High);
        }

        #endregion
    }
}