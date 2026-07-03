using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HackYourSummerGame
{
    /// <summary>
    /// Class representing a character - ally or enemy
    /// </summary>
    internal class Character
    {
        // Stats
        protected int health;
        protected int strength;

        // screen
        protected Vector2 location;
        protected Texture2D sprite;

        // health property r/w
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        // Constructor
        public Character(int health, int strength, Vector2 location, Texture2D sprite)
        {
            this.health = health;
            this.strength = strength;
            this.location = location ;
            this.sprite = sprite;
        }

        //
        public virtual void GenericAttack(Character target)
        {
            target.health -= strength;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(sprite, location, Color.White);
        }
    }
}                                                           
