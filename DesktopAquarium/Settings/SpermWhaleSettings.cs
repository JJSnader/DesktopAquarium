using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAquarium.Settings
{
    public class SpermWhaleSettings : BaseSettings
    {
        public bool DoWhaleNoises { get; set; }
        public Frequency WhaleNoiseFrequency { get; set; }
    }
}
