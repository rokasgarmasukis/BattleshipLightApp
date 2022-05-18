using static BattleshipLight.Methods;
using BattleshipLibrary.Models;
using BattleshipLibrary;

WelcomeMessage();

PlayerModel activePlayer = CreatePlayer("Player 1");
PlayerModel opponent = CreatePlayer("Player 2");
PlayerModel winner = null;

do
{
    DisplayShotGrid(activePlayer);

    RecordPlayerShot(activePlayer, opponent);

    bool doesGameContinue = GameLogic.PlayerStillActive(opponent);

    if (doesGameContinue == true)
    {
        (activePlayer, opponent) = (opponent, activePlayer);
    }
    else
    {
        winner = activePlayer;
    }

} while (winner == null);

IdentifyWinner(winner);

Console.ReadLine();


