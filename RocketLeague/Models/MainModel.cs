using System;
using System.Diagnostics;
using System.Drawing;
using RocketLeagueOrion.Utilities.Memory;
using RocketLeagueOrion.Views;

namespace RocketLeagueOrion.Models
{
    public class MainModel
    {
        #region Backing fields
        // Game data
        private int _boostAmount;
        // GUI
        private string _status;
		#endregion

		private CorsairSDK keyboard;

        public MainModel(MainView mainView)
        {
            MainView = mainView;
			keyboard = new CorsairSDK();
			if (!keyboard.isKeyboardWorking())
			{
				Console.WriteLine("FATAL ERROR: Corsair keyboard is not available");
				System.Windows.Forms.Application.Exit();
			}
        }

        private MainView MainView { get; }
        public Process RocketLeagueProcess { get; set; }

        public IntPtr BoostAddress { get; set; }
        public int PreviousBoost { get; set; }

        // Colors
        public Color MainColor { get; set; }
        public Color SecondaryColor { get; set; }

        // GUI
        public int BoostAmount
        {
            get { return _boostAmount; }
            set
            {
                _boostAmount = value;
                MainView.labelBoostAmount.InvokeIfRequired(() => { MainView.labelBoostAmount.Text = value.ToString(); });
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                MainView.labelStatus.InvokeIfRequired(() => { MainView.labelStatus.Text = value; });
            }
        }

        public void GetBoostAmount()
        {
            var memory = new Memory(RocketLeagueProcess);
            BoostAddress = memory.GetAddress("\"RocketLeague.exe\"+019425CC+54+1FC+398+77C+740");

            var boostFloat = memory.ReadFloat(BoostAddress) * 100;
			keyboard.setlevel((int)boostFloat);
            PreviousBoost = BoostAmount;
            BoostAmount = (int)Math.Ceiling(boostFloat);
        }
    }
}