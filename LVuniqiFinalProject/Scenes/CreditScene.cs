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

namespace LVuniqiFinalProject.Scenes
{
    /// <summary>
    /// The credit scene for the game, inheriting from gamescene base class
    /// </summary>
    public class CreditScene : GameScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;

        /// <summary>
        /// Creates a credit scene with the credit scene background
        /// </summary>
        /// <param name="game">Game it belongs to</param>
        /// <param name="spriteBatch">The spritebatch to draw it to</param>
        public CreditScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = game.Content.Load<Texture2D>("Images/CreditBackground");
        }

        /// <summary>
        /// Draws the credit scene texture
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
