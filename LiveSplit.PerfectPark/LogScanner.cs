using System;
using System.IO;
using System.Text.RegularExpressions;

namespace LiveSplit.PerfectPark
{
    class LogScanner : IScanner
    {
        const string LogDir = @"\AppData\LocalLow\tjern\perfect-park";
        const string LogFile = @"\SPDRUN-LOG.txt";

        StreamReader _reader;
        long _lastLength;

#pragma warning disable 0067
        public event Action RaceStart;
        public event Action<long> MapStart;
        public event Action<long> MapTime; // Unused
        public event Action<long> Goal;
#pragma warning restore 0067

        public LogScanner()
        {
            string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            Directory.CreateDirectory(home + LogDir);
            FileStream fs = new FileStream(home + LogDir + LogFile, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
            fs.Seek(0, SeekOrigin.End);
            _lastLength = fs.Length;
            _reader = new StreamReader(fs);
        }

        public void Update()
        {
            long length = _reader.BaseStream.Length;
            if (length < _lastLength)
            {
                _reader.BaseStream.Seek(0, SeekOrigin.Begin);
                _reader.DiscardBufferedData();
            }
            _lastLength = length;

            string line;
            while ((line = _reader.ReadLine()) != null)
            {
                if (Regex.IsMatch(line, @"^v\d+ - RACE$")) RaceStart();
                if (Regex.IsMatch(line, @"^\[START\] - .*$")) MapStart(2000);
                Match time = Regex.Match(line, @"^\[FINISH\] - [^-]* - (\d+)\.(\d{3})$");
                if (time.Success) Goal(long.Parse(time.Groups[1].Value + time.Groups[2].Value));
            }
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}
