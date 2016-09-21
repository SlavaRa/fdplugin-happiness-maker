using System.ComponentModel;
using System.Windows.Forms;
using PluginCore;
using PluginCore.Managers;

namespace HappinessMaker
{
    public class HappinessMaker : IPlugin
    {
        #region Required Properties

        /// <summary>
        /// Api level of the plugin
        /// </summary>
        public int Api => 1;

        /// <summary>
        /// Name of the plugin
        /// </summary> 
        public string Name => "HappinessMaker";

        /// <summary>
        /// GUID of the plugin
        /// </summary>
        public string Guid => "b96691e9-8d89-4c8b-a21e-7c6d57607610";

        /// <summary>
        /// Author of the plugin
        /// </summary> 
        public string Author => "SlavaRa";

        /// <summary>
        /// Description of the plugin
        /// </summary> 
        public string Description => "Tried one - you will never be the same";

        /// <summary>
        /// Web address for help
        /// </summary> 
        public string Help => "";

        /// <summary>
        /// Object that contains the settings
        /// </summary>
        [Browsable(false)]
        public object Settings => null;

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
            var handled = ((KeyEvent) e).Value == (Keys.Shift | Keys.Return);
            e.Handled = handled;
            if (!handled) return;
            var sci = PluginBase.MainForm.CurrentDocument.SciControl;
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