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
        protected int speed;
        protected double turnmeter;

        // screen
        protected Vector2 location;
        protected Texture2D sprite;
        protected Vector2 attackOffset;
        protected Texture2D healthBar;
        protected Texture2D healthContainer;
        protected Vector2 healthPos;

        // health property r/w
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        // speed get property
        public int Speed
        {
            get { return speed; }
        }

        // turnmeter property r/w
        public double Turnmeter
        {
            get { return turnmeter; }
            set { turnmeter = value; }
        }

        // Constructor
        public Character(int health, int strength, int speed, Vector2 location, Texture2D sprite, Texture2D healthBar, Texture2D healthContainer)
        {
            this.health = health;
            this.strength = strength;
            this.speed = speed;
            turnmeter = 0;
            this.location = location ;
            this.sprite = sprite;
            this.healthBar = healthBar;
            this.healthContainer = healthContainer;
            healthPos = location - new Vector2(45, healthContainer.Height);
        }

        //
        public virtual void GenericAttack(Character target)
        {
            target.health -= strength;
        }

        //
        public virtual void Update()
        {
            
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(sprite, location + attackOffset, Color.White);
            sb.Draw(healthBar, new Rectangle((int)healthPos.X, (int)healthPos.Y, healthBar.Width * health / 100, healthBar.Height), Color.White);
            sb.Draw(healthContainer, healthPos, Color.White);
        }
    }
}                                                           
