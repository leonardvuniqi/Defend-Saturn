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
    /// Enemy manager class manages the enemies on the screen
    /// </summary>
    public class EnemyManager : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;

        //List of enemies  
        public List<Enemy> enemies;

        private float timer;
        private Random ran = new Random();
        private Random ranPosition = new Random();
        public float enemyDelay = 1f;

        public EnemyManager(Game game, SpriteBatch spriteBatch, List<Enemy> enemiesList) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.enemies = enemiesList;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //Make the game gradually increase in difficulty the longer the player is alive by
            // making the spawn delay decrease every 30 seconds
            if (Math.Round((decimal)gameTime.TotalGameTime.TotalSeconds) % 30 == 0)
            {
                if(enemyDelay > 0.7f)
                {
                    enemyDelay -= 0.1f;
                }
            }

            //Creates a delay when spawning enemies according to enemyDelay = less delay is more spawns
            if (timer >= enemyDelay)
            {
                //limits it to a maximum of 20 enemies on the screen at once
                if (enemies.Count < 20)
                {
                    int ranNum = 0;
                    ranNum = ranPosition.Next(50, (int)Shared.stage.X - 50);
                    if (ran.Next(0, 2) == 1) // if the random number was 1 draw a small enemy
                    {
                        SmallEnemy smallEnemy = new SmallEnemy(this.Game, spriteBatch, new Vector2(ranNum, -50));
                        enemies.Add(smallEnemy);

                        //adding enemy to actionscene components through services
                        var actionSceneComponents = this.Game.Services.GetService<ActionScene>().Components;
                        actionSceneComponents.Add(smallEnemy);
                        timer = 0;
                    }
                    else // if it wasnt 1 than draw a medium enemy
                    {
                        MediumEnemy mediumEnemy = new MediumEnemy(this.Game, spriteBatch, new Vector2(ranNum, -50));
                        enemies.Add(mediumEnemy);

                        //adding enemy to actionscene components through services
                        var actionSceneComponents = this.Game.Services.GetService<ActionScene>().Components;
                        actionSceneComponents.Add(mediumEnemy);
                        timer = 0;
                    }
                }
            }

            //loops through each enemy checking if the enemy is off screen
            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                if (enemies[i].position.Y > Shared.stage.Y)
                {
                    //if the enemy is off screen than remove him from the actionscene components and from the list of enemies
                    var actionSceneComponents = this.Game.Services.GetService<ActionScene>().Components;
                    actionSceneComponents.Remove(enemy);
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            //update the timer
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime);
        }
    }
}
