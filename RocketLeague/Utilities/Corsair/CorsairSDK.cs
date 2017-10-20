using System;
using System.Collections.Generic;

using CUE.NET;
using CUE.NET.Groups;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Exceptions;
using CUE.NET.Devices.Generic;
using CUE.NET.Gradients;
using System.Drawing;
using CUE.NET.Brushes;

namespace RocketLeagueOrion
{
	//project->add_nu_get_packages->cue.net
	//project->edit references->system.drawing
	//https://www.unknowncheats.me/forum/programming-for-beginners/110375-cheat-engine-finding-base-address-pointer-scan.html
	public class CorsairSDK
	{
		private CorsairKeyboard keyboard;
		private RectangleLedGroup rectangle;
		private Boolean keyboardWorking;
		private IBrush emptyColor;
		private IBrush green;
		private int lastlvl;

		private void clearKeyboard()
		{
			rectangle = new RectangleLedGroup(keyboard, CorsairLedId.Escape, CorsairLedId.KeypadEnter, 0.5f, true);
			rectangle.Brush = emptyColor;
			keyboard.Update();
		}

		public void setlevel(int lvl)
		{
			if (lvl == lastlvl) return;
			if (lvl > 100 || lvl < 0)
			{
				Console.WriteLine("ERROR: Invalid level(" + lvl + ")");
				return;
			}
			lastlvl = lvl;
			int originalvl = lvl;
			clearKeyboard();
			float factor = 5.2631f;
			CorsairLedId from = CorsairLedId.LeftCtrl;
			CorsairLedId to = CorsairLedId.Escape;
			Console.WriteLine("1º lvl= " + lvl);
			lvl = ((int)(lvl / factor));
			Console.WriteLine("2º lvl= " + lvl);
			switch (lvl)
			{
				case 0:
					to = CorsairLedId.Escape;
					break;
				case 1:
					to = CorsairLedId.F1;
					break;
				case 2:
					to = CorsairLedId.F2;
					break;
				case 3:
					to = CorsairLedId.F3;
					break;
				case 4:
					to = CorsairLedId.F4;
					break;
				case 5:
					to = CorsairLedId.F5;
					break;
				case 6:
					to = CorsairLedId.F6;
					break;
				case 7:
					to = CorsairLedId.F7;
					break;
				case 8:
					to = CorsairLedId.F8;
					break;
				case 9:
					to = CorsairLedId.F9;
					break;
				case 10:
					to = CorsairLedId.F10;
					break;
				case 11:
					to = CorsairLedId.F11;
					break;
				case 12:
					to = CorsairLedId.F12;
					break;
				case 13:
					to = CorsairLedId.PrintScreen;
					break;
				case 14:
					to = CorsairLedId.ScrollLock;
					break;
				case 15:
					to = CorsairLedId.PauseBreak;
					break;
				case 16:
					to = CorsairLedId.Stop;
					break;
				case 17:
					to = CorsairLedId.ScanPreviousTrack;
					break;
				case 18:
					to = CorsairLedId.PlayPause;
					break;
				case 19:
					to = CorsairLedId.ScanNextTrack;
					break;

			}
			if (originalvl > 0)
			{
				rectangle = new RectangleLedGroup(keyboard, from, to, 0.5f, true);
				rectangle.Brush = green;
				keyboard.Update();

			}
		}

		public Boolean isKeyboardWorking() {
			return keyboardWorking;
		}

		public CorsairSDK()
		{
			lastlvl = -1;
			keyboardWorking = true;
			try {
			    CueSDK.Initialize();
			    Console.WriteLine("Initialized with " + CueSDK.LoadedArchitecture + "-SDK");
			    keyboard = CueSDK.KeyboardSDK;
				if (keyboard == null)
				{
					Console.WriteLine("ERROR: No keyboard found");
					keyboardWorking = false;
					return;
				}
			}
			catch (CUEException ex) {
			    Console.WriteLine("ERROR: CUE Exception! ErrorCode: " + Enum.GetName(typeof(CorsairError), ex.Error));
				keyboardWorking = false;
				return;
			}
			catch (WrapperException ex) {
			    Console.WriteLine("ERROR: Wrapper Exception! Message:" + ex.Message);
				keyboardWorking = false;
				return;
			}
			if (!CueSDK.IsSDKAvailable()) {
				Console.WriteLine("ERROR: Cant access any led on the keyboard");
				keyboardWorking = false;
				return;
			}
			emptyColor = new SolidColorBrush(Color.Black);
			green = new SolidColorBrush(Color.Green);
		}
	}
}
