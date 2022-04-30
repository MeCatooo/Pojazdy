// See https://aka.ms/new-console-template for more information
using PojazdyV2;
Console.WriteLine("Hello, World!");
Powietrzny Ladowy = new Powietrzny(new Wodny(), new Engine(100, Engine.TypPaliwa.benzyna));
//Wodny Ladowy1 = new Wodny(engine: new Engine(100, Engine.TypPaliwa.benzyna));
//List<ISrodowisko> list = Ladowy.DozwoloneSrodowiska;
//foreach (var item in list)
//{
//    Console.WriteLine(item.GetType().Name);
//}
//Console.WriteLine(Ladowy.ToString());
Ladowy.TurnON();
Ladowy.Move(new Speed());
Console.WriteLine(Ladowy.ToString());
Ladowy.Move(new Speed(20, Speed.SpeedUnits.ms));
Ladowy.MoveToNext(new Powietrze());
Console.WriteLine(Ladowy.ToString());
//Ladowy.MoveToNext(new Woda());
//Console.WriteLine();
//Console.WriteLine(Ladowy.ToString());
//Console.WriteLine();
//Ladowy.MoveToNext(new Powietrze());
//Console.WriteLine(Ladowy.ToString());
//AbstractPojazd AbstractPojazd = new Ladowy(new AbstractPojazd());