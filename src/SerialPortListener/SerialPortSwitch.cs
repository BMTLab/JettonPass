/********************************************
* -------------
* \ 1 2 3 4 5 /
*  \ 6 7 8 9 /
*   --------- 
* Principle:
  * 4[DTR] as a +6V power supply, you can also use [RTS] instead of [DTR]
  * Non-stop detection in the software
* 1[CD ]
* 6[DSR]
* 8[CTS]
  * Voltage change of three ports
*********************************************/

using System;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;

using JettonPass.SerialPortListener.Models;
using JettonPass.SerialPortListener.Models.Enums;
using JettonPass.SerialPortListener.Models.Options;


namespace JettonPass.SerialPortListener
{
    /// <summary>
    ///     Use a serial port to collect 3-way switch input signals (also called steel node or relay input)
    /// </summary>
    public class SerialPortSwitch
    {
        /*#region Consts
        /// <summary>
        ///     Polling period
        /// </summary>
        private const int Priority = 20;

        /// <summary>
        ///     Instantaneous signal filtering time
        /// </summary>
        private const int Filter = 40;
        #endregion _Consts*/


        #region Fields
        private readonly PinState[] _pins;
        private readonly SerialPortOptions _options;
        #endregion _Fields

        
        #region Ctors
        public SerialPortSwitch(SerialPortOptions options)
        {
            if (!IsAnyPortsOnSystem())
                throw new IOException(@"No ports connected in the system");

            var portName = options.PortName;

            if (string.IsNullOrWhiteSpace(options.PortName) || options.PortName.Equals("Auto", StringComparison.CurrentCultureIgnoreCase))
                portName = SerialPort.GetPortNames()[0];

            Port.PortName = portName;
            Port.BaudRate = options.BaudRate;
            Port.Parity = Parity.None;
            Port.DataBits = 8;
            Port.StopBits = StopBits.One;
            Port.ReadBufferSize = options.ReadBufferSize;
            Port.WriteBufferSize = options.WriteBufferSize;
            Port.DtrEnable = options.DtrEnable;
            Port.RtsEnable = options.RtsEnable;
            Port.Handshake = Handshake.None;
            
            _pins = options.TrackedPins
               .Where(t => t.Value)
               .Select(t => new PinState { PinType = t.Key })
               .ToArray();
            
            _options = options;
        }
        #endregion _Ctors


        #region Properties
        public bool IsRunning { get; private set; }
        public bool StopPending { get; private set; }
        public SerialPort Port { get; } = new();
        #endregion _Properties

        
        #region Events
        public event EventHandler<PinSwitchEventArgs> Switch = delegate { };
        #endregion _Events


        #region Methods
        public static bool IsAnyPortsOnSystem() =>
            SerialPort.GetPortNames().Any();
        
        public void Start()
        {
            if (IsRunning)
                return;

            IsRunning = true;
            StopPending = false;

            try
            {
                Thread thread = new (OnRunning) 
                { 
                    Name = nameof(SerialPortSwitch), 
                    CurrentCulture = CultureInfo.InvariantCulture, 
                    IsBackground = false
                };
                thread.Start();
            }
            catch
            {
                IsRunning = false;
                StopPending = false;

                throw;
            }
        }


        public void Stop(bool waitUntilStoped = true)
        {
            if (IsRunning)
                StopPending = true;

            if (!waitUntilStoped)
                return;

            var timeout = Environment.TickCount + 10 * 1000;

            while (Environment.TickCount < timeout)
            {
                Thread.Sleep(100);

                if (!IsRunning)
                    return;
            }

            throw new TimeoutException($"Stop {nameof(SerialPortSwitch)} failed");
        }


        private void OnRunning()
        {
            try
            {
                Port.Open();

                while (!StopPending)
                {
                    foreach (var pin in _pins)
                        CheckState(pin);

                    Thread.Sleep(_options.Priority);
                }
            }
            catch (Exception exc)
            {
                // TODO:log error.
                Console.WriteLine($"ERROR: {nameof(SerialPortSwitch)}: {exc}");
            }
            finally
            {
                IsRunning = false;
                StopPending = false;

                try
                {
                    Port.Close();
                }
                catch
                {
                    // ignored
                }
            }
        }


        private void CheckState(PinState pinState)
        {
            var pinHolding = GetPinHolding(pinState.PinType);
            
            if (pinState.IsHolding == pinHolding)
                pinState.HoldingStableTime = Environment.TickCount;

            if (Environment.TickCount - pinState.HoldingStableTime <= _options.Filter)
                return;

            pinState.IsHolding = pinHolding;

            Switch.Invoke
            (
                this,
                pinState.IsHolding
                    ? new PinSwitchEventArgs(pinState.PinType, true)
                    : new PinSwitchEventArgs(pinState.PinType, false)
            );
        }


        private bool GetPinHolding(PinType pinType) =>
            pinType switch
            {
                PinType.Cd   => Port.CDHolding,
                PinType.Dsr  => Port.DsrHolding,
                PinType.Dtr  => Port.DtrEnable,
                PinType.Cts  => Port.CtsHolding,
                PinType.None => throw new ArgumentOutOfRangeException(nameof(pinType)),
                var _    => throw new ArgumentOutOfRangeException(nameof(pinType))
            };
        #endregion _Methods
    }
}