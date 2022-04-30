// See https://aka.ms/new-console-template for more information
using PojazdyV2;
Console.WriteLine("Hello, World!");
Ladowy Ladowy = new Ladowy(new Wodny(new Powietrzny()), new Engine(100, Engine.TypPaliwa.benzyna));
Wodny Ladowy1 = new Wodny(engine: new Engine(100, Engine.TypPaliwa.benzyna));
List<ISrodowisko> list = Ladowy.DozwoloneSrodowiska;
foreach (var item in list)
{
    Console.WriteLine(item.GetType().Name);
}
Console.WriteLine(Ladowy.ToString());
