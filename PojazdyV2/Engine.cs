using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PojazdyV2
{
    public class Engine
    {
        public enum TypPaliwa
        {
            benzyna,
            olej,
            LPG,
            prad
        }
        public TypPaliwa Paliwo { get; init; }
        public int Moc { get; init; }
        public Engine(int moc, TypPaliwa paliwo)
        {
            Moc = moc;
            Paliwo = paliwo;
        }
        public override string ToString()
        {
            return $"Typ paliwa: {Paliwo}\nMoc: {Moc}\n";
        }
    }
    public interface IEngined
    {
        public Engine? Engine { get; }
    }
}
