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
using SaveMyRPGClient.Model;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Windows.Media.Protection.PlayReady;
using System.Windows.Threading;
using SaveMyRPGClient.Model;
using System.ComponentModel.DataAnnotations;
using SaveMyRPGClient.View.UserControls;

namespace SaveMyRPGClient
{

    public class ServerInfo 
    {
        public string Name { get; set; }
        public DateTime LoggedAt { get; set; }
    }

    public class JoinCampaignRequest 
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string id { get; set; }
    }
    public class FullSaveFileJson {

        public string FolderName  { get; set; }
        public Int64 FolderSize   { get; set; }
        public string ZipDataB64  { get; set; }
    }

    public class SMRPGClient
    {

        public const string _ip = "https://savemyrpg.com";
        public const string _ipRaw = "savemyrpg.com";
        public HttpClient _client;

        public string default_path = @"%USERPROFILE%\AppData\Local\Larian Studios\Baldur's Gate 3\PlayerProfiles\Public\Savegames\Story";
        public string bg3_save_location;
        public string save_prefix = "Adam";
        public string temp_folder_path;
        private string token_signature = "";

        public JwtSecurityToken jwt_token;
        public string TokenSignature { get => token_signature; set => token_signature = value; }

        public SMRPGClient()
        {
            bg3_save_location = Environment.ExpandEnvironmentVariables(default_path);
            Debug.WriteLine("Save Location: " + bg3_save_location);
            if (init()) {
                Console.WriteLine("Login Failed");
            };
        }
        public bool init() {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_ip);
            if (_client == null)
            {
                return false;
            }
            return true;
        }

        public string[] ListLocalSaves() {

            string newest_save = new DirectoryInfo(bg3_save_location).GetDirectories().OrderByDescending(t => t.LastWriteTimeUtc).First().FullName;

            Debug.WriteLine(newest_save);

            return Directory.GetDirectories(bg3_save_location);
        }

        public async void CompressSave(string save_path) {

        }

        public string GetLatestSave(string group_id = "")
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
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"Exception: {ex}");
                return null;
            }

        }

        public async Task<List<GroupModel>> RetrieveAllJoinedCampaigns(UserModel userModel) {
            UserModel um = new Model.UserModel(userModel.Username, userModel.Email);
            byte[] user_info = JsonSerializer.SerializeToUtf8Bytes<UserModel>(um);

            HttpContent user = new ByteArrayContent(user_info);
            List<GroupModel> joinedCampaigns = new List<GroupModel>();

            try
            {
                HttpResponseMessage resp = await _client.PutAsync("/rc", user);

                resp.EnsureSuccessStatusCode();

                var content = await resp.Content.ReadAsStreamAsync();
                var contentString = await resp.Content.ReadAsByteArrayAsync();

                Debug.WriteLine(contentString);
                /*
                var groupList = JsonSerializer.Deserialize<List<GroupModel>>(contentString);

                Debug.WriteLine(joinedCampaigns);
                return joinedCampaigns;
                */
                var groupList = await JsonSerializer.DeserializeAsync<List<GroupModel>>(content,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }); ;

                return groupList;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;

        }
        public async Task<List<GroupModel>> RetrieveAllJoinedCampaigns(string email)
        {
            UserModel um = new Model.UserModel("Anon", email);
            byte[] user_info = JsonSerializer.SerializeToUtf8Bytes<UserModel>(um);

            HttpContent user = new ByteArrayContent(user_info);
            List<GroupModel> joinedCampaigns = new List<GroupModel>();

            try
            {
                HttpResponseMessage resp = await _client.PutAsync("/rc", user);

                resp.EnsureSuccessStatusCode();

                var content = await resp.Content.ReadAsStreamAsync();
                var contentString = await resp.Content.ReadAsByteArrayAsync();

                Debug.WriteLine(contentString);
                /*
                var groupList = JsonSerializer.Deserialize<List<GroupModel>>(contentString);

                Debug.WriteLine(joinedCampaigns);
                return joinedCampaigns;
                */
                var groupList = await JsonSerializer.DeserializeAsync<List<GroupModel>>(content,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }); ;

                return groupList;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;

        }

        public async Task<List<SaveModel>> RetrieveAllCampaignSaves(string id) {

            string group_id_json = "{\"id\":\"" + id + "\"}";
            
            HttpContent gid = new ByteArrayContent(Encoding.ASCII.GetBytes(group_id_json));

            List<SaveModel> campaignSaves = new List<SaveModel>();

            try
            {
                HttpResponseMessage resp = await _client.PutAsync("/cs", gid);

                resp.EnsureSuccessStatusCode();

                var content = await resp.Content.ReadAsStreamAsync();
                var contentString = await resp.Content.ReadAsByteArrayAsync();

                Debug.WriteLine(contentString);

                var saveList = await JsonSerializer.DeserializeAsync<List<SaveModel>>(content,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }); ;
                Debug.WriteLine(saveList);
                for (int i = 0; i < saveList.Count(); i++){
                    Debug.WriteLine(saveList[i].Folder_Name);
                }
                return saveList;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
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
            catch (HttpRequestException ex)
            {
                Debug.WriteLine($"Exception: {ex}");
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

        public async Task<bool> AuthenticateUser(UserModel userModel)
        {

            UserModel um = new Model.UserModel(userModel.Username, userModel.Email);
            byte[] user_login_info = JsonSerializer.SerializeToUtf8Bytes<UserModel>(um);

            HttpContent user_login = new ByteArrayContent(user_login_info);
            try
            {
                HttpResponseMessage resp = await App.Client._client.PostAsync("/login", user_login);
                resp.EnsureSuccessStatusCode();

                App.Client.TokenSignature = resp.Headers.GetValues("jwt-token").First().ToString();
                App.Client.jwt_token = new JwtSecurityToken(App.Client.TokenSignature);

                Debug.WriteLine(resp);
                Debug.WriteLine("Token");
                Debug.WriteLine("Logged In!");


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

            return true;

        }
        public void Register(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> JoinCampaign(UserModel um, string group_id) 
        {
            JoinCampaignRequest jcr = new JoinCampaignRequest();

            jcr.Username = um.Username;
            jcr.Email = um.Email;
            jcr.id = group_id;

            HttpContent jcrJson = new ByteArrayContent(JsonSerializer.SerializeToUtf8Bytes<JoinCampaignRequest>(jcr));
            try
            {
                HttpResponseMessage resp = await App.Client._client.PostAsync("/jc", jcrJson);

                resp.EnsureSuccessStatusCode();
                var contentString = await resp.Content.ReadAsStringAsync();
                Debug.WriteLine(contentString);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

        }

        public async Task<GroupModel> RetrieveCampaignInfo(string group_id) 
        {
            string group_id_json = "{\"id\":\"" + group_id + "\"}";

            HttpContent gid = new ByteArrayContent(Encoding.ASCII.GetBytes(group_id_json));

            GroupModel joinedCampaigns = new GroupModel();

            try
            {
                HttpResponseMessage resp = await _client.PutAsync("/rci", gid);

                resp.EnsureSuccessStatusCode();

                var content = await resp.Content.ReadAsStreamAsync();

                var group = await JsonSerializer.DeserializeAsync<GroupModel>(content,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return group;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }

        }

        public async Task<GroupModel?> CreateCampaign(GroupModel gm)
        {

            HttpContent gid = new ByteArrayContent(JsonSerializer.SerializeToUtf8Bytes<GroupModel>(gm));

            try
            {
                HttpResponseMessage resp = await _client.PutAsync("/cc", gid);

                resp.EnsureSuccessStatusCode();
                var content = await resp.Content.ReadAsStreamAsync();

                var group = await JsonSerializer.DeserializeAsync<GroupModel>(content,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                return group;


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }

        }

        public void Remove(UserModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}
