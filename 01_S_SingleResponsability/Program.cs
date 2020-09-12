using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using static System.Console;

namespace _01_S_SingleResponsability
{
    class Program
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

            public string GetAlarmsToString()
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
                File.WriteAllText(fileName, alarm.GetAlarmsToString());
            }

            public void ReadFromDisk(string fileName)
            {
                Write(File.ReadAllText(fileName));
            }
        }

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
