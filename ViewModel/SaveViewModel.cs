using SaveMyRPGClient.Commands;
using SaveMyRPGClient.Model;
using System;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace SaveMyRPGClient.ViewModel
{
    public class SaveViewModel : ViewModelBase
    {
        public readonly SaveModel _save;
        public SaveListViewModel SaveListVM { get; set; }
        private bool _isLocal;
        public DownloadSaveCommand DownloadSaveCMD { get; set; }
        public string GroupID
        {
            get
            {
                return _save.Group_Id;
            }
            set
            {
                _save.Group_Id = value;
                OnPropertyChanged(nameof(GroupID));
            }
        }
        public string SaveOwner
        {
            get
            {
                return _save.Save_Owner;
            }
            set
            {
                _save.Save_Owner = value;
                OnPropertyChanged(nameof(SaveOwner));
            }
        }
        public string FolderName
        {
            get
            {
                return _save.Folder_Name;
            }
            set
            {
                _save.Folder_Name = value;
                OnPropertyChanged(nameof(FolderName));
            }
        }
        public string CDNPath
        {
            get
            {
                return _save.CDN_Path;
            }
            set
            {
                _save.CDN_Path = value;
                OnPropertyChanged(nameof(CDNPath));
            }
        }
        public DateTime DateCreated
        {
            get
            {
                return _save.Date_Created;
            }
            set
            {
                _save.Date_Created = value;
                OnPropertyChanged(nameof(DateCreated));
            }
        }

        public string Comment
        {
            get
            {
                return _save.Comment;
            }
            set
            {
                _save.Comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public bool IsLocal
        {
            get
            {
                return _isLocal;
            }
            set
            {
                _isLocal = value;
                OnPropertyChanged(nameof(IsLocal));
            }
        }

        

        public System.Windows.Media.ImageSource ImageURL
        {
            get
            {

                if (IsLocal)

                {
                    string[] imgPath = System.IO.Directory.GetFiles(Properties.Settings.Default.SavePath + "\\" + FolderName, "*.WebP");

                    if (imgPath.Count() < 1)
                    {
                        return new BitmapImage(new Uri(@"Images\imagenotfound.WebP"));
                    }
                    else 
                    {
                        return new BitmapImage(new Uri(imgPath[0]));
                    }
                   
                }
                return new BitmapImage(new Uri(@"Images\imagenotfound.WebP"));
            }


        }

        public SaveViewModel(SaveModel save,SaveListViewModel _saveListVM)
        {

            _save = save;
            SaveListVM = _saveListVM;

            IsLocal = Directory.Exists(Properties.Settings.Default.SavePath + "\\" + FolderName);

            DownloadSaveCMD = new DownloadSaveCommand(this);
        }
    }
}
