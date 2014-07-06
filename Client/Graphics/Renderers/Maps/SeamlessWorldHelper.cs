using System;
using System.Collections.Generic;
using System.Text;

using Client.Logic.Maps;

namespace Client.Logic.Graphics.Renderers.Maps
{
    class SeamlessWorldHelper
    {
        public static void ConvertCoordinatesToBorderless(Map activeMap, Enums.MapID targetMapID, ref int x, ref int y) {
            switch (targetMapID) {
                case Enums.MapID.Right: {
                        x += activeMap.MaxX + 1;
                    }
                    break;
                case Enums.MapID.Left: {
                        x -= MapHelper.Maps[targetMapID].MaxX + 1;
                    }
                    break;
                case Enums.MapID.Down: {
                        y += activeMap.MaxY + 1;
                    }
                    break;
                case Enums.MapID.Up: {
                        y -= MapHelper.Maps[targetMapID].MaxY + 1;
                    }
                    break;
                case Enums.MapID.TopLeft: {
                        y -= MapHelper.Maps[targetMapID].MaxY + 1;
                        x -= MapHelper.Maps[targetMapID].MaxX + 1;
                    }
                    break;
                case Enums.MapID.TopRight: {
                        y -= MapHelper.Maps[targetMapID].MaxY + 1;
                        x += activeMap.MaxX + 1;
                    }
                    break;
                case Enums.MapID.BottomLeft: {
                        y += activeMap.MaxY + 1;
                        x -= MapHelper.Maps[targetMapID].MaxX + 1;
                    }
                    break;
                case Enums.MapID.BottomRight: {
                        y += activeMap.MaxY + 1;
                        x += activeMap.MaxX + 1;
                    }
                    break;
            }
        }

        public static Enums.MapID GetVisibleData(ref int x, ref int y) {
            Enums.MapID tileOwnerMap = Enums.MapID.Active;
            Map activeMap = MapHelper.Maps[Enums.MapID.Active];
            if (x > activeMap.MaxX) {
                if (IsMapSeamless(Enums.MapID.Right)) {
                    // The camera x value is greater than the active map bounds
                    // Tile is on the map to the right of the active map
                    x -= activeMap.MaxX + 1;
                    tileOwnerMap = Enums.MapID.Right;
                } else {
                    x = -1;
                    y = -1;
                }
            }
            if (x < 0) {
                if (IsMapSeamless(Enums.MapID.Left)) {
                    // The camera x value is smaller than the active map bounds
                    // Tile is on the map to the left of the active map
                    x += MapHelper.Maps[Enums.MapID.Left].MaxX + 1;
                    tileOwnerMap = Enums.MapID.Left;
                } else {
                    x = -1;
                    y = -1;
                }
            }
            if (y < 0) {
                if (IsMapSeamless(Enums.MapID.Up)) {
                    bool changed = false;
                    if (tileOwnerMap == Enums.MapID.Left) {
                        tileOwnerMap = Enums.MapID.TopLeft;
                        changed = true;
                    }
                    if (tileOwnerMap == Enums.MapID.Right) {
                        tileOwnerMap = Enums.MapID.TopRight;
                        changed = true;
                    }

                    if (!changed) {
                        tileOwnerMap = Enums.MapID.Up;
                    }
                    y += MapHelper.Maps[Enums.MapID.Up].MaxY + 1;
                } else {
                    x = -1;
                    y = -1;
                }
            }

            if (y > activeMap.MaxY) {
                if (IsMapSeamless(Enums.MapID.Down)) {
                    bool changed = false;
                    if (tileOwnerMap == Enums.MapID.Left) {
                        tileOwnerMap = Enums.MapID.BottomLeft;
                        changed = true;
                    }
                    if (tileOwnerMap == Enums.MapID.Right) {
                        tileOwnerMap = Enums.MapID.BottomRight;
                        changed = true;
                    }

                    if (!changed) {
                        tileOwnerMap = Enums.MapID.Down;
                    }
                    y -= activeMap.MaxY + 1;
                } else {
                    x = -1;
                    y = -1;
                }
            }

            return tileOwnerMap;
        }

        public static Tile GetVisibleTile(int x, int y) {
            Enums.MapID tileOwnerMap = Enums.MapID.Active;
            Map activeMap = MapHelper.Maps[Enums.MapID.Active];
            if (x > activeMap.MaxX) {
                if (IsMapSeamless(Enums.MapID.Right)) {
                    // The camera x value is greater than the active map bounds
                    // Tile is on the map to the right of the active map
                    x -= activeMap.MaxX + 1;
                    tileOwnerMap = Enums.MapID.Right;
                } else {
                    return new Tile();
                }
            }
            if (x < 0) {
                if (IsMapSeamless(Enums.MapID.Left)) {
                    // The camera x value is smaller than the active map bounds
                    // Tile is on the map to the left of the active map
                    x += MapHelper.Maps[Enums.MapID.Left].MaxX + 1;
                    tileOwnerMap = Enums.MapID.Left;
                } else {
                    return new Tile();
                }
            }
            if (y < 0) {
                if (IsMapSeamless(Enums.MapID.Up)) {
                    bool changed = false;
                    if (tileOwnerMap == Enums.MapID.Left) {
                        tileOwnerMap = Enums.MapID.TopLeft;
                        changed = true;
                    }
                    if (tileOwnerMap == Enums.MapID.Right) {
                        tileOwnerMap = Enums.MapID.TopRight;
                        changed = true;
                    }

                    if (!changed) {
                        tileOwnerMap = Enums.MapID.Up;
                    }
                    y += MapHelper.Maps[Enums.MapID.Up].MaxY + 1;
                } else {
                    return new Tile();
                }
            }

            if (y > activeMap.MaxY) {
                if (IsMapSeamless(Enums.MapID.Down)) {
                    bool changed = false;
                    if (tileOwnerMap == Enums.MapID.Left) {
                        tileOwnerMap = Enums.MapID.BottomLeft;
                        changed = true;
                    }
                    if (tileOwnerMap == Enums.MapID.Right) {
                        tileOwnerMap = Enums.MapID.BottomRight;
                        changed = true;
                    }

                    if (!changed) {
                        tileOwnerMap = Enums.MapID.Down;
                    }
                    y -= activeMap.MaxY + 1;
                } else {
                    return new Tile();
                }
            }

            Map map = MapHelper.Maps[tileOwnerMap];
            if (map != null) {
                return MapHelper.Maps[tileOwnerMap].Tile[x, y];
            } else {
                return new Tile();
            }
        }

