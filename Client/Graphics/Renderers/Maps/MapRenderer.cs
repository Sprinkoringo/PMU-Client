using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;
using Client.Logic.Maps;
using Client.Logic.Graphics.Renderers.Screen;
using Client.Logic.Players;

namespace Client.Logic.Graphics.Renderers.Maps
{
    class MapRenderer
    {
        public static void DrawTile(RendererDestinationData destData, int sheet, int tileNum, int tileX, int tileY) {
            DrawTile(destData, sheet, tileNum, tileX, tileY, true);
        }

        public static void DrawTile(RendererDestinationData destData, int sheet, int tileNum, int tileX, int tileY, bool useGlobalCamera) {
            DrawTile(destData, Logic.Graphics.GraphicsManager.Tiles[sheet].GetTileGraphic(tileNum), tileX, tileY, useGlobalCamera);
        }

        public static void DrawTile(RendererDestinationData destData, TileGraphic tileGraphic, int tileX, int tileY, bool useGlobalCamera) {
            useGlobalCamera = false;
            if (useGlobalCamera) {
                destData.Blit(tileGraphic.Tile, new Point(ScreenRenderer.ToTileX(tileX), ScreenRenderer.ToTileY(tileY)));//new Point((tileX * Constants.TILE_WIDTH) /*- (NewPlayerX * Constants.TILE_WIDTH) */- ScreenRenderer.MapXOffset, (tileY * Constants.TILE_HEIGHT) /*- (NewPlayerY * Constants.TILE_HEIGHT)*/ - ScreenRenderer.MapYOffset));
            } else {
                destData.Blit(tileGraphic.Tile, new Point((tileX * Constants.TILE_WIDTH) /*- (NewPlayerX * Constants.TILE_WIDTH) */- ScreenRenderer.MapXOffset, (tileY * Constants.TILE_HEIGHT) /*- (NewPlayerY * Constants.TILE_HEIGHT)*/ - ScreenRenderer.MapYOffset));
            }
        }

        public static void DrawTileToSurface(Surface destData, int sheet, int tileNum, int tileX, int tileY) {
            destData.Blit(Logic.Graphics.GraphicsManager.Tiles[sheet][tileNum], new Point((tileX * Constants.TILE_WIDTH), (tileY * Constants.TILE_HEIGHT)));
        }

        public static void DrawTileToSurfaceScaled(Surface destData, int sheet, int tileNum, int tileX, int tileY, double scaleX, double scaleY) {
            Surface scaledTile = Logic.Graphics.GraphicsManager.Tiles[sheet][tileNum].CreateScaledSurface(scaleX, scaleY);
            destData.Blit(scaledTile, new Point((int)((tileX * Constants.TILE_WIDTH) * scaleX), (int)((tileY * Constants.TILE_HEIGHT) * scaleY)));
            scaledTile.Close();
        }

