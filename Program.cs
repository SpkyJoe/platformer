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
                Scene scene = new Scene();
<<<<<<< Updated upstream
                scene.Load("level0");
                
                window.SetView(new View(
                    new Vector2f(200,150),
                    new Vector2f(400,300)
                    ));
                
=======

                scene.Load("level0");
                
                
               
>>>>>>> Stashed changes
                
                while (window.IsOpen)
                {
                    float deltaTime = clock.Restart().AsSeconds();
                    if(deltaTime > 0.1f) deltaTime = 0.1f;
                    
                    window.DispatchEvents();
                    scene.UpdateAll(deltaTime);
                    
                    window.Clear();
                    scene.RenderAll(window);
                    
                    
                    window.Display();
                }
            }
        }
    }
}

