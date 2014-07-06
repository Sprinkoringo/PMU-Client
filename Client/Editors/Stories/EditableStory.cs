using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Editors.Stories
{
    class EditableStory
    {
        #region Fields

        public List<int> ExitAndContinue;
        List<EditableStorySegment> segments;

        #endregion Fields

        #region Constructors

        public EditableStory() {
            ExitAndContinue = new List<int>();
            segments = new List<EditableStorySegment>();
        }

        #endregion Constructors

        #region Properties

        public string Name {
            get;
            set;
        }

        public List<EditableStorySegment> Segments {
            get { return segments; }
            set { segments = value; }
        }

        public int StoryStart {
            get;
            set;
        }

        #endregion Properties
    }
}
