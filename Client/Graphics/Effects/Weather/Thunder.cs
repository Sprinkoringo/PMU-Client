/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


using System;
using System.Collections.Generic;
using System.Text;

using Client.Logic.Graphics.Effects.Overlays;
using SdlDotNet.Graphics;
using System.Drawing;

namespace Client.Logic.Graphics.Effects.Weather
{
	/// <summary>
	/// Description of Thunder.
	/// </summary>
	class Thunder : IWeather
	{
		
		#region Fields

        bool disposed;
        List<Raindrop> raindrops = new List<Raindrop>();
        

        #endregion Fields

        #region Constructors

        public Thunder() {
            disposed = false;
            for (int i = 0; i < 100; i++) {
                raindrops.Add(new Raindrop());
            }
            
        }

        #endregion Constructors

        #region Properties

        public bool Disposed {
            get { return disposed; }
        }

        #endregion Properties

        #region Methods

        public void FreeResources() {
            disposed = true;
            for (int i = raindrops.Count - 1; i >= 0; i--) {
                raindrops[i].Dispose();
                raindrops.RemoveAt(i);
            }
        }

        public void Render(Renderers.RendererDestinationData destData, int tick) {
            for (int i = 0; i < raindrops.Count; i++) {
                raindrops[i].UpdateLocation(2);
                destData.Blit(raindrops[i], new Point(raindrops[i].X, raindrops[i].Y));
            }
        }

        #endregion Methods
		
		public Enums.Weather ID {
            get { return Enums.Weather.Thunder; }
        }
	}
}
