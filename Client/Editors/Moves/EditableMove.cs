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

namespace Client.Logic.Editors.Moves
{
    class EditableMove
    {
        #region Properties

        public bool Big {
            get;
            set;
        }

        public int Data1 {
            get;
            set;
        }

        public int Data2 {
            get;
            set;
        }

        public int Data3 {
            get;
            set;
        }

        public Enums.MoveType EffectType {
            get;
            set;
        }

        public bool IsKey {
            get;
            set;
        }

        public int KeyItem {
            get;
            set;
        }

        public int LevelReq {
            get;
            set;
        }

        public int MaxPP {
            get;
            set;
        }

        public Enums.PokemonType MoveType {
            get;
            set;
        }

        public string Name {
            get;
            set;
        }

        public Enums.MoveRange Range {
            get;
            set;
        }

        public int Sound {
            get;
            set;
        }

        public int SpellAnim {
            get;
            set;
        }

        public int SpellDone {
            get;
            set;
        }

        public int SpellTime {
            get;
            set;
        }

        public Enums.MoveTarget TargetType {
            get;
            set;
        }

        #endregion Properties

    }
}
