using SFML.Graphics;
using System.IO;
using SFML.Window;

namespace Pacman;
public delegate void ValueChangedEvent(Scene scene, int value);
public sealed class Scene
{ 
    
    private List<Entity> entities;
    public readonly SceneLoader loader;
    public readonly AssetManager Assets;
    public event ValueChangedEvent GainScore;
    public event ValueChangedEvent LoseHealth;
    public event ValueChangedEvent CandyEaten;
    private int scoreGained;
    private int lostHealth;
    private int candyEaten;
    public bool gameOverState;
   

    
    public void PublishGainedScore(int amount) => scoreGained += amount;
    public void PublishLostHealth(int amount) => lostHealth += amount;
    public void PublishCandyEaten(int amount) => candyEaten += amount;
    
    public Scene()
    {
        
        entities = new List<Entity>();
        loader = new SceneLoader();
        Assets = new AssetManager();
        gameOverState = true;

    }
    
    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }

    public void UpdateAll(float deltaTime)
    {
        
        
        if (gameOverState && Keyboard.IsKeyPressed(Keyboard.Key.Space))
        {
            gameOverState = false;
            loader.HandleSceneLoad(this);
        }
        else if (gameOverState) return;
        
        for (int i = entities.Count -1; i >= 0; i--)
        {
            Entity entity = entities[i];
            entity.Update(this, deltaTime);
        }

        if (scoreGained != 0)
        {
            GainScore?.Invoke(this, scoreGained);
            scoreGained = 0;
        }
        if (lostHealth != 0)
        {
            LoseHealth?.Invoke(this, lostHealth);
            lostHealth = 0;
        }

        if (candyEaten != 0)
        {
            CandyEaten?.Invoke(this, candyEaten);
            candyEaten = 0;
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
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Render(target);
        }
    }

    public void Clear()
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            Entity entity = entities[i];
            if (!entity.DontDestroyOnLoad)
            {
                entities.RemoveAt(i);
                entity.Destroy(this);
            }
            
        }
    }

    public IEnumerable<Entity> FindIntersects(FloatRect bounds)
    {
        int lastEntity = entities.Count - 1;
        for (int i = lastEntity; i >= 0; i--)
        {
            Entity entity = entities[i];
            if (entity.Dead) continue;
            if (entity.Bounds.Intersects(bounds))
            {
                yield return entity;
            }
        }
    }

    public void GameOver(Scene scene)
    {
        foreach (var entity in entities)
        {
            if (entity is Actor)
            {
            }
        }
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

}