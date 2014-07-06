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


namespace Client.Logic.Items
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;

    class ItemHelper
    {
        #region Fields

        private static int dataLoadPercent = 0;
        private static ItemCollection mItems;

        #endregion Fields

        #region Properties

        public static int DataLoadPercent
        {
            get { return dataLoadPercent; }
        }

        public static ItemCollection Items
        {
            get { return mItems; }
        }

        #endregion Properties

        #region Methods

        public static void InitItemCollection()
        {
            mItems = new ItemCollection(MaxInfo.MaxItems);
        }

        public static void LoadItemsFromPacket(string[] parse)
        {
            try {
                int n, temp;
                n = 1;
                for (int i = 0; i < MaxInfo.MaxItems; i++) {
                    temp = Logic.MathFunctions.CalculatePercent(i, MaxInfo.MaxItems);
                    if (temp >= 100) {
                        temp = 99;
                    }
                    dataLoadPercent = temp;
                    Item item = new Item();
                    if (parse[n + 1] != "0") {
                        // Update the item
                        item.Name = parse[n + 1];
                        item.Desc = parse[n + 2];
                        item.Pic = parse[n + 3].ToInt();
                        item.Type = (Enums.ItemType)parse[n + 4].ToInt();
                        item.Data1 = parse[n + 5].ToInt();
                        item.Data2 = parse[n + 6].ToInt();
                        item.Data3 = parse[n + 7].ToInt();
                        item.Price = parse[n + 8].ToInt();
                        item.StackCap = parse[n + 9].ToInt();
                        item.Bound = parse[n + 10].ToBool();
                        item.Loseable = parse[n + 11].ToBool();
                        item.Rarity = parse[n + 12].ToInt();
                        item.AttackReq = parse[n + 13].ToInt();
                        item.DefenseReq = parse[n + 14].ToInt();
                        item.SpAtkReq = parse[n + 15].ToInt();
                        item.SpDefReq = parse[n + 16].ToInt();
                        item.SpeedReq = parse[n + 17].ToInt();
                        item.ScriptedReq = parse[n + 18].ToInt();
                        item.AddHP = parse[n + 19].ToInt();
                        item.AddPP = parse[n + 20].ToInt();
                        item.AddAttack = parse[n + 21].ToInt();
                        item.AddDefense = parse[n + 22].ToInt();
                        item.AddSpAtk = parse[n + 23].ToInt();
                        item.AddSpDef = parse[n + 24].ToInt();
                        item.AddSpeed = parse[n + 25].ToInt();
                        item.AddEXP = parse[n + 26].ToInt();
                        item.AttackSpeed = parse[n + 27].ToInt();
                        item.RecruitBonus = parse[n + 28].ToInt();
                        n += 29;
                    } else {
                        n += 2;
                    }

                    mItems.AddItem(i, item);
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