using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Model
{
    public class SaveModel
    {
        public string Hash { get; set; }
        public string GroupId { get; set; }
        public string SaveOwner { get; set; }
        public string FolderName { get; set; }
        public string FullPath { get; set; }
        public DateTime DateCreated { get; set; }

        public SaveModel(
             string hash,
             string groupId,
             string saveOwner,
             string folderName,
             string fullPath,
             DateTime date)
        {
            Hash = hash;
            GroupId = groupId;
            SaveOwner = saveOwner;
            FolderName = folderName;
            FullPath = fullPath;
            DateCreated = date;
            
        }
    }
}
