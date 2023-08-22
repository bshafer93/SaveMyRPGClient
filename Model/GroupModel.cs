using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Model
{
    public class GroupModel
    {

        public string GroupID { get; set; }
        public string Name { get; set; }
        public string HostEmail { get; set; }
        public string? Player02Email { get; set; }
        public string? Player03Email { get; set; }
        public string? Player04Email { get; set; }
        public string? LastSaveHash { get; set; }


        public GroupModel(string groupID,string name, string hostEmail, string? P2Email, string? P3Email, string? P4Email,string lastSaveHash)
        { 
            GroupID = groupID;
            Name = name;
            HostEmail = hostEmail;
            Player02Email = P2Email;
            Player03Email = P3Email;
            Player04Email = P4Email;
            LastSaveHash = lastSaveHash;
        }
        
    }
}
