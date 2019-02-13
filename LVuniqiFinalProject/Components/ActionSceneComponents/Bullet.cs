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
    /// Bullet class, every bullet will inherit from this class
    /// </summary>
    public abstract class Bullet : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        public Texture2D texture;
        public Vector2 position;
        public Vector2 speed;
        public Rectangle boundingBox;
        
        public Bullet(Game game) : base(game){}

        /// <summary>
        /// Draws the bullet
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Updates the bullets boundingbox
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //Update the bullets boundingbox for collision detection
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            base.Update(gameTime);
        }
    }
}
