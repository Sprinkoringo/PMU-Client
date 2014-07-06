using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Editors.NPCs
{
    class EditableNpcDrop
    {
        #region Properties

        public int Chance {
            get;
            set;
        }

        public int ItemNum {
            get;
            set;
        }

        public int ItemValue {
            get;
            set;
        }

        public string Tag
        {
            get;
            set;
        }

        #endregion Properties
    }
}
