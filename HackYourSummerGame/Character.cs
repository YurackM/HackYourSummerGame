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
        protected int maxHealth;
        protected int strength;
        protected int speed;
        protected double turnmeter;
        protected bool attacking;
        protected bool ongoingAnimation;
        protected int tempSpeed;
        protected int dot;

        // screen
        protected Vector2 position;
        protected Texture2D sprite;
        protected Vector2 attackOffset;
        protected Texture2D healthBar;
        protected Texture2D healthContainer;
        protected Vector2 healthPos;
        protected int debuffTint;
        protected int buffTint;

        // health property r/w
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        // max health property r/w
        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        // speed get property
        public int Speed
        {
            get { return speed; }
        }

        // temp speed change get property
        public int TempSpeed
        {
            get { return tempSpeed; }
            set 
            { 
                if(value < 0)
                {
                    debuffTint = 255;
                }
                tempSpeed = value; 
            }
        }

        // r/w Damage over time
        public int DOT
        {
            get;
            set;
        }

        // turnmeter property r/w
        public double Turnmeter
        {
            get { return turnmeter; }
            set { turnmeter = value; }
        }

        // check if character is attacking still
        public bool Attacking
        {
            get { return attacking; }
            set { attacking = value; }
        }

        // check if character is in an animation
        public bool OngoingAnimation
        {
            get { return ongoingAnimation; }
            set { ongoingAnimation = value; }
        }

        // Return hitbox of character
        public Rectangle Hitbox
        {
            get { return new Rectangle((int)(position.X + sprite.Width / 4),
                (int)(position.Y + sprite.Height / 4), sprite.Width / 2, sprite.Height / 2); }
        }

        // Constructor
        public Character(int health, int strength, int speed, Vector2 position, Texture2D sprite, Texture2D healthBar, Texture2D healthContainer)
        {
            this.health = health;
            maxHealth = health;
            this.strength = strength;
            this.speed = speed;
            turnmeter = 0;
            this.position = position ;
            this.sprite = sprite;
            this.healthBar = healthBar;
            this.healthContainer = healthContainer;
            healthPos = position - new Vector2(45, healthContainer.Height);
            debuffTint = 0;
            buffTint = 0;
            dot = 0;
        }

        //
        public virtual void GenericAttack(Character target)
        {
            target.health -= strength;
        }

        //
        public virtual void Update()
        {
            // Decrease player tinting
            if(debuffTint > 0)
            {
                debuffTint = debuffTint * 4 / 5;
            }
            if (buffTint > 0)
            {
                buffTint = buffTint * 4 / 5;
            }
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(sprite, position + attackOffset, new Color(255 - buffTint,255 - debuffTint, (255 - Math.Max(debuffTint, buffTint))));
            sb.Draw(healthBar, new Rectangle((int)healthPos.X, (int)healthPos.Y, healthBar.Width * health / 100, healthBar.Height), Color.White);
            sb.Draw(healthContainer, healthPos, Color.White);
        }

        // Reset character to pre battle state
        public virtual void Reset()
        {
            health = maxHealth;
            tempSpeed = 0;
            turnmeter = 0;
            dot = 0;
        }
    }
}                                                           
