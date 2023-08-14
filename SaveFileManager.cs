using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SaveMyRPGClient
{
    public struct SaveFile {
        public string FolderName { get; set; }
        public string LSV_Name { get; set; }
        public string Pic_Path { get; set; }

    }
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
    public class SaveFileManager
    {
        static public string default_path = @"C:\%USERPROFILE%\AppData\Local\Larian Studios\Baldur's Gate 3\PlayerProfiles\Public\Savegames\Story";
        static public List<DirectoryInfo> saves_list;
        SaveFileManager()
        {
            string[] save_folders = SaveFileManager.ListAllSaves();
            for (int i = 0; i < save_folders.Length; i++)
            {
                saves_list.Add ( new DirectoryInfo(save_folders[i]));
                Debug.WriteLine($"FolderName: {saves_list.Last().Name}");
                Debug.WriteLine($"Full Path: {saves_list.Last().FullName}");
                Debug.WriteLine($"Created: {saves_list.Last().CreationTimeUtc}");
                Debug.WriteLine($"Last Modified: {saves_list.Last().LastWriteTimeUtc}");
            }
        }

        static public string[] ListAllSaves() 
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Larian Studios\Baldur's Gate 3\PlayerProfiles\Public\Savegames\Story";
            Debug.Print(path);
            return Directory.GetDirectories(path,"*.",SearchOption.TopDirectoryOnly);


        }


    }
}
