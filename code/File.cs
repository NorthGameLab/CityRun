using Godot;
using System;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Text.Json;

public partial class File : Node
{
    public static string SavePath = "res://data/SaveFile.json";
    public static string UserSavePath = OS.GetUserDataDir() + "/SaveFile.json";
    public static string ItemPath = "res://data/ItemData.json";
    public static string UserSaveItemPath = OS.GetUserDataDir() + "/ItemSaveData.json";

    public static Godot.Collections.Dictionary getDictionary(string path)
    {
        string loadedData = LoadText(path);

        Json jsonLoader = new Json();

        Error error = jsonLoader.Parse(loadedData);

        if (error != Error.Ok)
        {
            GD.PrintErr("JSON Parsing Failed: " + error);
            return new Godot.Collections.Dictionary();
        }

        Godot.Collections.Dictionary data = (Godot.Collections.Dictionary)jsonLoader.Data;

        return data;
    }

    public static string LoadText(string path)
    {
        var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);
        string data = file.GetAsText();
        return data;
    }

    public static void SaveGame(Data data)
    {
        string json = JsonSerializer.Serialize(data);
        using Godot.FileAccess file = Godot.FileAccess.Open(UserSavePath, Godot.FileAccess.ModeFlags.Write);
        file.StoreString(json);
    }

    public static Data LoadGame()
    {
        if (!Godot.FileAccess.FileExists(UserSavePath))
        {
            CopyFileToUser(SavePath, UserSavePath);
        }
        using Godot.FileAccess file = Godot.FileAccess.Open(UserSavePath, Godot.FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        GD.Print("Loaded JSON: " + json);
        Data loadedData = JsonSerializer.Deserialize<Data>(json);

        using Godot.FileAccess initDataFile = Godot.FileAccess.Open(SavePath, Godot.FileAccess.ModeFlags.Read);
        string initDataString = initDataFile.GetAsText();
        GD.Print("Initial JSON: " + initDataString);
        Data initData = JsonSerializer.Deserialize<Data>(initDataString);

        if (loadedData.OwnedSkins == null)
        {
            GD.Print("UserSavePath: " + UserSavePath);
            GD.Print("SavePath: " + SavePath);
            CopyFileToUser(SavePath, UserSavePath);
            return initData;
        }

        if (loadedData.OwnedSkins.Length != initData.OwnedSkins.Length)
        {
            GD.Print("UserSavePath: " + UserSavePath);
            GD.Print("SavePath: " + SavePath);
            CopyFileToUser(SavePath, UserSavePath);
            return initData;
        }

        return loadedData;
    }


    private static void CopyFileToUser(string path1, string path2)
    {
        if (ResourceLoader.Exists(path1))
        {
            Godot.FileAccess file = Godot.FileAccess.Open(path1, Godot.FileAccess.ModeFlags.Read);
            string content = file.GetAsText();
            file.Close();

            System.IO.File.WriteAllText(path2, content);
            GD.Print("File copied to: " + path2);
        }
        else
        {
            GD.PrintErr("File not found in res://");
        }
    }

}
