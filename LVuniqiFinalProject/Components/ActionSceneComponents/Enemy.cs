/*
 * Leonard Vuniqi
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LVuniqiFinalProject.Components.ActionSceneComponents
{
    /// <summary>
    /// The enemy class, each enemy will inherit from this class
    /// </summary>
    public abstract class Enemy : DrawableGameComponent
    {
        public SpriteBatch spriteBatch;
        public Texture2D texture;
        public Vector2 position;
        public Vector2 speed;
        public Rectangle boundingBox;

        public bool isActive { get; set; } = false;

        protected SoundEffect shootingSoundEffect;

        public Enemy(Game game) : base(game)
        {
            this.shootingSoundEffect = game.Content.Load<SoundEffect>("SoundEffects/EnemyLaser");
        }

        /// <summary>
        /// Draws the enemy
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
        /// Updates the enemies bounding box and if they are on the screen than make them active
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            position += speed;

            if(position.Y >= 0 && position.Y <= Shared.stage.Y)
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            base.Update(gameTime);
        }
    }
}
