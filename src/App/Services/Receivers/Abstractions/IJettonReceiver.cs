using System;


namespace JettonPass.App.Services.Receivers.Abstractions
{
    public interface IJettonReceiver
    {
        event EventHandler JettonPassed;
    }
}