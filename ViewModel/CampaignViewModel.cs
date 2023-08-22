using SaveMyRPGClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.ViewModel
{
    public class CampaignViewModel
    {
        private readonly GroupModel _group;

        public string GroupID => _group.GroupID;
        public string Name => _group.Name;
        public string HostEmail => _group.HostEmail;
        public string? Player02Email => _group.Player02Email;
        public string? Player03Email => _group.Player03Email;
        public string? Player04Email => _group.Player04Email;
        public string? LastSaveHash => _group.LastSaveHash;

        public CampaignViewModel(GroupModel group)
        {
            _group = group;
        }
    }
}