        public static void DrawGroundTilesSeamless(RendererDestinationData destData, Map activeMap, bool mapAnim, int cameraX, int cameraX2, int cameraY, int cameraY2) {
            bool locYSub = (cameraY > 0 /*&& cameraY2 < activeMap.MaxY + 1*/ && ScreenRenderer.MapYOffset != 0);
            //if (cameraX > 0 /*&& cameraX2 != activeMap.MaxX + 1*/ && ScreenRenderer.MapXOffset != 0) {
            //    cameraX--;
            //    cameraX2++;
            //}
            int startX = cameraX;
            int startY = cameraY;
            if (ScreenRenderer.MapXOffset < 0) {
                startX--;
            }
            if (ScreenRenderer.MapYOffset < 0) {
                startY--;
            } else if (ScreenRenderer.MapYOffset > 0) {
                startY--;
            }
            //if (locYSub) {
            //    cameraY--;
            //    cameraY2++;
            //}
            for (int x = startX; x < cameraX2; x++) {
                for (int y = startY; y < cameraY2; y++) {
                    if (PlayerManager.MyPlayer.CurrentRoom != null && PlayerManager.MyPlayer.CurrentRoom.IsInRoom(x, y)) {

                        Tile currentTile = SeamlessWorldHelper.GetVisibleTile(x, y);

                        // Draw ground
                        if (!mapAnim || (currentTile.GroundAnim == 0 && mapAnim)) {
                            if (currentTile.Ground == 0) {
                                // Do Nothing
                            } else {
                                TileGraphic graphic = currentTile.GroundGraphic;
                                int tileNum = currentTile.Ground;
                                int sheet = currentTile.GroundSet;
                                if (graphic == null || !(tileNum == graphic.TileNum && sheet == graphic.TileSet)) {
                                    graphic = Logic.Graphics.GraphicsManager.Tiles[sheet].GetTileGraphic(tileNum);

                                    currentTile.GroundGraphic = graphic;
                                }
                                DrawTile(destData, graphic, x - cameraX, y - cameraY, true);
                            }
                        }

                        //Draw ground anim
                        if (mapAnim) {
                            if (currentTile.GroundAnim == 0) {
                                // Do Nothing
                            } else {
                                TileGraphic graphic = currentTile.GroundAnimGraphic;
                                int tileNum = currentTile.GroundAnim;
                                int sheet = currentTile.GroundAnimSet;
                                if (graphic == null || !(tileNum == graphic.TileNum && sheet == graphic.TileSet)) {
                                    graphic = Logic.Graphics.GraphicsManager.Tiles[sheet].GetTileGraphic(tileNum);

                                    currentTile.GroundAnimGraphic = graphic;
                                }

                                DrawTile(destData, graphic, x - cameraX, y - cameraY, false);

                            }
                        }

                        // Draw Mask
                        if (!mapAnim || (currentTile.Anim == 0 && mapAnim)) {
                            if (currentTile.Mask == 0 || (currentTile.Anim != 0 && mapAnim) || currentTile.DoorOpen) {
                                // Do Nothing
                            } else {
                                TileGraphic graphic = currentTile.MaskGraphic;
                                int tileNum = currentTile.Mask;
                                int sheet = currentTile.MaskSet;
                                if (graphic == null || !(tileNum == graphic.TileNum && sheet == graphic.TileSet)) {
                                    graphic = Logic.Graphics.GraphicsManager.Tiles[sheet].GetTileGraphic(tileNum);

                                    currentTile.MaskGraphic = graphic;
                                }

                                DrawTile(destData, graphic, x - cameraX, y - cameraY, false);
                            }
                        }

                        // Draw Mask Anim
                        if (mapAnim) {
                            if (currentTile.Anim == 0) {
                                // Do Nothing
                            } else {
                                TileGraphic graphic = currentTile.AnimGraphic;
                                int tileNum = currentTile.Anim;
                                int sheet = currentTile.AnimSet;
                                if (graphic == null || !(tileNum == graphic.TileNum && sheet == graphic.TileSet)) {
                                    graphic = Logic.Graphics.GraphicsManager.Tiles[sheet].GetTileGraphic(tileNum);

                                    currentTile.AnimGraphic = graphic;
                                }

                                DrawTile(destData, graphic, x - cameraX, y - cameraY, false);
                            }
                        }

                        // Draw Mask 2
                        if (!mapAnim || (currentTile.M2Anim == 0 && mapAnim)) {
                            if (currentTile.Mask2 == 0 || (currentTile.M2Anim != 0 && mapAnim)) {
                                // Do Nothing
                            } else {
                                TileGraphic graphic = currentTile.Mask2Graphic;
                                int tileNum = currentTile.Mask2;
                                int sheet = currentTile.Mask2Set;
                                if (graphic == null || !(tileNum == graphic.TileNum && sheet == graphic.TileSet)) {
                                    graphic = Logic.Graphics.GraphicsManager.Tiles[sheet].GetTileGraphic(tileNum);

                                    currentTile.Mask2Graphic = graphic;
                                }

                                DrawTile(destData, graphic, x - cameraX, y - cameraY, false);
                            }
                        }

                        // Draw Mask 2 Anim
                        if (mapAnim) {
                            if (currentTile.M2Anim == 0) {
                                // Do Nothing
                            } else {
                                TileGraphic graphic = currentTile.M2AnimGraphic;
                                int tileNum = currentTile.M2Anim;
                                int sheet = currentTile.M2AnimSet;
                                if (graphic == null || !(tileNum == graphic.TileNum && sheet == graphic.TileSet)) {
                                    graphic = Logic.Graphics.GraphicsManager.Tiles[sheet].GetTileGraphic(tileNum);

                                    currentTile.M2AnimGraphic = graphic;
                                }

                                DrawTile(destData, graphic, x - cameraX, y - cameraY, false);
                            }
                        }

                        DrawSpriteChangeTile(destData, activeMap, x, y);
                    }
                }
            }
            DrawMissionGoalTileSeamless(destData, activeMap, cameraX, cameraY);
        }

        public static void DrawGroundTiles(RendererDestinationData destData, Map activeMap, bool mapAnim, int cameraX, int cameraX2, int cameraY, int cameraY2) {
            bool locYSub = (cameraY > 0 /*&& cameraY2 < activeMap.MaxY + 1*/ && ScreenRenderer.MapYOffset != 0);
            if (cameraX > 0 /*&& cameraX2 != activeMap.MaxX + 1*/ && ScreenRenderer.MapXOffset != 0) {
                cameraX--;
                cameraX2++;
            }
            if (locYSub) {
                cameraY--;
                cameraY2++;
            }
            if (cameraX2 > activeMap.Tile.GetUpperBound(0) + 1) {
                cameraX2 = activeMap.Tile.GetUpperBound(0) + 1;
            }
            if (cameraY2 > activeMap.Tile.GetUpperBound(1) + 1) {
                cameraY2 = activeMap.Tile.GetUpperBound(1) + 1;
            }
            for (int x = cameraX; x < cameraX2; x++) {
                for (int y = cameraY; y < cameraY2; y++) {
                    if (PlayerManager.MyPlayer.CurrentRoom != null && PlayerManager.MyPlayer.CurrentRoom.IsInRoom(x, y)) {

                        // Draw ground
                        if (!mapAnim || (activeMap.Tile[x, y].GroundAnim == 0 && mapAnim)) {
                            if (activeMap.Tile[x, y].Ground == 0) {
                                // Do Nothing
                            } else {
                                DrawTile(destData, activeMap.Tile[x, y].GroundSet, activeMap.Tile[x, y].Ground, x, y);
                            }
                        }

                        //Draw ground anim
                        if (mapAnim) {
                            if (activeMap.Tile[x, y].GroundAnim == 0) {
                                // Do Nothing
                            } else {
                                DrawTile(destData, activeMap.Tile[x, y].GroundAnimSet, activeMap.Tile[x, y].GroundAnim, x, y);

                            }
                        }

                        // Draw Mask
                        if (!mapAnim || (activeMap.Tile[x, y].Anim == 0 && mapAnim)) {
                            if (activeMap.Tile[x, y].Mask == 0 || (activeMap.Tile[x, y].Anim != 0 && mapAnim) || activeMap.Tile[x, y].DoorOpen) {
                                // Do Nothing
                            } else {
                                DrawTile(destData, activeMap.Tile[x, y].MaskSet, activeMap.Tile[x, y].Mask, x, y);
                            }
                        }

                        // Draw Mask Anim
                        if (mapAnim) {
                            if (activeMap.Tile[x, y].Anim == 0) {
                                // Do Nothing
                            } else {
                                DrawTile(destData, activeMap.Tile[x, y].AnimSet, activeMap.Tile[x, y].Anim, x, y);
                            }
                        }

                        // Draw Mask 2
                        if (!mapAnim || (activeMap.Tile[x, y].M2Anim == 0 && mapAnim)) {
                            if (activeMap.Tile[x, y].Mask2 == 0 || (activeMap.Tile[x, y].M2Anim != 0 && mapAnim)) {
                                // Do Nothing
                            } else {
                                DrawTile(destData, activeMap.Tile[x, y].Mask2Set, activeMap.Tile[x, y].Mask2, x, y);
                            }
                        }

                        // Draw Mask 2 Anim
                        if (mapAnim) {
                            if (activeMap.Tile[x, y].M2Anim == 0) {
                                // Do Nothing
                            } else {
                                DrawTile(destData, activeMap.Tile[x, y].M2AnimSet, activeMap.Tile[x, y].M2Anim, x, y);
                            }
                        }

                        DrawMissionGoalTile(destData, activeMap);
                        DrawSpriteChangeTile(destData, activeMap, x, y);
                    }
                }
            }
        }

