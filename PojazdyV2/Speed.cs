using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PojazdyV2
{
    public class Speed
    {
        public enum SpeedUnits
        {
            kmh,
            ms,
            knots
        }
        private int speed;
        private SpeedUnits unit;
        public Speed(int speed = 10, SpeedUnits unit = SpeedUnits.kmh)
        {
            switch (unit)
            {
                case SpeedUnits.kmh:
                    this.speed = speed;
                    unit = SpeedUnits.kmh;
                    break;
                case SpeedUnits.ms:
                    this.speed = (int)(speed * 3.6);
                    this.unit = SpeedUnits.ms;
                    break;
                case SpeedUnits.knots:
                    this.speed = (int)(speed * 1.852);
                    this.unit = SpeedUnits.knots;
                    break;
            }
        }
        public int GetSpeed( SpeedUnits unit = SpeedUnits.kmh)
        {
            switch (unit)
            {
                case SpeedUnits.kmh:
                    return this.speed;
                case SpeedUnits.ms:
                    return (int)(speed / 3.6);
                case SpeedUnits.knots:
                    return (int)(speed / 1.852);
                default: return speed;
            }
        }
        public static Speed operator +(Speed a, Speed b)
        {
            return new Speed(a.GetSpeed()+b.GetSpeed(),a.unit);
        }
    }
}