        public static bool IsMapSeamless(Enums.MapID direction) {
            Map map = MapHelper.Maps[direction];
            if (map != null && map.Loaded) {
                return true;
            }

            return false;
        }

        //public static bool IsInSight(Map map, int x, int y, string targetMapID, int targetX, int targetY, int leftDistance, int rightDistance, int topDistance, int bottomDistance) {
        //    int leftX = x - leftDistance;
        //    int rightX = x + rightDistance;

        //    int topY = y - topDistance;
        //    int bottomY = y + bottomDistance;

        //    Enums.MapID targetMapDirection = Enums.MapID.Active;
        //    for (int i = 0; i < 9; i++) {
        //        Map targetMap = MapHelper.Maps[(Enums.MapID)i];
        //        if (targetMap != null && targetMap.Loaded && targetMap.MapID == targetMapID) {
        //            targetMapDirection = (Enums.MapID)i;
        //            break;
        //        }
        //    }

        //    if (map.MapID == targetMap.MapID || !IsMapSeamless(Enums.MapID.Active) || !IsMapSeamless(targetMap, Enums.MapID.Active)) {
        //        return (targetX >= leftX && targetX <= rightX &&
        //            targetY >= topY && targetY <= bottomY);
        //    }

        //    int newLeftX = -1;
        //    int newRightX = -1;
        //    int newTopY = -1;
        //    int newBottomY = -1;

        //    Enums.MapID mapY = Enums.MapID.Active;
        //    Enums.MapID mapX = Enums.MapID.Active;
        //    Enums.MapID targetMapID = Enums.MapID.Active;

        //    // Target is on a different map
        //    if (leftX < 0) {
        //        // Test the left map
        //        IMap leftMap = MapManager.RetrieveBorderingMap(map, Enums.MapID.Left, true);
        //        if (leftMap != null) {
        //            newLeftX = leftMap.MaxX - System.Math.Abs(leftX);
        //            newRightX = leftMap.MaxX;

        //            mapX = Enums.MapID.Left;
        //        } else {
        //            newLeftX = 0;
        //            newRightX = rightX;
        //        }
        //    }

        //    if (rightX > map.MaxX) {
        //        // Test the right map
        //        IMap rightMap = MapManager.RetrieveBorderingMap(map, Enums.MapID.Right, true);
        //        if (rightMap != null) {
        //            newLeftX = 0;
        //            newRightX = (rightX - (map.MaxX - x));

        //            mapX = Enums.MapID.Right;
        //        } else {
        //            newLeftX = leftX;
        //            newRightX = map.MaxX;
        //        }
        //    }

        //    if (topY < 0) {
        //        // Test the top map
        //        IMap topMap = MapManager.RetrieveBorderingMap(map, Enums.MapID.Up, true);
        //        if (topMap != null) {
        //            newTopY = topMap.MaxY - System.Math.Abs(topY);
        //            newBottomY = topMap.MaxY;

        //            mapY = Enums.MapID.Up;
        //        } else {
        //            newTopY = 0;
        //            newBottomY = bottomY;
        //        }
        //    }

        //    if (bottomY > map.MaxY) {
        //        // Test the bottom map
        //        IMap bottomMap = MapManager.RetrieveBorderingMap(map, Enums.MapID.Down, true);
        //        if (bottomMap != null) {
        //            newTopY = 0;
        //            newBottomY = (bottomY - (map.MaxY - y));

        //            mapY = Enums.MapID.Down;
        //        } else {
        //            newTopY = topY;
        //            newBottomY = map.MaxY;
        //        }
        //    }

        //    if (mapY == Enums.MapID.Up) {
        //        if (mapX == Enums.MapID.Left) {
        //            targetMapID = Enums.MapID.TopLeft;
        //        } else if (mapX == Enums.MapID.Right) {
        //            targetMapID = Enums.MapID.TopRight;
        //        }
        //    } else if (mapY == Enums.MapID.Down) {
        //        if (mapX == Enums.MapID.Left) {
        //            targetMapID = Enums.MapID.BottomLeft;
        //        } else if (mapX == Enums.MapID.Right) {
        //            targetMapID = Enums.MapID.BottomRight;
        //        }
        //    }

        //    IMap testMap;
        //    if (targetMapID != Enums.MapID.Active) {
        //        testMap = MapManager.RetrieveBorderingMap(map, targetMapID, true);
        //    } else {
        //        testMap = map;
        //    }

        //    if (testMap.MapID == targetMap.MapID) {
        //        // The test map is the target map! Test for visibility on the target map
        //        return (targetX >= newLeftX && targetX <= newRightX &&
        //            targetY >= newTopY && targetY <= newBottomY);
        //    } else {
        //        // The test map is not the target map...
        //        return false;
        //    }
        //}
    }
}
