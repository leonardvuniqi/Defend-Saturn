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
using LVuniqiFinalProject.Scenes;

namespace LVuniqiFinalProject.Components.ActionSceneComponents
{
    /// <summary>
    /// Create a medium enemy inherting from enemy class
    /// </summary>
    public class MediumEnemy : Enemy
    {
        private float shootingDelay = 2f;

        /// <summary>
        /// Instantiates a new medium enemy
        /// </summary>
        /// <param name="game">The game the enemy belongs to</param>
        /// <param name="spriteBatch">The spritebatch to draw it to</param>
        /// <param name="position">The position the medium enemy will be created at</param>
        public MediumEnemy(Game game, SpriteBatch spriteBatch, Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = game.Content.Load<Texture2D>("Images/MediumEnemyShip");
            this.position = position;
            this.speed = new Vector2(0, 3);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            if (isActive)
            {
                if (shootingDelay >= 2)
                {
                    EnemyBullet enemyBullet1 = new EnemyBullet(this.Game, spriteBatch,
                        new Vector2(position.X, position.Y + texture.Height - 5));
                    EnemyBullet enemyBullet2 = new EnemyBullet(this.Game, spriteBatch,
                        new Vector2(position.X + texture.Width - 29, position.Y + texture.Height));

                    //adding the bullets to the action scenes components list through services
                    var actionSceneComponents = this.Game.Services.GetService<ActionScene>().Components;
                    actionSceneComponents.Add(enemyBullet1);
                    actionSceneComponents.Add(enemyBullet2);

                    //adding the bullets to the list of bullets through services
                    var bullets = this.Game.Services.GetService<ActionScene>().bullets;
                    bullets.Add(enemyBullet1);
                    bullets.Add(enemyBullet2);

                    this.shootingSoundEffect.Play();

                    shootingDelay = 0;
                }

                shootingDelay += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            base.Update(gameTime);
        }
    }
}
