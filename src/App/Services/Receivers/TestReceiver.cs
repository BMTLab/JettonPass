using System;
using System.Threading.Tasks;

using JettonPass.App.Services.Receivers.Abstractions;


namespace JettonPass.App.Services.Receivers
{
    public sealed class TestReceiver : IJettonReceiver
    {
        #region Fields
        private readonly int _delayBefore;
        #endregion _Fields


        #region Ctors
        public TestReceiver()
        {
            var rnd = new Random();
            _delayBefore = rnd.Next(10000, 15000);

            OnReceive();
        }
        #endregion


        #region Events
        public event EventHandler JettonPassed = delegate { };
        #endregion _Events


        #region Methods
        private async void OnReceive()
        {
            await Task.Delay(_delayBefore);
            
            JettonPassed.Invoke(this, EventArgs.Empty);
        }
        #endregion _Methods
    }
}