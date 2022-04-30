using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PojazdyV2
{
    public abstract class AbstractPojazd : IState, IEngined, ISrodowiskoChanger
    {
        protected AbstractPojazd? KolejnyTyp { get; init; }
        //public int Speed
        //{
        //    //get => Speed; protected set
        //    //{
        //    //    if (Speed - value > 0)
        //    //        Speed = value;
        //    //    else
        //    //        throw new ArgumentOutOfRangeException();
        //    //}
        //    //get;
        //    //protected set;
        //}
        public Speed Speed { get; protected set; } = new Speed(0);
        public IState State { get; protected set; } = new StateOff();
        public ISrodowisko Srodowisko { get; protected set; } = new Lad();
        protected abstract ISrodowisko DozwoloneSrodowisko { get; }
        public Engine? Engine { get; } = null;
        public List<ISrodowisko> DozwoloneSrodowiska { get; } = new List<ISrodowisko>();

        public int Move(Speed speed)
        {
            //this.Speed = new Speed(State.Move(speed), Srodowisko.SpeedUnits);
            Speed += speed;
            return Speed.GetSpeed();
        }
        public void TurnON()
        {
            State = new StateOn();
            Speed = new Speed(Srodowisko.Min);
        }
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
        public override string ToString()
        {
            string Type = $"Typ: Pojazd\nPrzeznaczenie: ";
            if (KolejnyTyp != null)
                Type += "Wielozadaniowy\n";
            else
                Type += GetType().Name;
            Type += $"Srodowisko: {Srodowisko.GetType().Name}\nAktualna predkosc: {Speed.GetSpeed(Srodowisko.SpeedUnits)} {Srodowisko.SpeedUnits}\nMin: {Srodowisko.Min} Max: {Srodowisko.Max}\n";
            if (Engine != null)
                Type += Engine.ToString();
            if (KolejnyTyp != null)
                return Type + KolejnyTyp.ToString();
            return Type;
        }
        protected bool IsSameType(AbstractPojazd abstractPojazd, AbstractPojazd? abstractPojazd1)
        {
            if (abstractPojazd1 == null)
                return false;
            if (abstractPojazd.GetType().Name == abstractPojazd1.GetType().Name)
                return true;
            else
                return false;
        }

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
    }
    public class Ladowy : AbstractPojazd
    {
        public Ladowy(AbstractPojazd? kolejnyTyp = null, Engine? engine = null) : base(kolejnyTyp, engine) { }
        protected override ISrodowisko DozwoloneSrodowisko { get; } = new Lad();
        public int IloscKol { get; }
        public override string ToString()
        {
            if (KolejnyTyp != null)
                return KolejnyTyp.ToString() + $"Ilość kół: {IloscKol}\n";
            else
                return $"Ilość kół: {IloscKol}\n";
        }
    }
    public class Powietrzny : AbstractPojazd
    {
        public Powietrzny(AbstractPojazd? kolejnyTyp = null, Engine? engine = null) : base(kolejnyTyp, engine) { }

        protected override ISrodowisko DozwoloneSrodowisko { get; } = new Powietrze();
        public override ISrodowisko MoveToNext(ISrodowisko srodowisko)
        {
            //throw new Exception(Speed.GetSpeed(Srodowisko.SpeedUnits).ToString());
            //int tmp = new Speed(Srodowisko.Min, Srodowisko.SpeedUnits).GetSpeed(Speed.SpeedUnits.ms);
            //throw new Exception(Srodowisko.Min.ToString());
            if (Speed.GetSpeed(Speed.SpeedUnits.ms) < new Speed(srodowisko.Min, srodowisko.SpeedUnits).GetSpeed(Speed.SpeedUnits.ms))
                throw new Exception("Pojazd musi się rozpiędzić");
            return base.MoveToNext(srodowisko);
        }
    }
    public class Wodny : AbstractPojazd
    {
        public Wodny(AbstractPojazd? kolejnyTyp = null, Engine? engine = null) : base(kolejnyTyp, engine) { }

        protected override ISrodowisko DozwoloneSrodowisko { get; } = new Woda();
        public int Wypornosc { get; } = 10;
        public override string ToString()
        {
            if (KolejnyTyp != null)
                return KolejnyTyp.ToString() + $"Wypornosc: {Wypornosc}\n";
            else
                return $"Wypornosc: {Wypornosc}\n";
        }
    }

}
