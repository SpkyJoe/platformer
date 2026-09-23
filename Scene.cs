using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Text;
using Platformer;
using System.IO;

namespace platformer;

public class Scene
{
    private readonly Dictionary<string, Texture> textures;
    private readonly List<Entity> entities;
    private string currentScene;
    private string nextScene;
    
    
    public Scene()
    {
        textures = new Dictionary<string, Texture>();
        entities = new List<Entity>();
    }

    public bool FindByType<T>(out T found) where T : Entity
    {
        foreach (Entity entity in entities)
        {
            
            if (!entity.Dead && entity is T typed)
            {
                found = typed;
                return true;
            }
        }
        found = default(T);
        return false;
    }

    public void Spawn(Entity entity) //"Spawnar" en instans av den Entity som kallas, väldigt lätt att skapa kopior på koordinater
    {
        entities.Add(entity);
        entity.Create(this);
    }
    
    public Texture LoadTexture(string name) //Testar att hämta textur, som funktion i Entity. Som sedan constructorn i subklasserna kan ange filen där texturen hämtas från.
    {
        if (textures.TryGetValue(name, out Texture found))
        {
            return found;
        }
        string fileName = $"assets/{name}.png";
        Texture texture = new Texture(fileName);
        textures.Add(name, texture);
        return texture;
    }

    public void UpdateAll(float deltaTime)
    {
        HandleSceneChange(); // byter bana det första som händer vid ett framebyte
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];
            entity.Update(this, deltaTime);
            
        }

        for (int i = 0; i < entities.Count;)
        {
            Entity entity = entities[i];
            if (entity.Dead) entities.RemoveAt(i);
            else i++;
        }
        
    }

    public void RenderAll(RenderTarget target) //Renderar alla entities som är spawnade och som finns i listan, dvs allt som inte är "Dead"
    {
        for (int i = 0; i < entities.Count ; i++)
        {
            entities[i].Render(target);
        }
    }

    public void Load(string level) //Initierar bl.a "level0" så det finns en nivå att rendera när spelet startas.
    {
        nextScene = level;
    }

    public void Reload() // Kommer göra att om Hero åker utanför skärmen, kommer leveln spelas om.
    {
        nextScene = currentScene;
    }

    private void HandleSceneChange()
    {
        if (nextScene == null) return; //om nextScene inte har något värde kommer inget hända, och funktionen hoppas över.
        entities.Clear();
        Spawn(new Background());
       
        
        string file = $"assets/{nextScene}.txt";
        Console.WriteLine($"loading scene '{file}'");

        // TODO Load scene from file
        foreach (var line in File.ReadLines(file, Encoding.UTF8)) //Läser av alla rader i .txt filer som ges in.
        {
           
            if (line.Length != 0) //Hoppar över nya rader som inte har någon värdeindex (dvs blank rad)
            {
                string parsed = line.Trim();
                int commentAt = parsed.IndexOf('#'); //Om det finns # innan tecken, räknas det som 0, annars -1, som går vidare.
                if (commentAt >= 0) // känner av om det är en "kommentar" kännetecknat av # pga värdet som gavs innan. Trimmar allt efter detta till "icke-arraybart",
                {
                    parsed = parsed.Substring(0, commentAt);
                    parsed = parsed.Trim();
                }

                string[] words = parsed.Split(" "); // Splittar upp raden till lika många index i en array där mellanrum finns.
                if (words.Length >= 3) //Kollar om antalet Index i Arrayen är lika med elr högre än 3, så inte programmet krashar pga att det inte finns värden som går att tyda.
                {
                    string entityType = words[0];
                    float posX = float.Parse(words[1]);
                    float posY = float.Parse(words[2]);
                    
                    switch (entityType) // Läser av den splittade raden där den bestämmer typ av entity, och lägger en position för denna med hjälp av de nästkommande värderna.
                    {
                        case "w":
                            Spawn(new Platform
                            {
                                Position = new Vector2f(posX, posY)
                            });
                            break;
                        case "d":
                            Spawn(new Door()
                            {
                                Position = new Vector2f(posX, posY),
                                NextRoom = words[3]
                            });
                            break;
                        case "k":
                            Spawn(new Key()
                            {
                                Position = new Vector2f(posX, posY)
                            });
                            break;
                        case "h":
                            Spawn(new Hero()
                            {
                                Position = new Vector2f(posX, posY)
                            });
                            break;
                    }

                }
            }
            
        }
        currentScene = nextScene; //uppdaterar currentScene som samma värde som nextScene
        nextScene = null; //Uppdaterar nextScene som inget nytt värde (null)
    }

    public bool TryMove(Entity entity, Vector2f movement)
    {
        entity.Position += movement;
        bool collided = false;
        for (int i = 0; i < entities.Count; i++)
        {
         Entity other = entities[i];
         if (!other.Solid) continue;
         if (other == entity) continue;
         FloatRect boundsA = entity.Bounds;
         FloatRect boundsB = other.Bounds;
         if (Collision.RectangleRectangle(boundsA, boundsB, out Collision.Hit hit)) // Kontrollerar om Boundsen på spritsen överlappar/kolliderar. Uppdaterar position att inta gå längre.
         {
             entity.Position += hit.Normal * hit.Overlap;
             i = -1;
             collided = true;
         }
        }
        return collided;
    }
}