using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PojazdyV2;

namespace PojazdyV2.Pojazdy
{
    public class Skuter : Ladowy
    {
       public Skuter() : base(new Wodny(), new Engine(100, Engine.TypPaliwa.benzyna)){}
    }
    public class Samolot : Powietrzny
    {
        public Samolot() :base(null, new Engine(100, Engine.TypPaliwa.benzyna)){}
    }
    public class Lodka : Wodny
    {
        public Lodka() :base(null, new Engine(100, Engine.TypPaliwa.benzyna)){}
    }

    public class Amfibia : Ladowy
    {
        public Amfibia() : base(new Wodny(wypornosc:100), new Engine(100, Engine.TypPaliwa.benzyna)){}
        
        public override int IloscKol { get; } = 6;

    }
    public class Zaglowka : Wodny
    {
        public Zaglowka() : base ( null, null) { }
        public override int Wypornosc { get; } = 10;

    }
}
