using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PojazdyV2
{
    public interface ISrodowisko
    {
        public int Min { get; }
        public int Max { get; }
        public Speed.SpeedUnits SpeedUnits { get; }

    }
    public interface ISrodowiskoChanger
    {
        public ISrodowisko Srodowisko { get; protected set; }
        public List<ISrodowisko> DozwoloneSrodowiska { get; }
        public ISrodowisko MoveToNext(ISrodowisko srodowisko)
        {
            if (!DozwoloneSrodowiska.Contains(srodowisko))
                throw new ArgumentException();
            return srodowisko;
        }
    }
    public class Lad : ISrodowisko
    {
        public int Min => 1;
        public int Max => 350;
        public Speed.SpeedUnits SpeedUnits => Speed.SpeedUnits.kmh;
    }
    public class Woda : ISrodowisko
    {
        public int Min => 1;
        public int Max => 3550;
        public Speed.SpeedUnits SpeedUnits => Speed.SpeedUnits.kmh;
    }
    public class Powietrze : ISrodowisko
    {
        public int Min => 1;
        public int Max => 35550;
        public Speed.SpeedUnits SpeedUnits => Speed.SpeedUnits.kmh;
    }
}