        public static void DrawFringeTilesSeamless(RendererDestinationData destData, Map activeMap, bool mapAnim, int cameraX, int cameraX2, int cameraY, int cameraY2) {
            bool locYSub = (cameraY > 0 /*&& cameraY2 != activeMap.MaxY + 1*/ && ScreenRenderer.MapYOffset != 0);
            //if (cameraX > 0 /*&& cameraX2 != activeMap.MaxX + 1*/ && ScreenRenderer.MapXOffset != 0) {
            //    cameraX--;
            //    cameraX2++;
            //}
            int startX = cameraX;
            int startY = cameraY;
            if (ScreenRenderer.MapXOffset < 0) {
                startX--;
            }
            if (ScreenRenderer.MapYOffset < 0) {
                startY--;
            }
            //if (locYSub) {
            //    cameraY--;
            //    cameraY2++;
            //}
            for (int x = startX; x < cameraX2; x++) {
                for (int y = startY; y < cameraY2; y++) {
                    if (PlayerManager.MyPlayer.CurrentRoom != null && PlayerManager.MyPlayer.CurrentRoom.IsInRoom(x, y)) {

                        Tile currentTile = SeamlessWorldHelper.GetVisibleTile(x, y);

                        // Draw Fringe
                        if (!mapAnim || (currentTile.FAnim == 0 && mapAnim)) {
                            if (currentTile.Fringe == 0 || (currentTile.FAnim != 0 && mapAnim)) {
                                // Do Nothing
                            } else {
                                TileGraphic graphic = currentTile.FringeGraphic;
                                int tileNum = currentTile.Fringe;
                                int sheet = currentTile.FringeSet;
                                if (graphic == null || !(tileNum == graphic.TileNum && sheet == graphic.TileSet)) {
                                    graphic = Logic.Graphics.GraphicsManager.Tiles[sheet].GetTileGraphic(tileNum);

                                    currentTile.FringeGraphic = graphic;
                                }

                                DrawTile(destData, graphic, x - cameraX, y - cameraY, false);
                            }
                        }

                        // Draw Fringe Anim
                        if (mapAnim) {
                            if (currentTile.FAnim == 0) {
                                // Do Nothing
                            } else {
                                TileGraphic graphic = currentTile.FAnimGraphic;
                                int tileNum = currentTile.FAnim;
                                int sheet = currentTile.FAnimSet;
                                if (graphic == null || !(tileNum == graphic.TileNum && sheet == graphic.TileSet)) {
                                    graphic = Logic.Graphics.GraphicsManager.Tiles[sheet].GetTileGraphic(tileNum);

                                    currentTile.FAnimGraphic = graphic;
                                }

                                DrawTile(destData, graphic, x - cameraX, y - cameraY, false);
                            }
                        }

                        // Draw Fringe 2
                        if (!mapAnim || (currentTile.F2Anim == 0 && mapAnim)) {
                            if (currentTile.Fringe2 == 0 || (currentTile.F2Anim != 0 && mapAnim)) {
                                // Do Nothing
                            } else {
                                TileGraphic graphic = currentTile.Fringe2Graphic;
                                int tileNum = currentTile.Fringe2;
                                int sheet = currentTile.Fringe2Set;
                                if (graphic == null || !(tileNum == graphic.TileNum && sheet == graphic.TileSet)) {
                                    graphic = Logic.Graphics.GraphicsManager.Tiles[sheet].GetTileGraphic(tileNum);

                                    currentTile.Fringe2Graphic = graphic;
                                }

                                DrawTile(destData, graphic, x - cameraX, y - cameraY, false);
                            }
                        }

                        // Draw Fringe 2 Anim
                        if (mapAnim) {
                            if (currentTile.F2Anim == 0) {
                                // Do Nothing
                            } else {
                                TileGraphic graphic = currentTile.F2AnimGraphic;
                                int tileNum = currentTile.F2Anim;
                                int sheet = currentTile.F2AnimSet;
                                if (graphic == null || !(tileNum == graphic.TileNum && sheet == graphic.TileSet)) {
                                    graphic = Logic.Graphics.GraphicsManager.Tiles[sheet].GetTileGraphic(tileNum);

                                    currentTile.F2AnimGraphic = graphic;
                                }

                                DrawTile(destData, graphic, x - cameraX, y - cameraY, false);
                            }
                        }
                    }
                }
            }
        }

