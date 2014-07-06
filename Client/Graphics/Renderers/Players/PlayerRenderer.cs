using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;
using Client.Logic.Maps;
using System.Drawing;
using Client.Logic.Players;
using Client.Logic.Graphics.Renderers.Sprites;

namespace Client.Logic.Graphics.Renderers.Players
{
    class PlayerRenderer
    {
        public static void DrawPlayer(RendererDestinationData destData, IPlayer player, Map activeMap, Enums.MapID targetMapID) {

            if (!player.Hunted && !player.Dead && activeMap.Moral == Enums.MapMoral.None) {// && Ranks.IsDisallowed(player, Enums.Rank.Moniter)
                int flashIndex = Globals.Tick / 100 % 2;
                if (flashIndex == 0) {
                    SpriteRenderer.DrawSprite(destData, activeMap, targetMapID, player);
                }
            } else {

                SpriteRenderer.DrawSprite(destData, activeMap, targetMapID, player);
            }
        }

        public static void DrawPlayerName(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, IPlayer player) {
            Color playerColor = Color.DarkRed;

            //if (player.PK) {
            //    playerColor = Color.DarkRed;
            //} else {
                playerColor = Ranks.GetRankColor(player.Access);
            //}

            string name = player.Name;
            if (!string.IsNullOrEmpty(player.Status)) {

                string status = player.Status;

                if (Globals.FoolsMode) {
                    status = "Itemized";
                }

                name += " [" + status + "]";
            }
            SpriteRenderer.DrawSpriteName(destData, activeMap, targetMapID, player, playerColor, name);
        }

        public static void DrawPlayerGuild(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, IPlayer player) {
            Color color = Color.DarkRed;

            // Check access level
            //if (player.PK == false) {
                switch (player.GuildAccess) {
                    case Enums.GuildRank.None:
                        color = Color.Red;
                        break;
                    case Enums.GuildRank.Member:
                        color = Color.LightSkyBlue;
                        break;
                    case Enums.GuildRank.Admin:
                        color = Color.Yellow;
                        break;
                    case Enums.GuildRank.Founder:
                        color = Color.LawnGreen;
                        break;
                }
            //} else {
            //    color = Color.DarkRed;
            //}

            string guild = player.Guild;

            if (guild == null) {
                guild = "";
            }

            //if (Globals.FoolsMode) {
            //    guild = "MewTeam";
            //}

            SpriteRenderer.DrawSpriteGuild(destData, activeMap, targetMapID, player, color, guild);
        }

        public static void DrawPlayerBar(RendererDestinationData destData) {
            SpriteRenderer.DrawSpriteHPBar(destData, PlayerManager.MyPlayer, PlayerManager.MyPlayer.GetActiveRecruit().HP, PlayerManager.MyPlayer.GetActiveRecruit().MaxHP);
        }

        public static void DrawPlayerEmote(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, IPlayer player) {
            SpriteRenderer.DrawStatus(destData, activeMap, targetMapID, player, player.CurrentEmote.EmoteIndex, player.CurrentEmote.EmoteFrame, new Point(16, -32));
        }

        public static void DrawPlayerStatus(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, IPlayer player, int statusNum) {
            switch (statusNum) {//Enums not used to account for confusion status
                case 1: {//Burn
                        SpriteRenderer.DrawStatus(destData,activeMap, targetMapID, player, 0, new Point(0, -32));
                    }
                    break;
                case 2: {//Freeze
                    SpriteRenderer.DrawStatus(destData, activeMap, targetMapID, player, 1, new Point(0, 0));
                    }
                    break;
                case 3: {//paralyze
                        //Nothing here
                    }
                    break;
                case 4: {//poison
                    SpriteRenderer.DrawStatus(destData, activeMap, targetMapID, player, 2, new Point(0, -32));
                    }
                    break;
                case 5: {//sleep
                    SpriteRenderer.DrawStatus(destData, activeMap, targetMapID, player, 3, new Point(0, -32));
                    }
                    break;
                //case 6: {//confuse
                //    SpriteRenderer.DrawStatus(destData, activeMap, targetMapID, player, 4, new Point(0, -24));
                //    }
                //    break;
                case 6: {//KO
                        SpriteRenderer.DrawStatus(destData, activeMap, targetMapID, player, 9, new Point(16, -24));
                    }
                    break;
            }

        }

