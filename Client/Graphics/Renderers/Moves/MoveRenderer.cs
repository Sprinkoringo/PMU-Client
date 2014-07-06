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


namespace Client.Logic.Graphics.Renderers.Moves
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using SdlDotNet.Graphics;

    class MoveRenderer
    {
        #region Fields

        static List<Moves.IMoveAnimation> activeAnimations;
        static Surface srfcMoveTargetTile;
        static Surface srfcMoveTargetTileHit;
        static Surface srfcMoveTargetTileDark;
        static Surface srfcMoveTargetUnknown;

        #endregion Fields

        #region Properties

        public static List<Moves.IMoveAnimation> ActiveAnimations {
            get { return activeAnimations; }
        }

        #endregion Properties

        #region Methods

        public static void Initialize() {
            activeAnimations = new List<Moves.IMoveAnimation>();
            srfcMoveTargetTile = new Surface(Constants.TILE_WIDTH, Constants.TILE_HEIGHT);
            srfcMoveTargetTile.Blit(GraphicsManager.Tiles[10][77], new Point(0, 0));
            srfcMoveTargetTile.Transparent = true;
            //srfcMoveTargetTile.Alpha = 150;
            //srfcMoveTargetTile.AlphaBlending = true;

            srfcMoveTargetTileHit = new Surface(Constants.TILE_WIDTH, Constants.TILE_HEIGHT);
            srfcMoveTargetTileHit.Blit(GraphicsManager.Tiles[10][91], new Point(0,0));
            srfcMoveTargetTileHit.Transparent = true;
            //srfcMoveTargetTileHit.Alpha = 150;
            //srfcMoveTargetTileHit.AlphaBlending = true;

            srfcMoveTargetTileDark = new Surface(Constants.TILE_WIDTH, Constants.TILE_HEIGHT);
            srfcMoveTargetTileDark.Blit(GraphicsManager.Tiles[10][105], new Point(0, 0));
            srfcMoveTargetTileDark.Transparent = true;

            srfcMoveTargetUnknown = new Surface(Constants.TILE_WIDTH * 3, Constants.TILE_HEIGHT * 3);
            for (int i = 0; i < 9; i++)
            {
                srfcMoveTargetUnknown.Blit(GraphicsManager.Tiles[10][8 + i % 3 + i / 3 * 14], new Point(i % 3 * Constants.TILE_WIDTH, i / 3 * Constants.TILE_HEIGHT));
            }
            srfcMoveTargetUnknown.Transparent = true;
            srfcMoveTargetUnknown.Alpha = 150;
            srfcMoveTargetUnknown.AlphaBlending = true;
            
        }

        public static void RenderMoveAnimation(RendererDestinationData destData, Moves.IMoveAnimation animation, Point pinnedPoint) {
            int animTime = 400;
            switch (animation.AnimType) {
                case Enums.MoveAnimationType.Normal: {
                        #region Normal
                        NormalMoveAnimation specifiedAnim = animation as NormalMoveAnimation;
                        if (specifiedAnim.CompletedLoops < specifiedAnim.RenderLoops) {
                            SpellSheet spriteSheet = GraphicsManager.GetSpellSheet(Enums.StationaryAnimType.Spell, specifiedAnim.AnimationIndex, false);
                            Surface spriteToBlit = null;
                            if (spriteSheet != null) {
                                spriteToBlit = spriteSheet.Sheet;
                            } else {
                                specifiedAnim.Active = false;
                                return;
                            }
                            
                            Rectangle sourceRec = new Rectangle(specifiedAnim.Frame * spriteToBlit.Height,
                                0, spriteToBlit.Height, spriteToBlit.Height);

                            pinnedPoint.X = pinnedPoint.X + Constants.TILE_WIDTH / 2 - spriteToBlit.Height / 2;
                            pinnedPoint.Y = pinnedPoint.Y + Constants.TILE_HEIGHT / 2 - spriteToBlit.Height / 2;

                            //blit
                            destData.Blit(spriteToBlit, pinnedPoint, sourceRec);
                            
                            if (Globals.Tick > specifiedAnim.MoveTime + specifiedAnim.FrameLength) {
                                specifiedAnim.MoveTime = Globals.Tick;
                                specifiedAnim.Frame++;
                            }

                            if (specifiedAnim.Frame >= spriteToBlit.Width / spriteToBlit.Height) {
                                specifiedAnim.CompletedLoops++;
                                specifiedAnim.Frame = 0;
                            }

                            
                        } else {
                            specifiedAnim.Active = false;
                        }
                        #endregion
                    }
                    break;
                case Enums.MoveAnimationType.Arrow: {
                        #region Arrow
                        ArrowMoveAnimation specifiedAnim = animation as ArrowMoveAnimation;
                        int time = Globals.Tick - specifiedAnim.TotalMoveTime;
                        if (time < animTime) {

                            SpellSheet spriteSheet = GraphicsManager.GetSpellSheet(Enums.StationaryAnimType.Arrow, specifiedAnim.AnimationIndex, false);
                            Surface spriteToBlit = null;
                            if (spriteSheet != null) {
                                spriteToBlit = spriteSheet.Sheet;
                            } else {
                                specifiedAnim.Active = false;
                                return;
                            }

                            Rectangle sourceRec = new Rectangle(specifiedAnim.Frame * spriteToBlit.Height / 8,
                                GraphicsManager.GetAnimDirInt(specifiedAnim.Direction) * spriteToBlit.Height / 8, spriteToBlit.Height / 8, spriteToBlit.Height / 8);



                            pinnedPoint.X = pinnedPoint.X + Constants.TILE_WIDTH / 2 - spriteToBlit.Height / 2 / 8;
                            pinnedPoint.Y = pinnedPoint.Y + Constants.TILE_HEIGHT / 2 - spriteToBlit.Height / 2 / 8;

                            switch (specifiedAnim.Direction) {
                                case Enums.Direction.Up: {
                                        pinnedPoint.Y -= specifiedAnim.Distance * Constants.TILE_HEIGHT * time / animTime;
                                    }
                                    break;
                                case Enums.Direction.Down: {
                                        pinnedPoint.Y += specifiedAnim.Distance * Constants.TILE_HEIGHT * time / animTime;
                                    }
                                    break;
                                case Enums.Direction.Left: {
                                        pinnedPoint.X -= specifiedAnim.Distance * Constants.TILE_WIDTH * time / animTime;
                                    }
                                    break;
                                case Enums.Direction.Right: {
                                        pinnedPoint.X += specifiedAnim.Distance * Constants.TILE_WIDTH * time / animTime;
                                    }
                                    break;
                                case Enums.Direction.UpRight:
                                case Enums.Direction.DownRight:
                                case Enums.Direction.DownLeft:
                                case Enums.Direction.UpLeft:
                                    break;
                            }

                            //blit
                            destData.Blit(spriteToBlit, pinnedPoint, sourceRec);

                            if (Globals.Tick > specifiedAnim.MoveTime + specifiedAnim.FrameLength) {
                                specifiedAnim.MoveTime = Globals.Tick;
                                specifiedAnim.Frame++;
                            }

                            if (specifiedAnim.Frame >= spriteToBlit.Width / (spriteToBlit.Height / 8)) {
                                specifiedAnim.CompletedLoops++;
                                specifiedAnim.Frame = 0;
                            }


                        } else {
                            specifiedAnim.Active = false;
                        }
#endregion
                    }
                    break;
                case Enums.MoveAnimationType.Throw: {
                        #region Throw
                        ThrowMoveAnimation specifiedAnim = animation as ThrowMoveAnimation;
                        int time = Globals.Tick - specifiedAnim.TotalMoveTime;
                        if (time < animTime) {

                            SpellSheet spriteSheet = GraphicsManager.GetSpellSheet(Enums.StationaryAnimType.Spell, specifiedAnim.AnimationIndex, false);
                            Surface spriteToBlit = null;
                            if (spriteSheet != null) {
                                spriteToBlit = spriteSheet.Sheet;
                            } else {
                                specifiedAnim.Active = false;
                                return;
                            }

                            Rectangle sourceRec = new Rectangle(specifiedAnim.Frame * spriteToBlit.Height,
                                0, spriteToBlit.Height, spriteToBlit.Height);


                            double distance = Math.Sqrt(Math.Pow(specifiedAnim.XChange * Constants.TILE_WIDTH, 2) + Math.Pow(specifiedAnim.YChange * Constants.TILE_HEIGHT, 2));

                            int x = pinnedPoint.X + specifiedAnim.XChange * Constants.TILE_WIDTH * time / animTime;
                            int y = (int)(pinnedPoint.Y + specifiedAnim.YChange * Constants.TILE_HEIGHT * time / animTime - ((-4) * distance * Math.Pow(time, 2) / animTime + 4 * distance * time) / animTime);

                            x = x + Constants.TILE_WIDTH / 2 - spriteToBlit.Height / 2;
                            y = y + Constants.TILE_HEIGHT / 2 - spriteToBlit.Height / 2;

                            //blit
                            destData.Blit(spriteToBlit, new Point(x, y), sourceRec);

                            if (Globals.Tick > specifiedAnim.MoveTime + specifiedAnim.FrameLength) {
                                specifiedAnim.MoveTime = Globals.Tick;
                                specifiedAnim.Frame++;
                            }

                            if (specifiedAnim.Frame >= spriteToBlit.Width / spriteToBlit.Height) {
                                specifiedAnim.CompletedLoops++;
                                specifiedAnim.Frame = 0;
                            }


                        } else {
                            specifiedAnim.Active = false;
                        }
#endregion
                    }
                    break;
                case Enums.MoveAnimationType.Beam: {
                        #region Beam
                        BeamMoveAnimation specifiedAnim = animation as BeamMoveAnimation;
                        if (specifiedAnim.CompletedLoops < specifiedAnim.RenderLoops + specifiedAnim.Distance) {
                            SpellSheet spriteSheet = GraphicsManager.GetSpellSheet(Enums.StationaryAnimType.Beam, specifiedAnim.AnimationIndex, false);
                            Surface spriteToBlit = null;
                            if (spriteSheet != null) {
                                spriteToBlit = spriteSheet.Sheet;
                            } else {
                                specifiedAnim.Active = false;
                                return;
                            }

                            int curDistance = specifiedAnim.Distance;
                            Rectangle sourceRec = new Rectangle();
                            if (specifiedAnim.CompletedLoops < specifiedAnim.Distance) curDistance = specifiedAnim.CompletedLoops;
                            for (int i = 0; i <= curDistance; i++) {
                                if (i == 0) {
                                    //draw beginning
                                    sourceRec = new Rectangle(specifiedAnim.Frame * spriteToBlit.Height / 32,
                                GraphicsManager.GetAnimDirInt(specifiedAnim.Direction) * 4 * spriteToBlit.Height / 32, spriteToBlit.Height / 32, spriteToBlit.Height / 32);
                                } else if (i == curDistance) {
                                    if (curDistance == specifiedAnim.Distance) {
                                        sourceRec = new Rectangle(specifiedAnim.Frame * spriteToBlit.Height / 32,
                                (GraphicsManager.GetAnimDirInt(specifiedAnim.Direction) * 4 + 3) * spriteToBlit.Height / 32, spriteToBlit.Height / 32, spriteToBlit.Height / 32);
                                    } else {
                                        sourceRec = new Rectangle(specifiedAnim.Frame * spriteToBlit.Height / 32,
                                (GraphicsManager.GetAnimDirInt(specifiedAnim.Direction) * 4 + 2) * spriteToBlit.Height / 32, spriteToBlit.Height / 32, spriteToBlit.Height / 32);
                                    }
                                } else {
                                    //draw body
                                    sourceRec = new Rectangle(specifiedAnim.Frame * spriteToBlit.Height / 32,
                                (GraphicsManager.GetAnimDirInt(specifiedAnim.Direction) * 4 + 1) * spriteToBlit.Height / 32, spriteToBlit.Height / 32, spriteToBlit.Height / 32);
                                }

                                Point blitPoint = new Point();

                                switch (specifiedAnim.Direction) {
                                    case Enums.Direction.Up: {
                                        blitPoint = new Point(pinnedPoint.X, pinnedPoint.Y - i * Constants.TILE_HEIGHT);
                                        }
                                        break;
                                    case Enums.Direction.Down: {
                                        blitPoint = new Point(pinnedPoint.X, pinnedPoint.Y + i * Constants.TILE_HEIGHT);
                                        }
                                        break;
                                    case Enums.Direction.Left: {
                                        blitPoint = new Point(pinnedPoint.X - i * Constants.TILE_WIDTH, pinnedPoint.Y);
                                        }
                                        break;
                                    case Enums.Direction.Right: {
                                        blitPoint = new Point(pinnedPoint.X + i * Constants.TILE_WIDTH, pinnedPoint.Y);
                                        }
                                        break;
                                    case Enums.Direction.UpRight:
                                    case Enums.Direction.DownRight:
                                    case Enums.Direction.DownLeft:
                                    case Enums.Direction.UpLeft:
                                        break;
                                }

                                blitPoint.X = blitPoint.X + Constants.TILE_WIDTH / 2 - spriteToBlit.Height / 2 / 32;
                                blitPoint.Y = blitPoint.Y + Constants.TILE_HEIGHT / 2 - spriteToBlit.Height / 2 / 32;

                                //blit
                                destData.Blit(spriteToBlit, blitPoint, sourceRec);
                            }

                            

                            

                            if (Globals.Tick > specifiedAnim.MoveTime + specifiedAnim.FrameLength) {
                                specifiedAnim.MoveTime = Globals.Tick;
                                specifiedAnim.Frame++;
                            }

                            if (specifiedAnim.Frame >= spriteToBlit.Width / (spriteToBlit.Height / 32)) {
                                specifiedAnim.CompletedLoops++;
                                specifiedAnim.Frame = 0;
                            }


                        } else {
                            specifiedAnim.Active = false;
                        }
#endregion
                    }
                    break;
                case Enums.MoveAnimationType.Overlay: {
                        #region Overlay
                        OverlayMoveAnimation specifiedAnim = animation as OverlayMoveAnimation;
                        if (specifiedAnim.CompletedLoops < specifiedAnim.RenderLoops) {
                            SpellSheet spriteSheet = GraphicsManager.GetSpellSheet(Enums.StationaryAnimType.Spell, specifiedAnim.AnimationIndex, true);
                            Surface spriteToBlit = null;
                            if (spriteSheet != null) {
                                spriteToBlit = spriteSheet.Sheet;
                            } else {
                                specifiedAnim.Active = false;
                                return;
                            }

                            Rectangle sourceRec = new Rectangle(specifiedAnim.Frame * spriteToBlit.Height,
                                0, spriteToBlit.Height, spriteToBlit.Height);

                            //blit
                            for (int y = 0; y < Constants.TILE_HEIGHT * 15; y += spriteToBlit.Height) {
                                for (int x = 0; x < Constants.TILE_WIDTH * 20; x += spriteToBlit.Height) {
                                    destData.Blit(spriteToBlit, new Point(x, y), sourceRec);
                                }
                            }

                            if (Globals.Tick > specifiedAnim.MoveTime + specifiedAnim.FrameLength) {
                                specifiedAnim.MoveTime = Globals.Tick;
                                specifiedAnim.Frame++;
                            }

                            if (specifiedAnim.Frame >= spriteToBlit.Width / spriteToBlit.Height) {
                                specifiedAnim.CompletedLoops++;
                                specifiedAnim.Frame = 0;
                            }

                        } else {
                            specifiedAnim.Active = false;
                        }
#endregion
                    }
                    break;
                case Enums.MoveAnimationType.Tile: {
                        #region Tile
                        TileMoveAnimation specifiedAnim = animation as TileMoveAnimation;
                        if (specifiedAnim.CompletedLoops < specifiedAnim.RenderLoops) {
                            SpellSheet spriteSheet = GraphicsManager.GetSpellSheet(Enums.StationaryAnimType.Spell, specifiedAnim.AnimationIndex, false);
                            Surface spriteToBlit = null;
                            if (spriteSheet != null) {
                                spriteToBlit = spriteSheet.Sheet;
                            } else {
                                specifiedAnim.Active = false;
                                return;
                            }

                            Rectangle sourceRec = new Rectangle(specifiedAnim.Frame * spriteToBlit.Height,
                                0, spriteToBlit.Height, spriteToBlit.Height);

                            Point blitPoint = new Point(pinnedPoint.X + Constants.TILE_WIDTH / 2 - spriteToBlit.Height / 2, pinnedPoint.Y + Constants.TILE_HEIGHT / 2 - spriteToBlit.Height / 2);

                            //blit
                            switch (specifiedAnim.RangeType) {
                                case Enums.MoveRange.FrontOfUserUntil:
                                case Enums.MoveRange.LineUntilHit: {
                                        #region Front of user Until
                                        switch (specifiedAnim.Direction) {
                                            case Enums.Direction.Up: {
                                                    int y = specifiedAnim.StartY;
                                                    for (int i = 1; i <= specifiedAnim.Range; i++) {

                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X, blitPoint.Y - Constants.TILE_HEIGHT * i), sourceRec);

                                                        if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                            specifiedAnim.StartX, specifiedAnim.StartY - i)) {
                                                            break;
                                                        }
                                                    }
                                                }
                                                break;
                                            case Enums.Direction.Down: {
                                                    int y = specifiedAnim.StartY;
                                                    for (int i = 1; i <= specifiedAnim.Range; i++) {
                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X, blitPoint.Y + Constants.TILE_HEIGHT * i), sourceRec);

                                                        if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                            specifiedAnim.StartX, specifiedAnim.StartY + i)) {
                                                            break;
                                                        }
                                                    }
                                                }
                                                break;
                                            case Enums.Direction.Left: {
                                                    int x = specifiedAnim.StartX;
                                                    for (int i = 1; i <= specifiedAnim.Range; i++) {

                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X - Constants.TILE_WIDTH * i, blitPoint.Y), sourceRec);

                                                        if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                            specifiedAnim.StartX - i, specifiedAnim.StartY)) {
                                                            break;
                                                        }

                                                    }
                                                }
                                                break;
                                            case Enums.Direction.Right: {
                                                    int x = specifiedAnim.StartX;
                                                    for (int i = 1; i <= specifiedAnim.Range; i++) {
                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X + Constants.TILE_WIDTH * i, blitPoint.Y), sourceRec);

                                                        if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                            specifiedAnim.StartX + i, specifiedAnim.StartY)) {
                                                            break;
                                                        }
                                                    }
                                                }
                                                break;
                                        }
                                        #endregion
                                    }
                                    break;
                                case Enums.MoveRange.StraightLine:
                                case Enums.MoveRange.FrontOfUser: {
                                        #region Front of user
                                        switch (specifiedAnim.Direction) {
                                            case Enums.Direction.Up: {
                                                    int y = specifiedAnim.StartY;
                                                    for (int i = 1; i <= specifiedAnim.Range; i++) {
                                                        
                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X, blitPoint.Y - Constants.TILE_HEIGHT * i), sourceRec);

                                                    }
                                                }
                                                break;
                                            case Enums.Direction.Down: {
                                                    int y = specifiedAnim.StartY;
                                                    for (int i = 1; i <= specifiedAnim.Range; i++) {
                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X, blitPoint.Y + Constants.TILE_HEIGHT * i), sourceRec);

                                                    }
                                                }
                                                break;
                                            case Enums.Direction.Left: {
                                                    int x = specifiedAnim.StartX;
                                                    for (int i = 1; i <= specifiedAnim.Range; i++) {

                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X - Constants.TILE_WIDTH * i, blitPoint.Y), sourceRec);

                                                    }
                                                }
                                                break;
                                            case Enums.Direction.Right: {
                                                    int x = specifiedAnim.StartX;
                                                    for (int i = 1; i <= specifiedAnim.Range; i++) {
                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X + Constants.TILE_WIDTH * i, blitPoint.Y), sourceRec);

                                                    }
                                                }
                                                break;
                                        }
                                        #endregion
                                    }
                                    break;
                                case Enums.MoveRange.User:
                                case Enums.MoveRange.Special: {
                                        #region user
                                        destData.Blit(spriteToBlit, blitPoint, sourceRec);
                                        #endregion
                                    }
                                    break;
                                case Enums.MoveRange.Floor: {
                                        #region Floor
                                        
                                        for (int x = 0; x < 20; x++) {
                                            for (int y = 0; y < 15; y++) {

                                                destData.Blit(spriteToBlit, new Point(x * Constants.TILE_WIDTH + Constants.TILE_WIDTH / 2 - spriteToBlit.Height / 2, y * Constants.TILE_HEIGHT + Constants.TILE_HEIGHT / 2 - spriteToBlit.Height / 2), sourceRec);
                                            }
                                        }
                                        #endregion
                                    }
                                    break;
                                case Enums.MoveRange.Room: {
                                        #region Room
                                        for (int x = (-specifiedAnim.Range); x <= specifiedAnim.Range; x++) {
                                            for (int y = (-specifiedAnim.Range); y <= specifiedAnim.Range; y++) {
                                                destData.Blit(spriteToBlit, new Point(blitPoint.X + x * Constants.TILE_WIDTH, blitPoint.Y + y * Constants.TILE_HEIGHT), sourceRec);
                                            }
                                        }
                                        #endregion
                                    }
                                    break;
                                case Enums.MoveRange.FrontAndSides: {
                                        #region Front and Sides
                                        for (int r = 0; r <= specifiedAnim.Range; r++) {

                                            //check adjacent tiles
                                            switch (specifiedAnim.Direction) {
                                                case Enums.Direction.Down: {
                                                        
                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X, blitPoint.Y + r * Constants.TILE_HEIGHT), sourceRec);

                                                        for (int s = 1; s <= r; s++) {

                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X - s * Constants.TILE_WIDTH, blitPoint.Y + r * Constants.TILE_HEIGHT), sourceRec);

                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X + s * Constants.TILE_WIDTH, blitPoint.Y + r * Constants.TILE_HEIGHT), sourceRec);

                                                        }

                                                    }
                                                    break;
                                                case Enums.Direction.Up: {

                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X, blitPoint.Y - r * Constants.TILE_HEIGHT), sourceRec);


                                                        for (int s = 1; s <= r; s++) {

                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X - s * Constants.TILE_WIDTH, blitPoint.Y - r * Constants.TILE_HEIGHT), sourceRec);

                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X + s * Constants.TILE_WIDTH, blitPoint.Y - r * Constants.TILE_HEIGHT), sourceRec);

                                                        }

                                                    }
                                                    break;
                                                case Enums.Direction.Left: {
                                                    
                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X - r * Constants.TILE_WIDTH, blitPoint.Y), sourceRec);

                                                        for (int s = 1; s <= r; s++) {


                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X - r * Constants.TILE_WIDTH, blitPoint.Y - s * Constants.TILE_HEIGHT), sourceRec);

                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X - r * Constants.TILE_WIDTH, blitPoint.Y + s * Constants.TILE_HEIGHT), sourceRec);


                                                        }
                                                    }
                                                    break;
                                                case Enums.Direction.Right: {

                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X + r * Constants.TILE_WIDTH, blitPoint.Y), sourceRec);

                                                        for (int s = 1; s <= r; s++) {


                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X + r * Constants.TILE_WIDTH, blitPoint.Y - s * Constants.TILE_HEIGHT), sourceRec);

                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X + r * Constants.TILE_WIDTH, blitPoint.Y + s * Constants.TILE_HEIGHT), sourceRec);


                                                        }
                                                    }
                                                    break;
                                            }


                                        }
                                        #endregion
                                    }
                                    break;
                                case Enums.MoveRange.ArcThrow: {
                                        #region Arc Throw
                                        bool stopattile = false;

                                        for (int r = 0; r <= specifiedAnim.Range; r++) {

                                            //check adjacent tiles
                                            switch (specifiedAnim.Direction) {
                                                case Enums.Direction.Down: {
                                                        
                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X, blitPoint.Y + r * Constants.TILE_HEIGHT), sourceRec);

                                                        if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                            specifiedAnim.StartX, specifiedAnim.StartY + r)) {
                                                                stopattile = true;
                                                        }

                                                        if (stopattile) {
                                                            break;
                                                        }

                                                        for (int s = 1; s <= r; s++) {

                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X - s * Constants.TILE_WIDTH, blitPoint.Y + r * Constants.TILE_HEIGHT), sourceRec);

                                                            if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                            specifiedAnim.StartX - s, specifiedAnim.StartY + r)) {
                                                                stopattile = true;
                                                            }

                                                            if (stopattile) {
                                                                break;
                                                            }

                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X + s * Constants.TILE_WIDTH, blitPoint.Y + r * Constants.TILE_HEIGHT), sourceRec);

                                                            if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                            specifiedAnim.StartX + s, specifiedAnim.StartY + r)) {
                                                                stopattile = true;
                                                            }

                                                            if (stopattile) {
                                                                break;
                                                            }
                                                        }

                                                        if (stopattile) {
                                                            break;
                                                        }

                                                    }
                                                    break;
                                                case Enums.Direction.Up: {

                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X, blitPoint.Y - r * Constants.TILE_HEIGHT), sourceRec);

                                                        if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                            specifiedAnim.StartX, specifiedAnim.StartY - r)) {
                                                            stopattile = true;
                                                        }

                                                        if (stopattile) {
                                                            break;
                                                        }

                                                        for (int s = 1; s <= r; s++) {

                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X - s * Constants.TILE_WIDTH, blitPoint.Y - r * Constants.TILE_HEIGHT), sourceRec);

                                                            if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                            specifiedAnim.StartX - s, specifiedAnim.StartY - r)) {
                                                                stopattile = true;
                                                            }

                                                            if (stopattile) {
                                                                break;
                                                            }

                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X + s * Constants.TILE_WIDTH, blitPoint.Y - r * Constants.TILE_HEIGHT), sourceRec);

                                                            if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                            specifiedAnim.StartX + s, specifiedAnim.StartY - r)) {
                                                                stopattile = true;
                                                            }

                                                            if (stopattile) {
                                                                break;
                                                            }

                                                        }

                                                    }
                                                    break;
                                                case Enums.Direction.Left: {
                                                    
                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X - r * Constants.TILE_WIDTH, blitPoint.Y), sourceRec);
                                                    
                                                        if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                            specifiedAnim.StartX - r, specifiedAnim.StartY)) {
                                                            stopattile = true;
                                                        }

                                                        if (stopattile) {
                                                            break;
                                                        }

                                                        for (int s = 1; s <= r; s++) {


                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X - r * Constants.TILE_WIDTH, blitPoint.Y - s * Constants.TILE_HEIGHT), sourceRec);

                                                            if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                            specifiedAnim.StartX - r, specifiedAnim.StartY - s)) {
                                                                stopattile = true;
                                                            }

                                                            if (stopattile) {
                                                                break;
                                                            }

                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X - r * Constants.TILE_WIDTH, blitPoint.Y + s * Constants.TILE_HEIGHT), sourceRec);

                                                            if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                            specifiedAnim.StartX - r, specifiedAnim.StartY + s)) {
                                                                stopattile = true;
                                                            }

                                                            if (stopattile) {
                                                                break;
                                                            }


                                                        }
                                                    }
                                                    break;
                                                case Enums.Direction.Right: {

                                                        destData.Blit(spriteToBlit, new Point(blitPoint.X + r * Constants.TILE_WIDTH, blitPoint.Y), sourceRec);

                                                        if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                                specifiedAnim.StartX + r, specifiedAnim.StartY)) {
                                                            stopattile = true;
                                                        }

                                                        if (stopattile) {
                                                            break;
                                                        }

                                                        for (int s = 1; s <= r; s++) {

                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X + r * Constants.TILE_WIDTH, blitPoint.Y - s * Constants.TILE_HEIGHT), sourceRec);

                                                            if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                                specifiedAnim.StartX + r, specifiedAnim.StartY - s)) {
                                                                stopattile = true;
                                                            }

                                                            if (stopattile) {
                                                                break;
                                                            }

                                                            destData.Blit(spriteToBlit, new Point(blitPoint.X + r * Constants.TILE_WIDTH, blitPoint.Y + s * Constants.TILE_HEIGHT), sourceRec);

                                                            if (IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                                                specifiedAnim.StartX + r, specifiedAnim.StartY + s)) {
                                                                stopattile = true;
                                                            }

                                                            if (stopattile) {
                                                                break;
                                                            }

                                                        }
                                                    }
                                                    break;
                                            }


                                            if (stopattile) {
                                                break;
                                            }
                                        }

                                        
                                        #endregion
                                    }
                                    break;
                            }


                            //for (int y = specifiedAnim.StartY; y <= specifiedAnim.EndY; y++) {
                            //    for (int x = specifiedAnim.StartX; x <= specifiedAnim.EndX; x++) {
                                    
                            //        Point blitPoint = Screen.ScreenRenderer.ToTilePoint(new Point(x, y), useScrolling);
                            //        blitPoint.X = blitPoint.X + Constants.TILE_WIDTH / 2 - spriteToBlit.Height / 2;
                            //        blitPoint.Y = blitPoint.Y + Constants.TILE_HEIGHT / 2 - spriteToBlit.Height / 2;

                            //        destData.Blit(spriteToBlit, blitPoint, sourceRec);
                            //    }
                            //}

                            if (Globals.Tick > specifiedAnim.MoveTime + specifiedAnim.FrameLength) {
                                specifiedAnim.MoveTime = Globals.Tick;
                                specifiedAnim.Frame++;
                            }

                            if (specifiedAnim.Frame >= spriteToBlit.Width / spriteToBlit.Height) {
                                specifiedAnim.CompletedLoops++;
                                specifiedAnim.Frame = 0;
                            }


                        } else {
                            specifiedAnim.Active = false;
                        }
