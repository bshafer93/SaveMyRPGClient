using System;

namespace SaveMyRPGClient.Model
{
    public class SaveModel
    {
        public string Hash { get; set; }
        public string Group_Id { get; set; }
        public string Save_Owner { get; set; }
        public string Folder_Name { get; set; }
        public string CDN_Path { get; set; }
        public DateTime Date_Created { get; set; }

        public SaveModel(
             string hash,
             string groupId,
             string saveOwner,
             string folderName,
             string cdnPath,
             DateTime date)
        {
            Hash = hash;
            Group_Id = groupId;
            Save_Owner = saveOwner;
            Folder_Name = folderName;
            Date_Created = date;
            CDN_Path = cdnPath;

        }


        public SaveModel()
        {

        }
    }
}
