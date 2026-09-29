using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Text;

namespace Pacman;

public class AssetManager
{
    public static readonly string AssetPath = "assets";
    private readonly Dictionary<string, Texture> textures;
    private readonly Dictionary<string, Font> fonts;

    public AssetManager()
    {
        textures = new Dictionary<string, Texture>();
        fonts = new Dictionary<string, Font>();
    }

    public Texture LoadTexture(string name)
    {
        if (textures.TryGetValue(name, out Texture found))
        {
            return found;
        }
        string textureName = ($"assets/{name}.png");
        Texture texture = new Texture(textureName);
        textures.Add(name, texture);
        
        return texture;
    }

    public Font LoadFont(string name)
    {
        if (fonts.TryGetValue(name, out Font found))
        {
            return found;
        }
        string fontName = ($"assets/{name}.ttf");
        Font font = fonts[fontName];
        fonts.Add(name, font);
        return font;
        
    }

}