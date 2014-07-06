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


namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Client.Logic.Menus.Core;

    using PMU.Core;

    class ShowImageSegment : ISegment
    {
        #region Fields

        string imageID;
        ListPair<string, string> parameters;
        int x;
        int y;
        StoryState storyState;
        string file;

        #endregion Fields

        #region Constructors

        public ShowImageSegment(string file, string imageID, int x, int y) {
            Load(file, imageID, x, y);
        }

        public ShowImageSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.ShowImage; }
        }

        public string File {
            get { return file; }
            set { file = value; }
        }


        public ListPair<string, string> Parameters
        {
            get { return parameters; }
        }

        public string ImageID {
            get { return imageID; }
            set { imageID = value; }
        }

        public int X {
            get { return x; }
            set { x = value; }
        }

        public int Y {
            get { return y; }
            set { y = value; }
        }

        public bool UsesSpeechMenu {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public void Load(string file, string imageID, int x, int y) {
            this.file = file;
            this.imageID = imageID;
            this.x = x;
            this.y = y;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            Load(parameters.GetValue("File"), parameters.GetValue("ImageID"), parameters.GetValue("X").ToInt(), parameters.GetValue("Y").ToInt());
        }

        public void Process(StoryState state) {
            this.storyState = state;
            state.ResetWaitEvent();

            Components.ScreenImageOverlay imageOverlay = new Components.ScreenImageOverlay(file, imageID, x, y);
            imageOverlay.LoadImage();
            Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.ScreenImageOverlays.Add(imageOverlay);
        }

        #endregion Methods
    }
}