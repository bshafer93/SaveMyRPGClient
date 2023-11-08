using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace SaveMyRPGClient
{

    public struct SaveFileChunk
    {

        public UInt32 TotalChunks;
        public UInt32 ChunkNumber;
        public UInt32 ChunkSize;
        public byte[] Data;

        public SaveFileChunk(byte[] chunk)
        {
            TotalChunks = BitConverter.ToUInt32(chunk, 0);
            ChunkNumber = BitConverter.ToUInt32(chunk, 4);
            ChunkSize = BitConverter.ToUInt32(chunk, 8);
            Data = new byte[ChunkSize];
            Array.Copy(chunk, 12, Data, 0, ChunkSize);
        }

        public byte[] Serialize()
        {
            byte[] chunk = new byte[512012];
            BitConverter.GetBytes(TotalChunks).CopyTo(chunk, 0);
            BitConverter.GetBytes(ChunkNumber).CopyTo(chunk, 4);
            BitConverter.GetBytes(ChunkSize).CopyTo(chunk, 8);
            Data.CopyTo(chunk, 12);
            return chunk;
        }


    }
    public class SaveFileManager
    {
        static public string default_path = @"C:\%USERPROFILE%\AppData\Local\Larian Studios\Baldur's Gate 3\PlayerProfiles\Public\Savegames\Story";
        static public List<DirectoryInfo> saves_list;

        static public string[] ListAllSaves()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Larian Studios\Baldur's Gate 3\PlayerProfiles\Public\Savegames\Story";
            Debug.Print(path);
            return Directory.GetDirectories(path, "*.", SearchOption.TopDirectoryOnly);


        }


    }
}
