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
        public Battlefield(Ally[] playerParty, Enemy[] enemyTeam)
        {
            this.playerParty = playerParty;
            this.enemyTeam = enemyTeam;
            currentTurn = TurnOrder.Ally;
            playerScreen = PlayerScreen.MoveSelect;
            attackOrder = new Queue<Ally>();
        }

        // Run battle instance
        public void Update()
        {
            if(currentTurn == TurnOrder.Ally && playerScreen == PlayerScreen.MoveSelect)
            {

            }
        }
    }
}
