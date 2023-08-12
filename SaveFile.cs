using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SaveMyRPGClient
{
    
    public struct SaveFileChunk {

        public UInt32 TotalChunks;
        public UInt32 ChunkNumber;
        public UInt32 ChunkSize;
        public byte[] Data;

        public SaveFileChunk(byte[] chunk) {
            TotalChunks = BitConverter.ToUInt32(chunk,0);
            ChunkNumber = BitConverter.ToUInt32(chunk, 4);
            ChunkSize = BitConverter.ToUInt32(chunk, 8);
            Data = new byte[ChunkSize];
            Array.Copy(chunk, 12, Data, 0, ChunkSize);
        }
        public SaveFileChunk()
        {
            Data = new byte[1];
        }

        public void PrintSaveChunkInfo() {
            Console.WriteLine($"Total Chunks: {TotalChunks}");
            Console.WriteLine($"Chunk Number: {ChunkNumber}");
            Console.WriteLine($"Chunk Size:  {ChunkSize}");

        }

        public byte[] Serialize() {
            byte[] chunk = new byte[512012];
            BitConverter.GetBytes(TotalChunks).CopyTo(chunk, 0);
            BitConverter.GetBytes(ChunkNumber).CopyTo(chunk, 4);
            BitConverter.GetBytes(ChunkSize).CopyTo(chunk, 8);
            Data.CopyTo(chunk, 12);
            return chunk;
        }

        static public SaveFileChunk Deserialize(byte[] chunk) {
            SaveFileChunk sfc = new SaveFileChunk(chunk);
            return sfc;
            
        }

        static public byte[] Serialize(SaveFileChunk sfc)
        {
            return sfc.Serialize();
        }


    }
    internal class SaveFile
    {

    }
}
