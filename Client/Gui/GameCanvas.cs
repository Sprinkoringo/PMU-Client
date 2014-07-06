#region Header

/*
 * Created by SharpDevelop.
 * User: Pikachu
 * Date: 26/09/2009
 * Time: 1:35 PM
 *
 */

#endregion Header

namespace Client.Logic.Gui
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	using Gfx = SdlDotNet.Graphics;

	/// <summary>
	/// Description of GameCanvas.
	/// </summary>
	internal class GameCanvas : Core.Control
	{
		private GLRoutines mGameLoop;
		private int lastTick;
		
		#region Constructors

		Gfx.Surface mGameSurface;
		
		public GameCanvas()
		{
			mGameLoop = new GLRoutines();
		}
		
		public new Size Size {
			get { return base.Size; }
			set {
				base.Size = value;
				mGameSurface = new SdlDotNet.Graphics.Surface(base.Size);
			}
		}

		#endregion Constructors
		
		public override void Update(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
		{
			if (e.Tick > lastTick + 35) {
				mGameLoop.DrawScreen(mGameSurface, e);
				lastTick = e.Tick;
			}
			dstSrf.Blit(mGameSurface, this.Location);
		}
	}
}