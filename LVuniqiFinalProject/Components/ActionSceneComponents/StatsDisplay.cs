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

namespace LVuniqiFinalProject.Components.ActionSceneComponents
{
    /// <summary>
    /// This is used in the actionscene to draw the stats of the player the lives and scores
    /// </summary>
    class StatsDisplay : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont displayFont;
        private Vector2 scorePosition;
        private Vector2 livesPosition;

        public int score = 0;
        public int lives = 3;

        /// <summary>
        /// Creates a display to display the various states of the player
        /// </summary>
        /// <param name="game">The game this belongs to</param>
        /// <param name="spriteBatch">The spritebatch to draw it to</param>
        /// <param name="font">The spritefont for the text display</param>
        public StatsDisplay(Game game, SpriteBatch spriteBatch, SpriteFont font) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.displayFont = font;
            scorePosition = new Vector2(10, 25);
            livesPosition = new Vector2(Shared.stage.X - 150, 25);
        }

        /// <summary>
        /// Draws the scores and the lives left
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(displayFont, $"Score: {score}", scorePosition, Color.White);
            spriteBatch.DrawString(displayFont, $"Lives: {lives}", livesPosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
