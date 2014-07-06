using System;
using System.Collections.Generic;
using System.Text;
using PMU.Core;

namespace Client.Logic.Editors.Stories
{
    class EditableStorySegment
    {
        ListPair<string, string> parameters;

        public EditableStorySegment() {
            parameters = new ListPair<string, string>();
        }

        #region Properties

        public Enums.StoryAction Action {
            get;
            set;
        }

        public ListPair<string, string> Parameters {
            get { return parameters; }
        }

        public void AddParameter(string paramID, string value) {
            parameters.Add(paramID, value);
        }

        #endregion Properties
    }
}
