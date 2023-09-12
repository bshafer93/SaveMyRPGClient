using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Model
{
    [Serializable]
    public class GroupModel
    {

        public string Id { get; set; }


        public string Name { get; set; }

        public string Host_Email { get; set; }


        public string? P2_Email { get; set; }


        public string? P3_Email { get; set; }


        public string? P4_Email { get; set; }


        public string? Last_Save { get; set; }


        public GroupModel(string groupID,string name, string hostEmail, string? P2Email, string? P3Email, string? P4Email,string lastSaveHash)
        { 
            Id = groupID;
            Name = name;
            Host_Email = hostEmail;
            P2_Email = P2Email;
            P3_Email = P3Email;
            P4_Email = P4Email;
            Last_Save = lastSaveHash;
        }

        public GroupModel() { 
        
        }
        
    }
}
