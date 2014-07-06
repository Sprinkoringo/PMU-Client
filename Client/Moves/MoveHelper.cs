namespace Client.Logic.Moves
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using PMU.Core;

    class MoveHelper
    {
        #region Fields

        private static int dataLoadPercent = 0;
        private static MoveCollection mMoves;

        #endregion Fields

        #region Properties

        public static int DataLoadPercent
        {
            get { return dataLoadPercent; }
        }

        public static MoveCollection Moves
        {
            get { return mMoves; }
        }

        #endregion Properties

        #region Methods

        public static void InitMoveCollection()
        {
            mMoves = new MoveCollection(MaxInfo.MaxMoves);
        }

        public static void LoadMovesFromPacket(string[] parse)
        {
            try {
                int n = 1;
                for (int i = 1; i <= MaxInfo.MaxMoves; i++) {
                    dataLoadPercent = System.Math.Min(99, Logic.MathFunctions.CalculatePercent(i, MaxInfo.MaxMoves));
                    mMoves[i] = new Move();
                    mMoves[i].Name = parse[n + 1];
                    mMoves[i].RangeType = (Enums.MoveRange)parse[n + 2].ToInt();
                    mMoves[i].Range = parse[n + 3].ToInt();
                    mMoves[i].TargetType = (Enums.MoveTarget)parse[n + 4].ToInt();
                    mMoves[i].HitTime = parse[n + 5].ToInt();
                    mMoves[i].HitFreeze = parse[n + 6].ToBool();
                    n += 7;
                    ((Windows.winLoading)Windows.WindowSwitcher.FindWindow("winLoading")).UpdateLoadText("Recieving Data... " + DataManager.AverageLoadPercent().ToString() + "%");
                }
                dataLoadPercent = 100;
            } catch (Exception ex) {
                Exceptions.ExceptionHandler.OnException(ex);
            }
        }

        #endregion Methods
    }
}