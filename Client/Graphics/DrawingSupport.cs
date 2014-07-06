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
using System.Drawing;

namespace Client.Logic.Graphics
{
    class DrawingSupport
    {
        /// <summary>
        /// Gets the center point of two objects
        /// </summary>
        /// <param name="parentSize">The size of the parent object</param>
        /// <param name="childSize">The size of the child object</param>
        /// <returns>The center point of two objects</returns>
        public static Point GetCenter(Size parentSize, Size childSize) {
            return new Point((parentSize.Width / 2) - (childSize.Width / 2), (parentSize.Height / 2) - (childSize.Height / 2));
        }

        /// <summary>
        /// Gets the center X value of two objects
        /// </summary>
        /// <param name="parentWidth">The width of the parent object</param>
        /// <param name="childWidth">The width of the child object</param>
        /// <returns>The center X value of two objects</returns>
        public static int GetCenterX(int parentWidth, int childWidth) {
            return (parentWidth / 2) - (childWidth / 2);
        }

        /// <summary>
        /// Gets the center Y value of two objects
        /// </summary>
        /// <param name="parentHeight">The height of the parent object</param>
        /// <param name="childHeight">The height of the child object</param>
        /// <returns>The center Y value of two objects</returns>
        public static int GetCenterY(int parentHeight, int childHeight) {
            return (parentHeight / 2) - (childHeight / 2);
        }

        /// <summary>
        /// Checks if a point intersects with the specified rectangle
        /// </summary>
        /// <param name="pointToTest">The point to test</param>
        /// <param name="bounds">The rectangle used to determine if the point is inside</param>
        /// <returns>True if the point is inside the rectangle boundaries; otherwise, false</returns>
        public static bool PointInBounds(Point pointToTest, Rectangle bounds) {
            if (pointToTest.X >= bounds.X && pointToTest.Y >= bounds.Y && pointToTest.X - bounds.Location.X <= bounds.Width && pointToTest.Y - bounds.Location.Y <= bounds.Height) {
                return true;
            } else {
                return false;
            }
        }
    }
}
