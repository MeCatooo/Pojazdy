using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PojazdyV2
{
    public interface IState
    {
        public int Move(Speed speed);
    }

    public class StateOn : IState
    {
        public int Move(Speed speed)
        {
            return speed.GetSpeed();
        }
    }
    public class StateOff : IState
    {
        public int Move(Speed speed)
        {
            return 0;
        }
    }
}
