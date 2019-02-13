/*
 * Leonard Vuniqi
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using LVuniqiFinalProject.Components.ActionSceneComponents;

namespace LVuniqiFinalProject.Scenes
{
    /// <summary>
    /// Initializes a new actionscene with all of the required components to make the game playable
    /// </summary>
    public class ActionScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private ScrollingItem scrollingBackground;
        private Player playerSpaceShip;
        private EnemyManager enemyManager;
        private PlayerBullet playerBullet;
        private StatsDisplay statsDisplay;

        private Texture2D playerTex;
        private SpriteFont displayFont;
        private KeyboardState keyState;
        private KeyboardState oldKeyState;

        //Score
        private Vector2 scorePosition;
        public int Score { get; set; } = 0;

        //Sound Effects
        public Dictionary<String, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();

        //Lists
        private List<Enemy> enemies = new List<Enemy>();
        public List<Bullet> bullets = new List<Bullet>();

        public bool isGamePaused = false;

        //Offset vars to get sprites positioned properly
        private int smokeAnimXOffset = 25;
        private int playerBulletXOffset = 18;

        /// <summary>
        /// Will create all of the required components to make the game playable
        /// </summary>
        /// <param name="game">The game instance</param>
        /// <param name="spriteBatch">The spritebatch to draw it to</param>
        public ActionScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;

            //Get our textures for our objects
            Texture2D sbTex = game.Content.Load<Texture2D>("Images/spacebk2");
            playerTex = game.Content.Load<Texture2D>("Images/player");
            displayFont = game.Content.Load<SpriteFont>("Fonts/RegularFont");

            //Get our sound effects and add them to the soundeffects dictionary
            SoundEffect playerBulletSoundEffect = game.Content.Load<SoundEffect>("SoundEffects/PlayerLaser");
            SoundEffect explosionSoundEffect = game.Content.Load<SoundEffect>("SoundEffects/Explosion");
            SoundEffect playerHitSoundEffect = game.Content.Load<SoundEffect>("SoundEffects/PlayerHit");
            soundEffects.Add("PlayerBullet", playerBulletSoundEffect);
            soundEffects.Add("Explosion", explosionSoundEffect);
            soundEffects.Add("PlayerHit", playerHitSoundEffect);

            //Create the positions for our objects
            Vector2 backgroundPos1 = new Vector2(0, 0);
            Vector2 backgroundPos2 = new Vector2(0, -Shared.stage.Y);
            Vector2 backgroundSpeed = new Vector2(0, 4);
            Vector2 playerPos = new Vector2(Shared.stage.X / 2 - playerTex.Width, Shared.stage.Y / 2 - playerTex.Height);
            Vector2 playerSpeed = new Vector2(10, 10);

            //Create the game objects
            scrollingBackground = new ScrollingItem(game, spriteBatch, sbTex, backgroundPos1, backgroundPos2, backgroundSpeed);
            playerSpaceShip = new Player(game, spriteBatch, playerTex, playerPos, playerSpeed);
            enemyManager = new EnemyManager(game, spriteBatch, enemies);
            scorePosition = new Vector2(Shared.stage.X - 150, 0);
            statsDisplay = new StatsDisplay(game, spriteBatch, displayFont);

            //Add them to list of components
            this.Components.Add(scrollingBackground);
            this.Components.Add(playerSpaceShip);
            this.Components.Add(enemyManager);
            this.Components.Add(statsDisplay);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        /// <summary>
        /// Performs the updating for the components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();

            //If the user presses P pause the game, if they press it again unpause
            if (keyState.IsKeyDown(Keys.P) && oldKeyState.IsKeyUp(Keys.P))
            {
                if (isGamePaused == false)
                {
                    PauseGame(true);
                }
                else
                {
                    PauseGame(false);
                }
            }

            //If the player presses space, shoot a bullet add it to the components and play a sound
            if (keyState.IsKeyDown(Keys.Space) && oldKeyState.IsKeyUp(Keys.Space))
            {
                playerBullet = new PlayerBullet(this.Game, spriteBatch, new Vector2(playerSpaceShip.position.X + (playerTex.Width / 2 - playerBulletXOffset), playerSpaceShip.position.Y));
                bullets.Add(playerBullet);
                soundEffects["PlayerBullet"].Play();
                this.Components.Add(playerBullet);
            }

            oldKeyState = keyState;

            //Loop to check if bullets are beyond stage bounds if so remove them
            for (int i = 0; i < bullets.Count; i++)
            {
                Bullet bullet = bullets[i];
                if (bullet is PlayerBullet)
                {
                    if (bullet.position.Y < 0)
                    {
                        this.Components.Remove(bullet);
                        bullets.RemoveAt(i);
                        i--;
                    }
                }
                else if (bullet is EnemyBullet)
                {
                    if (bullet.position.Y > Shared.stage.Y)
                    {
                        this.Components.Remove(bullet);
                        bullets.RemoveAt(i);
                        i--;
                    }
                }
            }

            //Collision Detection
            for (int i = 0; i < bullets.Count; i++)
            {
                Bullet bullet = bullets[i];
                if (bullet is PlayerBullet)
                {
                    for (int j = 0; j < enemies.Count; j++)
                    {
                        Enemy enemy = enemies[j];
                        if (enemy.isActive && bullet.boundingBox.Intersects(enemy.boundingBox))
                        {
                            //Creating an explosion animation at the enemies location
                            Explosion explosion = new Explosion(this.Game, spriteBatch,
                                new Vector2(enemy.position.X - enemy.texture.Width / 2, enemy.position.Y - enemy.texture.Height));
                            explosion.Enabled = true;
                            explosion.Visible = true;
                            soundEffects["Explosion"].Play();
                            this.Components.Add(explosion);

                            //remove the bullet and enemy
                            this.Components.Remove(enemy);
                            this.Components.Remove(bullet);
                            enemies.RemoveAt(j);
                            bullets.RemoveAt(i);

                            //update our score 
                            if (enemy is SmallEnemy)
                            {
                                Score += 10;
                                statsDisplay.score += 10;
                            }
                            else
                            {
                                Score += 20;
                                statsDisplay.score += 20;
                            }
                            i--;
                            break;
                        }
                    }
                }
                else
                {   //hit detection for the player
                    if (bullet.boundingBox.Intersects(playerSpaceShip.boundingBox) && playerSpaceShip.isAlive)
                    {
                        //remove the bullet from the components, the list, and minus the players health and update the statsdisplay to the players health
                        this.Components.Remove(bullet);
                        bullets.RemoveAt(i);
                        playerSpaceShip.Lives -= 1;
                        statsDisplay.lives -= 1;

                        //Creates a smoke animation at the players location and play a sound effect
                        SmokeAnimation smokeAnimation = new SmokeAnimation(this.Game, spriteBatch,
                            new Vector2((playerSpaceShip.position.X - (playerSpaceShip.texture.Width/2)) - smokeAnimXOffset, playerSpaceShip.position.Y - (playerSpaceShip.texture.Height/2)));
                        smokeAnimation.Enabled = true;
                        smokeAnimation.Visible = true;
                        this.Components.Add(smokeAnimation);
                        soundEffects["PlayerHit"].Play();
                        //if the players health reaches 0 after a hit detection, hide the actionscene and show the gameover scene
                        if (playerSpaceShip.Lives == 0)
                        {
                            playerSpaceShip.isAlive = false;
                            GameOverScene.finalScore = Score;
                            MediaPlayer.Stop();
                            this.Hide();
                            this.Game.Services.GetService<GameOverScene>().Show();
                        }
                        i--;
                        break;
                    }
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Controls the Pausing and unpausing of the game
        /// </summary>
        /// <param name="pauseGame">True = pause, false = unpause</param>
        public void PauseGame(bool pauseGame)
        {
            DrawableGameComponent component = null;
            if (pauseGame)
            {
                foreach (GameComponent gameComponent in this.Components)
                {
                    if (gameComponent is DrawableGameComponent)
                    {
                        component = (DrawableGameComponent)gameComponent;
                        if (component.Enabled)
                        {
                            component.Enabled = false;
                        }
                    }
                }
                isGamePaused = true;
            }
            else
            {
                foreach (GameComponent gameComponent in this.Components)
                {
                    if (gameComponent is DrawableGameComponent)
                    {
                        component = (DrawableGameComponent)gameComponent;
                        if (component.Enabled == false)
                            component.Enabled = true;
                    }
                }
                isGamePaused = false;
            }
        }

        /// <summary>
        /// Resets the game to be able to be played again
        /// </summary>
        public void ResetGame()
        {
            playerSpaceShip.position = new Vector2(Shared.stage.X / 2 - playerTex.Width, Shared.stage.Y / 2 - playerTex.Height);
            playerSpaceShip.Lives = 3;
            playerSpaceShip.isAlive = true;
            Score = 0;
            statsDisplay.lives = 3;
            statsDisplay.score = 0;
            playerSpaceShip.Enabled = true;
            playerSpaceShip.Visible = true;
            enemyManager.enemyDelay = 1f;

            for (int i = 0; i < this.Components.Count; i++)
            {
                if(this.Components[i] is Bullet || this.Components[i] is Enemy ||
                    this.Components[i] is Explosion || this.Components[i] is SmokeAnimation)
                {
                    this.Components.RemoveAt(i);
                    i--;
                }
            }
            
            enemies.Clear();
            bullets.Clear();
        }
    }
}