        public static void DrawFringeTiles(RendererDestinationData destData, Map activeMap, bool mapAnim, int cameraX, int cameraX2, int cameraY, int cameraY2) {
            bool locYSub = (cameraY > 0 /*&& cameraY2 != activeMap.MaxY + 1*/ && ScreenRenderer.MapYOffset != 0);
            if (cameraX > 0 /*&& cameraX2 != activeMap.MaxX + 1*/ && ScreenRenderer.MapXOffset != 0) {
                cameraX--;
                cameraX2++;
            }
            if (locYSub) {
                cameraY--;
                cameraY2++;
            }
            if (cameraX2 > activeMap.Tile.GetUpperBound(0) + 1) {
                cameraX2 = activeMap.Tile.GetUpperBound(0) + 1;
            }
            if (cameraY2 > activeMap.Tile.GetUpperBound(1) + 1) {
                cameraY2 = activeMap.Tile.GetUpperBound(1) + 1;
            }
            for (int x = cameraX; x < cameraX2; x++) {
                for (int y = cameraY; y < cameraY2; y++) {
                    if (PlayerManager.MyPlayer.CurrentRoom != null && PlayerManager.MyPlayer.CurrentRoom.IsInRoom(x, y)) {
                        // Draw Fringe
                        if (!mapAnim || (activeMap.Tile[x, y].FAnim == 0 && mapAnim)) {
                            if (activeMap.Tile[x, y].Fringe == 0 || (activeMap.Tile[x, y].FAnim != 0 && mapAnim)) {
                                // Do Nothing
                            } else {
                                DrawTile(destData, activeMap.Tile[x, y].FringeSet, activeMap.Tile[x, y].Fringe, x, y);
                            }
                        }

                        // Draw Fringe Anim
                        if (mapAnim) {
                            if (activeMap.Tile[x, y].FAnim == 0) {
                                // Do Nothing
                            } else {
                                DrawTile(destData, activeMap.Tile[x, y].FAnimSet, activeMap.Tile[x, y].FAnim, x, y);
                            }
                        }

                        // Draw Fringe 2
                        if (!mapAnim || (activeMap.Tile[x, y].F2Anim == 0 && mapAnim)) {
                            if (activeMap.Tile[x, y].Fringe2 == 0 || (activeMap.Tile[x, y].F2Anim != 0 && mapAnim)) {
                                // Do Nothing
                            } else {
                                DrawTile(destData, activeMap.Tile[x, y].Fringe2Set, activeMap.Tile[x, y].Fringe2, x, y);
                            }
                        }

                        // Draw Fringe 2 Anim
                        if (mapAnim) {
                            if (activeMap.Tile[x, y].F2Anim == 0) {
                                // Do Nothing
                            } else {
                                DrawTile(destData, activeMap.Tile[x, y].F2AnimSet, activeMap.Tile[x, y].F2Anim, x, y);
                            }
                        }
                    }
                }
            }
        }

