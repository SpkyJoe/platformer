using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Text;
using Platformer;
using System.IO;
using System.Net.Security;

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

    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
        
    }
    
    public Texture LoadTexture(string name)
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
<<<<<<< Updated upstream
        
        HandleSceneChange();
=======
        HandleSceneChange(nextScene);
>>>>>>> Stashed changes
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

    public void RenderAll(RenderTarget target)
    {
        for (int i = 0; i < entities.Count ; i++)
        {
            entities[i].Render(target);
        }
    }

    public void Load(string level)
    {
        nextScene = level;
<<<<<<< Updated upstream

    }

    public void Reload()
=======
    }

    public void Reload(string level)
>>>>>>> Stashed changes
    {
        nextScene = currentScene;
    }

<<<<<<< Updated upstream
    private void HandleSceneChange()
=======
    private void HandleSceneChange(string level)
>>>>>>> Stashed changes
    {
        if (nextScene == null) return;
        entities.Clear();
        Spawn(new Background());
        string file = $"assets/{nextScene}.txt";
        Console.WriteLine($"loading scene '{file}'");

        // TODO Load scene from file
        foreach (var line in File.ReadLines(file, Encoding.UTF8))
        {
            if (line.Length != 0)
            {
<<<<<<< Updated upstream
                string parsed = line.Trim();
                int commentAt = parsed.IndexOf('#');
                Console.WriteLine(commentAt);
                if (commentAt >= 0)
                {
                    parsed = parsed.Substring(0, commentAt);
                    parsed = parsed.Trim();
                }

                string[] words = parsed.Split(" ");
                if (words.Length >= 3)
                {
                    string entityType = words[0];
                    float posX = float.Parse(words[1]);
                    float posY = float.Parse(words[2]);
                    switch (entityType)
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
                                Position = new Vector2f(posX, posY)
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

=======
                parsed = parsed.Substring(0, commentAt);
                parsed = parsed.Trim();
                switch (parsed.Length == 0)
                {
                    case true:
                        continue;
                }
                string[] words = parsed.Split(" ");
                string name = words[0];
                int posx = int.Parse(words[1]);
                int posy = int.Parse(words[2]);
                switch (name == "d")
                {
                    case true:
                        string dname = words[3];
                        break;
                    case false:
                        nextScene = words[3];
                        break;
                }
                
                switch (name)
                {
                    case "w" : 
                        Spawn(new Platform()
                        {
                            Position = new Vector2f(posx, posy)
                        });
                        continue;
                    case "d":
                        Spawn(new Door()
                        {
                            Position = new Vector2f(posx, posy)
                        });
                        continue;
                    case "k":
                        Spawn(new Key()
                        {
                            Position = new Vector2f(posx, posy)
                        });
                        continue;
                    case "h":
                        Spawn(new Hero()
                        {
                            Position = new Vector2f(posx, posy)
                        });
                        continue;
>>>>>>> Stashed changes
                }
            }

            currentScene = nextScene;
            nextScene = null;
        }
<<<<<<< Updated upstream
=======
        currentScene = nextScene;
        nextScene = null;
>>>>>>> Stashed changes
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
         if (Collision.RectangleRectangle(boundsA, boundsB, out Collision.Hit hit))
         {
             entity.Position += hit.Normal * hit.Overlap;
             i = -1;
             collided = true;
         }
        }
        return collided;
    }

}