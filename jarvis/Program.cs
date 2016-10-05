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
      //This will pull the current CPU load in percentage
      PerformanceCounter perfCPUCount = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
      PerformanceCounter perfMemCount = new PerformanceCounter("Memory", "Available MBytes");


      SpeechSynthesizer synth = new SpeechSynthesizer();
      synth.Speak("Hello how are you today!");

      // infinite loop
      while (true)
      {
        Thread.Sleep(1000);
        Console.WriteLine("CPU Load: {0}%", perfCPUCount.NextValue());
        Console.WriteLine("Available Mem: {0}MB", perfMemCount.NextValue());

      }
    }
  }
}
