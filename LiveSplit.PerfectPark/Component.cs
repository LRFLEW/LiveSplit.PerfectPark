using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.PerfectPark
{
    class Component : LogicComponent
    {
        public override string ComponentName => "Perfect Park Autosplitter";

        LiveSplitState _state;
        TimerModel _timer;
        Settings _settings = new Settings();
        IScanner _scan;
        long _gameTime = 0;

        public Component(LiveSplitState state)
        {
            _state = state;
            _state.IsGameTimePaused = true;
            _timer = new TimerModel { CurrentState = state };
            _timer.OnStart += On_TimerStart;
            _settings.MethodChanged += SetupScanner;
            SetupScanner();
        }

        private void SetupScanner()
        {
            switch (_settings.Method)
            {
                case 0: _scan = new MemScanner(); break;
                case 1: _scan = new LogScanner(); break;
                default: throw new Exception("Unknown Method");
            }
            _scan.RaceStart += On_RaceStart;
            _scan.MapStart += On_MapStart;
            _scan.MapTime += On_MapTime;
            _scan.Goal += On_Goal;
        }

        private void On_TimerStart(object sender, EventArgs e)
        {
            _gameTime = 0;
        }

        private void On_RaceStart()
        {
            if (_state.CurrentPhase != TimerPhase.NotRunning && _settings.AutoReset) _timer.Reset();
            if (_state.CurrentPhase == TimerPhase.NotRunning && _settings.AutoStart) _timer.Start();
        }

        private void On_MapStart(long offset)
        {
            if (_state.CurrentPhase == TimerPhase.Running)
            {
                _state.SetGameTime(TimeSpan.FromMilliseconds(_gameTime - offset));
                _state.IsGameTimePaused = false;
            }
        }

        private void On_MapTime(long time)
        {
            // Update game time to match the game
            if (_state.CurrentPhase == TimerPhase.Running)
            {
                _state.SetGameTime(TimeSpan.FromMilliseconds(_gameTime + time));
            }
        }

        private void On_Goal(long time)
        {
            if (_state.CurrentPhase == TimerPhase.Running)
            {
                _gameTime += time;
                _state.SetGameTime(TimeSpan.FromMilliseconds(_gameTime));
                _state.IsGameTimePaused = true;
                _timer.Split();
            }
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            _scan.Update();
        }

        public override XmlNode GetSettings(XmlDocument document) => _settings.GetSettings(document);

        public override Control GetSettingsControl(LayoutMode mode) => _settings;

        public override void SetSettings(XmlNode settings) => _settings.SetSettings(settings);

        public override void Dispose()
        {
            _scan.Dispose();
            _settings.Dispose();
        }
    }
}
