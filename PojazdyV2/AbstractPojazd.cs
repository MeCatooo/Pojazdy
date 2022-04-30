using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PojazdyV2
{
    public abstract class AbstractPojazd : IState, IEngined
    {
        protected AbstractPojazd? KolejnyTyp { get; init; }
        public int Speed { get; protected set; }
        public IState State { get; protected set; } = new StateOff();
        public ISrodowisko Srodowisko { get; protected set; } = new Lad();
        protected abstract ISrodowisko DozwoloneSrodowisko { get; }

        public Engine? Engine { get; } = null;

        public List<ISrodowisko> DozwoloneSrodowiska = new List<ISrodowisko>();

        public int Move(Speed speed)
        {
            this.Speed = State.Move(speed);
            return speed.GetSpeed();
        }
        public void TurnON()
        {
            State = new StateOn();
            Speed = Srodowisko.Min;
        }
        public void TurnOFF()
        {
            State = new StateOff();
            Speed = 0;
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
            Type += $"Srodowisko: {Srodowisko.GetType().Name}\nAktualna predkosc: {Speed}\nMin: {Srodowisko.Min} Max: {Srodowisko.Max}\n";
            if (Engine != null)
                Type += Engine.ToString() + "\n";
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
    }
    public class Ladowy : AbstractPojazd
    {
        public Ladowy(AbstractPojazd? kolejnyTyp = null, Engine? engine = null) : base(kolejnyTyp, engine) { }
        protected override ISrodowisko DozwoloneSrodowisko { get; } = new Lad();
        public int IloscKol { get; }
        public override string ToString()
        {
            return base.ToString() + $"Ilość kół: {IloscKol}";
        }
    }
    public class Powietrzny : AbstractPojazd
    {
        public Powietrzny(AbstractPojazd? kolejnyTyp = null, Engine? engine = null) : base(kolejnyTyp, engine) { }

        protected override ISrodowisko DozwoloneSrodowisko { get; } = new Powietrze();
    }
    public class Wodny : AbstractPojazd
    {
        public Wodny(AbstractPojazd? kolejnyTyp = null, Engine? engine = null) : base(kolejnyTyp, engine) { }

        protected override ISrodowisko DozwoloneSrodowisko { get; } = new Woda();
    }
}
