﻿// See https://aka.ms/new-console-template for more information
using PojazdyV2.Pojazdy;
using PojazdyV2;
//Console.WriteLine("Hello, World!");
//Powietrzny Ladowy = new Powietrzny(new Wodny(), new Engine(100, Engine.TypPaliwa.benzyna));
////Wodny Ladowy1 = new Wodny(engine: new Engine(100, Engine.TypPaliwa.benzyna));
////List<ISrodowisko> list = Ladowy.DozwoloneSrodowiska;
////foreach (var item in list)
////{
////    Console.WriteLine(item.GetType().Name);
////}
////Console.WriteLine(Ladowy.ToString());
//Ladowy.TurnON();
//Ladowy.Move(new Speed());
//Console.WriteLine(Ladowy.ToString());
//Ladowy.Move(new Speed(20, Speed.SpeedUnits.ms));
//Ladowy.MoveToNext(new Woda());
//Console.WriteLine(Ladowy.ToString());
////Ladowy.MoveToNext(new Woda());
////Console.WriteLine();
////Console.WriteLine(Ladowy.ToString());
////Console.WriteLine();
////Ladowy.MoveToNext(new Powietrze());
////Console.WriteLine(Ladowy.ToString());
////AbstractPojazd AbstractPojazd = new Ladowy(new AbstractPojazd());
List<AbstractPojazd> lista = new List<AbstractPojazd>();
Zaglowka skuter = new Zaglowka();
//Wodny Wodny = new Wodny(new Wodny());
skuter.TurnON();
skuter.Move(new Speed());
lista.Add(skuter);
Amfibia Amfibia = new Amfibia();
Amfibia.TurnON();
Amfibia.Move(new Speed(100));
lista.Add(Amfibia);
//foreach (var item in lista)
//    Console.WriteLine(item);
//lista.Sort();
//foreach (var item in lista)
//    Console.WriteLine(item);
foreach (var item in lista)
{
    if(item.DozwoloneSrodowiska.OfType<Lad>().Any())
        Console.WriteLine(item);
}
Amfibia.MoveToNext(new Woda());
foreach (var item in lista)
{
    if(item.Srodowisko is Lad)
        Console.WriteLine(item);
}
Console.WriteLine(skuter.GetType().BaseType);


