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
using Microsoft.Xna.Framework.Media;
using LVuniqiFinalProject.Components.MenuSceneComponents;
namespace LVuniqiFinalProject.Scenes
{
    /// <summary>
    /// The menuscene for the game, inheriting from the gamescene base class
    /// </summary>
    public class MenuScene : GameScene
    {
        //The menu component to display on the menu
        public MenuComponent Menu { get; set; }

        private SpriteBatch spriteBatch;
        public Song menuSong;
        private string[] menuItems =
        {
            "Play Game",
            "Help",
            "Credits",
            "Quit"
        };

        /// <summary>
        /// Creates a menu scene with the menu components
        /// </summary>
        /// <param name="game">The game the menu scene belongs to</param>
        /// <param name="spriteBatch">The spritebatch to draw it to</param>
        public MenuScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            SpriteFont regularFont = game.Content.Load<SpriteFont>("Fonts/RegularFont");
            Menu = new MenuComponent(game, spriteBatch, regularFont, menuItems);
            
            this.Components.Add(Menu);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }
    }
}
