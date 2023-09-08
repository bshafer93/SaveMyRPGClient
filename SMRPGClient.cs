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

        public string GetLatestSave(string group_id = "")
        {

            List<DirectoryInfo> save_list = new DirectoryInfo(bg3_save_location).GetDirectories().OrderByDescending(t => t.LastWriteTimeUtc).ToList();

            string latest_save = save_list.First().FullName;


            latest_save = save_list.Find(s => s.FullName.EndsWith(group_id)).FullName;

            Debug.WriteLine($"Latest Save: {latest_save}");

            return latest_save;
        }

        public async Task<List<GroupModel>> RetrieveAllJoinedCampaigns(UserModel userModel) {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("jwt-token", Properties.Settings.Default.JwtTokenString);
            UserModel um = new Model.UserModel(userModel.Password, userModel.Email);
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
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("jwt-token", Properties.Settings.Default.JwtTokenString);

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
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("jwt-token", Properties.Settings.Default.JwtTokenString);

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
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("jwt-token", Properties.Settings.Default.JwtTokenString);

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

        public async Task<bool> AuthenticateUser(UserModel userModel)
        {

            UserModel um = new Model.UserModel(userModel.Password, userModel.Email);
            byte[] user_login_info = JsonSerializer.SerializeToUtf8Bytes<UserModel>(um);

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("email", userModel.Email);
            _client.DefaultRequestHeaders.Add("pwd", userModel.Password);
            try
            {
                HttpResponseMessage resp = await App.Client._client.GetAsync("/login");
                resp.EnsureSuccessStatusCode();

                App.Client.TokenSignature = resp.Headers.GetValues("jwt-token").First().ToString();
                Properties.Settings.Default.JwtTokenString = resp.Headers.GetValues("jwt-token").First().ToString();
                Properties.Settings.Default.Save();
                App.Client.jwt_token = new JwtSecurityToken(App.Client.TokenSignature);
                Debug.WriteLine("Logged In!");


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

            return true;

        }
        public async Task<bool> AuthenticateUserToken()
        {

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("jwt-token", Properties.Settings.Default.JwtTokenString);
            _client.DefaultRequestHeaders.Add("email", Properties.Settings.Default.Email);
            try
            {
                HttpResponseMessage resp = await App.Client._client.GetAsync("/login");
                resp.EnsureSuccessStatusCode();
                Debug.WriteLine("Logged In!");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

            return true;

        }

        public async Task<bool> Register(UserModel userModel)
        {
            UserModel um = new Model.UserModel(userModel.Password, userModel.Email);
            byte[] user_login_info = JsonSerializer.SerializeToUtf8Bytes<UserModel>(um);

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("email", userModel.Email);
            _client.DefaultRequestHeaders.Add("pwd", userModel.Password);
            try
            {
                HttpResponseMessage resp = await App.Client._client.GetAsync("/ru");
                resp.EnsureSuccessStatusCode();
                Debug.WriteLine(resp);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

            return true;
        }

        public async Task<bool> JoinCampaign(UserModel um, string group_id) 
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("jwt-token", Properties.Settings.Default.JwtTokenString);

            JoinCampaignRequest jcr = new JoinCampaignRequest();

            jcr.Username = um.Email;
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
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("jwt-token", Properties.Settings.Default.JwtTokenString);
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
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("jwt-token", Properties.Settings.Default.JwtTokenString);
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

        public async Task<bool> UploadSaveFile(string group_id, string full_path,string save_folder_name,string save_name,string? save_owner)
        {

            string save_owner_email = save_owner != null ? save_owner.ToString() : Properties.Settings.Default.Email;

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("jwt-token", Properties.Settings.Default.JwtTokenString);
            _client.DefaultRequestHeaders.Add("email", save_owner_email);
            _client.DefaultRequestHeaders.Add("group_id", group_id);
            _client.DefaultRequestHeaders.Add("save_folder_name",save_folder_name);
            _client.DefaultRequestHeaders.Add("file_name", save_name);
            HttpContent save_file_raw = new ByteArrayContent(File.ReadAllBytes(full_path));

                try
                {
                    HttpResponseMessage resp = await _client.PutAsync("/guu", save_file_raw);

                    resp.EnsureSuccessStatusCode();
                    var contentString = await resp.Content.ReadAsStringAsync();
                    Debug.WriteLine(contentString);


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return false;
                }

            
            return true;

        }

        public async Task<bool> DownloadSaveFile(string group_id, string full_path, string save_folder_name, string save_name, string? save_owner)
        {

            string save_owner_email = save_owner != null ? save_owner.ToString() : Properties.Settings.Default.Email;

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("jwt-token", Properties.Settings.Default.JwtTokenString);
            _client.DefaultRequestHeaders.Add("email", save_owner_email);
            _client.DefaultRequestHeaders.Add("group_id", group_id);
            _client.DefaultRequestHeaders.Add("save_folder_name", save_folder_name);
            _client.DefaultRequestHeaders.Add("file_name", save_name);
            HttpContent save_file_raw = new ByteArrayContent(File.ReadAllBytes(full_path));

            try
            {
                HttpResponseMessage resp = await _client.PutAsync("/guu", save_file_raw);

                resp.EnsureSuccessStatusCode();
                var contentString = await resp.Content.ReadAsStringAsync();
                Debug.WriteLine(contentString);


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }


            return true;

        }

        public async Task<bool> UploadSaveImage(string group_id, string full_path)
        {


            FileInfo file = new FileInfo(full_path);

            string file_name = file.Name;
            string directory_name = file.Directory.Name;

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("jwt-token", Properties.Settings.Default.JwtTokenString);
            _client.DefaultRequestHeaders.Add("email", Properties.Settings.Default.Email);
            _client.DefaultRequestHeaders.Add("group_id", group_id);
            _client.DefaultRequestHeaders.Add("file_name", directory_name + "/" + file_name);

            HttpContent save_file_raw = new ByteArrayContent(File.ReadAllBytes(file.FullName));

                try
                {
                    HttpResponseMessage resp = await _client.PutAsync("/usm", save_file_raw);

                    resp.EnsureSuccessStatusCode();
                    var contentString = await resp.Content.ReadAsStringAsync();
                    Debug.WriteLine(contentString);


                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    return false;
                }

            return true;

        }

    }
}
