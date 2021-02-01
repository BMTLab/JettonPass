using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using JettonPass.App.Models.Time;
using JettonPass.App.Models.WinApiEnums;
using JettonPass.App.Services.Managers;

using static JettonPass.App.Utils.WindowUtils.NativeMethods;
using static JettonPass.App.Utils.WindowUtils.ScreenInfo;


namespace JettonPass.App.Forms
{
    [SuppressMessage("ReSharper", "LocalizableElement")]
    public sealed partial class TimeForm : Form
    {
        #region Consts&Fields
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private const int WM_SYSCOMMAND = 0x0112; 
        
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private const int W_PARAM = 0xf120;  
        
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private const int L_PARAM = 0x5073d;
        
        private readonly TimeManager _timeManager;
        
        private int _oldWindowLong;
        #endregion _Consts&Fields


        #region Ctors
        public TimeForm(TimeManager timeManager)
        {
            InitializeComponent();
            
            _timeManager = timeManager;
            _timeManager.TimeChanged += ManagerOnTimeChanged;
            _timeManager.TimeRunsOut += ManagerOnTimeRunsOut;
            _timeManager.TimeEnd += ManagerOnTimeEnd;
            
            MaximizeEverything(Handle);
            SetFormTransparent(Handle);
            SetTheLayeredWindowAttribute(Handle);
            
            UpdateLabel(_timeManager.LeftTime);
            
            TopMost = true;
        }
        #endregion _Ctors


        #region Methods
        /// <summary>
        ///     Make the form (specified by its handle) a window that supports transparency.
        /// </summary>
        /// <param name="handle">The window to make transparency supporting</param>
        public void SetFormTransparent(IntPtr handle)
        {
            _oldWindowLong = GetWindowLong(handle, (int) GetWindowLongConst.GWL_EXSTYLE);
            var hResult = SetWindowLong
            (
                handle,
                (int) GetWindowLongConst.GWL_EXSTYLE,
                Convert.ToInt32(_oldWindowLong | (uint) WindowStyles.WS_EX_LAYERED | (uint) WindowStyles.WS_EX_TRANSPARENT)
            );
        }


        /// <summary>
        ///     Make the form (specified by its handle) a normal type of window (doesn't support transparency).
        /// </summary>
        /// <param name="handle">The Window to make normal</param>
        public void SetFormNormal(IntPtr handle)
        {
            var hResult = SetWindowLong(handle, (int) GetWindowLongConst.GWL_EXSTYLE, Convert.ToInt32(_oldWindowLong | (uint) WindowStyles.WS_EX_LAYERED));
        }


        /// <summary>
        ///     Makes the form change White to Transparent and clickthrough-able
        ///     Can be modified to make the form translucent (with different opacities) and change the Transparency Color.
        /// </summary>
        public void SetTheLayeredWindowAttribute(IntPtr handle)
        {
            const uint transparentColor = 0xff_ff_ff_ff;

            SetLayeredWindowAttributes(handle, transparentColor, 125, 0x2);

            TransparencyKey = Color.White;
        }


        private void MaximizeEverything(IntPtr handle)
        {
            Location = GetTopLeft();
            Size = GetFullScreensSize();

            var hResult = SendMessage(handle, WM_SYSCOMMAND, (UIntPtr) W_PARAM, (IntPtr) L_PARAM);
        }

        
        private void ManagerOnTimeChanged(object? sender, TimeChangedEventArgs e)
        {
            UpdateLabel(e.NewValue);
        }
        
        
        private async void ManagerOnTimeEnd(object? sender, EventArgs _)
        {
            SetEndTimeLabel();
            await Task.Run(() => MessageBox.Show("Время вышло"));
        }
        
        
        private async void ManagerOnTimeRunsOut(object? sender, TimeChangedEventArgs e)
        {
            UpdateLabel(e.NewValue);
            await Task.Run(() => MessageBox.Show($"Время заканчивается! \r\n Осталось {e.NewValue.ToString()}"));
        }


        private void UpdateLabel(TimeSpan newTime)
        {
            timeLeftLabel.Text = $"Оставшиеся время: {newTime.ToString()}";
        }
        
        
        private void SetEndTimeLabel()
        {
            timeLeftLabel.Text = "Время вышло";
            timeLeftLabel.ForeColor = Color.Crimson;
        }
        #endregion _Methods
    }
}