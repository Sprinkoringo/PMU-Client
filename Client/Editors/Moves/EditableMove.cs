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
