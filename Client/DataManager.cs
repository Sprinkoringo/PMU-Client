namespace Client.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class DataManager
    {
        #region Methods

        public static int AverageLoadPercent() {
            return 100;
            //return (Items.ItemHelper.DataLoadPercent + Emotions.EmotionHelper.DataLoadPercent +
            //    + Arrows.ArrowHelper.DataLoadPercent + Npc.NpcHelper.DataLoadPercent + Shops.ShopHelper.DataLoadPercent
            //    + Moves.MoveHelper.DataLoadPercent + Evolutions.EvolutionHelper.DataLoadPercent + Stories.StoryHelper.DataLoadPercent + Missions.MissionHelper.DataLoadPercent +
            //    RDungeons.RDungeonHelper.DataLoadPercent + Dungeons.DungeonHelper.DataLoadPercent) / 10;
        }

        #endregion Methods
    }
}