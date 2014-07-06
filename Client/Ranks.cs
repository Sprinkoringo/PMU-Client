using System;
using System.Drawing;
using Client.Logic.Players;

namespace Client.Logic
{
    /// <summary>
    /// Description of Ranks.
    /// </summary>
    internal class Ranks
    {
        /// <summary>
        /// Checks if the player has the specified rank permissions
        /// </summary>
        /// <param name="index">The index of the player to check</param>
        /// <param name="RankToCheck">The rank permissions to test</param>
        /// <returns>True if the player has the rank permissions; otherwise, false</returns>
        public static bool IsAllowed(IPlayer player, Enums.Rank RankToCheck) {
            Enums.Rank PlayerRank = player.Access;
            switch (RankToCheck) {
                case Enums.Rank.Normal:
                    return true;
                case Enums.Rank.Moniter:
                    if (PlayerRank == Enums.Rank.Moniter || PlayerRank == Enums.Rank.Mapper || PlayerRank == Enums.Rank.Developer || PlayerRank == Enums.Rank.Admin || PlayerRank == Enums.Rank.ServerHost || PlayerRank == Enums.Rank.Scriptor) {
                        return true;
                    }
                    break;
                case Enums.Rank.Mapper:
                    if (PlayerRank == Enums.Rank.Mapper || PlayerRank == Enums.Rank.Developer || PlayerRank == Enums.Rank.Admin || PlayerRank == Enums.Rank.ServerHost || PlayerRank == Enums.Rank.Scriptor) {
                        return true;
                    }
                    break;
                case Enums.Rank.Developer:
                    if (PlayerRank == Enums.Rank.Developer || PlayerRank == Enums.Rank.Admin || PlayerRank == Enums.Rank.ServerHost || PlayerRank == Enums.Rank.Scriptor) {
                        return true;
                    }
                    break;
                case Enums.Rank.Admin:
                    if (PlayerRank == Enums.Rank.Admin || PlayerRank == Enums.Rank.ServerHost || PlayerRank == Enums.Rank.Scriptor) {
                        return true;
                    }
                    break;
                case Enums.Rank.ServerHost:
                    if (PlayerRank == Enums.Rank.ServerHost || PlayerRank == Enums.Rank.Scriptor) {
                        return true;
                    }
                    break;
                case Enums.Rank.Scriptor:
                    if (PlayerRank == Enums.Rank.Scriptor) {
                        return true;
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Checks if the player does not have the specified rank permissions
        /// </summary>
        /// <param name="index">The index of the player to check</param>
        /// <param name="RankToCheck">The rank permissions to test</param>
        /// <returns>True if the player does not have the rank permissions; otherwise, false</returns>
        public static bool IsDisallowed(IPlayer player, Enums.Rank RankToCheck) {
            return !IsAllowed(player, RankToCheck);
        }

        /// <summary>
        /// Gets the color associated with the specified rank
        /// </summary>
        /// <param name="rank">The rank used to determine the color returned</param>
        /// <returns>The color associated with the specified rank</returns>
        public static Color GetRankColor(Enums.Rank rank) {
            switch (rank) {
                case Enums.Rank.Normal:
                    return Color.Brown;
                case Enums.Rank.Moniter:
                    return Color.FromArgb(255, 254, 150, 46);
                case Enums.Rank.Mapper:
                    return Color.Cyan;
                case Enums.Rank.Developer:
                    return Color.FromArgb(255, 0, 110, 210);
                case Enums.Rank.Admin:
                    return Color.Pink;
                case Enums.Rank.ServerHost:
                    return Color.Yellow;
                case Enums.Rank.Scriptor:
                    return Color.LightCyan;
                default:
                    return Color.DarkRed;
            }
        }
    }
}
