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


#region Header

/*
 * Created by SharpDevelop.
 * User: Pikachu
 * Date: 27/09/2009
 * Time: 11:47 AM
 *
 */

#endregion Header

namespace Client.Logic.Maps
{
	using System;

	/// <summary>
	/// Description of MapItem.
	/// </summary>
   [Serializable]
	internal class MapItem
	{
		#region Constructors

		public MapItem()
		{
		}

		#endregion Constructors

		#region Properties

		public bool Sticky
		{
			get; set;
		}

		public int Num
		{
			get; set;
		}

		public int Value
		{
			get; set;
		}

		public int X
		{
			get; set;
		}

		public int Y
		{
			get; set;
		}

		#endregion Properties
	}
}