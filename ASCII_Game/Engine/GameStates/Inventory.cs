using System;
using System.Collections.Generic;
using System.Text;

namespace GameStates
{
    class Inventory : GameState
    {
        GameStates.Game game;

        public Inventory(GameStates.Game game)
        {
            this.game = game;
            hud = new IRenderable[]
            {
            new GUI.Container((2,1), new Vector2d16(Config.screenWidth - 8, Config.screenHeight-4), true, 3)
            };
        }

        public override void Physics(EInput input)
        {
            switch (input)
            {
                case EInput.inventory:
                case EInput.escape:
                    global::Game.gameState = game;
                    break;
            }
        }
    }
}