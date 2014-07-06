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
