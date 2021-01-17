using LiveSplit.ComponentUtil;
using System;
using System.Diagnostics;
using System.Linq;

namespace LiveSplit.PerfectPark
{
    class MemScanner : IScanner
    {
        MemoryWatcher<int> _sceneIndex;
        MemoryWatcher<byte> _countdownFinished, _someoneFinishedEvent;
        DeepPointer _eventTimer, _finishTime;
        Process _park = null;
        bool _racing = false;

#pragma warning disable 0067
        public event Action RaceStart;
        public event Action<long> MapStart;
        public event Action<long> MapTime;
        public event Action<long> Goal;
#pragma warning restore 0067

        public MemScanner()
        {
            _sceneIndex = new MemoryWatcher<int>(
                new DeepPointer("UnityPlayer.dll", 0x0168FF58, 0x08, 0x00, 0x98));
            _sceneIndex.OnChanged += SceneIndexChanged;

            _countdownFinished = new MemoryWatcher<byte>(
                new DeepPointer("UnityPlayer.dll", 0x0168FF58, 0x48, 0xB8, 0x80, 0xB9));
            _countdownFinished.OnChanged += CountdownFinishedChanged;

            _someoneFinishedEvent = new MemoryWatcher<byte>(
                new DeepPointer("UnityPlayer.dll", 0x0168FF58, 0x48, 0xB8, 0x80, 0xB8));
            _someoneFinishedEvent.OnChanged += SomeoneFinishedEventChanged;

            _eventTimer = new DeepPointer("UnityPlayer.dll", 0x0168FF58, 0x48, 0xB8, 0x80, 0xA8);
            _finishTime = new DeepPointer("UnityPlayer.dll", 0x0168FF58, 0x48, 0xB8, 0x80, 0xB4);
        }

        public void Update()
        {
            try
            {
                if (_park != null && _park.HasExited)
                {
                    _park = null;
                    _sceneIndex.Reset();
                    _countdownFinished.Reset();
                    _someoneFinishedEvent.Reset();
                }

                if (_park == null)
                {
                    Process[] processes = Process.GetProcessesByName("perfect-park_windows");
                    _park = processes.Length == 0 ? null : processes[0];
                }

                if (_park != null)
                {
                    _sceneIndex.Update(_park);
                    _countdownFinished.Update(_park);
                    _someoneFinishedEvent.Update(_park);
                    if (_racing) MapTime(RoundedMilliseconds(_eventTimer));
                }
            }
            catch
            {
                // Sometimes reads may fail due to race conditions.
                // Treat these exceptions as an exit event.
                _park = null;
                _sceneIndex.Reset();
                _countdownFinished.Reset();
                _someoneFinishedEvent.Reset();
            }
        }

        private long RoundedMilliseconds(DeepPointer timer) =>
            (long)Math.Round(timer.Deref<float>(_park) * 1000.0f);

        private void SceneIndexChanged(int old, int current)
        {
            if (current == 1) RaceStart();
            else _racing = false;
            _countdownFinished.Enabled = (current == 1);
            _someoneFinishedEvent.Enabled = (current == 1);
        }

        private void CountdownFinishedChanged(byte old, byte current)
        {
            if (old == 0 && current == 1)
            {
                MapStart(0);
                _racing = true;
            }
        }

        private void SomeoneFinishedEventChanged(byte old, byte current)
        {
            if (old == 0 && current == 1)
            {
                Goal(RoundedMilliseconds(_finishTime));
                _racing = false;
            }
        }

        public void Dispose() { }
    }
}
