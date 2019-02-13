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
using Microsoft.Xna.Framework.Audio;
using LVuniqiFinalProject.Scenes;
using LVuniqiFinalProject.Components.ActionSceneComponents;
namespace LVuniqiFinalProject.Components.ActionSceneComponents
{
    /// <summary>
    /// The class for the small enemy which inherits from the enemy class
    /// </summary>
    public class SmallEnemy : Enemy
    {
        private float shootingDelay = 2f;

        //This offset is to position the bullet in the correct spot when the enemy shoots
        private int bulletXOffset = 8;

        /// <summary>
        /// Instantiates a new small enemy 
        /// </summary>
        /// <param name="game">The game the enemy belongs to</param>
        /// <param name="spriteBatch">The spritebatch to draw it to</param>
        /// <param name="position">The position the small enemy will be created at</param>
        public SmallEnemy(Game game, SpriteBatch spriteBatch, Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = game.Content.Load<Texture2D>("Images/SmallEnemyShip");
            this.position = position;
            this.speed = new Vector2(0, 5);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //if the enemy is active meaning if they are fully on the screen
            if (isActive)
            {
                //Make the enemy shoot on a delay
                if(shootingDelay >= 2)
                {
                    EnemyBullet enemyBullet = new EnemyBullet(this.Game, spriteBatch,
                        new Vector2(position.X + bulletXOffset, position.Y + texture.Height));

                    //Add the bullet to the list of componnets in the action scene
                    var sceneComponent = this.Game.Services.GetService<ActionScene>().Components;
                    sceneComponent.Add(enemyBullet);

                    //Add the bullet to the list of bullets in the action scene
                    var bullets = this.Game.Services.GetService<ActionScene>().bullets;
                    bullets.Add(enemyBullet);

                    //Play the laser sound
                    this.shootingSoundEffect.Play();

                    //reset the shooting delay after they shoot
                    shootingDelay = 0;
                }
                //increment the shooting delay
                shootingDelay += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            base.Update(gameTime);
        }
    }
}
