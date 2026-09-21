using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace platformer;

public class Entity
{
    private readonly string textureName;
    protected readonly Sprite sprite;
    public bool Dead;
    public Vector2f Position;
    public readonly FloatRect Bounds;

    protected Entity(string textureName)
    {
        Position = new Vector2f();
        Bounds = new FloatRect();
    }

    public void Create(Scene scene)
    {

    }

    public void Update(Scene scene, float Deltatime)
    {

    }
}