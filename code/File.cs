using Godot;
using System;
using System.Data.Common;
using System.IO;
using System.Text.Json;

public partial class File : Node
{

    private static string SavePath = "res://data/SaveFile.json";
    private static string QuestPath = "res://data/QuestData.json";

    public override void _Ready()
    {

    }

    public static Godot.Collections.Dictionary getQuestions()
    {
        string loadedData = LoadQuestions();

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

    public static void SaveGame(Data data)
    {
        string json = JsonSerializer.Serialize(data);
        using Godot.FileAccess file = Godot.FileAccess.Open(SavePath, Godot.FileAccess.ModeFlags.Write);
        file.StoreString(json);
    }

    public static Data LoadGame()
    {
        if (!Godot.FileAccess.FileExists(SavePath))
            return new Data();

        using Godot.FileAccess file = Godot.FileAccess.Open(SavePath, Godot.FileAccess.ModeFlags.Read);
        string json = file.GetAsText();
        return JsonSerializer.Deserialize<Data>(json);
    }

    public static string LoadQuestions()
    {
        var file = Godot.FileAccess.Open(QuestPath, Godot.FileAccess.ModeFlags.Read);
        string data = file.GetAsText();
        return data;
    }
}
