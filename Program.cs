using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace platformer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var window = new RenderWindow(new VideoMode(800, 600), "Platformer"))
            {
                window.Closed += (o, e) => window.Close();
                Clock clock = new Clock();
                while (window.IsOpen)
                {
                    float deltaTime = clock.Restart().AsSeconds();
                    
                    window.Clear();
                    
                    
                    
                    window.Display();
                }
            }
        }
    }
}

