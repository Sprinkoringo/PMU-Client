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


using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;
using Client.Logic.Graphics.Renderers.Screen;
using SdlDotNet.Graphics.Primitives;
using Client.Logic.Maps;


namespace Client.Logic.Graphics.Renderers.Sprites
{
    class SpriteRenderer
    {
        //public static List<SpeechBubble> SpeechBubbles;

        public static void Initialize() {
            //SpeechBubbles = new List<SpeechBubble>();
        }

        public static string GetSpriteFormString(ISprite sprite) {
            string formString = "r";

            if (sprite.Form >= 0) {
                formString += "-" + sprite.Form;
                if ((int)sprite.Shiny >= 0) {
                    formString += "-" + (int)sprite.Shiny;
                    if ((int)sprite.Sex >= 0) {
                        formString += "-" + (int)sprite.Sex;
                    }
                }
            }
            return formString;
        }

        public static void DrawSpeechBubble(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, ISprite sprite, int tick) {
            if (sprite.CurrentSpeech != null) {
                if (sprite.CurrentSpeech.MarkedForRemoval == false) {
                    sprite.CurrentSpeech.Process(tick);
                    if (sprite.CurrentSpeech.RedrawRequested) {
                        sprite.CurrentSpeech.DrawBuffer();
                    }
                    int startX = (sprite.X * Constants.TILE_WIDTH) + sprite.Offset.X - (sprite.CurrentSpeech.Buffer.Width / 2) + 16;
                    int startY = ((sprite.Y + 1) * Constants.TILE_HEIGHT) + sprite.Offset.Y;
                    destData.Blit(sprite.CurrentSpeech.Buffer, new Point(ScreenRenderer.ToScreenX(startX), ScreenRenderer.ToScreenY(startY)));
                } else {
                    sprite.CurrentSpeech.FreeResources();
                    sprite.CurrentSpeech = null;
                }
            }
        }

