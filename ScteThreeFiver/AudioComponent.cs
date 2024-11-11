using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScteThreeFiver
{
    public class AudioComponent
    {
        public byte ComponentTag { get; set; }
        public byte[] IsoTag { get; set; }
        public byte BitStreamMode { get; set; }
        public byte NumChannels { get; set; }
        public bool FullSrvcAudio {  get; set; }

        public AudioComponent()
        {
            this.IsoTag = new byte[3];
        }
    }
}
