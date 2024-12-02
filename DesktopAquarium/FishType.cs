using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAquarium
{
    public enum FishType
    {
        Shark,
        Goldfish,
        Jellyfish,
        Pufferfish,
        Submarine,
        [Description("Sperm Whale")]
        SpermWhale
    }
}
