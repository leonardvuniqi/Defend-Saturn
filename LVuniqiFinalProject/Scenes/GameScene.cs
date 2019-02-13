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
    /// The main gamescene class, every game scene will inherit from this
    /// </summary>
    public abstract class GameScene : DrawableGameComponent
    {
        private List<GameComponent> components;
        public List<GameComponent> Components { get => components; set => components = value; }

        /// <summary>
        /// Hides the scene
        /// </summary>
        public void Hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        /// <summary>
        /// Shows the scene
        /// </summary>
        public void Show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        /// <summary>
        /// When a new scene is instantiaited create a new list of components and hide the scene
        /// </summary>
        /// <param name="game"></param>
        public GameScene(Game game) : base(game)
        {
            components = new List<GameComponent>();
            Hide();
        }

        /// <summary>
        /// If the component is visibile draw it
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent comp = null;
            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// If the component is enabled, update the component
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
            {
                if(components[i] is GameComponent)
                {
                    if (components[i].Enabled)
                    {
                        components[i].Update(gameTime);
                    }
                }
            }

            base.Update(gameTime);
        }
    }

}
