using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMyRPGClient.Model;

namespace SaveMyRPGClient.ViewModel
{
    public class SaveViewModel
    {
        private readonly SaveModel _save;
        public string GroupID => _save.Group_Id;
        public string SaveOwner => _save.Save_Owner;
        public string FolderName => _save.Folder_Name;
        public string CDNPath => _save.CDN_Path;
        public DateTime DateCreated => _save.Date_Created;

        public SaveViewModel(SaveModel save) { 
            
            _save = save;
        
        }
    }
}
