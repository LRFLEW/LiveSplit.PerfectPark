using System;

namespace LiveSplit.PerfectPark
{
    interface IScanner : IDisposable
    {
        event Action RaceStart;
        event Action<long> MapStart;
        event Action<long> MapTime;
        event Action<long> Goal;
        void Update();
    }
}
