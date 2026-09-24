using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;

namespace platformer
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory); 
            
            using (var window = new RenderWindow(new VideoMode(800, 600), "Platformer"))
            {
                window.Closed += (o, e) => window.Close();
                Clock clock = new Clock();
                Scene scene = new Scene();
                scene.Load("level0"); // Laddar "level0" innan allt så en bana finns färdigladdad vid start
                
                window.SetView(new View(
                    new Vector2f(200,150),
                    new Vector2f(400,300)
                    )); // Trots att videomode(800,600) är större än dessa värden vill vi zooma in till detta istället.
                
                
                
                
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

