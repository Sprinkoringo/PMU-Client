namespace Client.Logic.Gui.Core
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	using SdlDotNet.Graphics;

	/// <summary>
	/// Base Control class. All controls must inherit from this class.
	/// </summary>
	internal class Control : Windows.Core.WindowCore
	{
		#region Fields

		protected Rectangle mScreenBounds = new Rectangle(0, 0, 50, 50);

		private Rectangle mBounds = new Rectangle(0, 0, 50, 50);
		private SdlDotNet.Graphics.Surface mBuffer;
		private bool mEventsAdded = false;
		private bool mFocusOnClick = false;
		private bool mFocused = false;
		private bool mIsDisposing = false;
		private bool mOverridingTheme = false;
		private Control mParent;
		private Windows.Core.Window mParentWindow;
		private bool mVisible = true;
		private bool mVisibleSet = false;
		private int mZIndex;
		
		private bool mKeyRepeatEnabled = false;
		private int mKeyRepeatDelay;
		private int mKeyRepeatRate;
		//private int mLastKeyPress;

		#endregion Fields

		#region Constructors

		public Control()
			: base(false)
		{
			//AddEvents();
			ReinitBuffer();
			this.OnClick += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(Control_OnClick);
		}

		#endregion Constructors

		#region Events

		public event EventHandler<SdlDotNet.Input.MouseButtonEventArgs> OnClick;

		public event EventHandler<SdlDotNet.Input.KeyboardEventArgs> OnKeyDown;

		#endregion Events

		#region Properties
		
		public bool KeyRepeatEnabled {
			get { return mKeyRepeatEnabled; }
			set {
				mKeyRepeatEnabled = value;
			}
		}
		
		public int KeyRepeatDelay {
			get { return mKeyRepeatDelay; }
			set {
				mKeyRepeatDelay = value;
			}
		}
		
		public int KeyRepeatRate {
			get { return mKeyRepeatRate; }
			set {
				mKeyRepeatRate = value;
			}
		}

		public Rectangle Bounds
		{
			get { return mBounds; }
		}

		public SdlDotNet.Graphics.Surface Buffer
		{
			get { return mBuffer; }
		}

		public List<Control> Children
		{
			get { return Controls; }
		}

		public bool CopyParentVisibility
		{
			get { return !mVisibleSet; }
			set {
				mVisibleSet = !value;
			}
		}

		public bool FocusOnClick
		{
			get { return mFocusOnClick; }
			set { mFocusOnClick = value; }
		}

		public bool Focused
		{
			get { return mFocused; }
			set {
				if (value) {
					mParentWindow.UnsetControlFocus(mParentWindow);
				} else {
					// other stuff
				}
				mFocused = value;
			}
		}

		public int Height
		{
			get { return mBounds.Height; }
			set {
				mBounds.Height = value;
				ReinitBuffer();
			}
		}

		public Point Location
		{
			get { return mBounds.Location; }
			set {
				mBounds.Location = value;
				mScreenBounds.Location = GetTotalAddLocation(new Point(mBounds.X, mBounds.Y), this);
			}
		}

		public bool OverrideTheme
		{
			get { return mOverridingTheme; }
			set {
				mOverridingTheme = value;
			}
		}

		public Control Parent
		{
			get { return mParent; }
			set { mParent = value; }
		}

		public Windows.Core.Window ParentWindow
		{
			get { return mParentWindow; }
			set {
				mParentWindow = value;
			}
		}

		public Rectangle ScreenBounds
		{
			get { return mScreenBounds; }
		}

		public Point ScreenLocation
		{
			get { return mScreenBounds.Location; }
		}

		public Size Size
		{
			get { return mBounds.Size; }
			set {
				mBounds.Size = value;
				mScreenBounds.Size = value;
				ReinitBuffer();
			}
		}

		public bool Visible
		{
			get { return mVisible; }
			set {
				if (value != mVisible) {
					mVisible = value;
					mVisibleSet = true;
					if (mVisible) {
						AddEvents();
					} else {
						RemoveEvents();
					}
					foreach (Control c in this.Children) {
						if (!c.mVisibleSet) {
							c.Visible = value;
							c.mVisibleSet = false;
						}
					}
				}
			}
		}

		public int Width
		{
			get { return mBounds.Width; }
			set {
				mBounds.Width = value;
				ReinitBuffer();
			}
		}

		public int Z
		{
			get { return mZIndex; }
		}

		#endregion Properties

		#region Methods

		public new void AddControl(Control control)
		{
			control.Parent = this;
			control.UpdateControlInfo(this.Children.Count);
			base.AddControl(control);
		}

		public void AddEvents()
		{
			if (mEventsAdded == false) {
				SdlDotNet.Core.Events.MouseButtonDown += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(Events_MouseButtonDown);
				SdlDotNet.Core.Events.KeyboardDown += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(Events_KeyboardDown);

				mEventsAdded = true;
			}
		}

		public virtual void Close()
		{
			mIsDisposing = true;
			mBuffer.Close();
		}

		public bool PointInBounds(Point pointToTest)
		{
			Point totalAddLoc = GetTotalAddLocation(new Point(mBounds.X, mBounds.Y), this);
			int addX = totalAddLoc.X;
			int addY = totalAddLoc.Y;
			if (mParentWindow != null) {
				addX += mParentWindow.ControlBounds.X;
				addY += mParentWindow.ControlBounds.Y;
			}
			if (pointToTest.X >= addX && pointToTest.Y >= addY && pointToTest.X <= mBounds.Width + addX && pointToTest.Y <= mBounds.Height + addY) {
				return true;
			} else {
				return false;
			}
		}
		
		protected bool IsColorTransparent(Color colorToTest) {
			return (colorToTest.A == 0);
		}

		public bool PointInBounds(Point pointToTest, Rectangle bounds)
		{
			Point totalAddLoc = GetTotalAddLocation(new Point(bounds.X, bounds.Y), this);
			int addX = totalAddLoc.X;
			int addY = totalAddLoc.Y;
			if (pointToTest.X >= addX && pointToTest.Y >= addY && pointToTest.X <= bounds.Width + addX && pointToTest.Y <= bounds.Height + addY) {
				return true;
			} else {
				return false;
			}
		}

		public bool RectInBounds(Rectangle boundsToTest)
		{
			return boundsToTest.IntersectsWith(boundsToTest);
		}

		public void RemoveEvents()
		{
			if (mEventsAdded) {
				SdlDotNet.Core.Events.MouseButtonDown -= new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(Events_MouseButtonDown);
				SdlDotNet.Core.Events.KeyboardDown -= new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(Events_KeyboardDown);

				mEventsAdded = false;
			}
		}

		public virtual void Update(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
		{
			try {
				if (!mIsDisposing) {
//					Point totalAddLoc = GetTotalAddLocation(new Point(mBounds.X, mBounds.Y), this);
//					if (mScreenBounds.Location != totalAddLoc) {
//						mScreenBounds = new Rectangle(totalAddLoc, this.Size);
//					}
//					int addX = totalAddLoc.X;
//					int addY = totalAddLoc.Y;
					if (mBuffer.Size != mBounds.Size) {
						ReinitBuffer();
					}
					dstSrf.Blit(mBuffer, this.Location);
					UpdateControls(mBuffer, e);
				}
			} catch (Exception ex) {
				Console.WriteLine(ex.ToString());
			}
		}
		
		public void UpdateControlInfo(int zIndex)
		{
			mScreenBounds = new Rectangle(GetTotalAddLocation(new Point(mBounds.X, mBounds.Y), this), this.Size);
			mZIndex = zIndex;
		}

		protected Point ControlPointFromScreenPoint(Point screenPoint)
		{
			Point totalAddLoc = GetTotalAddLocation(new Point(0,0), this);
			int addX = totalAddLoc.X;
			int addY = totalAddLoc.Y;
			if (mParentWindow != null) {
				addX += mParentWindow.Location.X;
				addY += mParentWindow.Location.Y;
			}
			return new Point(screenPoint.X - addX, screenPoint.Y - addY);
		}

		protected Point GetTotalAddLocation(Point addLoc, Control control)
		{
			if (control.Parent != null) {
				return GetTotalAddLocation(new Point(addLoc.X + control.Parent.Location.X, addLoc.Y + control.Parent.Location.Y), control.Parent);
			} else {
				return addLoc;
			}
		}

		protected void ReinitBuffer()
		{
			if (mBuffer != null) {
				mBuffer.Close();
				mBuffer.Dispose();
			}
			mBuffer = new Surface(this.Size);
			mBuffer.Transparent = true;
			mBuffer.TransparentColor = Color.Transparent;
			mBuffer.Fill(Color.Transparent);
		}

		void Control_OnClick(object sender, SdlDotNet.Input.MouseButtonEventArgs e)
		{
			if (FocusOnClick) {
				if (this.Focused == false) {
					this.Focused = true;
				}
			}
		}

		void Events_KeyboardDown(object sender, SdlDotNet.Input.KeyboardEventArgs e)
		{
			if (mFocused) {
				if (mKeyRepeatEnabled == false) {
					if (SdlDotNet.Input.Keyboard.IsKeyPressed(e.Key)) {
						if (OnKeyDown != null)
							OnKeyDown(this, e);
					}
				} else {
					
				}
			}
		}

		void Events_MouseButtonDown(object sender, SdlDotNet.Input.MouseButtonEventArgs e)
		{
			if (this.PointInBounds(e.Position)) {
				if (OnClick != null)
					OnClick(this, e);
			}
		}

		#endregion Methods
	}
}