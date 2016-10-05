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
      synth.SelectVoiceByHints(VoiceGender.Neutral);
      synth.Speak("Welcome to jarvis version one point oh");


      #region My Performance Counters
      //This will pull the current CPU load in percentage
      PerformanceCounter perfCPUCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
      //This will pull the current Memory available in MB
      PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");
      // This wuill get system uptime in s
      PerformanceCounter perfUptimeCount = new PerformanceCounter("System", "System Up Time");

      // need to run Next value once to initialize the counter, if not first value is 0
      perfCPUCount.NextValue();
      perfMemCount.NextValue();
      perfUptimeCount.NextValue();

      #endregion

      TimeSpan uptimeSpan = TimeSpan.FromSeconds(perfUptimeCount.NextValue());
      string systemUptimeMessage = string.Format("The current system up time is {0} days, {1} hours {2} minutes and {3} seconds",
        (int)uptimeSpan.TotalDays,
        uptimeSpan.Hours,
        uptimeSpan.Minutes,
        uptimeSpan.Seconds
        );
      Console.WriteLine(systemUptimeMessage);

      synth.Speak(systemUptimeMessage);
      // infinite loop
      while (true)
      {

        int currentCPUPercentage = (int)perfCPUCount.NextValue();
        int currentAvailableMemory = (int)perfMemCount.NextValue();

        Console.WriteLine("CPU Load     : {0}%", currentCPUPercentage);
        Console.WriteLine("Available Mem: {0}MB", currentAvailableMemory);


        if (currentCPUPercentage > 80)
        {
          if (currentCPUPercentage == 100)
          {
            synth.SelectVoiceByHints(VoiceGender.Female);
            string cpuLoadVocalMessage = String.Format("Warning: CPU at max", currentCPUPercentage);
            synth.Speak(cpuLoadVocalMessage);
          }
          else
          {
            synth.SelectVoiceByHints(VoiceGender.Male);
            string cpuLoadVocalMessage = String.Format("The current CPU load is {0}", currentCPUPercentage);
            synth.Speak(cpuLoadVocalMessage);
          }
        }

        if (currentAvailableMemory < 1024)
        {
          synth.SelectVoiceByHints(VoiceGender.Male);
          string memVocalMessage = String.Format("You currently have {0} megabytes of memory available", currentAvailableMemory);
          synth.Speak(memVocalMessage);
        }
        Thread.Sleep(1000);

      }
    }
  }
}
