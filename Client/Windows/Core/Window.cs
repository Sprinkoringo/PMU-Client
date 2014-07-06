namespace Client.Logic.Windows.Core
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	class Window : WindowCore, IWindow
	{
		#region Fields

		private byte mAlpha = 255;
		private bool mAlwaysOnTop = false;
		private byte mBorderAlpha = 255;

		//private SdlDotNet.Graphics.Primitives.Box mBorderBox;
		private SdlDotNet.Graphics.Surface mBorderBuffer;
		private SdlDotNet.Graphics.Surface mBuffer;
		private Rectangle mCloseButtonBounds;
		private SdlDotNet.Graphics.Surface mCloseButtonSurf;
		private bool mCloseable = true;
		private Rectangle mControlBounds;
		private byte mControlsAlpha = 255;
		private bool mDrag;
		private Point mDragStart;
		private Rectangle mFullBounds;
		private bool mIsDisposing = false;
		private bool mMaximizable = true;
		private Rectangle mMaximizeButtonBounds;
		private SdlDotNet.Graphics.Surface mMaximizeButtonSurf;
		private Rectangle mMaximizedBounds = new Rectangle(0, 0, SdlDotNet.Graphics.Video.Screen.Width, SdlDotNet.Graphics.Video.Screen.Height - Globals.GameScreen.TaskBar.Height);
		private Rectangle mMaximizedControlBounds;
		private bool mMinimizable = true;
		private Rectangle mMinimizeButtonBounds;
		private SdlDotNet.Graphics.Surface mMinimizeButtonSurf;
		private Rectangle mPrevControlBounds;
		private Rectangle mPrevFullBounds;
		private bool mShowInTaskBar = true;
		private string mTaskBarText = "";
		private string mText = "";
		private Rectangle mTitleBarBounds;
		private SdlDotNet.Graphics.Surface mTitleBarSurface;
		private WindowManager.WindowState mWindowState = WindowManager.WindowState.Normal;
		private bool mWindowed = true;
		
		#endregion Fields

		#region Constructors

		public Window()
			: base(true)
		{
			mControlBounds = new Rectangle(0, 0, 300, 300);
			mMinimizeButtonSurf = new SdlDotNet.Graphics.Surface(IO.IO.CreateOSPath("Skins\\" + Globals.ActiveSkin + "\\General\\Windows\\minimizebutton.png"));
			mMaximizeButtonSurf = new SdlDotNet.Graphics.Surface(IO.IO.CreateOSPath("Skins\\" + Globals.ActiveSkin + "\\General\\Windows\\maximizebutton.png"));
			mCloseButtonSurf = new SdlDotNet.Graphics.Surface(IO.IO.CreateOSPath("Skins\\" + Globals.ActiveSkin + "\\General\\Windows\\closebutton.png"));
			InitBuffer();
			mMaximizedControlBounds = new Rectangle(new Point(mMaximizedBounds.Location.X + 1, mMaximizedBounds.Location.Y + 1 + mTitleBarBounds.Height), SubtractBorderSize(mMaximizedBounds.Size));
		}

		#endregion Constructors

		#region Events

		public event EventHandler OnWindowClosed;
		public event EventHandler<SdlDotNet.Input.KeyboardEventArgs> OnKeyDown;

		#endregion Events

		#region Properties

		public byte Alpha
		{
			get { return mAlpha; }
			set {
				mBorderAlpha = value;
				mControlsAlpha = value;
				mBorderBuffer.Alpha = value;
				mBuffer.Alpha = value;
			}
		}

		public bool AlwaysOnTop
		{
			get { return mAlwaysOnTop; }
			set { mAlwaysOnTop = value; }
		}

		public byte BorderAlpha
		{
			get { return mBorderAlpha; }
			set {
				mBorderAlpha = value;
				mBorderBuffer.Alpha = value;
			}
		}

		public bool Closeable
		{
			get { return mCloseable; }
			set {
				mCloseable = value;
				if (mWindowed) {
					AddBorder();
				}
			}
		}

		public Rectangle ControlBounds
		{
			get { return mControlBounds; }
			set {
				mControlBounds = value;
			}
		}

		public byte ControlsAlpha
		{
			get { return mControlsAlpha; }
			set {
				mControlsAlpha = value;
				mBuffer.Alpha = value;
			}
		}

		public Rectangle FullBounds
		{
			get { return mFullBounds; }
			set { mFullBounds = value; }
		}

		public Point Location
		{
			get { return mFullBounds.Location; }
			set {
				mFullBounds.Location = value;
				if (mWindowed) {
					mControlBounds.Location = new Point(mFullBounds.Location.X + 1, mFullBounds.Location.Y + 1 + mTitleBarBounds.Height);
				} else {
					mControlBounds.Location = value;
				}
				//InitBuffer();
			}
		}

		public bool Maximizable
		{
			get { return mMaximizable; }
			set {
				mMaximizable = value;
				if (mWindowed) {
					AddBorder();
				}
			}
		}

		public bool Minimizable
		{
			get { return mMinimizable; }
			set {
				mMinimizable = value;
				if (mWindowed) {
					AddBorder();
				}
			}
		}

		public bool ShowInTaskBar
		{
			get { return mShowInTaskBar; }
			set { mShowInTaskBar = value; }
		}

		public Size Size
		{
			get { return mControlBounds.Size; }
			set {
				mControlBounds.Size = value;
				mFullBounds.Size = AddBorderSize(this.Size);
				InitBuffer();
			}
		}

		public string TaskBarText
		{
			get { return mTaskBarText; }
			set { mTaskBarText = value; }
		}

		public string Text
		{
			get { return mText; }
			set {
				if (mText != value) {
					mText = value;
					InitBuffer();
				}
			}
		}

		public WindowManager.WindowState WindowState
		{
			get { return mWindowState; }
			set {
				switch (value) {
						case WindowManager.WindowState.Normal: {
							if (mWindowState == WindowManager.WindowState.Maximized) {
								this.Location = mPrevFullBounds.Location;
								this.Size = mPrevControlBounds.Size;
							}
							SetNormalState();
						}
						break;
						case WindowManager.WindowState.Minimized: {
							SetMinimizedState();
						}
						break;
						case WindowManager.WindowState.Maximized: {
							SetMaximizedState();
						}
						break;
				}
				mWindowState = value;
			}
		}

		public bool Windowed
		{
			get { return mWindowed; }
			set {
				mWindowed = value;
				InitBuffer();
			}
		}

		protected SdlDotNet.Graphics.Surface Buffer
		{
			get { return mBuffer; }
		}

		#endregion Properties

		#region Methods

		public new void AddControl(Gui.Core.Control controlToAdd)
		{
			if (controlToAdd != null) {
				controlToAdd.ParentWindow = this;
				base.AddControl(controlToAdd);
			}
		}

		public void BringToFront()
		{
			if (WindowManager.IsWindowOpen(this)) {
				WindowManager.BringWindowToFront(this);
			}
		}

		public virtual void CloseWindow()
		{
			base.RemoveControls();
			// Do cleanup
			mIsDisposing = true;
			mBuffer.Close();
			mBorderBuffer.Close();
			mCloseButtonSurf.Close();
			mMaximizeButtonSurf.Close();
			mMinimizeButtonSurf.Close();
			mTitleBarSurface.Close();
			WindowManager.RemoveWindowQuick(this);
			if (OnWindowClosed != null)
				OnWindowClosed(this, null);
		}

		public virtual void Keyboard_Down(SdlDotNet.Input.KeyboardEventArgs e)
		{
			if (OnKeyDown != null)
				OnKeyDown(this, e);
		}

		public virtual void Keyboard_Up(SdlDotNet.Input.KeyboardEventArgs e)
		{
		}

		public virtual void Mouse_Down(SdlDotNet.Input.MouseButtonEventArgs e)
		{
			if (mWindowed && mCloseable && this.PointInBounds(e.Position, new Rectangle(mCloseButtonBounds.Location.X + this.Location.X, mCloseButtonBounds.Y + this.Location.Y, mCloseButtonSurf.Size.Width, mCloseButtonSurf.Size.Height))) {
				this.CloseWindow();
			} else if (mWindowed && mMaximizable && this.PointInBounds(e.Position, new Rectangle(mMaximizeButtonBounds.Location.X + this.Location.X, mMaximizeButtonBounds.Y + this.Location.Y, mMaximizeButtonBounds.Size.Width, mMaximizeButtonBounds.Size.Height))) {
				if (mWindowState == WindowManager.WindowState.Normal) {
					SetMaximizedState();
				} else if (mWindowState == WindowManager.WindowState.Maximized) {
					SetNormalState();
					this.Location = mPrevFullBounds.Location;
					this.Size = mPrevControlBounds.Size;
				}
			} else if (mWindowed && mMinimizable && this.PointInBounds(e.Position, new Rectangle(mMinimizeButtonBounds.Location.X + this.Location.X, mMinimizeButtonBounds.Y + this.Location.Y, mMinimizeButtonBounds.Size.Width, mMinimizeButtonBounds.Size.Height))) {
				SetMinimizedState();
			} else if (mWindowed && this.PointInBounds(e.Position, new Rectangle(this.Location, mTitleBarBounds.Size))) {
				if (mWindowState != WindowManager.WindowState.Maximized) {
					this.mDrag = true;
					this.mDragStart = new Point(e.Position.X - this.Location.X, e.Position.Y - this.Location.Y);
				}
			}
		}

		public virtual void Mouse_Motion(SdlDotNet.Input.MouseMotionEventArgs e)
		{
			if (SdlDotNet.Input.Mouse.IsButtonPressed(SdlDotNet.Input.MouseButton.PrimaryButton)) {
				if (this.mDrag) {
					this.Location = new Point(e.Position.X - this.mDragStart.X, e.Position.Y - this.mDragStart.Y);
				}
			}
		}

		public virtual void Mouse_Up(SdlDotNet.Input.MouseButtonEventArgs e)
		{
			this.mDrag = false;
		}

		public bool PointInBounds(Point pointToTest, Rectangle bounds)
		{
			if (pointToTest.X >= bounds.X && pointToTest.Y >= bounds.Y && pointToTest.X - bounds.Location.X <= bounds.Width && pointToTest.Y - bounds.Location.Y <= bounds.Height) {
				return true;
			} else {
				return false;
			}
		}

		public new void RemoveControls()
		{
			base.RemoveControls();
			InitBuffer();
		}

		public virtual void Tick(SdlDotNet.Graphics.Surface dstSurf, SdlDotNet.Core.TickEventArgs e)
		{
			if (!mIsDisposing) {
				if (mWindowState != WindowManager.WindowState.Minimized) {
					this.UpdateControls(dstSurf, e);
				}
			}
		}

		public override void UpdateControls(SdlDotNet.Graphics.Surface dstSurf, SdlDotNet.Core.TickEventArgs e)
		{
			try {
				if (!mIsDisposing) {
//					mBuffer.Fill(SystemColors.Control);
					base.UpdateControls(mBuffer, e);
					if (mWindowed) {
						dstSurf.Blit(mBorderBuffer, mFullBounds.Location, new Rectangle(0, 0, this.FullBounds.Width, this.FullBounds.Height));
					}
					dstSurf.Blit(mBuffer, mControlBounds.Location, new Rectangle(0, 0, this.ControlBounds.Width, this.ControlBounds.Height));
				}
			} catch {
				
			}
		}

		private void AddBorder()
		{
			Size newSize = this.Size;
			switch (mWindowState) {
				case WindowManager.WindowState.Normal:
					newSize = this.Size;
					break;
				case WindowManager.WindowState.Maximized:
					newSize = new Size(SdlDotNet.Graphics.Video.Screen.Width, SdlDotNet.Graphics.Video.Screen.Height - Globals.GameScreen.TaskBar.Height);
					break;
				case WindowManager.WindowState.Minimized:
					return;
			}
			mBorderBuffer = new SdlDotNet.Graphics.Surface(AddBorderSize(newSize));
			mBorderBuffer.AlphaBlending = true;
			mBorderBuffer.Alpha = mBorderAlpha;
			mTitleBarBounds = new Rectangle(0, 0, newSize.Width + 2, 20);
			mTitleBarSurface = new SdlDotNet.Graphics.Surface(mTitleBarBounds.Size);
			mTitleBarSurface.Fill(Color.Blue);
			if (mText != "") {
				mTitleBarSurface.Blit(Logic.Graphics.FontManager.TextBoxFont.Render(mText, Color.Black, false), new Point(20, 3));
			}
			mCloseButtonBounds = new Rectangle(new Point(mTitleBarSurface.Width - 3 - mCloseButtonSurf.Width, 0), mCloseButtonSurf.Size);
			if (mCloseable) {
				mTitleBarSurface.Blit(mCloseButtonSurf, new Point(mTitleBarSurface.Width - 3 - mCloseButtonSurf.Width, 0));
			}
			mMaximizeButtonBounds = new Rectangle(new Point(mTitleBarSurface.Width - 3 - mCloseButtonSurf.Width - mMaximizeButtonSurf.Width, 0), mMaximizeButtonSurf.Size);
			if (mMaximizable) {
				mTitleBarSurface.Blit(mMaximizeButtonSurf, new Point(mTitleBarSurface.Width - 3 - mCloseButtonSurf.Width - mMaximizeButtonSurf.Width, 0));
			}
			mMinimizeButtonBounds = new Rectangle(new Point(mTitleBarSurface.Width - 3 - mCloseButtonSurf.Width - mMinimizeButtonSurf.Width - mMinimizeButtonSurf.Width, 0), mMinimizeButtonSurf.Size);
			if (mMinimizable) {
				mTitleBarSurface.Blit(mMinimizeButtonSurf, new Point(mTitleBarSurface.Width - 3 - mCloseButtonSurf.Width - mMinimizeButtonSurf.Width - mMinimizeButtonSurf.Width, 0));
			}
			mBorderBuffer.Blit(mTitleBarSurface, mTitleBarBounds.Location);
		}

		private Size AddBorderSize(Size prevSize)
		{
			if (!mWindowed) {
				return prevSize;
			} else {
				return new Size(prevSize.Width + 2, prevSize.Height + 2 + mTitleBarBounds.Height);
			}
		}

		private void InitBuffer()
		{
			Size newSize = this.Size;
			switch (mWindowState) {
				case WindowManager.WindowState.Normal:
					newSize = this.Size;
					break;
				case WindowManager.WindowState.Maximized:
					newSize = new Size(SdlDotNet.Graphics.Video.Screen.Width, SdlDotNet.Graphics.Video.Screen.Height - Globals.GameScreen.TaskBar.Height);
					break;
				case WindowManager.WindowState.Minimized:
					return;
			}
			if (mBuffer != null) {
				mBuffer.Close();
				mBuffer.Dispose();
			}
			mBuffer = new SdlDotNet.Graphics.Surface(AddBorderSize(newSize));
			mBuffer.AlphaBlending = true;
			mBuffer.Alpha = mControlsAlpha;
			mBuffer.Fill(SystemColors.Control);
			if (mWindowed) {
				AddBorder();
			} else {
				mCloseable = false;
				mMinimizable = false;
				mMaximizable = false;
			}
		}

		private void SetMaximizedState()
		{
			if (mWindowState == WindowManager.WindowState.Minimized) {
				List<Gui.Core.Control> controlList = new List<Client.Logic.Gui.Core.Control>();
				controlList = GetAllControls(controlList, true, this);
				for (int i = 0; i < controlList.Count; i++) {
					controlList[i].AddEvents();
				}
			}
			mWindowState = WindowManager.WindowState.Maximized;
			mPrevFullBounds = mFullBounds;
			mPrevControlBounds = mControlBounds;
			this.Location = new Point(0, 0);
			this.Size = mMaximizedControlBounds.Size;
			InitBuffer();
			Globals.GameScreen.TaskBar.Redraw();
		}

		private void SetMinimizedState()
		{
			List<Gui.Core.Control> controlList = new List<Client.Logic.Gui.Core.Control>();
			controlList = GetAllControls(controlList, true, this);
			for (int i = 0; i < controlList.Count; i++) {
				controlList[i].RemoveEvents();
			}
			mWindowState = WindowManager.WindowState.Minimized;
			Globals.GameScreen.TaskBar.Redraw();
		}

		private void SetNormalState()
		{
			if (mWindowState == WindowManager.WindowState.Minimized) {
				List<Gui.Core.Control> controlList = new List<Client.Logic.Gui.Core.Control>();
				controlList = GetAllControls(controlList, true, this);
				for (int i = 0; i < controlList.Count; i++) {
					controlList[i].AddEvents();
				}
			}
			mWindowState = WindowManager.WindowState.Normal;
			InitBuffer();
			Globals.GameScreen.TaskBar.Redraw();
		}

		private Size SubtractBorderSize(Size prevSize)
		{
			if (!mWindowed) {
				return prevSize;
			} else {
				return new Size(prevSize.Width - 2, prevSize.Height - 2 - mTitleBarBounds.Height);
			}
		}

		#endregion Methods
	}
}