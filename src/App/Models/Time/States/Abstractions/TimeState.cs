using JettonPass.App.Services.Managers;


namespace JettonPass.App.Models.Time.States.Abstractions
{
    public abstract class TimeState
    {
        #region Fields
        protected TimeManager Context;
        #endregion _Fields


        #region Properties
        public bool NoTime { get; protected set; }
        public TimeStateType Type { get; protected set; }
        #endregion _Properties


        #region Ctors
        protected TimeState(TimeManager context)
        {
            Context = context;
            
        }
        #endregion


        #region Methods
        public void SetContext(TimeManager context)
        {
            Context = context;
        }
        public abstract void UpdateTime();
        #endregion _Methods
    }
}