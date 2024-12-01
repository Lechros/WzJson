﻿using System.Diagnostics;
using Newtonsoft.Json;
using WzJson.Common;
using WzJson.Common.Exporter;
using WzJson.Parser;
using WzJson.Repository;

string outputRoot = Path.Join(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\output\");

Stopwatch sw = new();
bool exitFlag = false;

Console.WriteLine("Loading wz...");
sw.Restart();
WzProvider wz = new(@"C:\Nexon\Maple");
var gearNodeRepository = new GearNodeRepository(wz);
var stringEqpNodeRepository = new StringEqpNodeRepository(wz);
var itemOptionNodeRepository = new ItemOptionNodeRepository(wz);
var itemNodeRepository = new ItemNodeRepository(wz);
var skillNodeRepository = new SkillNodeRepository(wz);
var soulNodeRepository = new SoulNodeRepository(wz);
var soulStringNodeRepository = new SoulStringNodeRepository(wz);
var stringSkillNodeRepository = new StringSkillNodeRepository(wz);
var exporters = new List<IExporter>
{
    new JsonFileExporter(outputRoot, JsonSerializer.CreateDefault()),
    new PngFilesExporter(outputRoot)
};
sw.Stop();
Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");

void ExportDatas(IList<IData> datas)
{
    foreach (var data in datas)
    {
        exporters.First(exporter => exporter.Supports(data)).Export(data);
    }
}

void DisposeDatas(IList<IData> datas)
{
    foreach (var data in datas)
    {
        if (data is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }
}

List<(string, Action)> options = new()
{
    ("export gear data", () =>
        {
            Console.WriteLine("Loading gear data...");
            sw.Restart();
            var parser = new GearParser(wz.FindNode, gearNodeRepository, stringEqpNodeRepository);
            parser.ParseGearData = true;
            var datas = parser.Parse();
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");

            Console.WriteLine("Saving to file...");
            sw.Restart();
            ExportDatas(datas);
            DisposeDatas(datas);
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");
        }
    ),
    ("export gear icons/origins (takes long)", () =>
        {
            Console.WriteLine("Loading gear data...");
            sw.Restart();
            var parser = new GearParser(wz.FindNode, gearNodeRepository, stringEqpNodeRepository);
            parser.ParseGearIcon = true;
            parser.ParseGearIconOrigin = true;
            var datas = parser.Parse();
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");

            Console.WriteLine("Saving to file...");
            sw.Restart();
            ExportDatas(datas);
            DisposeDatas(datas);
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");
        }
    ),
    ("export gear raw icons/origins (takes long)", () =>
        {
            Console.WriteLine("Loading gear data...");
            sw.Restart();
            var parser = new GearParser(wz.FindNode, gearNodeRepository, stringEqpNodeRepository);
            parser.ParseGearIconRaw = true;
            parser.ParseGearIconRawOrigin = true;
            var datas = parser.Parse();
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");

            Console.WriteLine("Saving to file...");
            sw.Restart();
            ExportDatas(datas);
            DisposeDatas(datas);
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");
        }
    ),
    ("export item option data", () =>
        {
            Console.WriteLine("Loading item option data...");
            sw.Restart();
            var parser = new ItemOptionParser(itemOptionNodeRepository);
            var datas = parser.Parse();
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");

            Console.WriteLine("Saving to file...");
            sw.Restart();
            ExportDatas(datas);
            DisposeDatas(datas);
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");
        }
    ),
    ("export set item data", () =>
        {
            Console.WriteLine("Loading set item data...");
            sw.Restart();
            var parser = new SetItemParser(new SetItemNodeRepository(wz), itemOptionNodeRepository);
            var datas = parser.Parse();
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");

            Console.WriteLine("Saving to file...");
            sw.Restart();
            ExportDatas(datas);
            DisposeDatas(datas);
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");
        }
    ),
    ("export soul data", () =>
        {
            Console.WriteLine("Loading soul data...");
            sw.Restart();
            SoulParser parser = new SoulParser(soulNodeRepository, soulStringNodeRepository, stringSkillNodeRepository);
            var datas = parser.Parse();
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");

            Console.WriteLine("Saving to file...");
            sw.Restart();
            ExportDatas(datas);
            DisposeDatas(datas);
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");
        }
    ),
    ("export items icons + origins (takes long)", () =>
        {
            Console.WriteLine("Loading item data...");
            sw.Restart();
            var parser = new ItemParser(itemNodeRepository, wz.FindNode);
            var datas = parser.Parse();
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");

            Console.WriteLine("Saving to file...");
            sw.Restart();
            ExportDatas(datas);
            DisposeDatas(datas);
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");
        }
    ),
    ("export skill icons", () =>
        {
            Console.WriteLine("Loading skill data...");
            sw.Restart();
            var parser = new SkillParser(skillNodeRepository, wz.FindNode);
            var datas = parser.Parse();
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");

            Console.WriteLine("Saving to file...");
            Console.WriteLine(Path.GetFullPath(Path.Join(outputRoot, @"skillicon\")));
            sw.Restart();
            ExportDatas(datas);
            DisposeDatas(datas);
            sw.Stop();
            Console.WriteLine("Done!" + $" ({sw.ElapsedMilliseconds}ms)");
        }
    ),
};

while (!exitFlag)
{
    Console.WriteLine("----------\nChoose option:");
    for (int i = 0; i < options.Count; i++)
    {
        Console.WriteLine((i + 1) + ". " + options[i].Item1);
    }

    if (int.TryParse(Console.ReadLine(), out int input) && (uint)(input - 1) < options.Count)
    {
        options[input - 1].Item2();
    }
    else
    {
        exitFlag = true;
    }
}