        public static void DrawTiles(RendererDestinationData destData, Map activeMap, bool mapAnim, int cameraX, int cameraX2, int cameraY, int cameraY2, bool displayAttributes, bool displayMapGrid) {
            int locX = 0;
            int locY = 0;
            for (int x = cameraX; x < cameraX2; x++) {
                for (int y = cameraY; y < cameraY2; y++) {
                    // Draw ground
                    if (!mapAnim || (activeMap.Tile[x, y].GroundAnim == 0 && mapAnim)) {
                        if (activeMap.Tile[x, y].Ground == 0) {
                            // Do Nothing
                        } else {
                            DrawTile(destData, activeMap.Tile[x, y].GroundSet, activeMap.Tile[x, y].Ground, locX, locY, false);
                        }
                    }

                    //Draw ground anim
                    if (mapAnim) {
                        if (activeMap.Tile[x, y].GroundAnim == 0) {
                            // Do Nothing
                        } else {
                            DrawTile(destData, activeMap.Tile[x, y].GroundAnimSet, activeMap.Tile[x, y].GroundAnim, locX, locY, false);

                        }
                    }

                    // Draw Mask
                    if (!mapAnim || (activeMap.Tile[x, y].Anim == 0 && mapAnim)) {
                        if (activeMap.Tile[x, y].Mask == 0) {
                            // Do Nothing
                        } else {
                            DrawTile(destData, activeMap.Tile[x, y].MaskSet, activeMap.Tile[x, y].Mask, locX, locY, false);
                        }
                    }

                    // Draw Mask Anim
                    if (mapAnim) {
                        if (activeMap.Tile[x, y].Anim == 0) {
                            // Do Nothing
                        } else {
                            DrawTile(destData, activeMap.Tile[x, y].AnimSet, activeMap.Tile[x, y].Anim, locX, locY, false);

                        }
                    }

                    // Draw Mask 2
                    if (!mapAnim || (activeMap.Tile[x, y].M2Anim == 0 && mapAnim)) {
                        if (activeMap.Tile[x, y].Mask2 == 0) {
                            // Do Nothing
                        } else {
                            DrawTile(destData, activeMap.Tile[x, y].Mask2Set, activeMap.Tile[x, y].Mask2, locX, locY, false);
                        }
                    }

                    // Draw Mask 2 Anim
                    if (mapAnim) {
                        if (activeMap.Tile[x, y].M2Anim == 0) {
                            // Do Nothing
                        } else {
                            DrawTile(destData, activeMap.Tile[x, y].M2AnimSet, activeMap.Tile[x, y].M2Anim, locX, locY, false);
                        }
                    }

                    // Draw Fringe
                    if (!mapAnim || (activeMap.Tile[x, y].FAnim == 0 && mapAnim)) {
                        if (activeMap.Tile[x, y].Fringe == 0) {
                            // Do Nothing
                        } else {
                            DrawTile(destData, activeMap.Tile[x, y].FringeSet, activeMap.Tile[x, y].Fringe, locX, locY, false);
                        }
                    }

                    // Draw Fringe Anim
                    if (mapAnim) {
                        if (activeMap.Tile[x, y].FAnim == 0) {
                            // Do Nothing
                        } else {
                            DrawTile(destData, activeMap.Tile[x, y].FAnimSet, activeMap.Tile[x, y].FAnim, locX, locY, false);
                        }
                    }

                    // Draw Fringe 2
                    if (!mapAnim || (activeMap.Tile[x, y].F2Anim == 0 && mapAnim)) {
                        if (activeMap.Tile[x, y].Fringe2 == 0) {
                            // Do Nothing
                        } else {
                            DrawTile(destData, activeMap.Tile[x, y].Fringe2Set, activeMap.Tile[x, y].Fringe2, locX, locY, false);
                        }
                    }

                    // Draw Fringe 2 Anim
                    if (mapAnim) {
                        if (activeMap.Tile[x, y].F2Anim == 0) {
                            // Do Nothing
                        } else {
                            DrawTile(destData, activeMap.Tile[x, y].F2AnimSet, activeMap.Tile[x, y].F2Anim, locX, locY, false);
                        }
                    }

                    if (displayAttributes) {
                        // Draw Attributes
                        switch (activeMap.Tile[x, y].Type) {
                            case Enums.TileType.Blocked:
                                TextRenderer.DrawText(destData, "B", Color.Red, locX * Constants.TILE_WIDTH + 8, locY * Constants.TILE_HEIGHT + 8);
                                break;
                            case Enums.TileType.NPCAvoid:
                                TextRenderer.DrawText(destData, "N", Color.Black, locX * Constants.TILE_WIDTH + 8, locY * Constants.TILE_HEIGHT + 8);
                                break;
                            case Enums.TileType.Heal:
                                TextRenderer.DrawText(destData, "H", Color.LightGreen, locX * Constants.TILE_WIDTH + 8, locY * Constants.TILE_HEIGHT + 8);
                                break;
                            case Enums.TileType.Kill:
                                TextRenderer.DrawText(destData, "K", Color.Red, locX * Constants.TILE_WIDTH + 8, locY * Constants.TILE_HEIGHT + 8);
                                break;
                            case Enums.TileType.Sign:
                                TextRenderer.DrawText(destData, "SI", Color.Yellow, locX * Constants.TILE_WIDTH + 8, locY * Constants.TILE_HEIGHT + 8);
                                break;
                            case Enums.TileType.Notice:
                                TextRenderer.DrawText(destData, "N", Color.LightGreen, locX * Constants.TILE_WIDTH + 8, locY * Constants.TILE_HEIGHT + 8);
                                break;
                            case Enums.TileType.Guild:
                                TextRenderer.DrawText(destData, "G", Color.Green, locX * Constants.TILE_WIDTH + 8, locY * Constants.TILE_HEIGHT + 8);
                                break;
                            case Enums.TileType.LevelBlock:
                                TextRenderer.DrawText(destData, "LB", Color.Red, locX * Constants.TILE_WIDTH + 8, locY * Constants.TILE_HEIGHT + 8);
                                break;
                            case Enums.TileType.Ambiguous:
                                TextRenderer.DrawText(destData, "?", Color.Green, locX * Constants.TILE_WIDTH + 8, locY * Constants.TILE_HEIGHT + 8);
                                break;
                            case Enums.TileType.Slippery:
                                TextRenderer.DrawText(destData, "S", Color.Cyan, locX * Constants.TILE_WIDTH + 8, locY * Constants.TILE_HEIGHT + 8);
                                break;
                            case Enums.TileType.Slow:
                                TextRenderer.DrawText(destData, "SLO", Color.Green, locX * Constants.TILE_WIDTH + 8, locY * Constants.TILE_HEIGHT + 8);
                                break;
                        }
                    }

                    if (displayMapGrid) {
                        DrawTile(destData, 10, 0, locX, locY, false);
                    }
                    locY++;
                }
                locY = 0;
                locX++;
            }
        }

        public static void DrawSpriteChangeTile(RendererDestinationData destData, Map activeMap, int x, int y) {
            if (x >= 0 && x <= activeMap.MaxX && y >= 0 && y <= activeMap.MaxY) {
                if (activeMap.Tile[x, y].Type == Enums.TileType.SpriteChange) {

                    SpriteSheet spriteToBlit = GraphicsManager.GetSpriteSheet(activeMap.Tile[x, y].Data1);

                    //rec.Height = spriteToBlit.Height;
                    //rec.Width = spriteToBlit.Width / 14;

                    //rec.X = rec.Width * 3;
                    //rec.Y = 0;

                    Point loc = new Point(ScreenRenderer.ToTileX(x), ScreenRenderer.ToTileY(y));
                    loc.X -= (spriteToBlit.FrameData.FrameWidth / 2 - 16);
                    loc.Y -= (spriteToBlit.FrameData.FrameHeight - 32);
                    destData.Blit(spriteToBlit.GetSheet(FrameType.Idle, Enums.Direction.Down), loc, spriteToBlit.GetFrameBounds(FrameType.Walk, Enums.Direction.Down, 0));




                }
            }
        }

