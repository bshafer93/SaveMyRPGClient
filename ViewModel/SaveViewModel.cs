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
        public string GroupID => _save.GroupId;
        public string SaveOwner => _save.SaveOwner;
        public string FolderName => _save.FolderName;
        public string FullPath => _save.FullPath;
        public DateTime DateCreated => _save.DateCreated;

        public SaveViewModel(SaveModel save) { 
            
            _save = save;
        
        }
    }
}
