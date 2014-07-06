using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Client.Logic.Maps;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Primitives;
using Client.Logic.Graphics.Renderers.Sprites;
using Client.Logic.Graphics.Renderers.Screen;

namespace Client.Logic.Graphics.Renderers.Npcs
{
    class NpcRenderer
    {
        public static void DrawNpcBars(RendererDestinationData destData, Map map, Enums.MapID targetMapID, int npcSlot) {
            MapNpc npc = map.MapNpcs[npcSlot];
            int x, y;

            if (npc != null && npc.HP > 0 && npc.Num > 0 && npc.ScreenActive) {
                if (npc.HP != npc.MaxHP) {
                    //if (Npc.NpcHelper.Npcs[npc.Num].Big) {
                    //    x = (npc.Location.X * Constants.TILE_WIDTH - 9 + npc.Offset.X) - (Globals.NewMapX * Constants.TILE_WIDTH) - Globals.NewMapXOffset;
                    //    y = (npc.Location.Y * Constants.TILE_HEIGHT + npc.Offset.Y) - (Globals.NewMapY * Constants.TILE_HEIGHT) - Globals.NewMapYOffset;

                    //    Box hpBox = new SdlDotNet.Graphics.Primitives.Box(new Point(destData.Location.X + x, destData.Location.Y + y + 32), new Point(destData.Location.X + x + 50, destData.Location.Y + y + 36));
                    //    destData.Draw(hpBox, Color.Black, false, true);
                    //    if (npc.MaxHP < 1) {
                    //        hpBox = new SdlDotNet.Graphics.Primitives.Box(new Point(destData.Location.X + x, destData.Location.Y + y + 32), new Point(destData.Location.X + x + ((npc.HP / 100) / ((npc.MaxHP + 1) / 100) * 50), destData.Location.Y + y + 36));
                    //    } else {
                    //        double 
                    //        hpBox = new SdlDotNet.Graphics.Primitives.Box(new Point(destData.Location.X + x, destData.Location.Y + y + 32), new Point(destData.Location.X + x + ((npc.HP / 100) / (double)((double)npc.MaxHP / 100) * 50)), destData.Location.Y + y + 36));
                    //    }
                    //    destData.Draw(hpBox, Color.LightGreen, false, true);
                    //} else {
                    int npcX = npc.Location.X;
                    int npcY = npc.Location.Y;
                    Renderers.Maps.SeamlessWorldHelper.ConvertCoordinatesToBorderless(map, targetMapID, ref npcX, ref npcY);

                    x = ScreenRenderer.ToTileX(npcX) + npc.Offset.X;//(npc.X * Constants.TILE_WIDTH + sx + npc.XOffset) - (Globals.NewMapX * Constants.TILE_WIDTH) - Globals.NewMapXOffset;
                    y = ScreenRenderer.ToTileY(npcY) + npc.Offset.Y;//(npc.Y * Constants.TILE_HEIGHT + sx + npc.YOffset) - (Globals.NewMapY * Constants.TILE_HEIGHT) - Globals.NewMapYOffset;

                    Box hpBox = new SdlDotNet.Graphics.Primitives.Box(new Point(destData.Location.X + x, destData.Location.Y + y + 32), new Point(destData.Location.X + x + 32, destData.Location.Y + y + 36));
                    destData.Draw(hpBox, Color.Black, false, true);
                    if (npc.MaxHP < 1) {
                        hpBox = new SdlDotNet.Graphics.Primitives.Box(new Point(destData.Location.X + x, destData.Location.Y + y + 32), new Point(destData.Location.X + x + 32, destData.Location.Y + y + 36));
                    } else {
                        hpBox = new SdlDotNet.Graphics.Primitives.Box(new Point(destData.Location.X + x + 1, destData.Location.Y + y + 33), new Point(Convert.ToInt32(destData.Location.X + x + (Logic.MathFunctions.CalculatePercent(npc.HP, npc.MaxHP) * 0.01) * 31), destData.Location.Y + y + 35));
                    }
                    destData.Draw(hpBox, Color.LightGreen, false, true);
                    //}
                }
            }
        }

        public static void DrawNpc(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, int npcSlot) {
            
            MapNpc npc = activeMap.MapNpcs[npcSlot];
            if (npc != null && npc.Num > 0 && npc.ScreenActive) {
                SpriteRenderer.DrawSprite(destData, activeMap, targetMapID, npc);
            }


        }

        public static void DrawMapNpcName(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, int npcSlot) {
            Color nameColor = Color.White;
            if (activeMap.MapNpcs[npcSlot].Enemy) {
                nameColor = Color.Red;
            }
            Npc.Npc npc = Npc.NpcHelper.Npcs[activeMap.MapNpcs[npcSlot].Num];

            string name = npc.Name;

            //if (Globals.FoolsMode) {
            //    name = "EBIL Zubat";
            //}

            SpriteRenderer.DrawSpriteName(destData, activeMap, targetMapID, activeMap.MapNpcs[npcSlot], nameColor, name);
        }

        public static void DrawNpcStatus(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, int npcSlot, int statusNum) {
            MapNpc npc = activeMap.MapNpcs[npcSlot];
            switch (statusNum) {//Enums not used to account for confusion status
                case 1: {//Burn
                        SpriteRenderer.DrawStatus(destData, activeMap, targetMapID, npc, 0, new Point(0, -32));
                    }
                    break;
                case 2: {//Freeze
                        SpriteRenderer.DrawStatus(destData, activeMap, targetMapID, npc, 1, new Point(0, 0));
                    }
                    break;
                case 3: {//paralyze
                        //Nothing here
                    }
                    break;
                case 4: {//poison
                        SpriteRenderer.DrawStatus(destData, activeMap, targetMapID, npc, 2, new Point(0, -32));
                    }
                    break;
                case 5: {//sleep
                        SpriteRenderer.DrawStatus(destData, activeMap, targetMapID, npc, 3, new Point(0, -32));
                    }
                    break;
                case 6: {//confuse
                        SpriteRenderer.DrawStatus(destData, activeMap, targetMapID, npc, 4, new Point(0, -24));
                    }
                    break;
            }

        }

        public static void DrawNpcVolatileStatus(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, int npcSlot, List<int> extraStatus) {
            MapNpc npc = activeMap.MapNpcs[npcSlot];
            List<int> visibleStatus = new List<int>();
            foreach (int i in extraStatus) {
                if (i >= 0) {
                    visibleStatus.Add(i);
                }
            }
            if (visibleStatus.Count == 0) return;
            int statusIndex = Globals.Tick / 600 % visibleStatus.Count;

            SpriteRenderer.DrawStatus(destData, activeMap, targetMapID, npc, visibleStatus[statusIndex], new Point(24, -32));

        }

    }
}
