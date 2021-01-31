using System;
using System.IO.Ports;

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

            if (string.IsNullOrWhiteSpace(portName))
                throw new ArgumentException("ERROR: Invalid port name", nameof(portName));

            var serialPortSwitch = new SerialPortSwitch(new SerialPortOptions { PortName = portName });
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