        public static void DrawSprite(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, ISprite sprite) {
            
            int x, y;
            
            int spriteNum = sprite.Sprite;
            
            if (Globals.FoolsMode) {
                if (spriteNum == 420) {
                    spriteNum = 867;
                } else if (spriteNum == 582 || spriteNum == 583 || spriteNum == 584) {
                    spriteNum = 787;
                }
            }

            SpriteSheet spriteSheet = sprite.SpriteSheet;
            if (spriteSheet == null || !(spriteSheet.Num == sprite.Sprite && spriteSheet.Form == GetSpriteFormString(sprite))) {
                spriteSheet = GraphicsManager.GetSpriteSheet(spriteNum, sprite.Form, (int)sprite.Shiny, (int)sprite.Sex);

                sprite.SpriteSheet = spriteSheet;
            }

            Surface spriteToBlit = null;
            if (spriteSheet == null) {
                return;
            }

            Rectangle rec = Rectangle.Empty;
            bool moving = false;


            if (sprite.Attacking == false && sprite.WalkingFrame != -1) {
                int currentOffset = 0;
                switch (sprite.Direction) {
                    case Enums.Direction.Up: {
                            currentOffset = sprite.Offset.Y;
                        }
                        break;
                    case Enums.Direction.Down: {
                            currentOffset = sprite.Offset.Y * -1;
                        }
                        break;
                    case Enums.Direction.Left: {
                            currentOffset = sprite.Offset.X;
                        }
                        break;
                    case Enums.Direction.Right: {
                            currentOffset = sprite.Offset.X * -1;
                        }
                        break;
                }
                int frameCount = spriteSheet.FrameData.GetFrameCount(FrameType.Walk, sprite.Direction);
                while (sprite.MovementSpeed != Enums.MovementSpeed.Standing && Globals.Tick - sprite.LastWalkTime > 512 / GameProcessor.DetermineSpeed(sprite.MovementSpeed)) {
                    sprite.LastWalkTime += (512 / GameProcessor.DetermineSpeed(sprite.MovementSpeed));
                    sprite.WalkingFrame = (sprite.WalkingFrame + 1) % frameCount;
                }
                spriteToBlit = spriteSheet.GetSheet(FrameType.Walk, sprite.Direction);
                rec = spriteSheet.GetFrameBounds(FrameType.Walk, sprite.Direction, sprite.WalkingFrame);
            }

            if (sprite.Attacking && sprite.TotalAttackTime > 0) {
                //if there's more than one attack frame, we have a fluid motion
                if (spriteSheet.FrameData.GetFrameCount(FrameType.Attack, sprite.Direction) > 1 && (sprite.TotalAttackTime - sprite.AttackTimer + Globals.Tick) / sprite.TotalAttackTime < 1) {
                    spriteToBlit = spriteSheet.GetSheet(FrameType.Attack, sprite.Direction);
                    rec = spriteSheet.GetFrameBounds(FrameType.Attack, sprite.Direction,
                        (sprite.TotalAttackTime - sprite.AttackTimer + Globals.Tick) * spriteSheet.FrameData.GetFrameCount(FrameType.Attack, sprite.Direction) / sprite.TotalAttackTime);
                } else if (sprite.AttackTimer - Globals.Tick > sprite.TotalAttackTime / 2) {
                    spriteToBlit = spriteSheet.GetSheet(FrameType.Attack, sprite.Direction);
                    rec = spriteSheet.GetFrameBounds(FrameType.Attack, sprite.Direction, 0);
                }
            }
            
            // Check to see if we want to stop making him attack
            if (sprite.AttackTimer < Globals.Tick) {
                sprite.Attacking = false;
                sprite.AttackTimer = 0;
            }

            Point dstPoint = new Point();

            x = sprite.Location.X;// * Const.PIC_X + sx + player.XOffset;
            y = sprite.Location.Y;// * Const.PIC_Y + sx + player.YOffset;

            if (y < 0) {
                y = 0;
                //rec.Y = rec.Y + (y * -1);
            }

            Renderers.Maps.SeamlessWorldHelper.ConvertCoordinatesToBorderless(activeMap, targetMapID, ref x, ref y);

            dstPoint.X = ScreenRenderer.ToTileX(x) + sprite.Offset.X - (spriteSheet.FrameData.FrameWidth / 2 - 16);
            dstPoint.Y = ScreenRenderer.ToTileY(y) + sprite.Offset.Y - (spriteSheet.FrameData.FrameHeight - 32); // - (Constants.TILE_HEIGHT / 2);

            switch (sprite.StatusAilment) {
                case Enums.StatusAilment.Paralyze: {
                        dstPoint.X -= (2 + System.Math.Abs(Globals.Tick % 8 - 4));
                        break;
                    }
                default: {
                        //dstPoint.X = ScreenRenderer.ToTileX(x) + sprite.Offset.X;
                        break;
                    }
            }
            if (sprite.StatusAilment == Enums.StatusAilment.Sleep) {
                if (Globals.Tick > sprite.SleepTimer + 500) {
                    sprite.SleepTimer = Globals.Tick;
                    sprite.SleepFrame = (sprite.SleepFrame + 1) % spriteSheet.FrameData.GetFrameCount(FrameType.Sleep, Enums.Direction.Down);
                }
                spriteToBlit = spriteSheet.GetSheet(FrameType.Sleep, Enums.Direction.Down);
                rec = spriteSheet.GetFrameBounds(FrameType.Sleep, Enums.Direction.Down, sprite.SleepFrame);
            }

            if (!sprite.ScreenActive)
            {
                spriteToBlit = spriteSheet.GetSheet(FrameType.Sleep, Enums.Direction.Down);
                rec = spriteSheet.GetFrameBounds(FrameType.Sleep, Enums.Direction.Down, 0);
            }

            if ((sprite is Logic.Players.GenericPlayer || sprite is Logic.Players.MyPlayer))
            {
                if (((Logic.Players.IPlayer)sprite).Dead)
                {
                    spriteToBlit = spriteSheet.GetSheet(FrameType.Sleep, Enums.Direction.Down);
                    rec = spriteSheet.GetFrameBounds(FrameType.Sleep, Enums.Direction.Down, 0);
                }
            }

            if (rec == Rectangle.Empty && sprite.StatusAilment == Enums.StatusAilment.OK) {
                if (sprite.Offset == Point.Empty && spriteSheet.FrameData.GetFrameCount(FrameType.Idle, sprite.Direction) > 0) {
                    if (sprite.IdleTimer == -1) {
                        sprite.IdleTimer = Globals.Tick + 2000;
                    } else if (Globals.Tick > sprite.IdleTimer + 100) {
                        sprite.IdleTimer = Globals.Tick;

                        sprite.IdleFrame++;
                        if (sprite.IdleFrame >= spriteSheet.FrameData.GetFrameCount(FrameType.Idle, sprite.Direction)) {
                            sprite.IdleFrame = 0;
                        }
                        spriteToBlit = spriteSheet.GetSheet(FrameType.Idle, sprite.Direction);
                        rec = spriteSheet.GetFrameBounds(FrameType.Idle, sprite.Direction, sprite.IdleFrame);

                    }
                } else {
                    sprite.IdleTimer = -1;
                    sprite.IdleFrame = 0;
                }
            } else {
                sprite.IdleTimer = -1;
                sprite.IdleFrame = 0;
            }

            if (sprite.IdleTimer != -1) {
                spriteToBlit = spriteSheet.GetSheet(FrameType.Idle, sprite.Direction);
                rec = spriteSheet.GetFrameBounds(FrameType.Idle, sprite.Direction, sprite.IdleFrame);
            }

            if ((rec == Rectangle.Empty || (moving == true && sprite.Offset == Point.Empty)) && sprite.StatusAilment != Enums.StatusAilment.Sleep && sprite.IdleTimer == -1) {
                spriteToBlit = spriteSheet.GetSheet(FrameType.Walk, sprite.Direction);
                rec = spriteSheet.GetFrameBounds(FrameType.Walk, sprite.Direction, 0);
            }



            //if (sprite.Size == Enums.Size.Normal) {
            if (Globals.FoolsMode) {
                dstPoint.X = ScreenRenderer.ToTileX(x) + sprite.Offset.X;
                dstPoint.Y = ScreenRenderer.ToTileY(y) + sprite.Offset.Y;

                spriteNum = spriteNum % 872;
                rec = new Rectangle((spriteNum - (spriteNum / 6) * 6) * Constants.TILE_WIDTH,
                                               (spriteNum / 6) * Constants.TILE_HEIGHT, Constants.TILE_WIDTH, Constants.TILE_HEIGHT);
                spriteToBlit = Graphics.GraphicsManager.Items;
            }




            destData.Blit(spriteToBlit, dstPoint, rec);
            
            
            //spriteToBlit.AlphaBlending = false;


        }

