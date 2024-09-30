using System;
using System.Collections.Generic;
using System.Linq;

abstract class StorageDevice
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public StorageDevice(string name, string manufacturer, string model, int quantity, decimal price)
    {
        Name = name;
        Manufacturer = manufacturer;
        Model = model;
        Quantity = quantity;
        Price = price;
    }

    public virtual void Print()
    {
        Console.ForegroundColor = GetRandomColor();
        Console.WriteLine($"название: {Name}");
        Console.ForegroundColor = GetRandomColor();
        Console.WriteLine($"производитель: {Manufacturer}");
        Console.ForegroundColor = GetRandomColor();
        Console.WriteLine($"модель: {Model}");
        Console.ForegroundColor = GetRandomColor();
        Console.WriteLine($"количество: {Quantity}");
        Console.ForegroundColor = GetRandomColor();
        Console.WriteLine($"цена: {Price}");
        Console.ResetColor();
    }

    public abstract void LoadFromFile(string filename);
    public abstract void SaveToFile(string filename);

    protected ConsoleColor GetRandomColor()
    {
        var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
        colors.Remove(ConsoleColor.Black); 
        Random random = new Random();
        return colors[random.Next(colors.Count)];
    }
}

class Flash : StorageDevice
{
    public int MemorySize { get; set; }
    public int UsbSpeed { get; set; }

    public Flash(string name, string manufacturer, string model, int quantity, decimal price, int memorySize, int usbSpeed)
        : base(name, manufacturer, model, quantity, price)
    {
        MemorySize = memorySize;
        UsbSpeed = usbSpeed;
    }

    public override void Print()
    {
        base.Print();
        Console.ForegroundColor = GetRandomColor();
        Console.WriteLine($"объем памяти: {MemorySize} GB");
        Console.ForegroundColor = GetRandomColor();
        Console.WriteLine($"cкорость USB: {UsbSpeed} MB/s");
        Console.ResetColor();
    }

    public override void LoadFromFile(string filename)
    {
        Console.WriteLine("загрузка flash из файла");
    }

    public override void SaveToFile(string filename)
    {
        Console.WriteLine("cохранение flash в файл");
    }
}

class DVD : StorageDevice
{
    public int ReadSpeed { get; set; }
    public int WriteSpeed { get; set; }

    public DVD(string name, string manufacturer, string model, int quantity, decimal price, int readSpeed, int writeSpeed)
        : base(name, manufacturer, model, quantity, price)
    {
        ReadSpeed = readSpeed;
        WriteSpeed = writeSpeed;
    }

    public override void Print()
    {
        base.Print();
        Console.ForegroundColor = GetRandomColor();
        Console.WriteLine($"cкорость чтения: {ReadSpeed}x");
        Console.ForegroundColor = GetRandomColor();
        Console.WriteLine($"cкорость записи: {WriteSpeed}x");
        Console.ResetColor();
    }

    public override void LoadFromFile(string filename)
    {
        Console.WriteLine("загрузка dvd из файла");
    }

    public override void SaveToFile(string filename)
    {
        Console.WriteLine("сохранение dvd в файл");
    }
}

class RemovableHDD : StorageDevice
{
    public int DiskSize { get; set; }
    public int UsbSpeed { get; set; }

    public RemovableHDD(string name, string manufacturer, string model, int quantity, decimal price, int diskSize, int usbSpeed)
        : base(name, manufacturer, model, quantity, price)
    {
        DiskSize = diskSize;
        UsbSpeed = usbSpeed;
    }

    public override void Print()
    {
        base.Print();
        Console.ForegroundColor = GetRandomColor();
        Console.WriteLine($"размер диска: {DiskSize} GB");
        Console.ForegroundColor = GetRandomColor();
        Console.WriteLine($"скорость usb: {UsbSpeed} MB/s");
        Console.ResetColor();
    }

    public override void LoadFromFile(string filename)
    {
        Console.WriteLine("загрузка HDD из файла");
    }

    public override void SaveToFile(string filename)
    {
        Console.WriteLine("сохранение HDD в файл");
    }
}

class Program
{
    static List<StorageDevice> devices = new List<StorageDevice>();

    static void Main()
    {
        devices.Add(new Flash("usb 3.0", "sanDisk", "ultra", 10, 799.99m, 64, 100));
        devices.Add(new DVD("dvd rw", "sony", "rw123", 25, 99.99m, 16, 8));
        devices.Add(new RemovableHDD("portable HDD", "seagate", "backup plus", 5, 4999.99m, 1000, 150));

        PrintDevices();
        SearchDevice("sony");
        RemoveDevice("usb 3.0");

        PrintDevices();
    }

    static void PrintDevices()
    {
        Console.WriteLine("\nсписок устройств:");
        foreach (var device in devices)
        {
            device.Print();
            Console.WriteLine();
        }
    }

    static void SearchDevice(string searchTerm)
    {
        Console.WriteLine($"\nпоиск устройств по критериям: {searchTerm}");
        var results = devices.Where(d => d.Name.Contains(searchTerm) || d.Manufacturer.Contains(searchTerm)).ToList();

        foreach (var device in results)
        {
            device.Print();
            Console.WriteLine();
        }
    }

    static void RemoveDevice(string name)
    {
        Console.WriteLine($"\nудаление устройства с названием: {name}");
        devices.RemoveAll(d => d.Name == name);
    }
}
