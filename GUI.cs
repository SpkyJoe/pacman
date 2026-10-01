using SFML.Graphics;
using SFML.System;
using System.IO;
using System.Text;
namespace Pacman;

public class GUI : Entity
{
    public GUI() : base("pacman")
    {
        maxHealth = 3;
    }
    private Text scoreText;
    private int maxHealth;
    private int currentHealth;
    private int currentScore;
   
    public override void Create(Scene scene)
    {
        scoreText = new Text();
        scoreText.Color = Color.Black;
        scoreText.Font = scene.Assets.LoadFont("pixel-font");
        scoreText.DisplayedString = "Score";
        currentHealth = maxHealth;
        base.Create(scene);
        sprite.TextureRect = new IntRect(72, 36, 18, 18);
        scene.LoseHealth += OnLoseHealth;
        scene.GainScore += OnGainScore;
    }

    private void OnLoseHealth(Scene scene, int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            CompareScore(currentScore);
            DontDestroyOnLoad = false;
            scene.loader.Reload();
            scene.LoseHealth -= OnLoseHealth;
            
        }
    }

    private void OnGainScore(Scene scene, int score)
    {
        currentScore += score;
        if (!scene.FindByType<Coin>(out _))
        {
            DontDestroyOnLoad = true;
            scene.loader.Reload();
        }
    }

    public override void Render(RenderTarget target)
    {
        sprite.Position = new Vector2f(36, 396);
        for (int i = 0; i < maxHealth; i++)
        {
            sprite.TextureRect = i < currentHealth
                ? new IntRect(72, 36, 18, 18)
                : new IntRect(72, 0, 18, 18);
            base.Render(target);
            sprite.Position += new Vector2f(18, 0);
        }
        scoreText.DisplayedString = $"Score: {currentScore}";
        scoreText.Position = new Vector2f(414 - scoreText.GetGlobalBounds().Width, 396);
        target.Draw(scoreText);
    }

    private void CompareScore(int score)
    {
        if (!File.Exists("HighScore.txt"))
        {
            File.WriteAllText("HighScore.txt", $"{score}");
        }
        else
        {
            string CurrentHigh = (File.ReadAllText("HighScore.txt"));
            int currentHighScore = int.Parse((CurrentHigh));
            if (currentHighScore < score)
            {
                File.WriteAllText("HighScore.txt", $"{score}");
            }
        }
    }
}