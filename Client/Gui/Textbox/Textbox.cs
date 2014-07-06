namespace Client.Logic.Gui.Textbox
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	using Gfx = SdlDotNet.Graphics;

	class Textbox : Core.Control
	{
		#region Fields

		private Color mBackColor = Color.White;
		private Gfx.Surface mBackground;

		//private List<TextBoxChar> mChars;
		private int mCursorLocX = 0;
		private int mCursorLocY = 0;
		private int mCursorLocYExtra = 0;
		private bool mDoLineDraw = true;
		private Gfx.Font mFont;
		private Color mForeColor = Color.Black;
		private int mLastLineBlit = 0;
		private Size mLetterSize;
		private List<TextboxLine> mLines;
		private int mMaxX = -1;
		private int mMaxY = -1;
		private bool mMultiLine = false;
		private StringBuilder mText;
		private int mVisibleX = -1;
		private int mVisibleX2 = -1;
		private int mVisibleY = -1;
		private int mVisibleY2 = -1;
		private char mPasswordChar = '\0';

		#endregion Fields

		#region Constructors

		public Textbox()
			: base()
		{
			//mChars = new List<TextBoxChar>();
			mLines = new List<TextboxLine>();
			mFont = Logic.Graphics.FontManager.TextBoxFont;
			mLetterSize = mFont.SizeText(" ");
			mText = new StringBuilder();
			this.OnKeyDown += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(Textbox_OnKeyDown);
			this.OnClick += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(Textbox_OnClick);
			Redraw();
		}

		#endregion Constructors

		#region Events

		public event EventHandler<Events.KeyDownEventArgs> KeyDown;

		#endregion Events

		#region Properties

		public Color BackColor
		{
			get { return mBackColor; }
			set {
				mBackColor = value;
				Redraw();
			}
		}

		public Color ForeColor
		{
			get { return mForeColor; }
			set {
				mForeColor = value;
				Redraw();
			}
		}

		public bool MultiLine
		{
			get { return mMultiLine; }
			set {
				mMultiLine = value;
				Redraw();
			}
		}

		public new Size Size
		{
			get { return base.Size; }
			set {
				Size newSize = new Size(Logic.Math.RoundToMultiple(value.Width, mLetterSize.Width), Logic.Math.RoundToMultiple(value.Height, mLetterSize.Height));
				newSize.Width += 4;
				base.Size = newSize;
				mMaxX = base.Size.Width / mLetterSize.Width;
				if (mVisibleX == -1 && mVisibleX2 == -1) {
					mVisibleX = 0;
					mVisibleX2 = mMaxX;
				} else {
					if (mVisibleX > mMaxX) {
						mVisibleX = mMaxX;
					}
					if (mVisibleX2 > mMaxX) {
						mVisibleX2 = mMaxX;
					}
				}

				mMaxY = base.Size.Height / mLetterSize.Height;
				if (mVisibleY == -1 && mVisibleY2 == -1) {
					mVisibleY = 0;
					mVisibleY2 = mMaxY;
				} else {
					if (mVisibleY > mMaxY) {
						mVisibleY = mMaxY;
					}
					if (mVisibleY2 > mMaxY) {
						mVisibleY2 = mMaxY;
					}
				}
				Redraw();
			}
		}
		
		public char PasswordChar {
			get { return mPasswordChar; }
			set {
				mPasswordChar = value;
				Redraw();
			}
		}

		public string Text
		{
			get { return mText.ToString(); }
			set {
				mText = new StringBuilder();
				ClearLines();
				AppendText(null, value);
				Redraw();
			}
		}

		#endregion Properties

		#region Methods

		public void AppendText(List<CharOptions> charOptions, string text)
		{
			if (mLines.Count == 0) {
				mLines.Add(new TextboxLine(base.Size.Width, mFont, mForeColor));
			}

			int currentLine = mLines.Count - 1;

			for (int i = 0; i < text.Length; i++) {
				if (text[i] != '\n') {
					Size letterSize = mFont.SizeText(text[i].ToString());
					if (mLines[currentLine].LineFull(letterSize.Width)) {
						mLines.Add(new TextboxLine(base.Size.Width, mFont, mForeColor));
						currentLine++;
					}
					if (charOptions != null && charOptions.Count > i) {
						mLines[currentLine].AddChar(text[i].ToString(), charOptions[i]);
					} else {
						mLines[currentLine].AddChar(text[i].ToString());
					}
				} else {
					mLines[currentLine].AddChar("\n");
					mLines.Add(new TextboxLine(base.Size.Width, mFont, mForeColor));
					currentLine++;
				}
			}

			mText.Append(text);
			Redraw();
		}

		public override void Close()
		{
			base.Close();
		}

		public int GetCharNumFromCursorLoc()
		{
			return Convert.ToInt32((mCursorLocY + mCursorLocYExtra) * mMaxX + mCursorLocX);// - (mCursorLocYExtra * (mMaxX));
		}

		public int GetCharNumFromXY(int x, int y)
		{
			x = x / mLetterSize.Width;
			y = y / mLetterSize.Height;
			return Convert.ToInt32(y * mMaxX + x);
		}

		public Point GetXYFromCharNum(int charNum)
		{
			Point returnVal = new Point((charNum - Convert.ToInt32(charNum / mMaxX) * mMaxX) * mLetterSize.Width,
			                            Convert.ToInt32(charNum / mMaxX) * mLetterSize.Height);

			while (returnVal.X > mMaxX) {
				returnVal.Y++;
				returnVal.X -= mMaxX;
			}
			return returnVal;
		}

		public void ScrollToBottom()
		{
			if (mVisibleY != System.Math.Max(0, (mLines.Count) - mMaxY)) {
				mVisibleY = System.Math.Max(0, (mLines.Count) - mMaxY);
				Redraw();
			}
		}

		public override void Update(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
		{
			if (SdlDotNet.Core.Timer.TicksElapsed > mLastLineBlit + 500) {
				mDoLineDraw = !mDoLineDraw;
				mLastLineBlit = e.Tick;
			}
			//if (base.Focused) {
			//    if (mDoLineDraw) {
			//        //if (base.PointInBounds(new Point(((mCursorLocX * (mPrevLetterSize.Width)) + 4) + addX, (((mCursorLine * mLineSize.Height))) + addY)) && base.PointInBounds(new Point((mCursorLocX * (mPrevLetterSize.Width) + 4) + addX, (mCursorLine * mLineSize.Height + mLineSize.Height) + addY))) {
			//        Redraw();
			//        mLineCleared = false;
			//        //}
			//    } else {
			//        if (mLineCleared == false) {
			//            Redraw();
			//            mLineCleared = true;
			//        }
			//    }
			//}
			base.Update(dstSrf, e);
		}

		private void ClearLines()
		{
			for (int i = 0; i < mLines.Count; i++) {
				mLines[i].Chars.Clear();
			}
			mLines.Clear();
			mCursorLocX = 0;
			mCursorLocY = 0;
		}

		private int GetCharNumFromExactXY(int x, int y)
		{
			return Convert.ToInt32((y - mCursorLocYExtra) * mMaxX + x);
		}

		private void InsertText(string text, int lineNum, int charNum)
		{
			if (mLines.Count == 0) {
				mLines.Add(new TextboxLine(base.Size.Width, mFont, mForeColor));
			}

			if (lineNum < mLines.Count) {
				int currentLine = lineNum;

				for (int i = 0; i < text.Length; i++) {
					if (text[i] != '\n') {
						Size letterSize = mFont.SizeText(text[i].ToString());
						if (mLines[currentLine].LineFull(letterSize.Width)) {
							mLines.Add(new TextboxLine(base.Size.Width, mFont, mForeColor));
							currentLine++;
							mCursorLocY++;
							charNum = 0;
							mCursorLocX = 0;
						}
						mLines[currentLine].InsertChar(text[i].ToString(), charNum, null);
						mCursorLocX++;
						charNum++;
						PushChar(currentLine);
					} else {
						mLines[currentLine].InsertChar("\n", charNum, null);
						mLines.Add(new TextboxLine(base.Size.Width, mFont, mForeColor));
						currentLine++;
						mCursorLocY++;
						mCursorLocX = 0;
						charNum = 0;
					}
				}
			}
			if (mLines.Count == 0) {
				mLines.Add(new TextboxLine(base.Size.Width, mFont, mForeColor));
			}

			mText.Append(text);
		}

		private void PushChar(int lineNum)
		{
			if (mLines.Count < lineNum) {
				mLines.Add(new TextboxLine(base.Size.Width, mFont, mForeColor));
			}
			if (mLines[lineNum].LineFull()) {
				TextBoxChar charToSwitch = mLines[lineNum].Chars[mLines[lineNum].Chars.Count - 1];
				mLines[lineNum].Chars.Remove(charToSwitch);
				mLines[lineNum].RecalculateWidth();
				if (mLines.Count < lineNum + 1) {
					mLines.Add(new TextboxLine(base.Size.Width, mFont, mForeColor));
				}
				mLines[lineNum + 1].Chars.Insert(0, charToSwitch);
				mLines[lineNum + 1].RecalculateWidth();
				if (mLines[lineNum + 1].LineFull()) {
					PushChar(lineNum + 1);
				}
			}
		}
		
		private void RemoveChar(int lineNum, int charNum)
		{
			mLines[lineNum].RemoveChar(System.Math.Max(0, charNum - 1));
			int charsInLine = 0;
			for (int i = 0; i < lineNum; i++) {
				charsInLine += mLines[i].Chars.Count;
			}
			charsInLine += System.Math.Max(0, charNum - 1);
			mText.Remove(charsInLine, 1);
			mLines[lineNum].RecalculateWidth();
			PullChar(lineNum, System.Math.Max(0, charNum - 1));
		}
		
		private void PullChar(int lineNum, int charNum) {
			if (mLines.Count > lineNum + 1) {
				if (mLines[lineNum + 1].Chars.Count == 0) {
					mLines.RemoveAt(lineNum + 1);
				}
			}
			mLines[lineNum].RecalculateWidth();
			if (mLines[lineNum].LineFull() == false && mLines.Count > lineNum + 1) {
				TextBoxChar charToSwitch = mLines[lineNum + 1].Chars[0];
				mLines[lineNum + 1].Chars.Remove(charToSwitch);
				mLines[lineNum + 1].RecalculateWidth();
				mLines[lineNum].Chars.Insert(mLines[lineNum].Chars.Count - 1, charToSwitch);
				mLines[lineNum].RecalculateWidth();
				mLines[lineNum + 1].RecalculateWidth();
				if (mLines[lineNum].LineFull() == false) {
					PullChar(lineNum, charNum + 1);
				}
			}
		}
		
		private void Redraw()
		{
			try {
				base.Buffer.Fill(mBackColor);
				if (mBackground != null) {
					mBackground.Close();
					mBackground.Dispose();
				}
				mBackground = new SdlDotNet.Graphics.Surface(this.Size);
				mBackground.TransparentColor = Color.Transparent;
				mBackground.Transparent = true;
				mBackground.Fill(mBackColor);
				int lastY = 2;
				// Draw each line
				for (int i = mVisibleY; i < mVisibleY + mMaxY; i++) {
					if (mLines.Count > i) {
						SdlDotNet.Graphics.Surface lineSurf;
						if (mPasswordChar == '\0') {
							lineSurf = mLines[i].Render();
						} else {
							lineSurf = mLines[i].RenderPassword(mPasswordChar);
						}
						mBackground.Blit(lineSurf, new Point(2, lastY));
						lineSurf.Close();
						lastY += mFont.Height;
					} else
						break;
				}
				base.Buffer.Blit(mBackground);
				mBackground.Close();
				//if (mDoLineDraw) {
				//    Gfx.IPrimitive line = new Gfx.Primitives.Line(new Point((((mCursorLocX - mVisibleX) * (mLetterSize.Width))), (((((mCursorLocY) - mVisibleY) * mLetterSize.Height)) - 2)), new Point(((mCursorLocX - mVisibleX) * (mLetterSize.Width)), (((mCursorLocY) - mVisibleY) * mLetterSize.Height + mLetterSize.Height)));
				//    base.Buffer.Draw(line, Color.Blue);
				//}
			} catch (Exception ex) {
				Console.WriteLine(ex.ToString());
			}
		}

		void Textbox_OnClick(object sender, SdlDotNet.Input.MouseButtonEventArgs e)
		{
			//Point clickedPoint = base.ControlPointFromScreenPoint(e.Position);
			//mCursorLocX = (clickedPoint.X / mLetterSize.Width);
			//mCursorLocY = (clickedPoint.Y / mLetterSize.Height) - 1;
			//if (GetCharNumFromCursorLoc() > mChars.Count) {
			//    mCursorLocX = (mChars.Count) % mMaxX;
			//    mCursorLocY = (mChars.Count) / mMaxX;
			//}
		}

		void Textbox_OnKeyDown(object sender, SdlDotNet.Input.KeyboardEventArgs e)
		{
			if (KeyDown != null) {
				Events.KeyDownEventArgs keyDown = new Client.Logic.Gui.Events.KeyDownEventArgs(e, false);
				KeyDown(this, keyDown);
				if (keyDown.Cancel) {
					return;
				}
			}
			switch (e.Key) {
					//    case SdlDotNet.Input.Key.UpArrow: {
					//            if (mMultiLine) {
					//                if (mCursorLocY != 0) {
					//                    mCursorLocY--;
					//                    if (mCursorLocY + 1 >= mMaxY) {
					//                        mVisibleY--;
					//                        mVisibleY2--;
					//                    }
					//                }
					//            }
					//        }
					//        break;
					//    case SdlDotNet.Input.Key.DownArrow: {
					//            if (mMultiLine) {
					//                if (mCursorLocY < mChars.Count / mMaxX) {
					//                    mCursorLocY++;
					//                    if (mCursorLocY + 1 >= mVisibleY2) {
					//                        mVisibleY++;
					//                        mVisibleY2++;
					//                    }
					//                }
					//            }
					//        }
					//        break;
					//    case SdlDotNet.Input.Key.LeftArrow: {
					//            if (mMultiLine == false) {
					//                if (mCursorLocX != 0) {
					//                    mCursorLocX--;
					//                    if (mCursorLocX < mVisibleX) {
					//                        mVisibleX--;
					//                        mVisibleX2--;
					//                    }
					//                }
					//            } else {
					//                if (mCursorLocX - 1 <= 0 && mCursorLocY != 0) {
					//                    mCursorLocX = mMaxX;
					//                    mCursorLocY--;
					//                    if (mCursorLocY + 1 >= mMaxY) {
					//                        mVisibleY--;
					//                        mVisibleY2--;
					//                    }
					//                } else {
					//                    if (mCursorLocX != 0) {
					//                        mCursorLocX--;
					//                    }
					//                }
					//            }
					//        }
					//        break;
					//    case SdlDotNet.Input.Key.RightArrow: {
					//            if (GetCharNumFromCursorLoc() != mChars.Count) {
					//                if (mMultiLine) {
					//                    if (mCursorLocX + 1 > mMaxX) {
					//                        mCursorLocY++;
					//                        if (mCursorLocY >= mMaxY) {
					//                            mVisibleY++;
					//                            mVisibleY2++;
					//                        }
					//                        mCursorLocX = 1;
					//                    } else {
					//                        mCursorLocX++;
					//                    }
					//                } else {
					//                    mCursorLocX++;
					//                    if (mCursorLocX > mVisibleX2) {
					//                        mVisibleX++;
					//                        mVisibleX2++;
					//                    }
					//                }
					//            }
					//        }
					//        break;
					//    case SdlDotNet.Input.Key.Backspace: {
					//            int textPos = GetCharNumFromCursorLoc();
					//            if (textPos - 1 >= 0) {
					//                mText.Remove(textPos - 1, 1);
					//                mChars.RemoveAt(textPos - 1);

					//                if (mMultiLine == false) {
					//                    if (mCursorLocX != 0) {
					//                        mCursorLocX--;
					//                    }
					//                } else {
					//                    if (mCursorLocX - 1 <= 0 && mCursorLocY != 0) {
					//                        mCursorLocX = mMaxX;
					//                        mCursorLocY--;
					//                        if (mCursorLocY + 1 >= mMaxY) {
					//                            mVisibleY--;
					//                            mVisibleY2--;
					//                        }
					//                    } else {
					//                        if (mCursorLocX != 0) {
					//                            mCursorLocX--;
					//                        }
					//                    }
					//                }
					//                Redraw();
					//            }
					//            //int textPos = textLoc;
					//            //if (textPos - 1 >= 0) {
					//            //    Text = mText.Remove((mCursorLocX + (mCursorLine * mLineLength)) - 1, 1);
					//            //    if (mCursorLocX - 1 < 0) {
					//            //        mCursorLine--;
					//            //        mCursorLocX = mLineLength - 1;
					//            //    } else {
					//            //        mCursorLocX--;
					//            //    }
					//            //    UpdateCaretLoc();
					//            //}
					//        }
					//        break;
					//    case SdlDotNet.Input.Key.Delete: {
					//            //if (mText.Length > textLoc) {
					//            //    Text = mText.Remove(textLoc, 1);
					//            //}
					//        }
					//        break;
					//    case SdlDotNet.Input.Key.Space: {
					//            mText.Insert(GetCharNumFromCursorLoc(), " ");
					//            mChars.Insert(GetCharNumFromCursorLoc(), new TextBoxChar(" ", Color.Empty, mFont.SizeText(" ")));
					//            if (mMultiLine) {
					//                if (mCursorLocX + 1 > mMaxX) {
					//                    mCursorLocY++;
					//                    if (mCursorLocY >= mMaxY) {
					//                        mVisibleY++;
					//                        mVisibleY2++;
					//                    }
					//                    mCursorLocX = 1;
					//                } else {
					//                    mCursorLocX++;
					//                }
					//            } else {
					//                mCursorLocX++;
					//            }
					//            if (mMultiLine == false) {
					//                if (mChars.Count > mMaxX) {
					//                    mVisibleX++;
					//                    mVisibleX2++;
					//                }
					//            }
					//        }
					//        break;
					//    case SdlDotNet.Input.Key.Return: {
					//            if (mMultiLine) {
					//                mText.Insert(GetCharNumFromCursorLoc(), "\n");
					//                mChars.Insert(GetCharNumFromCursorLoc(), new TextBoxChar("\n", Color.Empty, mFont.SizeText("\n")));
					//                //for (int i = mCursorLocX + 1; i < mMaxX; i++) {
					//                //    mText.Insert((mCursorLocY * mMaxX) + i, "\0");
					//                //    mChars.Insert((mCursorLocY * mMaxX) + i, new TextBoxChar("\0", Color.Empty, mFont.SizeText("\0")));
					//                //}
					//                mCursorLocYExtra++;
					//                if (mCursorLocY + mCursorLocYExtra + 1 >= mMaxY) {
					//                    mVisibleY++;
					//                    mVisibleY2++;
					//                }
					//                mCursorLocX = 0;
					//            }
					//        }
					//        break;
					//    default: {
					//            string charToAdd = "";
					//            charToAdd = Input.Keyboard.GetCharString(e);
					//            if (charToAdd.Length == 1) {
					//                int charNum = GetCharNumFromCursorLoc();
					//                //if (charNum != mChars.Count - 1) {
					//                //    mText.Insert(charNum, charToAdd);
					//                //} else {
					//                mText.Append(charToAdd);
					//                //}
					//                if (mMultiLine) {
					//                    if (mCursorLocX + 1 > mMaxX) {
					//                        mCursorLocY++;
					//                        if (mCursorLocY >= mMaxY) {
					//                            mVisibleY++;
					//                            mVisibleY2++;
					//                        }
					//                        mCursorLocX = 1;
					//                    } else {
					//                        mCursorLocX++;
					//                    }
					//                } else {
					//                    mCursorLocX++;
					//                }
					//                //if (charNum != mChars.Count - 1) {
					//                //    mChars.Insert(charNum, new TextBoxChar(Input.Keyboard.GetCharString(e), Color.Empty, mFont.SizeText(Input.Keyboard.GetCharString(e))));
					//                //} else {
					//                mChars.Add(new TextBoxChar(Input.Keyboard.GetCharString(e), Color.Empty, mFont.SizeText(Input.Keyboard.GetCharString(e))));
					//                //}
					//                //UpdateCaretLoc();
					//                if (mMultiLine == false) {
					//                    if (mChars.Count > mMaxX) {
					//                        mVisibleX++;
					//                        mVisibleX2++;
					//                    }
					//                }
					//            }
					//        }
					//        break;
					case SdlDotNet.Input.Key.Backspace: {
						RemoveChar(mCursorLocY, mCursorLocX);
						mCursorLocX--;
						if (mCursorLocX < 1) {
							mCursorLocY = System.Math.Max(0, mCursorLocY - 1);
							mCursorLocX = mLines[mCursorLocY].Chars.Count;
						}
						mCursorLocX = System.Math.Max(0, mCursorLocX);
						break;
					}
					case SdlDotNet.Input.Key.Space: {
						InsertText(" ", mCursorLocY, mCursorLocX);
						ScrollToBottom();
						break;
					}
					default: {
						string key = Input.Keyboard.GetCharString(e);
						if (key.Length == 1) {
							InsertText(key, mCursorLocY, mCursorLocX);
							ScrollToBottom();
						}
					}
					break;
			}

			////mChars.Add(new TextBoxChar(Input.Keyboard.GetCharString(e), Color.Gray, mFont.SizeText(Input.Keyboard.GetCharString(e))));
			Redraw();
		}

		#endregion Methods

		#region Other

		//private bool mWordWrap;

		#endregion Other
	}
}