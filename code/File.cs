using Godot;
using System;
using System.Data.Common;
using System.IO;
using System.Net;
using System.Text.Json;

//Class for using files
public partial class File : Node
{
    //Path to initial save file
    public static string SavePath = "res://data/SaveFile.json";
    //Path to user's save file
    public static string UserSavePath = OS.GetUserDataDir() + "/SaveFile.json";
    //Path to data for items
    public static string ItemPath = "res://data/ItemData.json";

    //Loads data from a json file and returns it as a Godot dictionary
    public static Godot.Collections.Dictionary getDictionary(string path)
    {
        string loadedData = LoadText(path);

        Json jsonLoader = new Json();

        Error error = jsonLoader.Parse(loadedData);

        if (error != Error.Ok)
        {
            GD.PrintErr(error);
            return new Godot.Collections.Dictionary();
        }

        Godot.Collections.Dictionary data = (Godot.Collections.Dictionary)jsonLoader.Data;

        return data;
    }

    //Loads text from a file and returns it as a string
    public static string LoadText(string path)
    {
        var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);
        string data = file.GetAsText();
        return data;
    }

    //Takes save data as a parameter and saves it to a json file to the user's save file path
    public static void SaveGame(Data data)
    {
        string json = JsonSerializer.Serialize(data);
        using Godot.FileAccess file = Godot.FileAccess.Open(UserSavePath, Godot.FileAccess.ModeFlags.Write);
        file.StoreString(json);
    }

    //Reads data from the user's save file at the UserSavePath and returns it as Data
    public static Data LoadGame()
    {
        if (!Godot.FileAccess.FileExists(UserSavePath))
        {
            CopyFile(SavePath, UserSavePath);
        }
        using Godot.FileAccess file = Godot.FileAccess.Open(UserSavePath, Godot.FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        Data loadedData = JsonSerializer.Deserialize<Data>(json);

        using Godot.FileAccess initDataFile = Godot.FileAccess.Open(SavePath, Godot.FileAccess.ModeFlags.Read);
        string initDataString = initDataFile.GetAsText();
        Data initData = JsonSerializer.Deserialize<Data>(initDataString);

        if (loadedData.OwnedSkins == null)
        {
            CopyFile(SavePath, UserSavePath);
            return initData;
        }

        if (loadedData.OwnedSkins.Length != initData.OwnedSkins.Length)
        {
            CopyFile(SavePath, UserSavePath);
            return initData;
        }

        return loadedData;
    }


    //Copies a file's text to another file
    private static void CopyFile(string path1, string path2)
    {
        if (ResourceLoader.Exists(path1))
        {
            Godot.FileAccess file = Godot.FileAccess.Open(path1, Godot.FileAccess.ModeFlags.Read);
            string content = file.GetAsText();
            file.Close();

            System.IO.File.WriteAllText(path2, content);
        }
        else
        {
            GD.PrintErr("error");
        }
    }

}
