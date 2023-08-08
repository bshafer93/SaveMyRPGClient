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

namespace SaveMyRPGClient
{
    public class ServerInfo 
    {
        public string Name { get; set; }
        public DateTime LoggedAt { get; set; }
    }

    public class SMRPG_Client
    {

        public const string _ip = "http://192.168.1.33:8100";
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
        public async void init() {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_ip);


            HttpResponseMessage resp = await _client.GetAsync("/");

            if(resp.IsSuccessStatusCode)
            {
                string content = await resp.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
            }

            string[] local_saves = await listLocalSaves();


            HttpResponseMessage serverInfoResp = await _client.GetAsync("/serverinfo");

            if (serverInfoResp.IsSuccessStatusCode)
            {
                byte[] content = await serverInfoResp.Content.ReadAsByteArrayAsync();

                ServerInfo serverInfo = JsonSerializer.Deserialize<ServerInfo>(content);

                Debug.WriteLine($"Name: {serverInfo.Name}");
                Debug.WriteLine($"LoggedAt: {serverInfo.LoggedAt}");

            }
        }

        public async Task<string[]> listLocalSaves() {

            string newest_save = new DirectoryInfo(bg3_save_location).GetDirectories().OrderByDescending(t => t.LastWriteTimeUtc).First().FullName;

            Debug.WriteLine(newest_save);

            return Directory.GetDirectories(bg3_save_location);
        }

        public async void compressSave(string save_path) { 
            
        }

        public async Task<string> getLatestSave(string group_id = "") {

            List<DirectoryInfo> save_list = new DirectoryInfo(bg3_save_location).GetDirectories().OrderByDescending(t => t.LastWriteTimeUtc).ToList();

            string latest_save = save_list.First().FullName;

            
            latest_save = save_list.Find(s => s.FullName.EndsWith(group_id)).FullName;

            Debug.WriteLine($"Latest Save: {latest_save}");

            return latest_save;
        }

    }
}
