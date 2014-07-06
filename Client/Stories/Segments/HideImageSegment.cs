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

    class HideImageSegment : ISegment
    {
        #region Fields

        string imageID;
        ListPair<string, string> parameters;
        StoryState storyState;

        #endregion Fields

        #region Constructors

        public HideImageSegment(string imageID) {
            Load(imageID);
        }

        public HideImageSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.MapVisibility; }
        }

        public string ImageID {
            get { return imageID; }
            set { imageID = value; }
        }

        public ListPair<string, string> Parameters
        {
            get { return parameters; }
        }

        public bool UsesSpeechMenu {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public void Load(string imageID) {
            this.imageID = imageID;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            this.imageID = parameters.GetValue("ImageID");
        }

        public void Process(StoryState state) {
            int index = -1;
            for (int i = 0; i < Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.ScreenImageOverlays.Count; i++) {
                if (Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.ScreenImageOverlays[i].ImageID == imageID) {
                    index = i;
                    break;
                }
            }
            if (index > -1) {
                Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.ScreenImageOverlays.RemoveAt(index);
            }
        }

        #endregion Methods
    }
}