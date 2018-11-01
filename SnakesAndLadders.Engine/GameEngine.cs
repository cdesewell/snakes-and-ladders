using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakesAndLadders.Engine
{
    public class GameEngine
    {
        private readonly IList<Token> _tokens;
        private readonly Random _randomNumberGenerator;
        private const int WinningSpace = 100;

        public GameEngine()
        {
            _tokens = new List<Token>();
            _randomNumberGenerator = new Random();
        }

        public void PlaceToken(string playerName)
        {
            _tokens.Add(new Token
            {
                Name = playerName,
                Location = 1
            });
        }

        public int GetTokenLocation(string playerName) =>  _tokens.Single(t => t.Name == playerName).Location;

        public void MoveToken(string playerName, int spacesToMove)
        {
            var token = _tokens.Single(t => t.Name == playerName);

            var newToken = new Token
            {
                Name = playerName,
                Location = token.Location + spacesToMove <= WinningSpace ? token.Location + spacesToMove : token.Location
            };

            _tokens.Remove(token);
            _tokens.Add(newToken);
        }

        public int RollDie() => _randomNumberGenerator.Next(1, 6);

        public bool CheckForVictory(string playerName)
        {
            var token = _tokens.Single(t => t.Name == playerName);

            return token.Location == WinningSpace;
        }
    }
}