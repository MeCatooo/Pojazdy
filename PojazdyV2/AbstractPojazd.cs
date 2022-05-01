using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PojazdyV2
{
    public abstract class AbstractPojazd : IState, IEngined, ISrodowiskoChanger, IComparable<AbstractPojazd>
    {
        protected virtual AbstractPojazd? KolejnyTyp { get; init; }
        public Speed Speed { get; protected set; } = new Speed(0);
        public IState State { get; protected set; } = new StateOff();
        public ISrodowisko Srodowisko { get; protected set; } = new Lad();
        protected abstract ISrodowisko DozwoloneSrodowisko { get; }
        public Engine? Engine { get; } = null;
        public List<ISrodowisko> DozwoloneSrodowiska { get; } = new List<ISrodowisko>();
        /// <summary>
        /// Zmienia prędkość pojazdu
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public int Move(Speed speed)
        {
            this.Speed += new Speed(State.Move(speed), Srodowisko.SpeedUnits);
            //Speed += State.Move(speed);
            return Speed.GetSpeed();
        }
        /// <summary>
        /// Uruchamia pojazd
        /// </summary>
        public void TurnON()
        {
            State = new StateOn();
            Speed = new Speed(Srodowisko.Min);
        }
        /// <summary>
        /// Wyłącza pojazd
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void TurnOFF()
        {
            if (Srodowisko is Powietrze)
                throw new Exception("Nie można wyłączyć pojazdu w locie");
            State = new StateOff();
            Speed = new Speed(0, Srodowisko.SpeedUnits);
        }
        protected List<ISrodowisko> GetDozwoloneSrodowisko()
        {
            List<ISrodowisko> result = new List<ISrodowisko>();
            result.Add(DozwoloneSrodowisko);
            if (KolejnyTyp != null)
                result.AddRange(KolejnyTyp.GetDozwoloneSrodowisko());
            return result;
        }
        protected AbstractPojazd(AbstractPojazd? kolejnyTyp = null, Engine? engine = null)
        {
            if (IsSameType(this, kolejnyTyp))
                throw new ArgumentException();
            KolejnyTyp = kolejnyTyp;
            DozwoloneSrodowiska = GetDozwoloneSrodowisko();
            if (engine != null && DozwoloneSrodowiska.OfType<Woda>().Any())
                Engine = new Engine(engine.Moc, Engine.TypPaliwa.olej);
            else
                Engine = engine;
        }
        /// <summary>
        /// Zwraca stan oraz prametry pojazdu
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string Type = $"Typ: Pojazd\nPrzeznaczenie: ";
            if (KolejnyTyp != null)
                Type += "Wielozadaniowy\n";
            else
                Type += GetType().Name + "\n";
            if (Speed.GetSpeed() <= 0)
                Type += "W ruchu: false\n";
            else
                Type += $"W ruchu: true\nAktualna predkosc: { Speed.GetSpeed(Srodowisko.SpeedUnits)} { Srodowisko.SpeedUnits}\n";
            Type += $"Srodowisko: {Srodowisko.GetType().Name}\nMin: {Srodowisko.Min} Max: {Srodowisko.Max}\n";
            if (Engine != null)
                Type += Engine.ToString();
            return Type;
        }
        /// <summary>
        /// Wewnętrzna funkcja do określania czy nie powtórzono typów
        /// </summary>
        /// <param name="abstractPojazd"></param>
        /// <param name="abstractPojazd1"></param>
        /// <returns></returns>
        protected bool IsSameType(AbstractPojazd abstractPojazd, AbstractPojazd? abstractPojazd1)
        {
            if (abstractPojazd1 == null)
                return false;
            if (abstractPojazd.GetType().Name == abstractPojazd1.GetType().Name)
                return true;
            if (abstractPojazd.GetType().BaseType == abstractPojazd1.GetType().BaseType || abstractPojazd.GetType().BaseType == abstractPojazd1.GetType())
                return true;
            else
                return false;
        }
        /// <summary>
        /// Zmienia środowiko pojazdu
        /// </summary>
        /// <param name="srodowisko"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public virtual ISrodowisko MoveToNext(ISrodowisko srodowisko)
        {
            foreach (var item in DozwoloneSrodowiska)
            {
                if (item.GetType() == srodowisko.GetType())
                    return ChangeSrodowiko(srodowisko);
            }
            throw new ArgumentException();
        }
        
        private ISrodowisko ChangeSrodowiko(ISrodowisko srodowisko)
        {
            Srodowisko = srodowisko;
            Speed = new Speed(Speed.GetSpeed(srodowisko.SpeedUnits), srodowisko.SpeedUnits);
            return srodowisko;
        }
        /// <summary>
        /// Musi wywoływać następny typ i zwracać cechę lub gdy kolejny nie istnieje zwracać tylko cechę 
        /// </summary>
        /// <returns>Zwraca chrakterystyczne cechy danego typu</returns>
        public abstract string AddAtributes();

        public int CompareTo(AbstractPojazd? other)
        {
            if (other == null)
                throw new ArgumentNullException();
            if (this > other)
                return -1;
            else if (this < other)
                return 1;
            else
                return 0;
        }

        public static bool operator <(AbstractPojazd left, AbstractPojazd right)
        {
            return left.Speed.GetSpeed() < right.Speed.GetSpeed();
        }

        public static bool operator <=(AbstractPojazd left, AbstractPojazd right)
        {
            return left.Speed.GetSpeed() <= right.Speed.GetSpeed();
        }

        public static bool operator >(AbstractPojazd left, AbstractPojazd right)
        {
            return left.Speed.GetSpeed() > right.Speed.GetSpeed();
        }

        public static bool operator >=(AbstractPojazd left, AbstractPojazd right)
        {
            return left.Speed.GetSpeed() >= right.Speed.GetSpeed();
        }
    }
    public class Ladowy : AbstractPojazd
    {
        public Ladowy(AbstractPojazd? kolejnyTyp = null, Engine? engine = null, int kola = 10) : base(kolejnyTyp, engine)
        {
            IloscKol = kola;
        }
        protected override ISrodowisko DozwoloneSrodowisko { get; } = new Lad();
        public virtual int IloscKol { get; }
        public override string ToString()
        {
            return base.ToString() + AddAtributes();
        }

        public override string AddAtributes()
        {
            if (KolejnyTyp != null)
                return KolejnyTyp.AddAtributes() + $"Ilość kół: {IloscKol}\n";
            return $"Ilość kół: {IloscKol}\n";
        }
    }
    public class Powietrzny : AbstractPojazd
    {
        public Powietrzny(AbstractPojazd? kolejnyTyp = null, Engine? engine = null) : base(kolejnyTyp, engine) { }

        protected override ISrodowisko DozwoloneSrodowisko { get; } = new Powietrze();
        public override ISrodowisko MoveToNext(ISrodowisko srodowisko)
        {
            if (Speed.GetSpeed(Speed.SpeedUnits.ms) < new Speed(srodowisko.Min, srodowisko.SpeedUnits).GetSpeed(Speed.SpeedUnits.ms))
                throw new Exception("Pojazd musi się rozpiędzić");
            return base.MoveToNext(srodowisko);
        }
        public override string ToString()
        {
            return base.ToString() + AddAtributes();
        }
        public override string AddAtributes()
        {
            if (KolejnyTyp != null)
                return KolejnyTyp.AddAtributes();
            return "";
        }
    }
    public class Wodny : AbstractPojazd
    {
        public Wodny(AbstractPojazd? kolejnyTyp = null, Engine? engine = null, int wypornosc = 10) : base(kolejnyTyp, engine)
        {
            Wypornosc = wypornosc;
        }

        protected override ISrodowisko DozwoloneSrodowisko { get; } = new Woda();
        public virtual int Wypornosc { get; }
        public override string ToString()
        {
            return base.ToString() + AddAtributes();
        }
        public override string AddAtributes()
        {
            if (KolejnyTyp != null)
                return KolejnyTyp.AddAtributes() + $"Wypornosc: {Wypornosc}\n";
            return $"Wypornosc: {Wypornosc}\n";
        }
    }
}