        public static void DrawPlayerVolatileStatus(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, IPlayer player, List<int> extraStatus) {

            List<int> visibleStatus = new List<int>();
            foreach (int i in extraStatus) {
                if (i >= 0) {
                    visibleStatus.Add(i);
                }
            }
            if (visibleStatus.Count == 0) return;
            int statusIndex = Globals.Tick / 600 % visibleStatus.Count;

            SpriteRenderer.DrawStatus(destData, activeMap, targetMapID, player, visibleStatus[statusIndex], new Point(16, -32));

        }

        public static void DrawMiniBars(RendererDestinationData destData) {
            int userHPBarStartX = 40;//DrawingSupport.GetCenterX(destData.Size.Width, 100);
            TextRenderer.DrawText(destData, "HP:", Color.WhiteSmoke, Color.Black, new Point(userHPBarStartX - 30, 5));
            SdlDotNet.Graphics.Primitives.Box hpBox = new SdlDotNet.Graphics.Primitives.Box(new Point(destData.Location.X + userHPBarStartX - 1, destData.Location.Y + 17), new Point(destData.Location.X + userHPBarStartX + 100 + 1, destData.Location.Y + 27));
            destData.Draw(hpBox, Color.Black, false, false);
            hpBox = new SdlDotNet.Graphics.Primitives.Box(new Point(destData.Location.X + userHPBarStartX, destData.Location.Y + 17 + 1), new Point(Convert.ToInt32(destData.Location.X + userHPBarStartX + (Logic.MathFunctions.CalculatePercent(PlayerManager.MyPlayer.GetActiveRecruit().HP, PlayerManager.MyPlayer.GetActiveRecruit().MaxHP) * 0.01) * 100), destData.Location.Y + 27 - 1));

            Color hpColor;
            if (PlayerManager.MyPlayer.GetActiveRecruit().HP < PlayerManager.MyPlayer.GetActiveRecruit().MaxHP / 5) {
                hpColor = Color.Red;
            } else if (PlayerManager.MyPlayer.GetActiveRecruit().HP < PlayerManager.MyPlayer.GetActiveRecruit().MaxHP / 2) {
                hpColor = Color.Yellow;
            } else {
                hpColor = Color.Green;
            }

            destData.Draw(hpBox, hpColor, false, true);

            int userBellyBarStartX = 205;//DrawingSupport.GetCenterX(destData.Size.Width, 100);
            TextRenderer.DrawText(destData, "Belly:", Color.WhiteSmoke, Color.Black, new Point(userBellyBarStartX - 45, 5));
            SdlDotNet.Graphics.Primitives.Box bellyBox = new SdlDotNet.Graphics.Primitives.Box(new Point(destData.Location.X + userBellyBarStartX - 1, destData.Location.Y + 17), new Point(destData.Location.X + userBellyBarStartX + 100 + 1, destData.Location.Y + 27));
            destData.Draw(bellyBox, Color.Black, false, false);
            bellyBox = new SdlDotNet.Graphics.Primitives.Box(new Point(destData.Location.X + userBellyBarStartX, destData.Location.Y + 17 + 1), new Point(Convert.ToInt32(destData.Location.X + userBellyBarStartX + (Logic.MathFunctions.CalculatePercent(PlayerManager.MyPlayer.Belly, PlayerManager.MyPlayer.MaxBelly) * 0.01) * 100), destData.Location.Y + 27 - 1));

            Color bellyColor;
            if (PlayerManager.MyPlayer.Belly < PlayerManager.MyPlayer.MaxBelly / 5) {
                bellyColor = Color.Red;
            } else if (PlayerManager.MyPlayer.Belly < PlayerManager.MyPlayer.MaxBelly / 2) {
                bellyColor = Color.Yellow;
            } else {
                bellyColor = Color.Green;
            }

            destData.Draw(bellyBox, bellyColor, false, true);
        }
    }
}
