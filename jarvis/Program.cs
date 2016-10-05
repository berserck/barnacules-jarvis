using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Threading;

using System.Speech.Synthesis;

namespace jarvis
{
  class Program
  {
    static void Main(string[] args)
    {
      // This will greet the user in the default voice
      SpeechSynthesizer synth = new SpeechSynthesizer();
      //synth.Speak("Welcome to jarvis version one point o");


      #region My Performance Counters
      //This will pull the current CPU load in percentage
      PerformanceCounter perfCPUCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
      //This will pull the current Memory available in MB
      PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");
      // This wuill get system uptime in s
      PerformanceCounter perfUptimeCount = new PerformanceCounter("System", "System Up Time");
      #endregion

      // infinite loop
      while (true)
      {

        int currentCPUPercentage =  (int)perfCPUCount.NextValue();
        int currentAvailableMemory = (int)perfMemCount.NextValue();

        Console.WriteLine("CPU Load     : {0}%", currentCPUPercentage);
        Console.WriteLine("Available Mem: {0}MB", currentAvailableMemory);


        if (currentCPUPercentage > 80)
        {
          string cpuLoadVocalMessage = String.Format("The current CPU load is {0}", currentCPUPercentage);
          synth.Speak(cpuLoadVocalMessage);

        }

        if (currentAvailableMemory < 1024)
        {
          string memVocalMessage = String.Format("You currently have {0} megabytes of memory available", currentAvailableMemory);
          synth.Speak(memVocalMessage);
        }
        Thread.Sleep(1000);

      }
    }
  }
}
