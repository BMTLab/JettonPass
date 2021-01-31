using System;

using JettonPass.App.Models.Options;
using JettonPass.App.Services.Receivers.Abstractions;
using JettonPass.SerialPortListener;
using JettonPass.SerialPortListener.Models;

using Microsoft.Extensions.Options;


namespace JettonPass.App.Services.Receivers
{
    public sealed class SerialPortReceiver : IJettonReceiver, IDisposable
    {
        #region Fields
        private readonly SerialPortSwitch _serialPort;
        private readonly SerialPortReceiverOptions _options;

        #endregion _Fields


        #region Ctors
        public SerialPortReceiver(SerialPortSwitch serialPort, IOptions<SerialPortReceiverOptions> options)
        {
            _options = options.Value;
            _serialPort = serialPort;
            _serialPort.Switch += SerialPortOnSwitch;
            _serialPort.Start();
        }
        #endregion


        #region Events
        public event EventHandler JettonPassed = delegate { };
        #endregion _Events


        #region Methods
        private void SerialPortOnSwitch(object? sender, PinSwitchEventArgs e)
        {
            if (e.IsOn == _options.OnFrontSignal && e.PinType == _options.TrackingPin)
                JettonPassed.Invoke(sender, EventArgs.Empty);
        }
        #endregion _Methods


        #region IDisposable
        private bool _isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                _serialPort.Switch -= SerialPortOnSwitch;
                _serialPort.Stop();
            }

            _isDisposed = true;
        }


        ~SerialPortReceiver()
        {
            Dispose(false);
        }
        #endregion
    }
}