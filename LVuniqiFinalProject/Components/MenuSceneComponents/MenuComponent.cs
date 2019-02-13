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

namespace LVuniqiFinalProject.Components.MenuSceneComponents
{
    /// <summary>
    /// Menucomponent class controls all of the components on the menu ie. the text, images etc..
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private SpriteFont regularFont;
        private List<string> menuItems;

        public int SelectedIndex { get; set; } = 0;
        private Vector2 position;

        private int titleXOffset = 100;
        private int titleYOffset = 10;
        private int menuXOffset = 60;
        private int menuYOffset = 60;
        private Vector2 titlePosition;

        private Color regularColor = new Color(255, 168, 28);
        private Color highlightColor = new Color(255, 251, 211);
        private Color titleColor = new Color(171, 28, 255);

        private KeyboardState oldState;

        /// <summary>
        /// Creates a menu component
        /// </summary>
        /// <param name="game">The game the menu component belongs to</param>
        /// <param name="spriteBatch">The spritebatch to draw it to</param>
        /// <param name="regularFont">The font used for the text displays</param>
        /// <param name="menu">A list of menu items to display</param>
        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont regularFont, string[] menu) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.menuItems = menu.ToList<string>();
            this.texture = game.Content.Load<Texture2D>("Images/MenuBackground");
            this.position = new Vector2(Shared.stage.X / 2 - menuXOffset, Shared.stage.Y / 2 + menuYOffset);
            this.titlePosition = new Vector2(Shared.stage.X / 2 - titleXOffset, Shared.stage.Y / 2 + titleYOffset);
        }

        /// <summary>
        /// Draws the menu components onto the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            //stores a temp position to adjust spacing for each line
            Vector2 tempPos = position;
            spriteBatch.Begin();
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.DrawString(regularFont, "DEFEND SATURN", titlePosition, titleColor);
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (SelectedIndex == i) //if an item is selected change the color of the font
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPos, highlightColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
                else //if its not selected than display the menu item with the regular font color
                {
                    spriteBatch.DrawString(regularFont, menuItems[i], tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Updates the menu components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            //If the enemy presses the down key move the selected item down,
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                SelectedIndex++;
                if (SelectedIndex >= menuItems.Count)
                {
                    SelectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up)) //if they select up move the selected item up
            {
                SelectedIndex--;
                if (SelectedIndex < 0)
                {
                    SelectedIndex = menuItems.Count - 1;
                }
            }

            oldState = ks;

            base.Update(gameTime);
        }
    }
}
