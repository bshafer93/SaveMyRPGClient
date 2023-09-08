using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMyRPGClient.Model;
using static System.Net.WebRequestMethods;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Media;

namespace SaveMyRPGClient.ViewModel
{
    public class SaveViewModel : ViewModelBase
    {
        private readonly SaveModel _save;
        public string GroupID => _save.Group_Id;
        public string SaveOwner => _save.Save_Owner;
        public string FolderName => _save.Folder_Name;
        public string CDNPath => _save.CDN_Path;
        public DateTime DateCreated => _save.Date_Created;


        public System.Windows.Media.ImageSource ImageURL {
            get
            {
                return (System.Windows.Media.ImageSource)new ImageSourceConverter().ConvertFromString(CDNPath.Replace("https://ny.storage.bunnycdn.com", "https://savemyrpg.b-cdn.net").Replace(".lsv", ".WebP"));
            }

        }

        public SaveViewModel(SaveModel save) { 
            
            _save = save;
            
        }
    }
}
