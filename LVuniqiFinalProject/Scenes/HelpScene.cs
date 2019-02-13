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
    /// The help scene for the game, inheriting from the game scene class
    /// </summary>
    public class HelpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        public Texture2D texture;

        /// <summary>
        /// Creates a help scene with the help scenes texture
        /// </summary>
        /// <param name="game">Game the scene belongs to</param>
        /// <param name="spriteBatch">The spritebatch to draw it to</param>
        public HelpScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = game.Content.Load<Texture2D>("Images/HelpBackground");
        }

        /// <summary>
        /// Draws the help scene texture
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
