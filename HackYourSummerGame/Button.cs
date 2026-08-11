using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackYourSummerGame
{
    /// <summary>
    /// Purpose: Notifies an object upon a button being clicked
    /// </summary>
    public delegate void OnButtonClick();

    /// <summary>
    /// Class representing a button
    /// </summary>
    internal class Button
    {
        // Fields
        Texture2D image;
        Rectangle position;
        Rectangle sourceRect;
        MouseState previousMState;

        // Events
        /// <summary>
        /// Notifies client when left button clicked
        /// </summary>
        public event OnButtonClick OnLeftButtonClick;

        /// <summary>
        /// Constructor for a button object
        /// </summary>
        /// <param name="image">Image of button</param>
        /// <param name="position">Rectangle of button's position</param>
        /// <param name="sourceRect">Source rectangle</param>
        public Button(Texture2D image, Rectangle position, Rectangle sourceRect)
        {
            this.image = image;
            this.position = position;
            this.sourceRect = sourceRect;
            previousMState = Mouse.GetState();
        }

        /// <summary>
        /// Check if button has been clicked for subscribers
        /// </summary>
        public void Update()
        {
            // Check current mouse state
            MouseState mState = Mouse.GetState();

            // Check if the button is pressed
            if (mState.LeftButton == ButtonState.Pressed &&
                previousMState.LeftButton == ButtonState.Released &&
                position.Contains(mState.Position))
            {
                // Check if event has subscribers
                if (OnLeftButtonClick != null)
                {
                    // Call all attached methods
                    OnLeftButtonClick();
                }
            }

            // Assign current mouse state as previous state;
            previousMState = mState;
        }

        /// <summary>
        /// Draws button to screen
        /// </summary>
        /// <param name="sb">SpriteBatch instance</param>
        public void Draw(SpriteBatch sb)
        {
            // Check current mouse state
            MouseState mState = Mouse.GetState();

            // Check if image is hovered
            if (position.Contains(mState.Position))
            {
                // Draw the hovered version of the button
                sb.Draw(
                    image,
                    position,
                    sourceRect,
                    Color.Gray);
            }
            else
            {
                // Draw the normal version of the button
                sb.Draw(
                    image,
                    position,
                    sourceRect,
                    Color.White);
            }
        }

        /// <summary>
        /// Check if button has been clicked for conditionals
        /// </summary>
        /// <returns>Bool of if button has been clicked</returns>
        public bool Clicked()
        {
            // Check current mouse state
            MouseState mState = Mouse.GetState();

            // Check if the button is clicked, returning bool of answer
            if (mState.LeftButton == ButtonState.Pressed &&
                 previousMState.LeftButton == ButtonState.Released &&
                 position.Contains(mState.Position))
            {
                // Assign current mouse state as previous state;
                previousMState = mState;
                return true;
            }
            else
            {
                // Assign current mouse state as previous state;
                previousMState = mState;
                return false;
            }
        }
    }
}
