using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using System.Windows.Media.Animation;
using System.Configuration;
using System.Text.Json;
using System.Net.Sockets;
using System.Threading;

namespace SaveMyRPGClient
{
    public class ServerInfo 
    {
        public string Name { get; set; }
        public DateTime LoggedAt { get; set; }
    }

    public class FullSaveFileJson {

        public string FolderName  { get; set; }
        public Int64 FolderSize   { get; set; }
        public string ZipDataB64  { get; set; }
    }

public class SMRPG_Client
    {

        public const string _ip = "https://savemyrpg.com";
        public const string _ipRaw = "savemyrpg.com";
        public HttpClient _client;

        public string default_path = @"%USERPROFILE%\AppData\Local\Larian Studios\Baldur's Gate 3\PlayerProfiles\Public\Savegames\Story";
        public string bg3_save_location;
        public string save_prefix = "Adam";
        public string temp_folder_path;

        public SMRPG_Client()
        {
            bg3_save_location = Environment.ExpandEnvironmentVariables(default_path);
            Debug.WriteLine("Save Location: " + bg3_save_location);
        }
        public bool init() {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_ip);


            /*
            HttpResponseMessage resp = await _client.GetAsync("/serverinfo");

            if(resp.IsSuccessStatusCode)
            {
                string content = await resp.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
            }
            */

            return true;
        }

        public async Task<string[]> ListLocalSaves() {

            string newest_save = new DirectoryInfo(bg3_save_location).GetDirectories().OrderByDescending(t => t.LastWriteTimeUtc).First().FullName;

            Debug.WriteLine(newest_save);

            return Directory.GetDirectories(bg3_save_location);
        }

        public async void CompressSave(string save_path) {

        }

        public async Task<string> GetLatestSave(string group_id = "")
        {

            List<DirectoryInfo> save_list = new DirectoryInfo(bg3_save_location).GetDirectories().OrderByDescending(t => t.LastWriteTimeUtc).ToList();

            string latest_save = save_list.First().FullName;


            latest_save = save_list.Find(s => s.FullName.EndsWith(group_id)).FullName;

            Debug.WriteLine($"Latest Save: {latest_save}");

            return latest_save;
        }

        public async Task<SaveFileChunk> RequestSaveChunk() {

            HttpResponseMessage response = await _client.GetAsync("/getchunk");
            response.EnsureSuccessStatusCode();
            byte[] chunk = await response.Content.ReadAsByteArrayAsync();
            SaveFileChunk c = SaveFileChunk.Deserialize(chunk);
            return c;

        }

        public async Task<FullSaveFileJson> RequestFullSaveFile()
        {
         
            try
            {
                HttpResponseMessage fullSaveResponse = await _client.GetAsync("/getfullsave");
                fullSaveResponse.EnsureSuccessStatusCode();
                byte[] content = await fullSaveResponse.Content.ReadAsByteArrayAsync();

                FullSaveFileJson fullSave = JsonSerializer.Deserialize<FullSaveFileJson>(content);

                Console.WriteLine($"Name: {fullSave.FolderName}");
                Console.WriteLine($"LoggedAt: {fullSave.FolderSize}");
                byte[] zip_file_decode = Convert.FromBase64String(fullSave.ZipDataB64);
                string zip_path = "C:\\Users\\brent\\Documents\\Programming\\SaveMyRPGClient\\bg_temp_saves\\" + fullSave.FolderName;
                string folder_path = "C:\\Users\\brent\\Documents\\Programming\\SaveMyRPGClient\\bg_save_extracts\\Norbertle-31112316728_smrpg";
                File.WriteAllBytes(zip_path, zip_file_decode);
                System.IO.Directory.CreateDirectory(folder_path);
                ZipFile.ExtractToDirectory(zip_path, folder_path);
                return fullSave;

            }
            catch (HttpRequestException e)
            {
                return null;
            }

        }

        public async Task<string?> GetServerInfo() {


            try
            {
                HttpResponseMessage serverInfoResp = await _client.GetAsync("/serverinfo");
                serverInfoResp.EnsureSuccessStatusCode();
                byte[] content = await serverInfoResp.Content.ReadAsByteArrayAsync();

                ServerInfo serverInfo = JsonSerializer.Deserialize<ServerInfo>(content);
                Debug.WriteLine($"Name: {serverInfo.Name}");
                Debug.WriteLine($"LoggedAt: {serverInfo.LoggedAt}");
                return serverInfo.Name;

            }
            catch (HttpRequestException e)
            {
                return null;
            }

         

        }

        public void EstablishFileLink() {
            Debug.WriteLine($"Start File Sync");
            TcpClient client = new TcpClient();

            client.ConnectAsync(_ipRaw, 8100).Wait();

            NetworkStream stream = client.GetStream();

            new Thread(() =>
            {
                Debug.WriteLine($"Writing to server...");
                var sBuf = Encoding.UTF8.GetBytes("Getting Down And JIggy!");
                stream.Write(sBuf, 0, sBuf.Length);
            }).Start();
            Thread.Sleep(500);
            new Thread(() =>
            {
                Debug.WriteLine($"Reading from server...");
                byte[] buf = new byte[512012];
                int bytesRecv = stream.Read(buf, 0, buf.Length);

                Debug.WriteLine($"Received {bytesRecv} bytes");
            }).Start();

            
        }


    }
}
