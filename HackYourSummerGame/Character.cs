using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // health property r/w
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        // Constructor
        public Character(int health, int strength)
        {
            this.health = health;
            this.strength = strength;
        }

        //
        public virtual void Attack(Character target)
        {
            target.health -= strength;
        }
    }
}                                                           