        public static void DrawSpriteName(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, ISprite sprite, Color color, string name) {
            int textX;
            int textY;

            int x = sprite.Location.X;
            int y = sprite.Location.Y;
            Renderers.Maps.SeamlessWorldHelper.ConvertCoordinatesToBorderless(activeMap, targetMapID, ref x, ref y);

            //if (sprite.Size == Enums.Size.Normal) {
            textX = ScreenRenderer.ToTileX(x) + sprite.Offset.X + (Constants.TILE_WIDTH / 2) - (name.Length * 7 / 2);
            textY = ScreenRenderer.ToTileY(y) + sprite.Offset.Y - (Constants.TILE_HEIGHT / 2) - /*4*/ 32;
            TextRenderer.DrawText(destData, name, color, Color.Black, textX, textY);
            //} else {
            //    textX = ScreenRenderer.ToTileX(sprite.Location.X) + sprite.Offset.X + (Constants.TILE_WIDTH / 2) - ((name.Length / 2) * 8);
            //    textY = ScreenRenderer.ToTileY(sprite.Location.Y) + sprite.Offset.Y - (Constants.TILE_HEIGHT / 2) - 48;
            //    TextRenderer.DrawText(destData, name, color, Color.Black, textX, textY);
            //}
        }

        public static void DrawSpriteGuild(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, ISprite sprite, Color color, string guild) {
            int textX;
            int textY;

            int x = sprite.Location.X;
            int y = sprite.Location.Y;
            Renderers.Maps.SeamlessWorldHelper.ConvertCoordinatesToBorderless(activeMap, targetMapID, ref x, ref y);

            //if (sprite.Size == Enums.Size.Normal) {
            textX = ScreenRenderer.ToTileX(x) + sprite.Offset.X + (Constants.TILE_WIDTH / 2) - (guild.Length * 7 / 2);
            textY = ScreenRenderer.ToTileY(y) + sprite.Offset.Y - (Constants.TILE_HEIGHT / 2) - /*4*/ 50;
            TextRenderer.DrawText(destData, guild, color, Color.Black, textX, textY);
            //} else {
            //    textX = ScreenRenderer.ToTileX(sprite.Location.X) + sprite.Offset.X + (Constants.TILE_WIDTH / 2) - ((guild.Length / 2) * 8);
            //    textY = ScreenRenderer.ToTileY(sprite.Location.Y) + sprite.Offset.Y - (Constants.TILE_HEIGHT / 2) - 64;
            //    TextRenderer.DrawText(destData, guild, color, Color.Black, textX, textY);
            //}
        }