#endregion
                    }
                    break;
                case Enums.MoveAnimationType.ItemArrow: {
                        #region ItemArrow
                        ItemArrowMoveAnimation specifiedAnim = animation as ItemArrowMoveAnimation;
                        int time = Globals.Tick - specifiedAnim.TotalMoveTime;
                        if (time < animTime) {

                            if (specifiedAnim.AnimationIndex < 0) {
                                specifiedAnim.Active = false;
                                return;
                            }

                            Rectangle sourceRec = new Rectangle((specifiedAnim.AnimationIndex - (specifiedAnim.AnimationIndex / 6) * 6) * Constants.TILE_WIDTH,
                                               (specifiedAnim.AnimationIndex / 6) * Constants.TILE_HEIGHT, Constants.TILE_WIDTH, Constants.TILE_HEIGHT);

                            

                            switch (specifiedAnim.Direction) {
                                case Enums.Direction.Up: {
                                        pinnedPoint.Y -= specifiedAnim.Distance * Constants.TILE_HEIGHT * time / animTime;
                                    }
                                    break;
                                case Enums.Direction.Down: {
                                        pinnedPoint.Y += specifiedAnim.Distance * Constants.TILE_HEIGHT * time / animTime;
                                    }
                                    break;
                                case Enums.Direction.Left: {
                                        pinnedPoint.X -= specifiedAnim.Distance * Constants.TILE_WIDTH * time / animTime;
                                    }
                                    break;
                                case Enums.Direction.Right: {
                                        pinnedPoint.X += specifiedAnim.Distance * Constants.TILE_WIDTH * time / animTime;
                                    }
                                    break;
                                case Enums.Direction.UpRight:
                                case Enums.Direction.DownRight:
                                case Enums.Direction.DownLeft:
                                case Enums.Direction.UpLeft:
                                    break;
                            }
                            
                            //blit
                            destData.Blit(GraphicsManager.Items, pinnedPoint, sourceRec);

                            //if (Globals.Tick > specifiedAnim.MoveTime + specifiedAnim.FrameLength) {
                            //    specifiedAnim.MoveTime = Globals.Tick;
                            //    specifiedAnim.Frame++;
                            //}

                            //if (specifiedAnim.Frame >= spriteToBlit.Width / spriteToBlit.Height) {
                            //    specifiedAnim.CompletedLoops++;
                            //    specifiedAnim.Frame = 0;
                            //}


                        } else {
                            specifiedAnim.Active = false;
                        }
                        #endregion
                    }
                    break;
                case Enums.MoveAnimationType.ItemThrow: {
                        #region ItemThrow
                        ItemThrowMoveAnimation specifiedAnim = animation as ItemThrowMoveAnimation;
                        int time = Globals.Tick - specifiedAnim.TotalMoveTime;
                        if (time < animTime) {

                            if (specifiedAnim.AnimationIndex < 0) {
                                specifiedAnim.Active = false;
                                return;
                            }

                            Rectangle sourceRec = new Rectangle((specifiedAnim.AnimationIndex - (specifiedAnim.AnimationIndex / 6) * 6) * Constants.TILE_WIDTH,
                                               (specifiedAnim.AnimationIndex / 6) * Constants.TILE_HEIGHT, Constants.TILE_WIDTH, Constants.TILE_HEIGHT);


                            double distance = Math.Sqrt(Math.Pow(specifiedAnim.XChange * Constants.TILE_WIDTH, 2) + Math.Pow(specifiedAnim.YChange * Constants.TILE_HEIGHT, 2));

                            int x = pinnedPoint.X + specifiedAnim.XChange * Constants.TILE_WIDTH * time / animTime;
                            int y = (int)(pinnedPoint.Y + specifiedAnim.YChange * Constants.TILE_HEIGHT * time / animTime - ((-4) * distance * Math.Pow(time, 2) / animTime + 4 * distance * time) / animTime);


                            //blit
                            destData.Blit(GraphicsManager.Items, new Point(x, y), sourceRec);

                            //if (Globals.Tick > specifiedAnim.MoveTime + specifiedAnim.FrameLength) {
                            //    specifiedAnim.MoveTime = Globals.Tick;
                            //    specifiedAnim.Frame++;
                            //}

                            //if (specifiedAnim.Frame >= spriteToBlit.Width / spriteToBlit.Height) {
                            //    specifiedAnim.CompletedLoops++;
                            //    specifiedAnim.Frame = 0;
                            //}


                        } else {
                            specifiedAnim.Active = false;
                        }
                        #endregion
                    }
                    break;
            }
            

        }

        public static void RenderMoveTargettingDisplay(RendererDestinationData destData, Sprites.ISprite attacker, Logic.Moves.Move move) {
           
            switch (move.RangeType) {
                case Enums.MoveRange.FrontOfUserUntil:
                case Enums.MoveRange.LineUntilHit:
                    {
                        #region Front of user Until
                        switch (attacker.Direction)
                        {
                            case Enums.Direction.Up:
                                {
                                    int y = attacker.Y;
                                    for (int i = 1; i <= move.Range; i++)
                                    {
                                        y = attacker.Y - i;
                                        //if (!ShouldContinueRenderingTargettingDisplay(Logic.Maps.MapHelper.ActiveMap,
                                        //    attacker.X, y)) {
                                        //    break;
                                        //}
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X, y), Enums.MapID.Active))
                                        {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X), Screen.ScreenRenderer.ToTileY(y) + Logic.Players.PlayerManager.MyPlayer.Offset.Y));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X, y))
                                        {
                                            destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X), Screen.ScreenRenderer.ToTileY(y) + Logic.Players.PlayerManager.MyPlayer.Offset.Y));
                                        }
                                        else
                                        {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X), Screen.ScreenRenderer.ToTileY(y) + Logic.Players.PlayerManager.MyPlayer.Offset.Y));
                                            break;
                                        }
                                    }
                                }
                                break;
                            case Enums.Direction.Down:
                                {
                                    int y = attacker.Y;
                                    for (int i = 1; i <= move.Range; i++)
                                    {
                                        y = attacker.Y + i;
                                        //if (!ShouldContinueRenderingTargettingDisplay(Logic.Maps.MapHelper.ActiveMap,
                                        //    attacker.X, y)) {
                                        //    break;
                                        //}
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X, y), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X), Screen.ScreenRenderer.ToTileY(y) + Logic.Players.PlayerManager.MyPlayer.Offset.Y));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X, y))
                                        {
                                            destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X), Screen.ScreenRenderer.ToTileY(y) + Logic.Players.PlayerManager.MyPlayer.Offset.Y));
                                        }
                                        else
                                        {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X), Screen.ScreenRenderer.ToTileY(y) + Logic.Players.PlayerManager.MyPlayer.Offset.Y));
                                            break;
                                        }
                                    }
                                }
                                break;
                            case Enums.Direction.Left:
                                {
                                    int x = attacker.X;
                                    for (int i = 1; i <= move.Range; i++)
                                    {
                                        x = attacker.X - i;
                                        //if (!ShouldContinueRenderingTargettingDisplay(Logic.Maps.MapHelper.ActiveMap,
                                        //     x, attacker.Y)) {
                                        //    break;
                                        //}
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(x, attacker.Y), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(x) + Logic.Players.PlayerManager.MyPlayer.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y)));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            x, attacker.Y))
                                        {
                                            destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(x) + Logic.Players.PlayerManager.MyPlayer.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y)));
                                        }
                                        else
                                        {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(x) + Logic.Players.PlayerManager.MyPlayer.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y)));
                                            break;
                                        }
                                    }
                                }
                                break;
                            case Enums.Direction.Right:
                                {
                                    int x = attacker.X;
                                    for (int i = 1; i <= move.Range; i++)
                                    {
                                        x = attacker.X + i;
                                        //if (!ShouldContinueRenderingTargettingDisplay(Logic.Maps.MapHelper.ActiveMap,
                                        //    x, attacker.Y)) {
                                        //    break;
                                        //}
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(x, attacker.Y), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(x) + Logic.Players.PlayerManager.MyPlayer.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y)));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            x, attacker.Y))
                                        {
                                            destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(x) + Logic.Players.PlayerManager.MyPlayer.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y)));
                                        }
                                        else
                                        {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(x) + Logic.Players.PlayerManager.MyPlayer.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y)));
                                            break;
                                        }
                                    }
                                }
                                break;
                        }
                        #endregion
                    }
                    break;
                case Enums.MoveRange.StraightLine:
                case Enums.MoveRange.FrontOfUser: {
                        #region Front of user
                        switch (attacker.Direction) {
                            case Enums.Direction.Up: {
                                    int y = attacker.Y;
                                    for (int i = 1; i <= move.Range; i++) {
                                        y = attacker.Y - i;
                                        //if (!ShouldContinueRenderingTargettingDisplay(Logic.Maps.MapHelper.ActiveMap,
                                        //    attacker.X, y)) {
                                        //    break;
                                        //}
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X, y), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X), Screen.ScreenRenderer.ToTileY(y) + Logic.Players.PlayerManager.MyPlayer.Offset.Y));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X, y)) {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X), Screen.ScreenRenderer.ToTileY(y) + Logic.Players.PlayerManager.MyPlayer.Offset.Y));
                                        } else {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X), Screen.ScreenRenderer.ToTileY(y) + Logic.Players.PlayerManager.MyPlayer.Offset.Y));
                                        }
                                    }
                                }
                                break;
                            case Enums.Direction.Down: {
                                    int y = attacker.Y;
                                    for (int i = 1; i <= move.Range; i++) {
                                        y = attacker.Y + i;
                                        //if (!ShouldContinueRenderingTargettingDisplay(Logic.Maps.MapHelper.ActiveMap,
                                        //    attacker.X, y)) {
                                        //    break;
                                        //}
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X, y), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X), Screen.ScreenRenderer.ToTileY(y) + Logic.Players.PlayerManager.MyPlayer.Offset.Y));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X, y)) {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X), Screen.ScreenRenderer.ToTileY(y) + Logic.Players.PlayerManager.MyPlayer.Offset.Y));
                                        } else {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X), Screen.ScreenRenderer.ToTileY(y) + Logic.Players.PlayerManager.MyPlayer.Offset.Y));
                                        }
                                    }
                                }
                                break;
                            case Enums.Direction.Left: {
                                    int x = attacker.X;
                                    for (int i = 1; i <= move.Range; i++) {
                                        x = attacker.X - i;
                                        //if (!ShouldContinueRenderingTargettingDisplay(Logic.Maps.MapHelper.ActiveMap,
                                        //     x, attacker.Y)) {
                                        //    break;
                                        //}
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(x, attacker.Y), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(x) + Logic.Players.PlayerManager.MyPlayer.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y)));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            x, attacker.Y)) {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(x) + Logic.Players.PlayerManager.MyPlayer.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y)));
                                        } else {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(x) + Logic.Players.PlayerManager.MyPlayer.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y)));
                                        }
                                    }
                                }
                                break;
                            case Enums.Direction.Right: {
                                    int x = attacker.X;
                                    for (int i = 1; i <= move.Range; i++) {
                                        x = attacker.X + i;
                                        //if (!ShouldContinueRenderingTargettingDisplay(Logic.Maps.MapHelper.ActiveMap,
                                        //    x, attacker.Y)) {
                                        //    break;
                                        //}
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(x, attacker.Y), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(x) + Logic.Players.PlayerManager.MyPlayer.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y)));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            x, attacker.Y)) {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(x) + Logic.Players.PlayerManager.MyPlayer.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y)));
                                        } else {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(x) + Logic.Players.PlayerManager.MyPlayer.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y)));
                                        }
                                    }
                                }
                                break;
                        }
                        #endregion
                    }
                    break;
                case Enums.MoveRange.User: {
                        #region user
                        destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y) + attacker.Offset.Y));
                        #endregion
                    }
                    break;
                case Enums.MoveRange.Special:
                    {
                        #region Special
                        destData.Blit(srfcMoveTargetUnknown, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - 1) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - 1) + attacker.Offset.Y));
                        #endregion
                    }
                    break;
                case Enums.MoveRange.Floor:
                    {
                        #region Floor
                        int startX = Screen.ScreenRenderer.GetScreenLeft() - 1;
                        int startY = Screen.ScreenRenderer.GetScreenTop();
                        if (startX < 0)
                        {
                            startX = 0;
                        }
                        else if (startX + 19 > Screen.ScreenRenderer.RenderOptions.Map.MaxX)
                        {
                            startX = Screen.ScreenRenderer.RenderOptions.Map.MaxX - 19;
                        }
                        if (startY < 0)
                        {
                            startY = 0;
                        }
                        else if (startY + 14 > Screen.ScreenRenderer.RenderOptions.Map.MaxY)
                        {
                            startY = Screen.ScreenRenderer.RenderOptions.Map.MaxY - 14;
                        }


                        for (int x = startX; x < startX + 20; x++)
                        {
                            for (int y = startY; y < startY + 15; y++)
                            {
                                if (!Screen.ScreenRenderer.CanBeSeen(new Point(x, y), Enums.MapID.Active)) {
                                    destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(x), Screen.ScreenRenderer.ToTileY(y)));
                                } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                    x, y))
                                {
                                    destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(x), Screen.ScreenRenderer.ToTileY(y)));
                                }
                                else
                                {
                                    destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(x), Screen.ScreenRenderer.ToTileY(y)));
                                }
                            }
                        }
                        #endregion
                    }
                    break;
                case Enums.MoveRange.Room:
                    {
                        #region Room
                        for (int x = attacker.X - move.Range; x <= attacker.X + move.Range; x++)
                        {
                            for (int y = attacker.Y - move.Range; y <= attacker.Y + move.Range; y++)
                            {
                                if (!Screen.ScreenRenderer.CanBeSeen(new Point(x, y), Enums.MapID.Active)) {
                                    destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(x), Screen.ScreenRenderer.ToTileY(y)));
                                } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                    x, y))
                                {
                                    destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(x) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(y) + attacker.Offset.Y));
                                }
                                else
                                {
                                    destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(x) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(y) + attacker.Offset.Y));
                                }
                            }
                        }
                        #endregion
                    }
                    break;
                case Enums.MoveRange.FrontAndSides:
                    {
                        #region Front and Sides
                        for (int r = 0; r <= move.Range; r++)
                        {

                            //check adjacent tiles
                            switch (attacker.Direction)
                            {
                                case Enums.Direction.Down:
                                    {
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X, attacker.Y + r), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X, attacker.Y + r))
                                        {
                                            destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                        }
                                        else
                                        {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                        }



                                        for (int s = 1; s <= r; s++)
                                        {

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X - s, attacker.Y + r), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X - s, attacker.Y + r))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                            }

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X + s, attacker.Y + r), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X + s, attacker.Y + r))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                            }

                                        }

                                    }
                                    break;
                                case Enums.Direction.Up:
                                    {
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X, attacker.Y - r), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X, attacker.Y - r))
                                        {
                                            destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                        }
                                        else
                                        {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                        }



                                        for (int s = 1; s <= r; s++)
                                        {

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X - s, attacker.Y - r), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X - s, attacker.Y - r))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                            }

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X + s, attacker.Y - r), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X + s, attacker.Y - r))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                            }

                                        }

                                    }
                                    break;
                                case Enums.Direction.Left:
                                    {
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X - r, attacker.Y), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y) + attacker.Offset.Y));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X - r, attacker.Y))
                                        {
                                            destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y) + attacker.Offset.Y));
                                        }
                                        else
                                        {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y) + attacker.Offset.Y));
                                        }



                                        for (int s = 1; s <= r; s++)
                                        {

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X - r, attacker.Y - s), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - s) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X - r, attacker.Y - s))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - s) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - s) + attacker.Offset.Y));
                                            }

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X - r, attacker.Y + s), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + s) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X - r, attacker.Y + s))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + s) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + s) + attacker.Offset.Y));
                                            }

                                        }
                                    }
                                    break;
                                case Enums.Direction.Right:
                                    {
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X + r, attacker.Y), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y) + attacker.Offset.Y));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X + r, attacker.Y))
                                        {
                                            destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y) + attacker.Offset.Y));
                                        }
                                        else
                                        {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y) + attacker.Offset.Y));
                                        }



                                        for (int s = 1; s <= r; s++)
                                        {

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X + r, attacker.Y - s), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - s) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X + r, attacker.Y - s))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - s) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - s) + attacker.Offset.Y));
                                            }

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X + r, attacker.Y + s), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + s) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X + r, attacker.Y + s))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + s) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + s) + attacker.Offset.Y));
                                            }

                                        }
                                    }
                                    break;
                            }


                        }
                        #endregion
                    }
                    break;
                case Enums.MoveRange.ArcThrow:
                    {
                        #region Arc Throw
                        bool stopattile = false;
                        for (int r = 0; r <= move.Range; r++)
                        {
                            
                            //check adjacent tiles
                            switch (attacker.Direction)
                            {
                                case Enums.Direction.Down:
                                    {
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X, attacker.Y + r), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X, attacker.Y + r))
                                        {
                                            destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                        }
                                        else
                                        {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                            stopattile = true;
                                        }

                                        if (stopattile)
                                        {
                                            break;
                                        }

                                        for (int s = 1; s <= r; s++)
                                        {

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X - s, attacker.Y + r), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X - s, attacker.Y + r))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                                stopattile = true;
                                            }

                                            if (stopattile)
                                            {
                                                break;
                                            }

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X + s, attacker.Y + r), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X + s, attacker.Y + r))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + r) + attacker.Offset.Y));
                                                stopattile = true;
                                            }

                                            if (stopattile)
                                            {
                                                break;
                                            }

                                        }

                                    }
                                    break;
                                case Enums.Direction.Up:
                                    {
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X, attacker.Y - r), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X, attacker.Y - r))
                                        {
                                            destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                        }
                                        else
                                        {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                            stopattile = true;
                                        }

                                        if (stopattile)
                                        {
                                            break;
                                        }

                                        for (int s = 1; s <= r; s++)
                                        {

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X - s, attacker.Y - r), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X - s, attacker.Y - r))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                                stopattile = true;
                                            }

                                            if (stopattile)
                                            {
                                                break;
                                            }

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X + s, attacker.Y - r), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X + s, attacker.Y - r))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + s) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - r) + attacker.Offset.Y));
                                                stopattile = true;
                                            }

                                            if (stopattile)
                                            {
                                                break;
                                            }

                                        }

                                    }
                                    break;
                                case Enums.Direction.Left:
                                    {
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X - r, attacker.Y), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y) + attacker.Offset.Y));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X - r, attacker.Y))
                                        {
                                            destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y) + attacker.Offset.Y));
                                        }
                                        else
                                        {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y) + attacker.Offset.Y));
                                            stopattile = true;
                                        }

                                        if (stopattile)
                                        {
                                            break;
                                        }

                                        for (int s = 1; s <= r; s++)
                                        {

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X - r, attacker.Y - s), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - s) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X - r, attacker.Y - s))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - s) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - s) + attacker.Offset.Y));
                                                stopattile = true;
                                            }

                                            if (stopattile)
                                            {
                                                break;
                                            }

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X - r, attacker.Y + s), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + s) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X - r, attacker.Y + s))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + s) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X - r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + s) + attacker.Offset.Y));
                                                stopattile = true;
                                            }

                                            if (stopattile)
                                            {
                                                break;
                                            }

                                        }
                                    }
                                    break;
                                case Enums.Direction.Right:
                                    {
                                        if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X + r, attacker.Y), Enums.MapID.Active)) {
                                            destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y) + attacker.Offset.Y));
                                        } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X + r, attacker.Y))
                                        {
                                            destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y) + attacker.Offset.Y));
                                        }
                                        else
                                        {
                                            destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y) + attacker.Offset.Y));
                                            stopattile = true;
                                        }

                                        if (stopattile)
                                        {
                                            break;
                                        }

                                        for (int s = 1; s <= r; s++)
                                        {

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X + r, attacker.Y - s), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - s) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X + r, attacker.Y - s))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - s) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y - s) + attacker.Offset.Y));
                                                stopattile = true;
                                            }

                                            if (stopattile)
                                            {
                                                break;
                                            }

                                            if (!Screen.ScreenRenderer.CanBeSeen(new Point(attacker.X + r, attacker.Y + s), Enums.MapID.Active)) {
                                                destData.Blit(srfcMoveTargetTileDark, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + s) + attacker.Offset.Y));
                                            } else if (!IsRenderingTargetOnSprite(Logic.Maps.MapHelper.ActiveMap,
                                            attacker.X + r, attacker.Y + s))
                                            {
                                                destData.Blit(srfcMoveTargetTile, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + s) + attacker.Offset.Y));
                                            }
                                            else
                                            {
                                                destData.Blit(srfcMoveTargetTileHit, new Point(Screen.ScreenRenderer.ToTileX(attacker.X + r) + attacker.Offset.X, Screen.ScreenRenderer.ToTileY(attacker.Y + s) + attacker.Offset.Y));
                                                stopattile = true;
                                            }

                                            if (stopattile)
                                            {
                                                break;
                                            }

                                        }
                                    }
                                    break;
                            }

                            if (stopattile)
                            {
                                break;
                            }

                        }
                        #endregion
                    }
                    break;
            }
        }

        static bool ShouldContinueRenderingTargettingDisplay(Logic.Maps.Map activeMap, int x, int y) {
            if (x < 0 || y < 0 || x > activeMap.MaxX || y > activeMap.MaxY) {
                return false;
            }
            if (GameProcessor.IsBlocked(activeMap, x, y)) {
                return false;
            } else {
                return true;
            }
        }

        static bool IsRenderingTargetOnSprite(Logic.Maps.Map activeMap, int x, int y) {
            for (int i = 0; i < activeMap.MapNpcs.Length; i++) {
                if (activeMap.MapNpcs[i].Num > 0 && activeMap.MapNpcs[i].ScreenActive &&
                    activeMap.MapNpcs[i].X == x &&
                    activeMap.MapNpcs[i].Y == y) {
                    return true;
                }
            }

            if (activeMap.Players != null) {
                for (int i = 0; i < activeMap.Players.Count; i++) {
                    if (activeMap.Players[i].ScreenActive && activeMap.Players[i].X == x &&
                        activeMap.Players[i].Y == y) {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion Methods

    }
}