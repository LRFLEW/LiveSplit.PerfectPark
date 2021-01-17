using LiveSplit.Model;
using LiveSplit.UI.Components;
using System.Reflection;

namespace LiveSplit.PerfectPark
{
    public class ComponentFactory : IComponentFactory
    {
        public string ComponentName => "Perfect Park Autosplitter";
        public string Description => "Autosplitter tjern's perfect park (tjern.itch.io/perfect-park)";
        public ComponentCategory Category => ComponentCategory.Control;
        public IComponent Create(LiveSplitState state) => new Component(state);
        public string UpdateName => this.ComponentName;
        public string UpdateURL => "https://raw.githubusercontent.com/LRFLEW/LiveSplit.PerfectPark/";
        public string XMLURL => this.UpdateURL + "main/Components/Updates.xml";
        public System.Version Version => Assembly.GetExecutingAssembly().GetName().Version;
    }
}
