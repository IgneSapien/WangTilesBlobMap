using SadConsole;
using Console = SadConsole.Console;
using System.Collections.Generic;
using GoRogue.MapViews;
using Troschuetz.Random;
using Troschuetz.Random.Generators;
using Wang;
using Helpers = Wang.Helpers;

public static class Program
{
    static void Main()
    {
        // Setup the engine and create the main window.
        SadConsole.Game.Create(@"Cheepicus12.font", 80,80);

        // Hook the start event so we can add consoles to the system.
        SadConsole.Game.OnInitialize = Init;

        // Start the game.
        SadConsole.Game.Instance.Run();
        SadConsole.Game.Instance.Dispose();
    }

    static void Init()
    {
        //set up seed
        uint seed = TMath.Seed();
        
        //overide
        //seed = 809171303;

        System.Console.WriteLine(seed.ToString());

        var gen = new XorShift128Generator(seed);
        for (int i = 0; i < 3; i++) // Becuase of bug in RNG, make sure we are completely pseudo-random
            gen.Next();
        GoRogue.Random.SingletonRandom.DefaultRNG = gen;

        MapGen generator = new MapGen(BuildBlob9());

        ArrayMap<Cell> map = generator.GenerateMap(9);

        var console = new Console(map.Width, map.Height, map);

        SadConsole.Global.CurrentScreen = console;
    }


    //Hardcoded examples tile sets but you should be able to pass the generator any indexed dictionary of list of tiles
    //BuildBlob3() is the most basic 3x3 set
    //BuildBlob9() is a 9x9 set with veriations

