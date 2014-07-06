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


namespace Client.Logic.Security
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Class that creates MD5 hashes
    /// </summary>
    public class Hash
    {
        #region Methods

        /// <summary>
        /// Generates a MD5 hash based on the source text.
        /// </summary>
        /// <param name="SourceText">The text to hash.</param>
        /// <returns>The hashed text as a Base64 string.</returns>
        public static string GenerateMD5Hash(string SourceText) {
            SourceText = SourceText + "SALT";
            //Create a salted hash so its harder to use hashtables on it
            //Create an encoding object to ensure the encoding standard for the source text
            UnicodeEncoding Ue = new UnicodeEncoding();
            //Retrieve a byte array based on the source text
            byte[] ByteSourceText = Ue.GetBytes(SourceText);
            //Instantiate an MD5 Provider object
            MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider();
            //Compute the hash value from the source
            byte[] ByteHash = Md5.ComputeHash(ByteSourceText);
            //And convert it to String format for return
            return Convert.ToBase64String(ByteHash);
        }

        public static string GenerateSHA1Hash(string SourceText) {
            SourceText = SourceText + "SALT";
            //Create a salted hash so its harder to use hashtables on it
            //Create an encoding object to ensure the encoding standard for the source text
            UnicodeEncoding Ue = new UnicodeEncoding();
            //Retrieve a byte array based on the source text
            byte[] ByteSourceText = Ue.GetBytes(SourceText);
            //Instantiate an MD5 Provider object
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();
            //Compute the hash value from the source
            byte[] ByteHash = SHA1.ComputeHash(ByteSourceText);
            //And convert it to String format for return
            return Convert.ToBase64String(ByteHash);
        }

        #endregion Methods
    }
}