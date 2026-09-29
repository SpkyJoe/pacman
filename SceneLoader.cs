using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Collections.Generic;
using SFML.System;
namespace Pacman;

public class SceneLoader
{
    private readonly Dictionary<char, Func<Entity>> loaders;
    private string currentScene = "", nextScene = "";

    static Vector2f CalculateTilePosition(int row, int column, Vector2f tileSize)
    {
        return new Vector2f();
    }
    
    
    public SceneLoader()
    {
        loaders = new Dictionary<char, Func<Entity>>()
        {
            { '#', () => new Wall() },
            { '.', () => new Coin() },
            {'g', () => new Ghost() },
            {'p', () => new Pacman() }
        };
    }

    public void HandleSceneLoad(Scene scene)
    {
        string file = $"assets/{nextScene}.txt";
        Console.WriteLine($"loading scene '{file}'");
        
        if (nextScene == "") return;
        scene.Clear();
        int rad = 0;
        foreach (var line in File.ReadLines(file, Encoding.UTF8))
        {
            rad++;
            Console.WriteLine($"{rad}");
            if (line.Length != 0)
            {
                string eachLine = line.Trim();
                for (int i = 0; i < eachLine.Length; i++)
                {
                    char charObj = eachLine[i];
                    if (charObj == '|') continue;
                    Create(charObj, out Entity created);
                    scene.Spawn(created);
                    created.Position = CalculateTilePosition(rad, i, new Vector2f(18, 18));
                }
            }
        }
        currentScene = nextScene;
        nextScene = "";
    }
    
    public void Load(string scene) => nextScene = scene;
    
    public void Reload() => nextScene = currentScene;

    private bool Create(char symbol, out Entity created)
    {
        if (loaders.TryGetValue(symbol, out Func<Entity> loader))
        {
            created = loader();
            return true;
        }
        created = null;
        return false;
    }
}