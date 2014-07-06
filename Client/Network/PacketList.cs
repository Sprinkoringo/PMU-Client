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
using PMU.Core;
using PMU.Sockets;

namespace Client.Logic.Network
{
    class PacketList
    {
        List<TcpPacket> packets;

        public List<TcpPacket> Packets {
            get { return packets; }
        }

        public PacketList() {
            packets = new List<TcpPacket>();
        }

        public void AddPacket(TcpPacket packet) {
            packets.Add(packet);
        }

        public byte[] CombinePackets() {
            ByteArray[] packetBytes = new ByteArray[packets.Count];
            int totalSize = 0;
            for (int i = 0; i < packets.Count; i++) {
                packetBytes[i] = new ByteArray(ByteEncoder.StringToByteArray(packets[i].PacketString));
                totalSize += packetBytes[i].Length() + GetPacketSegmentHeaderSize();
            }
            byte[] packet = new byte[totalSize];
            int position = 0;
            for (int i = 0; i < packetBytes.Length; i++) {
                // Add the size of the packet segment
                Array.Copy(ByteArray.IntToByteArray(packetBytes[i].Length()), 0, packet, position, 4);
                position += 4;
                // Add the packet data
                Array.Copy(packetBytes[i].ToArray(), 0, packet, position, packetBytes[i].Length());
                position += packetBytes[i].Length();
            }
            return packet;
        }

        public int GetPacketSegmentHeaderSize() {
            return
                4 // [int32] Size of the packet segment
                ;
        }
    }
}
