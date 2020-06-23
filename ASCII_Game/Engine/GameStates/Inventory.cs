using System;
using System.Collections.Generic;
using System.Text;

namespace GameStates
{
    class Inventory : GameState
    {
        int selectionY = 0;
        int selectionX = 0;

        GameStates.Game game;

        public Inventory(GameStates.Game game)
        {
            this.game = game;
            hud = new IRenderable[]
            {
                new GUI.Container((4,2), new Vector2d16(Config.screenWidth - 9, Config.screenHeight-3), true, 3),
                new GUI.Container((6,3), new Vector2d16(Config.screenWidth * 0.2, Config.screenHeight-5), true, 3)
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