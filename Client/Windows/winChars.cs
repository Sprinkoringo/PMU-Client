 namespace Client.Logic.Windows
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	using Gfx = Client.Logic.Graphics;

	class winChars : Window
	{
		#region Fields

		internal Gui.Button btnChar1;
		internal Gui.Button btnChar2;
		internal Gui.Button btnChar3;

		Gui.Label lblDeleteCharacter;
		Gui.Label lblLoginScreen;
		Gui.Label lblNewCharacter;
		Gui.Label lblUseCharacter;
		Gui.PictureBox picBackground;

		#endregion Fields

		#region Constructors

		public winChars()
		{
			picBackground = new Client.Logic.Gui.PictureBox();
			picBackground.Image = Gfx.GuiManager.CharSelect;
			picBackground.ImageSizeMode = Client.Logic.Gui.PictureBox.SizeMode.AutoSize;
			picBackground.Location = new Point(0, 0);

			this.Location = GetCenter(picBackground.Size);
			this.Size = picBackground.Size;
			this.Text = "Pokemon Mystery Universe - Character Select";
			this.ShowInTaskBar = true;
			this.TaskBarText = "Character Select";

			btnChar1 = new Client.Logic.Gui.Button(Gfx.FontManager.LoadFont("PMU", 24));
			btnChar1.Location = new Point(16, 104);
			btnChar1.Size = new Size(359, 41);
			btnChar1.FocusOnClick = true;
			btnChar1.Backcolor = Color.SteelBlue;
			btnChar1.HoverColor = Color.SkyBlue;
			btnChar1.Text = "Character 1";

			btnChar2 = new Client.Logic.Gui.Button(Gfx.FontManager.LoadFont("PMU", 24));
			btnChar2.Location = new Point(16, 145);
			btnChar2.Size = new Size(359, 41);
			btnChar2.FocusOnClick = true;
			btnChar2.Backcolor = Color.SteelBlue;
			btnChar2.HoverColor = Color.SkyBlue;
			btnChar2.Text = "Character 2";

			btnChar3 = new Client.Logic.Gui.Button(Gfx.FontManager.LoadFont("PMU", 24));
			btnChar3.Location = new Point(16, 186);
			btnChar3.Size = new Size(359, 41);
			btnChar3.FocusOnClick = true;
			btnChar3.Backcolor = Color.SteelBlue;
			btnChar3.HoverColor = Color.SkyBlue;
			btnChar3.Text = "Character 3";

			lblUseCharacter = new Client.Logic.Gui.Label(Gfx.FontManager.LoadFont("PMU", 18));
			lblUseCharacter.Location = new Point(26, 232);
			lblUseCharacter.Size = new Size(75, 14);
			lblUseCharacter.ForeColor = Color.Black;
			lblUseCharacter.TextYOffset = -4;
			lblUseCharacter.Text = "Use Character";
			lblUseCharacter.OnClick += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(lblUseCharacter_OnClick);

			lblDeleteCharacter = new Client.Logic.Gui.Label(Gfx.FontManager.LoadFont("PMU", 18));
			lblDeleteCharacter.Location = new Point(158, 232);
			lblDeleteCharacter.Size = new Size(87, 14);
			lblDeleteCharacter.ForeColor = Color.Black;
			lblDeleteCharacter.TextYOffset = -4;
			lblDeleteCharacter.Text = "Delete Character";

			lblNewCharacter = new Client.Logic.Gui.Label(Gfx.FontManager.LoadFont("PMU", 18));
			lblNewCharacter.Location = new Point(286, 232);
			lblNewCharacter.Size = new Size(78, 14);
			lblNewCharacter.ForeColor = Color.Black;
			lblNewCharacter.TextYOffset = -4;
			lblNewCharacter.Text = "New Character";

			lblLoginScreen = new Client.Logic.Gui.Label(Gfx.FontManager.LoadFont("PMU", 18));
			lblLoginScreen.Location = new Point(145, 349);
			lblLoginScreen.Size = new Size(110, 14);
			lblLoginScreen.ForeColor = Color.Black;
			lblLoginScreen.TextYOffset = -4;
			lblLoginScreen.TextXOffset = 3;
			lblLoginScreen.Text = "Back to Login Screen";
			lblLoginScreen.OnClick += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(lblMainMenu_OnClick);

			this.AddControl(picBackground);
			this.AddControl(btnChar1);
			this.AddControl(btnChar2);
			this.AddControl(btnChar3);

			this.AddControl(lblUseCharacter);
			this.AddControl(lblDeleteCharacter);
			this.AddControl(lblNewCharacter);

			this.AddControl(lblLoginScreen);
            this.AddControl(lblUseCharacter);
			
			btnChar1.Focused = true;
		}

		#endregion Constructors

		#region Methods

		void lblLoginScreen_OnClick(object sender, SdlDotNet.Input.MouseButtonEventArgs e)
		{
			WindowManager.RemoveWindow(this);
			WindowManager.AddWindow(WindowSwitcher.Window.Login);
		}

		void lblUseCharacter_OnClick(object sender, SdlDotNet.Input.MouseButtonEventArgs e)
		{
			bool charSelected = false;
			if (btnChar1.Focused) {
				WindowSwitcher.GetWindow(WindowSwitcher.Window.Game, true);
				Tcp.TcpResponder.SendUseChar(1);
				charSelected = true;
			} else if (btnChar2.Focused) {
				WindowSwitcher.GetWindow(WindowSwitcher.Window.Game, true);
				Tcp.TcpResponder.SendUseChar(2);
				charSelected = true;
			} else if (btnChar3.Focused) {
				WindowSwitcher.GetWindow(WindowSwitcher.Window.Game, true);
				Tcp.TcpResponder.SendUseChar(3);
				charSelected = true;
			}
			if (charSelected) {
				WindowManager.RemoveWindow(this);
				WindowManager.AddWindow(WindowSwitcher.Window.Loading);
				Windows.WindowSwitcher.LoadingWindow.UpdateLoadText("Logging in...");
			}
		}

		#endregion Methods
	}
}