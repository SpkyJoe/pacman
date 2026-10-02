using SFML.Graphics;
using SFML.System;
using System.IO;
using System.Text;
using SFML.Window;


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
    private Text highscoreText;
    private Text restartText;
   
    public override void Create(Scene scene)
    {
        scoreText = new Text();
        scoreText.Color = Color.Black;
        scoreText.Font = scene.Assets.LoadFont("pixel-font");
        currentHealth = maxHealth;
        base.Create(scene);
        sprite.TextureRect = new IntRect(72, 36, 18, 18);
        scene.LoseHealth += OnLoseHealth;
        scene.GainScore += OnGainScore;
        highscoreText = new Text();
        highscoreText.Color = Color.White;
        highscoreText.Font = scene.Assets.LoadFont("pixel-font");
        restartText = new Text();
        restartText.Color = Color.White;
        restartText.Font = scene.Assets.LoadFont("pixel-font");
    }

    private void OnLoseHealth(Scene scene, int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            scene.gameOverState = true;
            CompareScore(currentScore);
            DontDestroyOnLoad = false;
            scene.GameOver(scene);
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
        if (currentHealth <= 0)
        {
            
            highscoreText.DisplayedString = $"High Score: {File.ReadAllText("HighScore.txt")}";
            highscoreText.Position = new Vector2f(414 - highscoreText.GetGlobalBounds().Width, 396);
            target.Draw(highscoreText);
            restartText.DisplayedString = "Press [Spacebar]\n" +
                                          "to Restart";
            restartText.Position = new Vector2f(207 - scoreText.GetGlobalBounds().Width / 2, 225 - scoreText.GetGlobalBounds().Height / 2);
            
            target.Draw(restartText);
        }
        else
        {
            scoreText.DisplayedString = $"Score: {currentScore}";
            scoreText.Position = new Vector2f(414 - scoreText.GetGlobalBounds().Width, 396);
            target.Draw(scoreText);
        }
    }

    private int CompareScore(int score)
    {
        if (!File.Exists("HighScore.txt"))
        {
            File.WriteAllText("HighScore.txt", $"{score}");
            return score;
        }
        string CurrentHigh = (File.ReadAllText("HighScore.txt"));
        int currentHighScore = int.Parse((CurrentHigh));
        if (currentHighScore < score)
        {
            File.WriteAllText("HighScore.txt", $"{score}");
            return score;
        }
        return currentHighScore;
    }
}