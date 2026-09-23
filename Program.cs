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
<<<<<<< HEAD
<<<<<<< Updated upstream
                scene.Load("level0");
=======
                scene.Load("level0"); // Laddar "level0" innan allt så en bana finns färdigladdad vid start
>>>>>>> 32953c5bd82b9071865664228f2660d4faa46e45
                
                window.SetView(new View(
                    new Vector2f(200,150),
                    new Vector2f(400,300)
                    )); // Trots att videomode(800,600) är större än dessa värden vill vi zooma in till detta istället.
                
=======

                scene.Load("level0");
                
                
               
>>>>>>> Stashed changes
                
                while (window.IsOpen)
                {
                    float deltaTime = clock.Restart().AsSeconds(); 
                    if(deltaTime > 0.1f) deltaTime = 0.1f; //Limitar deltaTime att inte flippa ut över 0.1sek, så inte mekaniken går sönder helt.
                    
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

