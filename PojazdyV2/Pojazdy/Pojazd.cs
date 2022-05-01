using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PojazdyV2;

namespace PojazdyV2.Pojazdy
{
    //Aby stworzyć właśną klasę pojazdu należy odziedziczyć jedną z klas typów (Ladowy, Wodny, Powietrzny).
    //Następnie stworzyć konstrktor który wywołuje konstruktor bazowy.
    //W konstruktorze można podaj opcjonalnie kolejny typ lub silnik.
    //Aby utworzyć >2 typowy pojazd należy przekazać do konstruktora Typ wraz z kolejnym typem jako jego paramatr np. new Wodny(new Powietrzny()) - w ten sposób można stworzyć 3 typowy pojazd.
    //Aby edytować charakterystyczną cechę dla danego typu należy napisać ją w klasie, jednak dla kolejnego typu należy to już zrobić w jego kostruktorze, przykład Amfibia 
    //Przykładowe poprawnie stworzone pojazdy:
    public class Skuter : Ladowy
    {
       public Skuter() : base(new Wodny(), new Engine(30, Engine.TypPaliwa.benzyna)){}
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
        public Zaglowka() : base (null, null) { }
        public override int Wypornosc { get; } = 10;

    }
}
