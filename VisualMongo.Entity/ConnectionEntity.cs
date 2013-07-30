using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualMongo.Entity
{
    public class ConnectionEntity
    {
        public string Name { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public bool IsUseSSHTunnel { get; set; }
        public string SSHHost { get; set; }
        public string SSHUserName { get; set; }
        public string SSHPort { get; set; }
        public bool IsUserSSHPassword { get; set; }
        public string SSHPassword { get; set; }
        public bool IsUserSSHPublicKey { get; set; }
        public string SSHPassphrase { get; set; }
        public string SSHPrivateKey { get; set; }
    }
}
