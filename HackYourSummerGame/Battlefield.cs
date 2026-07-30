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
    public enum PlayerScreen
    {
        CharacterSelect,
        MoveSelect
    }
    internal class Battlefield
    {
        // Teams
        private Ally[] playerParty;
        private Enemy[] enemyTeam;

        private Queue<Ally> attackOrder;
        private PlayerScreen playerScreen;
        private TurnOrder currentTurn;

        // Constructor
        public Battlefield(List<Ally> playerParty, List<Enemy> enemyTeam)
        {
            this.playerParty = new Ally[4];
            this.enemyTeam = new Enemy[4];

            for (int i = 0; i < playerParty.Count; i++)
            {
                this.playerParty[i] = playerParty[i];
            }

            for (int i = 0; i < enemyTeam.Count; i++)
            {
                this.enemyTeam[i] = enemyTeam[i];
            }
            
            currentTurn = TurnOrder.Ally;
            playerScreen = PlayerScreen.MoveSelect;
            attackOrder = new Queue<Ally>();
        }

        // Run battle instance
        public void Update()
        {
            
        }


        //
        private void NextTurn()
        {
            List<Character> aliveChar = new List<Character>();
            Double percentChange = int.MaxValue;
            aliveChar.AddRange(playerParty);
            aliveChar.AddRange(enemyTeam);

            // Determine change in tm
            for (int i = 0; i < aliveChar.Count; i++)
            {
                if ((100 - aliveChar[i].Turnmeter) / aliveChar[i].Speed < percentChange)
                {
                    percentChange = (100 - aliveChar[i].Turnmeter) / aliveChar[i].Speed;
                }
            }

            // Increase turn meter
            for (int i = 0; i < aliveChar.Count; i++)
            {
                aliveChar[i].Turnmeter += (aliveChar[i].Speed * percentChange);
            }
        }
    }
}