    static Dictionary<int, List<int[,]>> BuildBlob9()
    {
        Dictionary<int, List<int[,]>> returnBlob = new Dictionary<int, List<int[,]>>();

        List<int[,]> zero = new List<int[,]>
        {
            new int[,]{
            {  0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {  0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {  0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {  0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {  0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {  0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {  0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {  0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {  0,  0,  0,  0,  0,  0,  0,  0,  0   }}

        };

        returnBlob.Add(0, zero);

        List<int[,]> one = new List<int[,]>
        {
            new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   }
            }

             ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  1,  1,  1,  1,  1,  0,  0   },
            {   0,  1,  1,  1,  0,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  0,  1,  1,  1,  0   },
            {   0,  1,  1,  0,  0,  0,  1,  1,  0   },
            {   0,  0,  1,  1,  1,  1,  1,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   } }


           ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  1,  0,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  0,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  1,  0,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  0,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  0,  1,  1,  1,  1,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   } }


            ,new int[,] {
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  0,  0,  1,  0,  0,  1,  0   },
            {   0,  1,  0,  0,  1,  0,  0,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  0,  0,  1,  0,  0,  1,  0   },
            {   0,  1,  0,  0,  1,  0,  0,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   } }
        };

        returnBlob.Add(1, one);

        //Add rotations 
        returnBlob.Add(4,  Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(1)));
        returnBlob.Add(16, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(4)));
        returnBlob.Add(64, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(16)));

        List<int[,]> five = new List<int[,]>
        {
            new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   }
            }

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  0,  0,  0,  0,  0,  1,  0   },
            {   0,  1,  0,  1,  1,  1,  0,  1,  0   },
            {   0,  1,  0,  1,  1,  1,  0,  1,  1   },
            {   0,  1,  0,  1,  1,  1,  0,  1,  0   },
            {   0,  1,  0,  0,  1,  0,  0,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   }}

        };

        returnBlob.Add(5, five);

        //Add rotations 
        returnBlob.Add(20, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(5)));
        returnBlob.Add(80, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(20)));
        returnBlob.Add(65, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(80)));

        List<int[,]> seven = new List<int[,]>
        {
            new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   }
            }

            ,new int[,] {
            {   0,  0,  0,  0,  0,  0,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  0,  0,  0,  0,  0,  0,  0   },
            {   0,  1,  0,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  0,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  0,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  0,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  1,  1,  1,  0,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  0,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  0,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  0,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  0,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  0,  1,  1,  0   },
            {   0,  0,  0,  0,  0,  0,  0,  0,  0   }}



        };

        returnBlob.Add(7, seven);

        //Add rotations 
        returnBlob.Add(28,  Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(7)));
        returnBlob.Add(112, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(28)));
        returnBlob.Add(193, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(112)));


        List<int[,]> oneSeven = new List<int[,]>
        {
            new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }
            }

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  0,  1,  0,  1,  0,  1,  0   },
            {   0,  1,  0,  1,  1,  1,  0,  1,  0   },
            {   0,  1,  0,  1,  0,  1,  0,  1,  0   },
            {   0,  1,  0,  1,  1,  1,  0,  1,  0   },
            {   0,  1,  0,  1,  0,  1,  0,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}

        };

        returnBlob.Add(17, oneSeven);

        //Add rotations 
        returnBlob.Add(68, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(17)));

        List<int[,]> twoOne = new List<int[,]>
        {
            new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }
            }

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  0,  0,  1,  0,  0,  1,  0   },
            {   0,  1,  0,  1,  1,  1,  0,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  0,  1,  1,  1,  0,  1,  0   },
            {   0,  1,  0,  0,  1,  0,  0,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  1,  0,  1,  1,  0,  0,  0   },
            {   0,  1,  1,  0,  1,  1,  1,  0,  0   },
            {   0,  1,  1,  0,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  0,  1,  1,  1,  1,  0   },
            {   0,  1,  1,  0,  1,  1,  1,  0,  0   },
            {   0,  1,  1,  0,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}

        };

        returnBlob.Add(21, twoOne);

        //Add rotations 
        returnBlob.Add(84, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(21)));
        returnBlob.Add(81, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(84)));
        returnBlob.Add(69, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(81)));


        List<int[,]> twoThree = new List<int[,]>
        {
            new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }
            }

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  0,  0,  1   },
            {   0,  1,  1,  0,  1,  1,  0,  0,  1   },
            {   0,  1,  1,  0,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  0,  0,  1,  1,  1,  1,  0   },
            {   0,  1,  0,  0,  0,  0,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  0,  1,  1,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}

        };

        returnBlob.Add(23, twoThree);

        //Add rotations 
        returnBlob.Add(92,  Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(23)));
        returnBlob.Add(113, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(92)));
        returnBlob.Add(197, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(113)));

        List<int[,]> twoNine = new List<int[,]>
        {
            new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   }
            }

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  0,  0,  1,  1,  1   },
            {   0,  0,  0,  0,  0,  0,  1,  1,  1   },
            {   0,  0,  0,  0,  0,  0,  1,  1,  1   },
            {   0,  0,  0,  0,  0,  0,  1,  1,  1   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  1,  1,  1,  1,  1,  1,  1   }}

        };

        returnBlob.Add(29, twoNine);

        //Add rotations 
        returnBlob.Add(116, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(29)));
        returnBlob.Add(209, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(116)));
        returnBlob.Add(71,  Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(209)));

        List<int[,]> threeOne = new List<int[,]>
        {
            new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   }
            }

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  1,  1,  1,  1,  0,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  1,  1,  1,  1,  0,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  1,  1,  1,  1,  0,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  1,  1,  1,  1,  0,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  1,  1,  1,  0,  1,  1,  0   },
            {   0,  1,  1,  0,  1,  0,  1,  1,  0   },
            {   0,  1,  1,  0,  1,  0,  1,  1,  0   },
            {   0,  1,  1,  0,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  0,  1,  0,  1,  1,  0   },
            {   0,  1,  1,  0,  1,  0,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  0,  1,  1,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}
										
            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  0,  0,  0,  0,  0,  0,  1   },
            {   0,  1,  0,  1,  1,  1,  1,  0,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  0,  1,  1,  1,  1,  0,  1   },
            {   0,  1,  0,  0,  0,  0,  0,  0,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   }}

        };

        returnBlob.Add(31, threeOne);


        //Add rotations 
        returnBlob.Add(124, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(31)));
        returnBlob.Add(241, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(124)));
        returnBlob.Add(199, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(241)));

        List<int[,]> eightFive = new List<int[,]>
        {
            new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  1,  1,  1,  1,  1,  0,  0   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  1,  1,  1,  1,  1,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }
            }

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  1,  0,  0,  0,  0,  0,  1,  0   },
            {   0,  1,  0,  0,  0,  0,  0,  1,  0   },
            {   1,  1,  0,  0,  0,  0,  0,  1,  1   },
            {   0,  1,  0,  0,  0,  0,  0,  1,  0   },
            {   0,  1,  0,  0,  0,  0,  0,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  1,  1,  1,  1,  1,  0,  0   },
            {   0,  1,  1,  1,  0,  1,  1,  1,  0   },
            {   0,  1,  1,  0,  0,  0,  1,  1,  0   },
            {   1,  1,  0,  0,  0,  0,  0,  1,  1   },
            {   0,  1,  1,  0,  0,  0,  1,  1,  0   },
            {   0,  1,  1,  1,  0,  1,  1,  1,  0   },
            {   0,  0,  1,  1,  1,  1,  1,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}

        };

        returnBlob.Add(85, eightFive);

        List<int[,]> eightSeven = new List<int[,]>
        {
            new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }
            }

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  0,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   },
            {   0,  0,  0,  1,  1,  1,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  0,  0,  0,  1,  1,  1   },
            {   1,  1,  0,  0,  0,  0,  0,  1,  1   },
            {   0,  1,  0,  0,  0,  0,  0,  1,  0   },
            {   0,  1,  1,  0,  0,  0,  1,  1,  0   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  1,  1,  0,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  0,  1   },
            {   0,  0,  0,  1,  1,  0,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  1,  0   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  0,  1,  0,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  0   }}

        };

        returnBlob.Add(87, eightSeven);

        //Add rotations 
        returnBlob.Add(93,  Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(87)));
        returnBlob.Add(117, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(93)));
        returnBlob.Add(213, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(117)));

        List<int[,]> nineFive = new List<int[,]>
        {
            new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   }
            }

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  0,  1,  1   },
            {   1,  0,  0,  0,  1,  1,  0,  1,  1   },
            {   1,  0,  0,  0,  1,  1,  0,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  0,  0,  0,  1,  1,  0,  1,  1   },
            {   1,  0,  0,  0,  1,  1,  0,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  0,  1,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  1,  1   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  0,  1,  0,  1   },
            {   0,  1,  1,  1,  1,  1,  0,  1,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  1   },
            {   1,  1,  1,  1,  1,  1,  0,  1,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  1   },
            {   0,  1,  1,  1,  1,  1,  0,  1,  1   },
            {   0,  1,  1,  1,  1,  0,  1,  0,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   }}

        };

        returnBlob.Add(95, nineFive);

        //Add rotations 
        returnBlob.Add(125, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(95)));
        returnBlob.Add(245, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(125)));
        returnBlob.Add(215, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(245)));

        List<int[,]> oneOneNine = new List<int[,]>
        {
            new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  0,  0   },
            {   1,  1,  1,  1,  1,  1,  0,  0,  0   },
            {   1,  1,  1,  1,  1,  0,  0,  0,  0   },
            {   1,  1,  1,  1,  1,  0,  0,  0,  0   }
            }

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  1   },
            {   0,  0,  0,  0,  1,  0,  0,  0,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   1,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   1,  0,  0,  0,  1,  0,  0,  0,  0   },
            {   1,  1,  1,  1,  1,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  0,  0,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  0,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  0,  1,  1   },
            {   0,  0,  0,  0,  0,  0,  0,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  0,  0,  0,  0,  0,  0,  0   },
            {   1,  1,  0,  1,  1,  1,  1,  1,  0   },
            {   1,  1,  0,  1,  1,  1,  1,  1,  0   },
            {   1,  1,  0,  0,  1,  0,  0,  0,  0   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  0,  0   },
            {   0,  0,  0,  0,  1,  1,  1,  0,  0   },
            {   0,  0,  0,  1,  1,  0,  0,  1,  1   },
            {   0,  0,  1,  1,  1,  0,  1,  1,  1   },
            {   1,  1,  1,  0,  1,  0,  1,  1,  1   },
            {   1,  1,  1,  0,  1,  1,  1,  0,  0   },
            {   1,  1,  0,  0,  1,  1,  0,  0,  0   },
            {   0,  0,  1,  1,  1,  0,  0,  0,  0   },
            {   0,  0,  1,  1,  1,  0,  0,  0,  0   }}

        };

        returnBlob.Add(119, oneOneNine);

        //Add rotations 
        returnBlob.Add(221, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(119)));


        List<int[,]> oneTwoSeven = new List<int[,]>
        {
            new int[,] { 
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  1,  1,  1,  1,  1,  1   },
            {   0,  0,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   }
            }

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   0,  1,  0,  0,  0,  0,  0,  0,  1   },
            {   0,  1,  0,  1,  1,  1,  1,  0,  1   },
            {   1,  1,  0,  1,  1,  1,  1,  0,  1   },
            {   1,  1,  0,  1,  1,  1,  1,  0,  1   },
            {   1,  1,  0,  1,  1,  1,  1,  0,  1   },
            {   1,  1,  0,  0,  1,  1,  0,  0,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  0,  0,  1   },
            {   0,  0,  0,  1,  1,  0,  0,  0,  1   },
            {   0,  0,  1,  1,  0,  0,  1,  0,  1   },
            {   1,  1,  1,  0,  0,  1,  1,  0,  1   },
            {   1,  1,  0,  0,  1,  1,  1,  1,  1   },
            {   1,  0,  0,  1,  1,  1,  1,  0,  1   },
            {   1,  0,  0,  0,  0,  1,  0,  0,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   }}

            ,new int[,] {
            {   0,  0,  0,  0,  1,  1,  1,  1,  1   },
            {   0,  0,  0,  0,  1,  1,  0,  1,  1   },
            {   0,  0,  0,  1,  1,  0,  0,  0,  1   },
            {   0,  0,  1,  1,  1,  1,  0,  1,  1   },
            {   1,  1,  1,  1,  0,  1,  1,  1,  1   },
            {   1,  1,  0,  1,  1,  1,  0,  1,  1   },
            {   1,  0,  0,  0,  1,  0,  0,  0,  1   },
            {   1,  1,  0,  1,  1,  1,  0,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   }}

        };

        returnBlob.Add(127, oneTwoSeven);

        //Add rotations 
        returnBlob.Add(253, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(127)));
        returnBlob.Add(247, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(253)));
        returnBlob.Add(223, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(247)));

        List<int[,]> twoFiveFive = new List<int[,]>
        {
            new int[,] { 
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   }
            }

            ,new int[,] {
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  0,  0,  0,  0,  0,  0,  0,  1   },
            {   1,  0,  1,  1,  1,  1,  1,  0,  1   },
            {   1,  0,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  0,  1,  1,  1,  1,  1,  0,  1   },
            {   1,  0,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  0,  1,  1,  1,  1,  1,  0,  1   },
            {   1,  0,  0,  0,  0,  0,  0,  0,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   }}

            ,new int[,] {
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  0,  1,  0,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  0,  1,  1,  1,  1,  1,  0,  1   },
            {   1,  1,  1,  1,  0,  1,  1,  1,  1   },
            {   1,  0,  1,  1,  1,  1,  1,  0,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  1,  1,  0,  1,  0,  1,  1,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   }}
										
            ,new int[,] {
            {   1,  1,  0,  1,  1,  1,  0,  1,  1   },
            {   1,  0,  0,  1,  0,  1,  0,  0,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  0,  0,  1,  0,  1,  0,  0,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  0,  0,  1,  0,  1,  0,  0,  1   },
            {   1,  1,  1,  1,  1,  1,  1,  1,  1   },
            {   1,  0,  0,  1,  0,  1,  0,  0,  1   },
            {   1,  1,  0,  1,  1,  1,  0,  1,  1   }}

        };

        returnBlob.Add(225, twoFiveFive);

        return returnBlob;
    }

    static Dictionary<int, List<int[,]>> BuildBlob3()
    {
        Dictionary<int, List<int[,]>> returnBlob = new Dictionary<int, List<int[,]>>();

        List<int[,]> zero = new List<int[,]>
        {
            new int[,] { { 0, 0, 0 },
                       { 0, 0, 0 },
                       { 0, 0, 0 } }

        };

        returnBlob.Add(0, zero);

        List<int[,]> one = new List<int[,]>
        {
            new int[,] { { 0, 1, 0 },
                         { 0, 1, 0 },
                         { 0, 0, 0 } }
        };

        returnBlob.Add(1, one);

        //Add rotations 
        returnBlob.Add(4, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(1)));
        returnBlob.Add(16, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(4)));
        returnBlob.Add(64, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(16)));

        List<int[,]> five = new List<int[,]>
        {
            new int[,] { { 0, 1, 0 },
                         { 0, 1, 1 },
                                 { 0, 0, 0 } }
        };

        returnBlob.Add(5, five);

        //Add rotations 
        returnBlob.Add(20, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(5)));
        returnBlob.Add(80, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(20)));
        returnBlob.Add(65, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(80)));

        List<int[,]> seven = new List<int[,]>
        {
           new int[,] { { 0, 1, 1 },
                        { 0, 1, 1 },
                        { 0, 0, 0 } }
        };

        returnBlob.Add(7, seven);

        //Add rotations 
        returnBlob.Add(28, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(7)));
        returnBlob.Add(112, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(28)));
        returnBlob.Add(193, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(112)));


        List<int[,]> oneSeven = new List<int[,]>
        {
            new int[,] { { 0, 1, 0 },
                        { 0, 1, 0 },
                        { 0, 1, 0 } }
        };

        returnBlob.Add(17, oneSeven);

        //Add rotations 
        returnBlob.Add(68, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(17)));

        List<int[,]> twoOne = new List<int[,]>
        {
            new int[,] { { 0, 1, 0 },
                        { 0, 1, 1 },
                        { 0, 1, 0 } }
        };

        returnBlob.Add(21, twoOne);

        //Add rotations 
        returnBlob.Add(84, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(21)));
        returnBlob.Add(81, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(84)));
        returnBlob.Add(69, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(81)));


        List<int[,]> twoThree = new List<int[,]>
        {
           new int[,] { { 0, 1, 1 },
                        { 0, 1, 1 },
                        { 0, 1, 0 } }

        };

        returnBlob.Add(23, twoThree);

        //Add rotations 
        returnBlob.Add(92, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(23)));
        returnBlob.Add(113, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(92)));
        returnBlob.Add(197, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(113)));

        List<int[,]> twoNine = new List<int[,]>
        {
            new int[,] { { 0, 1, 0 },
                        { 0, 1, 1 },
                        { 0, 1, 1 } }


        };

        returnBlob.Add(29, twoNine);

        //Add rotations 
        returnBlob.Add(116, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(29)));
        returnBlob.Add(209, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(116)));
        returnBlob.Add(71, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(209)));

        List<int[,]> threeOne = new List<int[,]>
        {
            new int[,] { { 0, 1, 1 },
                        { 0, 1, 1 },
                        { 0, 1, 1 } }
        };

        returnBlob.Add(31, threeOne);


        //Add rotations 
        returnBlob.Add(124, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(31)));
        returnBlob.Add(241, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(124)));
        returnBlob.Add(199, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(241)));

        List<int[,]> eightFive = new List<int[,]>
        {
            new int[,] { { 0, 1, 0 },
                         { 1, 1, 1 },
                         { 0, 1, 0 } }

        };

        returnBlob.Add(85, eightFive);

        List<int[,]> eightSeven = new List<int[,]>
        {
           new int[,] { { 0, 1, 1 },
                        { 1, 1, 1 },
                        { 0, 1, 0 } }
        };

        returnBlob.Add(87, eightSeven);

        //Add rotations 
        returnBlob.Add(93, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(87)));
        returnBlob.Add(117, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(93)));
        returnBlob.Add(213, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(117)));

        List<int[,]> nineFive = new List<int[,]>
        {
           new int[,] { { 0, 1, 1 },
                        { 1, 1, 1 },
                        { 0, 1, 1 } }

        };

        returnBlob.Add(95, nineFive);

        //Add rotations 
        returnBlob.Add(125, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(95)));
        returnBlob.Add(245, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(125)));
        returnBlob.Add(215, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(245)));

        List<int[,]> oneOneNine = new List<int[,]>
        {
           new int[,] { { 0, 1, 1 },
                        { 1, 1, 1 },
                        { 1, 1, 0 } }

        };

        returnBlob.Add(119, oneOneNine);

        //Add rotations 
        returnBlob.Add(221, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(119)));


        List<int[,]> oneTwoSeven = new List<int[,]>
        {
            new int[,] { { 0, 1, 1 },
                        { 1, 1, 1 },
                        { 1, 1, 1 } }

        };

        returnBlob.Add(127, oneTwoSeven);

        //Add rotations 
        returnBlob.Add(253, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(127)));
        returnBlob.Add(247, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(253)));
        returnBlob.Add(223, Helpers.TranlateArrayList(returnBlob.GetValueOrDefault(247)));

        List<int[,]> twoFiveFive = new List<int[,]>
        {
           new int[,] { { 1, 1, 1 },
                        { 1, 1, 1 },
                        { 1, 1, 1 } }
        };

        returnBlob.Add(225, twoFiveFive);

        return returnBlob;
    }

}

