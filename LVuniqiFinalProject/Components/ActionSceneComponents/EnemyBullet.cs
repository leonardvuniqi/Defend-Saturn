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
using LVuniqiFinalProject.Components.ActionSceneComponents;

namespace LVuniqiFinalProject.Components.ActionSceneComponents
{
    /// <summary>
    /// An enemy bullet inherting from enemy class
    /// </summary>
    public class EnemyBullet : Bullet
    {
        /// <summary>
        /// Instantiates a new enemy bullet
        /// </summary>
        /// <param name="game">The game the enemy bullet belongs to</param>
        /// <param name="spriteBatch">The spritebatch to draw the bullet to</param>
        /// <param name="position">The initial position of the bullet</param>
        public EnemyBullet(Game game, SpriteBatch spriteBatch, Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = game.Content.Load<Texture2D>("Images/EnemyBullet");
            this.position = position;
            this.speed = new Vector2(0, 6);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        /// <summary>
        /// Updates the bullets position according to the speed
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            position += speed;
            base.Update(gameTime);
        }
    }
}
