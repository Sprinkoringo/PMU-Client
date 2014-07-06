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

    using PMU.Core;

    class CreateFNPCSegment : ISegment
    {
        #region Fields

        string id;
        string parentMapID;
        StoryState storyState;
        int x;
        int y;
        int sprite;
        int form;
        Enums.Coloration shiny;
        Enums.Sex gender;
        ListPair<string, string> parameters;

        #endregion Fields

        #region Constructors

        public CreateFNPCSegment(string id, string parentMapID, int x, int y, int sprite, int form, Enums.Coloration shiny, Enums.Sex gender) {
            Load(id, parentMapID, x, y, sprite, form, shiny, gender);
        }

        public CreateFNPCSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.CreateFNPC; }
        }

        public string ID {
            get { return id; }
            set { id = value; }
        }

        public ListPair<string, string> Parameters
        {
            get { return parameters; }
        }

        public string ParentMapID {
            get { return parentMapID; }
            set { parentMapID = value; }
        }

        public int X {
            get { return x; }
            set { x = value; }
        }

        public int Y {
            get { return y; }
            set { y = value; }
        }

        public int Sprite {
            get { return sprite; }
            set { sprite = value; }
        }

        public int Form {
            get { return form; }
            set { form = value; }
        }

        public Enums.Sex Gender {
            get { return gender; }
            set { gender = value; }
        }

        public Enums.Coloration Shiny {
            get { return shiny; }
            set { shiny = value; }
        }

        public bool UsesSpeechMenu {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public void Load(string id, string parentMapID, int x, int y, int sprite, int form, Enums.Coloration shiny, Enums.Sex gender) {
            this.id = id;
            this.parentMapID = parentMapID;
            this.x = x;
            this.y = y;
            this.sprite = sprite;
            this.form = form;
            this.shiny = shiny;
            this.gender = gender;

            if (this.parentMapID == "s-2") {
                this.parentMapID = "-2";
            }
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            Load(parameters.GetValue("ID"), parameters.GetValue("ParentMapID"),
                parameters.GetValue("X").ToInt(), parameters.GetValue("Y").ToInt(),
                parameters.GetValue("Sprite").ToInt(), 0, Enums.Coloration.Normal, Enums.Sex.Genderless);
        }

        public void Process(StoryState state) {
            this.storyState = state;
            FNPCs.FNPC fnpc = new FNPCs.FNPC();
            fnpc.MapID = parentMapID;
            fnpc.X = x;
            fnpc.Y = y;
            fnpc.Sprite = sprite;
            fnpc.Form = form;
            fnpc.Shiny = shiny;
            fnpc.Sex = gender;
            fnpc.ID = id;

            state.FNPCs.Add(fnpc);
        }

        #endregion Methods
    }
}