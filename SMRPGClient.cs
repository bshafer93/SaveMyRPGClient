using SaveMyRPGClient.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SaveMyRPGClient
{


    public class JoinCampaignRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string id { get; set; }

        public JoinCampaignRequest(string username, string email, string Id)
        {
            Username = username;
            Email = email;
            id = Id;
        }
    }

    public class SMRPGClient
    {

        public const string _ip = "https://savemyrpg.com";
        public const string _ipRaw = "savemyrpg.com";
        public HttpClient _client;

        public string default_path = @"%USERPROFILE%\AppData\Local\Larian Studios\Baldur's Gate 3\PlayerProfiles\Public\Savegames\Story";
        private string token_signature = "";

        public JwtSecurityToken? jwt_token;
        public string TokenSignature { get => token_signature; set => token_signature = value; }

        public SMRPGClient()
        {

            Properties.Settings.Default.SavePath = (Properties.Settings.Default.SavePath != "...") ?
                Properties.Settings.Default.SavePath :
                Environment.ExpandEnvironmentVariables(default_path);

            Debug.WriteLine("Save Location: " + Properties.Settings.Default.SavePath);
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_ip);
            //TO DO ERROR HANDLING
            if (init())
            {
                Console.WriteLine("Login Failed");
            };
        }
        public bool init()
        {
            return (_client != null);
        }

        public string[] ListLocalSaves()
        {

            string newest_save = new DirectoryInfo(Properties.Settings.Default.SavePath).GetDirectories().OrderByDescending(t => t.LastWriteTimeUtc).First().FullName;

            Debug.WriteLine(newest_save);

            return Directory.GetDirectories(Properties.Settings.Default.SavePath);
        }

        public async Task<List<GroupModel>?> RetrieveAllJoinedCampaigns(UserModel userModel)
        {
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
        public async Task<List<GroupModel>?> RetrieveAllJoinedCampaigns(string email)
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

        public async Task<List<SaveModel>?> RetrieveAllCampaignSaves(string id)
        {
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

                var saveList = await JsonSerializer.DeserializeAsync<List<SaveModel>>(content,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }); ;

                return saveList;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
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

        public async Task<bool> UploadSaveFolder(string save_folder_path, string group_id)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(save_folder_path);
            FileInfo[] files = dirInfo.GetFiles();

            foreach (FileInfo file in files)
            {
                if (file.Extension.EndsWith("lsv"))
                {
                    SaveModel save = new SaveModel();
                    string directory_name = dirInfo.Name;
                    string file01_name = file.Name;
                    save.Save_Owner = Properties.Settings.Default.Email;
                    save.Group_Id = group_id;
                    save.Hash = SHA256.HashData(File.ReadAllBytes(file.FullName)).ToString();
                    save.CDN_Path = group_id + "/" + directory_name + "/" + file01_name;
                    save.Date_Created = new FileInfo(file.FullName).CreationTime;

                    bool didUpload = await App.Client.UploadSaveFile(save.Group_Id, file.FullName, directory_name, file01_name, save.Save_Owner);

                    if (!didUpload)
                    {
                        Debug.WriteLine("Upload Failed");
                        return false;
                    }

                }
                else
                {
                    bool didUpload = await App.Client.UploadSaveImage(group_id, file.FullName);

                    if (!didUpload)
                    {
                        Debug.WriteLine("Upload Failed");
                        return false;
                    }

                }

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

            JoinCampaignRequest jcr = new JoinCampaignRequest(um.Email, um.Email, group_id);

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
        public async Task<GroupModel?> RetrieveCampaignInfo(string group_id)
        {
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("jwt-token", Properties.Settings.Default.JwtTokenString);
            string group_id_json = "{\"id\":\"" + group_id + "\"}";

            HttpContent gid = new ByteArrayContent(Encoding.ASCII.GetBytes(group_id_json));

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
        public async Task<bool> UploadSaveFile(string group_id, string full_path, string save_folder_name, string save_name, string? save_owner)
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

        public async Task<bool> DownloadSaveFile(string folder_name, string baseSaveName, string url)
        {
            try
            {
                using (var s = await _client.GetStreamAsync(new Uri(url)))
                {
                    Directory.CreateDirectory(Properties.Settings.Default.SavePath + "\\" + folder_name);

                    using (var fs = new FileStream(Properties.Settings.Default.SavePath + "\\" + folder_name + "\\" + baseSaveName, FileMode.OpenOrCreate))
                    {
                        await s.CopyToAsync(fs);
                    }
                }
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
            FileInfo file;
            string file_name;
            string directory_name;

            try
            {
                file = new FileInfo(full_path);
                file_name = file.Name;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

            if (file.Directory == null)
            {
                return false;
            }
            else
            {
                directory_name = file.Directory.Name;
            }

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
