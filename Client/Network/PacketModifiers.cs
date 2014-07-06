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
using PMU.Sockets;
using Client.Logic.Security;
using System.IO;
using System.IO.Compression;

namespace Client.Logic.Network
{
    class PacketModifiers
    {
        Security.Encryption crypt;
        bool obtainedKey;

        bool encryptionEnabled = true;

        const string DEFAULT_KEY = "abcdefgh!6876b)(gjhgfy8u7y";//"abcdefgh76876bfgjhgfy8u7iy";

        public bool ObtainedKey {
            get { return obtainedKey; }
        }

        public PacketModifiers() {
            crypt = new Security.Encryption();
            crypt.SetKey(DEFAULT_KEY);
        }

        public void SetKey(string key) {
            if (string.IsNullOrEmpty(key)) {
                encryptionEnabled = false;
            } else {
                if (!obtainedKey) {
                    crypt = new Encryption(key);
                    obtainedKey = true;
                }
            }
        }

        //public string DecryptPacket(string data) {
        //    if (encryptionEnabled) {
        //        return crypt.DecryptData(data);
        //    } else {
        //        return data;
        //    }
        //}

        public byte[] DecryptPacket(byte[] packet) {
            return crypt.DecryptBytes(packet);
        }

        //public string EncryptPacket(IPacket packet) {
        //    if (encryptionEnabled) {
        //        return crypt.EncryptData(packet.PacketString);
        //    } else {
        //        return packet.PacketString;
        //    }
        //}

        public byte[] EncryptPacket(byte[] packet) {
            return crypt.EncryptBytes(packet);
        }

        public byte[] CompressPacket(byte[] packet) {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) {
                using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress, true)) {
                    gzip.Write(packet, 0, packet.Length);
                }
                return ms.ToArray();
            }
        }

        public byte[] DecompressPacket(byte[] packet) {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (GZipStream stream = new GZipStream(new MemoryStream(packet), CompressionMode.Decompress)) {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream()) {
                    int count = 0;
                    do {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0) {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

    }
}
