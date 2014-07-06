namespace Client.Logic.Maps
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class MapCollection
    {
        #region Fields

        Map activeMap;
        Map mapDown;
        Map mapLeft;
        Map mapRight;
        Map mapUp;

        Map mapTopLeft;
        Map mapBottomLeft;
        Map mapTopRight;
        Map mapBottomRight;

        Map tempActiveMap;
        Map tempUpMap;
        Map tempDownMap;
        Map tempLeftMap;
        Map tempRightMap;

        Map tempTopLeft;
        Map tempBottomLeft;
        Map tempTopRight;
        Map tempBottomRight;

        #endregion Fields

        #region Constructors

        internal MapCollection() {
        }

        #endregion Constructors

        #region Properties

        public Map ActiveMap {
            get { return activeMap; }
        }

        public Map MapDown {
            get { return mapDown; }
        }

        public Map MapLeft {
            get { return mapLeft; }
        }

        public Map MapRight {
            get { return mapRight; }
        }

        public Map MapUp {
            get { return mapUp; }
        }

        public Map TempActiveMap {
            get { return tempActiveMap; }
        }

        public Map TempUpMap {
            get { return tempUpMap; }
        }

        public Map TempDownMap {
            get { return tempDownMap; }
        }

        public Map TempLeftMap {
            get { return tempLeftMap; }
        }

        public Map TempRightMap {
            get { return tempRightMap; }
        }

        #endregion Properties

        #region Indexers

        public Map this[Enums.MapID id] {
            get { return GetMapFromID(id); }
            set {
                switch (id) {
                    case Enums.MapID.Active:
                        activeMap = value;
                        break;
                    case Enums.MapID.Down:
                        mapDown = value;
                        break;
                    case Enums.MapID.Left:
                        mapLeft = value;
                        break;
                    case Enums.MapID.Right:
                        mapRight = value;
                        break;
                    case Enums.MapID.Up:
                        mapUp = value;
                        break;

                    case Enums.MapID.TopLeft:
                        mapTopLeft = value;
                        break;
                    case Enums.MapID.BottomLeft:
                        mapBottomLeft = value;
                        break;
                    case Enums.MapID.TopRight:
                        mapTopRight = value;
                        break;
                    case Enums.MapID.BottomRight:
                        mapBottomRight = value;
                        break;

                    case Enums.MapID.TempActive:
                        tempActiveMap = value;
                        break;
                    case Enums.MapID.TempUp:
                        tempUpMap = value;
                        break;
                    case Enums.MapID.TempDown:
                        tempDownMap = value;
                        break;
                    case Enums.MapID.TempLeft:
                        tempLeftMap = value;
                        break;
                    case Enums.MapID.TempRight:
                        tempRightMap = value;
                        break;

                    case Enums.MapID.TempTopLeft:
                        tempTopLeft = value;
                        break;
                    case Enums.MapID.TempBottomLeft:
                        tempBottomLeft = value;
                        break;
                    case Enums.MapID.TempTopRight:
                        tempTopRight = value;
                        break;
                    case Enums.MapID.TempBottomRight:
                        tempBottomRight = value;
                        break;
                }
            }
        }

        #endregion Indexers

        #region Methods

        private Map GetMapFromID(Enums.MapID id) {
            switch (id) {
                case Enums.MapID.Active:
                    return activeMap;
                case Enums.MapID.Down:
                    return mapDown;
                case Enums.MapID.Left:
                    return mapLeft;
                case Enums.MapID.Right:
                    return mapRight;
                case Enums.MapID.Up:
                    return mapUp;

                case Enums.MapID.TopLeft:
                    return mapTopLeft;
                case Enums.MapID.BottomLeft:
                    return mapBottomLeft;
                case Enums.MapID.TopRight:
                    return mapTopRight;
                case Enums.MapID.BottomRight:
                    return mapBottomRight;

                case Enums.MapID.TempActive:
                    return tempActiveMap;
                case Enums.MapID.TempUp:
                    return tempUpMap;
                case Enums.MapID.TempDown:
                    return tempDownMap;
                case Enums.MapID.TempLeft:
                    return tempLeftMap;
                case Enums.MapID.TempRight:
                    return tempRightMap;

                case Enums.MapID.TempTopLeft:
                    return tempTopLeft;
                case Enums.MapID.TempBottomLeft:
                    return tempBottomLeft;
                case Enums.MapID.TempTopRight:
                    return tempTopRight;
                case Enums.MapID.TempBottomRight:
                    return tempBottomRight;

                default:
                    return null;
            }
        }

        #endregion Methods
    }
}