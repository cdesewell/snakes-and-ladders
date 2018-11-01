using System;
using SnakesAndLadders.Engine;
using Xunit;


namespace SnakesAndLadders.Test
{
    public class EngineShould
    {
        private GameEngine _engine;
        
        //TokenCanMoveAcrossTheBoard
        [Fact]
        public void PlaceTokenOnSquareOneWhenGameStarts()
        {
            GivenTheGameHasStarted();
            WhenTheTokenIsPlacedOnTheBoard();
            ThenTheTokenIsOnSquare(1);
        }

        private void WhenTheTokenIsPlacedOnTheBoard()
        {
            _engine.PlaceToken("James Bond");
        }

        private void GivenTheGameHasStarted()
        {
            _engine = new GameEngine();
        }

        [Fact]
        public void PlaceTokenOnSquareFourWhenMovingThreeSpaces()
        {   
            GivenTheGameHasStarted();
            WhenTheTokenIsPlacedOnTheBoard();
            WhenTheTokenIsMoved(3);
            ThenTheTokenIsOnSquare(4);
        }

        private void ThenTheTokenIsOnSquare(int expected)
        {
            var tokenLocation = _engine.GetTokenLocation("James Bond");
            
            Assert.Equal(expected,tokenLocation);        
        }

        private void WhenTheTokenIsMoved(int amountToMove)
        {
            _engine.MoveToken("James Bond",amountToMove);
        }

        [Fact]
        public void PlaceTokenOnSquareEightWhenMovingThreeSpacesAndThenFourSpaces()
        {   
            GivenTheGameHasStarted();
            WhenTheTokenIsPlacedOnTheBoard();
            WhenTheTokenIsMoved(3);
            WhenTheTokenIsMoved(4);
            ThenTheTokenIsOnSquare(8);
        }

        //MovesAreDeterminedByDiceRolls
        [Fact]
        public void AlwaysGivenDiceRollBetweenOneAndSix()
        {
            GivenTheGameHasStarted();
            var result = WhenThePlayerRollsADie();
            ThenTheResultShouldBeBetweenOneAndSixInclusive(result);
        }

        private void ThenTheResultShouldBeBetweenOneAndSixInclusive(int rollResult)
        {
            Assert.True(rollResult >= 1 && rollResult <= 6);
        }

        private int WhenThePlayerRollsADie()
        {
            return _engine.RollDie();
        }

        [Fact]
        public void PlaceTokenOnSquareFourGivenDiceRollsAFour()
        {
            GivenTheGameHasStarted();
            WhenTheTokenIsPlacedOnTheBoard();
            var result = WhenThePlayerRollsADie();
            WhenTheTokenIsMoved(result);
            ThenTheTokenIsOnSquare(result + 1);
        }

        //PlayerCanWinTheGame
        [Fact]
        public void GrantVictoryFromSquareNinetySevenWhenMovedThreeSpaces()
        {
            GivenTheGameHasStarted();
            WhenTheTokenIsPlacedOnTheBoard();
            WhenTheTokenIsMoved(96);
            ThenTheTokenIsOnSquare(97);
            
            WhenTheTokenIsMoved(3);
            ThenTheTokenIsOnSquare(100);
            ThenThePlayerHasWonTheGame();
        }

        private void ThenThePlayerHasWonTheGame()
        {
            var hasWon = _engine.CheckForVictory("James Bond");
            Assert.True(hasWon);
        }

        [Fact]
        public void DoesNotMoveFromSquareNinetySevenWhenDiceRollsFour()
        {
            GivenTheGameHasStarted();
            WhenTheTokenIsPlacedOnTheBoard();
            WhenTheTokenIsMoved(96);
            ThenTheTokenIsOnSquare(97);
            
            WhenTheTokenIsMoved(4);
            ThenTheTokenIsOnSquare(97);
            ThenThePlayerHasNotWonTheGame();
        }

        private void ThenThePlayerHasNotWonTheGame()
        {
            var hasWon = _engine.CheckForVictory("James Bond");
            Assert.False(hasWon);        
        }
    }
}