using System;
using System.Collections.Generic;
using System.IO.Ports;

using JettonPass.SerialPortListener.Models.Enums;
using JettonPass.SerialPortListener.Models.Options;


namespace JettonPass.SerialPortListener
{
    public static class Program
    {
        static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            {
                Console.WriteLine($"UNHANDLED ERROR: {(Exception) e.ExceptionObject}");
            };
        }
        
        
        public static void Main()
        {
            Console.Write($"ENTER PORT ({string.Join(", ", SerialPort.GetPortNames())}): ");
            var portName = Console.ReadLine();

            var serialPortSwitch = new SerialPortSwitch
            (
                new SerialPortOptions
                {
                    PortName = portName ?? "Auto",
                    BaudRate = 9600,
                    Filter = 40,
                    Priority = 20,
                    DtrEnable = true,
                    RtsEnable = true,
                    TrackedPins = new Dictionary<PinType, bool> { { PinType.Cd, true } },
                    ReadBufferSize = 8,
                    WriteBufferSize = 8
                }
            );
            Console.WriteLine(@"CREATE PORT HANDLER");
            
            serialPortSwitch.Switch += (_, e) =>
            {
                Console.WriteLine($"PIN {Enum.GetName(e.PinType)}: {nameof(serialPortSwitch.Switch)} {e.IsOn.ToString()}");
            };
            
            serialPortSwitch.Start();
            Console.WriteLine(@"START LISTENING");
            
            Console.ReadKey();
        }
    }
}