        public static void DrawMapName(RendererDestinationData destData, Map map) {
            Size textSize = TextRenderer.SizeText(map.Name);
            int textPosX = DrawingSupport.GetCenterX(destData.Size.Width, textSize.Width);
            //int mapNameLoc = Convert.ToInt32((20.5) * Constants.TILE_WIDTH / 2) - ((Convert.ToInt32(map.Name.Length) / 2) * 8);

            switch (map.Moral) {
                case Enums.MapMoral.None: {
                        TextRenderer.DrawText(destData, map.Name, Color.Black, Color.Black, new Point(textPosX, 25));
                    }
                    break;
                case Enums.MapMoral.House: {
                        TextRenderer.DrawText(destData, map.Name, Color.Yellow, Color.Black, new Point(textPosX, 25));
                    }
                    break;
                case Enums.MapMoral.Safe: {
                        TextRenderer.DrawText(destData, map.Name, Color.White, Color.Black, new Point(textPosX, 25));
                    }
                    break;
                case Enums.MapMoral.NoPenalty: {
                        TextRenderer.DrawText(destData, map.Name, Color.Red, Color.Black, new Point(textPosX, 25));
                    }
                    break;
            }
        }

        public static void DrawMapGrid(RendererDestinationData destData, Map activeMap, int cameraX, int cameraX2, int cameraY, int cameraY2) {
            int startX = cameraX;
            int startY = cameraY;
            if (ScreenRenderer.MapXOffset < 0) {
                startX--;
            }
            if (ScreenRenderer.MapYOffset < 0) {
                startY--;
            } else if (ScreenRenderer.MapYOffset > 0) {
                startY--;
            }
            //if (locYSub) {
            //    cameraY--;
            //    cameraY2++;
            //}
            for (int x = startX; x < cameraX2; x++) {
                for (int y = startY; y < cameraY2; y++) {
                    if (x >= 0 && x <= activeMap.MaxX && y >= 0 && y <= activeMap.MaxY) {
                        DrawTile(destData, 10, 0, x - cameraX, y - cameraY, false);

                        if (x == 0) {
                            SdlDotNet.Graphics.Primitives.Line line = new SdlDotNet.Graphics.Primitives.Line((short)(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset), (short)(((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset), (short)(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset), (short)((((y - cameraY) * Constants.TILE_HEIGHT) + Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                            destData.Surface.Draw(line, Color.Red);
                        } else if (x == activeMap.MaxX) {
                            SdlDotNet.Graphics.Primitives.Line line = new SdlDotNet.Graphics.Primitives.Line((short)((((x - cameraX) * Constants.TILE_WIDTH) + Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset), (short)(((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset), (short)((((x - cameraX) * Constants.TILE_WIDTH) + Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset), (short)((((y - cameraY) * Constants.TILE_HEIGHT) + Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                            destData.Surface.Draw(line, Color.Red);
                        }

                        if (y == 0) {
                            SdlDotNet.Graphics.Primitives.Line line = new SdlDotNet.Graphics.Primitives.Line((short)(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset), (short)(((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset), (short)((((x - cameraX) * Constants.TILE_WIDTH) + Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset), (short)(((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                            destData.Surface.Draw(line, Color.Red);
                        } else if (y == activeMap.MaxY) {
                            SdlDotNet.Graphics.Primitives.Line line = new SdlDotNet.Graphics.Primitives.Line((short)(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset), (short)((((y - cameraY) * Constants.TILE_HEIGHT) + Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset), (short)((((x - cameraX) * Constants.TILE_WIDTH) + Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset), (short)((((y - cameraY) * Constants.TILE_HEIGHT) + Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                            destData.Surface.Draw(line, Color.Red);
                        }
                    }
                }
            }
        }

        public static void DrawMissionGoalTileSeamless(RendererDestinationData destData, Map activeMap, int cameraX, int cameraY) {
            MyPlayer player = Logic.Players.PlayerManager.MyPlayer;
            if (player != null) {
                foreach (Missions.MissionGoal goal in player.MapGoals) {
                    if (goal.GoalX >= 0 && goal.GoalX <= activeMap.MaxX && goal.GoalY >= 0 && goal.GoalY <= activeMap.MaxY) {
                        DrawTile(destData, 10, 20, goal.GoalX - cameraX, goal.GoalY - cameraY);

                        int species = PlayerManager.MyPlayer.JobList.Jobs[goal.JobListSlot].ClientSpecies;
                        int form = PlayerManager.MyPlayer.JobList.Jobs[goal.JobListSlot].ClientForm;
                        if (PlayerManager.MyPlayer.JobList.Jobs[goal.JobListSlot].MissionType == Enums.MissionType.Escort) {
                            species = PlayerManager.MyPlayer.JobList.Jobs[goal.JobListSlot].Data1;
                            form = PlayerManager.MyPlayer.JobList.Jobs[goal.JobListSlot].Data2;
                        }

                        SpriteSheet spriteToBlit = GraphicsManager.GetSpriteSheet(species);

                        //rec.Height = spriteToBlit.Height;
                        //rec.Width = spriteToBlit.Width / 14;

                        //rec.X = rec.Width * 3;
                        //rec.Y = 0;

                        Point loc = new Point(ScreenRenderer.ToTileX(goal.GoalX), ScreenRenderer.ToTileY(goal.GoalY));
                        loc.X -= (spriteToBlit.FrameData.FrameWidth / 2 - 16);
                        loc.Y -= (spriteToBlit.FrameData.FrameHeight - 32);
                        destData.Blit(spriteToBlit.GetSheet(FrameType.Idle, Enums.Direction.Down), loc, spriteToBlit.GetFrameBounds(FrameType.Walk, Enums.Direction.Down, 0));

                    }
                }
            }
        }

        public static void DrawMissionGoalTile(RendererDestinationData destData, Map activeMap) {
            MyPlayer player = Logic.Players.PlayerManager.MyPlayer;
            if (player != null) {
                foreach (Missions.MissionGoal goal in player.MapGoals) {
                    DrawTile(destData, 10, 20, goal.GoalX, goal.GoalY);

                    int species = PlayerManager.MyPlayer.JobList.Jobs[goal.JobListSlot].ClientSpecies;
                    int form = PlayerManager.MyPlayer.JobList.Jobs[goal.JobListSlot].ClientForm;
                    if (PlayerManager.MyPlayer.JobList.Jobs[goal.JobListSlot].MissionType == Enums.MissionType.Escort) {
                        species = PlayerManager.MyPlayer.JobList.Jobs[goal.JobListSlot].Data1;
                        form = PlayerManager.MyPlayer.JobList.Jobs[goal.JobListSlot].Data2;
                    }

                    SpriteSheet spriteToBlit = GraphicsManager.GetSpriteSheet(species);

                    //rec.Height = spriteToBlit.Height;
                    //rec.Width = spriteToBlit.Width / 14;

                    //rec.X = rec.Width * 3;
                    //rec.Y = 0;

                    Point loc = new Point(ScreenRenderer.ToTileX(goal.GoalX), ScreenRenderer.ToTileY(goal.GoalY));
                    loc.X -= (spriteToBlit.FrameData.FrameWidth / 2 - 16);
                    loc.Y -= (spriteToBlit.FrameData.FrameHeight - 32);
                    destData.Blit(spriteToBlit.GetSheet(FrameType.Idle, Enums.Direction.Down), loc, spriteToBlit.GetFrameBounds(FrameType.Walk, Enums.Direction.Down, 0));

                }
            }
        }

        public static void DrawAttributes(RendererDestinationData destData, Map activeMap, int cameraX, int cameraX2, int cameraY, int cameraY2) {
            //int (x - cameraX)  = 0;
            //int (y - cameraY)  = 0;
            //bool locYSub = (cameraY > 0 /*&& cameraY2 != activeMap.MaxY + 1*/ && ScreenRenderer.MapYOffset != 0);
            //if (cameraX > 0 /*&& cameraX2 != activeMap.MaxX + 1*/ && ScreenRenderer.MapXOffset != 0) {
            //    (x - cameraX)  = -1;
            //    cameraX--;
            //    cameraX2++;
            //}
            //if (locYSub) {
            //    (y - cameraY)  = -1;
            //    cameraY--;
            //    cameraY2++;
            //}
            //if (cameraX2 > activeMap.Tile.GetUpperBound(0) + 1) {
            //    cameraX2 = activeMap.Tile.GetUpperBound(0) + 1;
            //}
            //if (cameraY2 > activeMap.Tile.GetUpperBound(1) + 1) {
            //    cameraY2 = activeMap.Tile.GetUpperBound(1) + 1;
            //}
            //for (int x = cameraX; x < cameraX2; x++) {
            //    for (int y = cameraY; y < cameraY2; y++) {
            int startX = cameraX;
            int startY = cameraY;
            if (ScreenRenderer.MapXOffset < 0) {
                startX--;
            }
            if (ScreenRenderer.MapYOffset < 0) {
                startY--;
            } else if (ScreenRenderer.MapYOffset > 0) {
                startY--;
            }
            //if (locYSub) {
            //    cameraY--;
            //    cameraY2++;
            //}
            for (int x = startX; x < cameraX2; x++) {
                for (int y = startY; y < cameraY2; y++) {
                    // Draw Attributes
                    switch (SeamlessWorldHelper.GetVisibleTile(x, y).Type) {
                        case Enums.TileType.Blocked:
                            TextRenderer.DrawText(destData, "B", Color.Red, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.NPCAvoid:
                            TextRenderer.DrawText(destData, "N", Color.Black, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Heal:
                            TextRenderer.DrawText(destData, "H", Color.LightGreen, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Kill:
                            TextRenderer.DrawText(destData, "K", Color.Red, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Sign:
                            TextRenderer.DrawText(destData, "SI", Color.Yellow, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Notice:
                            TextRenderer.DrawText(destData, "N", Color.LightGreen, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Guild:
                            TextRenderer.DrawText(destData, "G", Color.Green, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.LevelBlock:
                            TextRenderer.DrawText(destData, "LB", Color.Red, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Warp:
                            TextRenderer.DrawText(destData, "W", Color.LightBlue, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Item:
                            TextRenderer.DrawText(destData, "I", Color.Black, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Key:
                            TextRenderer.DrawText(destData, "K", Color.Black, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.KeyOpen:
                            TextRenderer.DrawText(destData, "O", Color.Black, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Shop:
                            TextRenderer.DrawText(destData, "SH", Color.Yellow, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Arena:
                            TextRenderer.DrawText(destData, "A", Color.LightGreen, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Sound:
                            TextRenderer.DrawText(destData, "PS", Color.Yellow, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.SpriteChange:
                            TextRenderer.DrawText(destData, "SC", Color.Gray, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.ScriptedSign:
                            TextRenderer.DrawText(destData, "SS", Color.Yellow, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Door:
                            TextRenderer.DrawText(destData, "D", Color.Black, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Chest:
                            TextRenderer.DrawText(destData, "C", Color.Brown, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.LinkShop:
                            TextRenderer.DrawText(destData, "L", Color.Black, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Scripted:
                            TextRenderer.DrawText(destData, "SC", Color.Yellow, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.House:
                            TextRenderer.DrawText(destData, "PH", Color.Yellow, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Bank:
                            TextRenderer.DrawText(destData, "BANK", Color.LightPink, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.SpriteBlock:
                            TextRenderer.DrawText(destData, "SB", Color.Black, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.MobileBlock:
                            TextRenderer.DrawText(destData, "MB", Color.Gray, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Assembly:
                            TextRenderer.DrawText(destData, "ASSMBLY", Color.Black, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Evolution:
                            TextRenderer.DrawText(destData, "EVO", Color.Yellow, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Story:
                            TextRenderer.DrawText(destData, "STRY", Color.Yellow, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.MissionBoard:
                            TextRenderer.DrawText(destData, "MSN", Color.Purple, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Ambiguous:
                            TextRenderer.DrawText(destData, "?", Color.Green, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Slippery:
                            TextRenderer.DrawText(destData, "S", Color.Cyan, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.Slow:
                            TextRenderer.DrawText(destData, "SLO", Color.Green, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                        case Enums.TileType.DropShop:
                            TextRenderer.DrawText(destData, "DS", Color.Black, ((x - cameraX) * Constants.TILE_WIDTH + 8) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT + 8) - ScreenRenderer.MapYOffset);
                            break;
                    }
                }
            }
        }

        public static void DrawDungeonValues(RendererDestinationData destData, Map activeMap, int cameraX, int cameraX2, int cameraY, int cameraY2) {
            //int (x - cameraX)  = 0;
            //int (y - cameraY)  = 0;
            //bool locYSub = (cameraY > 0 /*&& cameraY2 != activeMap.MaxY + 1*/ && ScreenRenderer.MapYOffset != 0);
            //if (cameraX > 0 /*&& cameraX2 != activeMap.MaxX + 1*/ && ScreenRenderer.MapXOffset != 0) {
            //    (x - cameraX)  = -1;
            //    cameraX--;
            //    cameraX2++;
            //}
            //if (locYSub) {
            //    (y - cameraY)  = -1;
            //    cameraY--;
            //    cameraY2++;
            //}
            //if (cameraX2 > activeMap.Tile.GetUpperBound(0) + 1) {
            //    cameraX2 = activeMap.Tile.GetUpperBound(0) + 1;
            //}
            //if (cameraY2 > activeMap.Tile.GetUpperBound(1) + 1) {
            //    cameraY2 = activeMap.Tile.GetUpperBound(1) + 1;
            //}
            //for (int x = cameraX; x < cameraX2; x++) {
            //    for (int y = cameraY; y < cameraY2; y++) {
            int startX = cameraX;
            int startY = cameraY;
            if (ScreenRenderer.MapXOffset < 0) {
                startX--;
            }
            if (ScreenRenderer.MapYOffset < 0) {
                startY--;
            } else if (ScreenRenderer.MapYOffset > 0) {
                startY--;
            }
            //if (locYSub) {
            //    cameraY--;
            //    cameraY2++;
            //}
            for (int x = startX; x < cameraX2; x++) {
                for (int y = startY; y < cameraY2; y++) {
                    // Draw Attributes
                    int tileNum = SeamlessWorldHelper.GetVisibleTile(x, y).RDungeonMapValue;
                    if (tileNum == 0) {
                        TextRenderer.DrawText(destData, FontManager.GameFontSmall, SeamlessWorldHelper.GetVisibleTile(x, y).RDungeonMapValue.ToString(), Color.WhiteSmoke, new Point(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                    } else if (tileNum == 1) {
                        TextRenderer.DrawText(destData, FontManager.GameFontSmall, SeamlessWorldHelper.GetVisibleTile(x, y).RDungeonMapValue.ToString(), Color.SaddleBrown, new Point(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                    } else if (tileNum == 2) {
                        TextRenderer.DrawText(destData, FontManager.GameFontSmall, SeamlessWorldHelper.GetVisibleTile(x, y).RDungeonMapValue.ToString(), Color.Aqua, new Point(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                    } else if (tileNum == 3) {
                        TextRenderer.DrawText(destData, FontManager.GameFontSmall, SeamlessWorldHelper.GetVisibleTile(x, y).RDungeonMapValue.ToString(), Color.ForestGreen, new Point(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                    } else if (tileNum == 4) {
                        TextRenderer.DrawText(destData, FontManager.GameFontSmall, SeamlessWorldHelper.GetVisibleTile(x, y).RDungeonMapValue.ToString(), Color.Purple, new Point(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                    } else if (tileNum == 5) {
                        TextRenderer.DrawText(destData, FontManager.GameFontSmall, SeamlessWorldHelper.GetVisibleTile(x, y).RDungeonMapValue.ToString(), Color.Red, new Point(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                    } else if (tileNum == 6) {
                        TextRenderer.DrawText(destData, FontManager.GameFontSmall, SeamlessWorldHelper.GetVisibleTile(x, y).RDungeonMapValue.ToString(), Color.GreenYellow, new Point(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                    } else if (tileNum == 7) {
                        TextRenderer.DrawText(destData, FontManager.GameFontSmall, SeamlessWorldHelper.GetVisibleTile(x, y).RDungeonMapValue.ToString(), Color.DarkRed, new Point(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                    } else if (tileNum == 8) {
                        TextRenderer.DrawText(destData, FontManager.GameFontSmall, SeamlessWorldHelper.GetVisibleTile(x, y).RDungeonMapValue.ToString(), Color.Orange, new Point(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                    } else if (tileNum == 9) {
                        TextRenderer.DrawText(destData, FontManager.GameFontSmall, SeamlessWorldHelper.GetVisibleTile(x, y).RDungeonMapValue.ToString(), Color.Gold, new Point(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                    } else {
                        TextRenderer.DrawText(destData, FontManager.GameFontSmall, SeamlessWorldHelper.GetVisibleTile(x, y).RDungeonMapValue.ToString(), Color.Black, new Point(((x - cameraX) * Constants.TILE_WIDTH) - ScreenRenderer.MapXOffset, ((y - cameraY) * Constants.TILE_HEIGHT) - ScreenRenderer.MapYOffset));
                    }
                }
            }
        }
    }
}
