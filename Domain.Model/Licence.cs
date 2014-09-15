using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Licence:EntityBaseFields
    {
        public int LicenceId { get; set; }
        public string LicenceKey { get; set; }
        public string HolderName { get; set; }
        public bool IsValid { get; set; }
        public int MaxVolume { get; set; }
        public int MaxLedgerVolume { get; set; }
        public int CurrentLedgerVolume { get; set; }
        public int MaxTransactionVolume { get; set; }
        public int CurrentTransaction { get; set; }
        public int MaxActiveTransactionVolume { get; set; }
        public int CurrentActiveTransactionVolume { get; set; }
        public int MaxAccountVolume { get; set; }
        public int MaxActiveAccountVolume { get; set; }
        public int CurrentAccountVolume { get; set; }
        public int CurrentVolume { get; set; }
    }
}