        public static void DrawSpriteHPBar(RendererDestinationData destData, ISprite sprite, int hp, int maxHP) {
            int x = 0;
            int y = 0;

            x = ScreenRenderer.ToTileX(sprite.Location.X) + sprite.Offset.X;
            y = ScreenRenderer.ToTileY(sprite.Location.Y) + sprite.Offset.Y;
            //if (sprite == ScreenRenderer.Camera.FocusedSprite) {
            //    x = ScreenRenderer.NewX;
            //    y = ScreenRenderer.NewY;
            //} else {

            //}

            //if (MaxInfo.SpriteSize == 0) {
            //    Box hpBox = new SdlDotNet.Graphics.Primitives.Box(new Point(x, y + 32), new Point(x + 50, y + 36));
            //    destSurf.Draw(hpBox, Color.Black, false, true);
            //    if (maxHP < 1) {
            //        hpBox = new SdlDotNet.Graphics.Primitives.Box(new Point(x, y + 32), new Point(x + ((hp / 100) / ((maxHP + 1) / 100) * 50), y + 36));
            //    } else {
            //        hpBox = new SdlDotNet.Graphics.Primitives.Box(new Point(x, y + 32), new Point(x + ((hp / 100) / (maxHP / 100) * 50), y + 36));
            //    }
            //    destSurf.Draw(hpBox, Color.LightGreen, false, true);
            //} else {
            Box hpBox = new SdlDotNet.Graphics.Primitives.Box(new Point(destData.Location.X + x, destData.Location.Y + y + 36), new Point(destData.Location.X + x + 32, destData.Location.Y + y + 40));
            destData.Draw(hpBox, Color.Black, false, true);
            if (maxHP > 0) {
                hpBox = new SdlDotNet.Graphics.Primitives.Box(new Point(destData.Location.X + x + 1, destData.Location.Y + y + 37), new Point(Convert.ToInt32(destData.Location.X + x + (Logic.MathFunctions.CalculatePercent(hp, maxHP) * 0.01) * 31), destData.Location.Y + y + 39));
            }
            destData.Draw(hpBox, Color.LightGreen, false, true);
            //}
        }

        public static void DrawStatus(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, ISprite sprite, int emoteIndex, Point offset) {
            DrawStatus(destData, activeMap, targetMapID, sprite, emoteIndex, Globals.Tick / 50 % 12, offset);
        }

        public static void DrawStatus(RendererDestinationData destData, Map activeMap, Enums.MapID targetMapID, ISprite sprite, int emoteIndex, int emoteFrame, Point offset) {

            int x, y;
            int width = Constants.TILE_WIDTH;
            int height = Constants.TILE_HEIGHT;


            Rectangle rec = new Rectangle();
            Point dstPoint = new Point();

            rec.X = emoteFrame * width;
            rec.Width = width;
            rec.Y = 0;
            rec.Height = height;

            x = sprite.Location.X;// * Const.PIC_X + sx + player.XOffset;
            y = sprite.Location.Y;// * Const.PIC_Y + sx + player.YOffset;

            Maps.SeamlessWorldHelper.ConvertCoordinatesToBorderless(activeMap, targetMapID, ref x, ref y);

            dstPoint.X = ScreenRenderer.ToTileX(x) + sprite.Offset.X + offset.X;
            dstPoint.Y = ScreenRenderer.ToTileY(y) + sprite.Offset.Y + offset.Y; // - (Constants.TILE_HEIGHT / 2);





            destData.Blit(Graphics.GraphicsManager.GetEmoteSheet(emoteIndex).Sheet, dstPoint, rec);

        }

    }
}
