using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

namespace _01_S_SingleResponsability
{
    public class Alarm
    {
        private List<DateTime> _alarmList = new List<DateTime>();

        public Alarm()
        { }

        public void AddAlarm(DateTime alarm)
        {
            _alarmList.Add(alarm);
        }

        public override string ToString()
        {
            string alarmText = "";
            foreach (var alarm in _alarmList)
            {
                alarmText += alarm + "\r\n";
            }
            return alarmText;
        }
    }

    public class Persistence
    {
        public void SaveToDisk(string fileName, Alarm alarm)
        {
            File.WriteAllText(fileName, alarm.ToString());
        }

        public void ReadFromDisk(string fileName)
        {
            Write(File.ReadAllText(fileName));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var alarms = new Alarm();            
            alarms.AddAlarm(DateTime.Now.AddHours(1));
            alarms.AddAlarm(DateTime.Today.AddDays(1));

            var persistence = new Persistence();
            var fileName = @"alarms.txt";
            persistence.SaveToDisk(fileName, alarms);
            persistence.ReadFromDisk(fileName);
        }
    }
}
