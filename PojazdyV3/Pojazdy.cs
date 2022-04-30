using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PojazdyV3
{
    public interface IPojazd
    {
        enum Typ
        {
            asd,
            asd1

        }
        //public int typ { get; set; }    
    }

    public abstract class Pojazdy
    {
        public int typ { get;}
        public Pojazdy(int abc)
        {
            typ = abc;
        }
    }
    public class Typ : Pojazdy
    {
        public Typ(int abc) : base(abc)
        {
        }
    }
    public class Typ1 : Typ
    {
        public Typ1(int abc) : base(abc)
        {
        }
    }
}
