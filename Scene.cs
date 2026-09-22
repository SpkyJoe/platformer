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

    private void HandleSceneChange()
    {
        if (nextScene == null) return;
        entities.Clear();
        Spawn(new Background());

        string file = $"assets/{nextScene}.txt";
        Console.WriteLine($"loading scene '{file}'");
        
        // TODO Load scene from file
        foreach (var line in File.ReadLines(file, Encoding.UTF8))
        {
            string parsed = line.Trim();
            int commentAt = parsed.IndexOf('#');
            if (commentAt >= 0)
            {
                parsed = parsed.Substring(0, commentAt);
                parsed = parsed.Trim();

                string[] words = parsed.Split(" ");
            }
        }
        
        currentScene = nextScene;
        nextScene = null;
        
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