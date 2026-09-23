using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;
using System.Text;
using Platformer;
using System.IO;

namespace platformer;

public class Entity
{
    private readonly string textureName;
    protected readonly Sprite sprite;
    public bool Dead;
    public virtual bool Solid => false;

    public virtual FloatRect Bounds => sprite.GetGlobalBounds(); 
    
    protected Entity(string textureName)
    {
        this.textureName = textureName;
        sprite = new Sprite();
    }

    public Vector2f Position
    {
        get => sprite.Position;
        set => sprite.Position = value;
    }
    
    public virtual void Create(Scene scene)
    {
        sprite.Texture = scene.LoadTexture(textureName);
    }

    public virtual void Update(Scene scene, float Deltatime)
    {

    }

    public virtual void Render(RenderTarget target)
    {
        target.Draw(sprite);
    }
    
}