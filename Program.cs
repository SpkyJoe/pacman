using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;


namespace Pacman
{
    class Program
    {
        static void Main(string[] args)
        {
            Scene scene = new Scene();
            if (scene.loader != null) scene.loader.Load("maze");
            

            using (var window = new RenderWindow(
                       new VideoMode(828, 900), "Pacman"))
            {
                window.Closed += (o, e) => window.Close();
                window.SetView(new View(new FloatRect(18, 0, 414, 450)));
                //TODO: initialize
                Clock clock = new Clock();
                while (window.IsOpen)
                {
                    window.DispatchEvents();
                    float deltatime = clock.Restart().AsSeconds();
                    deltatime = MathF.Min(deltatime, 0.1f);
                    if (deltatime > 0.1f) deltatime = 0.1f;
                    scene.UpdateAll(deltatime);
                    window.Clear(new Color(223, 246, 245));
                    scene.RenderAll(window);
                    window.Display();
                }
            }
        }
    }
}