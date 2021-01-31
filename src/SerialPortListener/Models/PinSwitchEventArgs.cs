using System;

using JettonPass.SerialPortListener.Models.Enums;


namespace JettonPass.SerialPortListener.Models
{
    public class PinSwitchEventArgs : EventArgs
    {
        #region Ctors
        public PinSwitchEventArgs(PinType pinType, bool isOn)
        {
            PinType = pinType;
            IsOn = isOn;
        }
        #endregion _Ctors
        

        #region Properties
        public PinType PinType { get; }
        public bool IsOn { get; }
        #endregion _Properties


        #region Overrides of Object
        public override string ToString() =>
            $"{Enum.GetName(PinType)} is {IsOn.ToString()}";
        #endregion
    }
}