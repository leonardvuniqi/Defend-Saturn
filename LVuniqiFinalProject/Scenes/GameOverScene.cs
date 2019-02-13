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

namespace LVuniqiFinalProject.Scenes
{
    /// <summary>
    /// The gameover scene for the game, inheriting from gamescene base class
    /// </summary>
    public class GameOverScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private SpriteFont scoreFont;
        private Vector2 scorePosition;
        public static int finalScore = 0;

        private int displayXOffset = 180;

        /// <summary>
        /// Creates the gameover scene with the required components
        /// </summary>
        /// <param name="game">The game the scene belongs to</param>
        /// <param name="spriteBatch">The spritebatch to draw the scene to</param>
        public GameOverScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = game.Content.Load<Texture2D>("Images/GameOverBackground");
            scoreFont = game.Content.Load<SpriteFont>("Fonts/RegularFont");
            scorePosition = new Vector2(Shared.stage.X / 2 - displayXOffset, Shared.stage.Y / 2);
        }

        /// <summary>
        /// Draws the gameover scene components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.DrawString(scoreFont, $"Your final score: {finalScore.ToString()}", scorePosition, Color.Orange);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
