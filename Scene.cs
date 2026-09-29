using SFML.Graphics;

namespace Pacman;

public class Scene
{
    private List<Entity> entities;
    public readonly SceneLoader loader;
    public readonly AssetManager Assets;

    public Scene()
    {
        entities = new List<Entity>();
        loader = new SceneLoader();
        Assets = new AssetManager();
    }
    
    public void Spawn(Entity entity)
    {
        entities.Add(entity);
        entity.Create(this);
    }

    public void UpdateAll(float deltaTime)
    {
        loader.HandleSceneLoad(this);
        
        for (int i = entities.Count -1; i >= 0; i--)
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
            entities.RemoveAt(i);
            entity.Destroy(this);
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