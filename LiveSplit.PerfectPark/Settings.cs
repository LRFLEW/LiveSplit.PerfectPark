using LiveSplit.UI;
using System;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.PerfectPark
{
    public partial class Settings : UserControl
    {
        public bool AutoStart { get => autoStartBox.Checked; }
        public bool AutoReset { get => autoResetBox.Checked; }
        public int Method { get => methodCombo.SelectedIndex; }

        public event Action MethodChanged;

        public Settings()
        {
            InitializeComponent();
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            XmlElement settingsNode = document.CreateElement("Settings");

            settingsNode.AppendChild(SettingsHelper.ToElement(document, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));
            settingsNode.AppendChild(SettingsHelper.ToElement(document, "AutoStart", autoStartBox.Checked));
            settingsNode.AppendChild(SettingsHelper.ToElement(document, "AutoReset", autoResetBox.Checked));
            settingsNode.AppendChild(SettingsHelper.ToElement(document, "Method", methodCombo.SelectedIndex));

            return settingsNode;
        }

        public void SetSettings(XmlNode settings)
        {
            var element = (XmlElement)settings;

            autoStartBox.Checked = SettingsHelper.ParseBool(settings["AutoStart"], true);
            autoResetBox.Checked = SettingsHelper.ParseBool(settings["AutoReset"], true);

            int newMethod = SettingsHelper.ParseInt(settings["Method"], 0);
            bool methodChanged = newMethod != methodCombo.SelectedIndex;
            methodCombo.SelectedIndex = newMethod;
            if (methodChanged) MethodChanged();
        }
    }
}
