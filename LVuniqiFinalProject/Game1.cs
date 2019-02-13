/*
 * Leonard Vuniqi
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using LVuniqiFinalProject.Scenes;

namespace LVuniqiFinalProject
{
    /// <summary>
    /// This is the main game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private MenuScene menuScene;
        private ActionScene actionScene;
        private HelpScene helpScene;
        private CreditScene creditScene;
        private GameOverScene gameOverScene;

        private Song menuSong;
        private Song actionSong;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 650;
        }

        /// <summary>
        /// Runs any initialization logic before the game loop
        /// </summary>
        protected override void Initialize()
        {
            // Sets the shared stage height and width to be used when needed in calculations
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            
            base.Initialize();
        }

        /// <summary>
        /// Loads all of the content to play the game, loads all the scenes and the music for the scenes
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Adding our scenes to our components aswell as to the services
            // to be able to directly access the components list from other classes

            menuScene = new MenuScene(this, spriteBatch);
            this.Components.Add(menuScene);
            this.Services.AddService<MenuScene>(menuScene);
            menuScene.Show();

            actionScene = new ActionScene(this, spriteBatch);
            this.Components.Add(actionScene);
            this.Services.AddService<ActionScene>(actionScene);
            actionScene.Hide();

            helpScene = new HelpScene(this, spriteBatch);
            this.Components.Add(helpScene);
            this.Services.AddService<HelpScene>(helpScene);
            helpScene.Hide();

            creditScene = new CreditScene(this, spriteBatch);
            this.Components.Add(creditScene);
            this.Services.AddService<CreditScene>(creditScene);
            creditScene.Hide();

            gameOverScene = new GameOverScene(this, spriteBatch);
            this.Components.Add(gameOverScene);
            this.Services.AddService<GameOverScene>(gameOverScene);
            gameOverScene.Hide();

            // Getting the music for the scenes
            menuSong = this.Content.Load<Song>("Music/Menu");
            actionSong = this.Content.Load<Song>("Music/Action");
            MediaPlayer.Play(menuSong);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent(){ }

        /// <summary>
        /// Controls the various scenes for the game, waiting for user input and responding with the correct follow up action
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            //the index of which menu item is currently hovered/selected
            int selectedIndex = 0;

            //check to see if were on menu screen
            if (menuScene.Enabled)
            {
                selectedIndex = menuScene.Menu.SelectedIndex; //sets the selected index
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter)) //if Play game is selected, hide the menu and menu music and play action scene with music
                {
                    menuScene.Hide();
                    MediaPlayer.Stop();
                    actionScene.ResetGame();
                    actionScene.Show();
                    MediaPlayer.Play(actionSong);
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = 0.1f;
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter)) //if Help scene is selected, hide menu and show help scene
                {
                    menuScene.Hide();
                    helpScene.Show();
                }
                else if(selectedIndex == 2 && ks.IsKeyDown(Keys.Enter)) //if Credit scene is selected, hide menu and show credit scene
                {
                    menuScene.Hide();
                    creditScene.Show();
                }
                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter)) //if Quit is selected, close the application
                {
                    Exit();
                }
            }
            else if (actionScene.Enabled) //check to see if the user is on the action scene
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    MediaPlayer.Stop();
                    actionScene.Hide();
                    menuScene.Show();
                    MediaPlayer.Play(menuSong);
                    MediaPlayer.IsRepeating = true;
                }
            }
            else if (helpScene.Enabled) //check to see if user is on the help scene
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    helpScene.Hide();
                    menuScene.Show();
                }
            }
            else if (creditScene.Enabled) //check to see if the user is on the credit scene
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    creditScene.Hide();
                    menuScene.Show();
                }
            }
            else if (gameOverScene.Enabled) //check to see if the user is on the game over scene
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    gameOverScene.Hide();
                    menuScene.Show();
                    MediaPlayer.Play(menuSong);
                    MediaPlayer.IsRepeating = true;
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
