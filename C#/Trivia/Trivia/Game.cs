using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<Player> _players = new();
        private Player _currentPlayer;
        
        private bool _isGettingOutOfPenaltyBox;

        public bool IsPlayable() => (HowManyPlayers() >= 2);

        public Game()
        {
            Categorie.InitQuestions();
        }

        public void Add(string playerName)
        {
            var player = new Player(playerName);
            _players.Add(player);

            if(_currentPlayer is null)
                _currentPlayer = player;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine($"They are {_players.Count} players");
        }

        public int HowManyPlayers() => _players.Count;

        public void Roll(int roll)
        {
            Console.WriteLine(_currentPlayer.Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            bool canRoll = !_currentPlayer.InPenaltyBox || !IsInPenalty(roll);

            if (canRoll)
                HandleRoll(roll);
        }

        private bool IsInPenalty(int roll)
        {
            bool isGettingOutOfPenalty = _currentPlayer.InPenaltyBox && roll % 2 != 0;

            if (isGettingOutOfPenalty)
            {
                _isGettingOutOfPenaltyBox = true;
                Console.WriteLine(_currentPlayer.Name + " is getting out of the penalty box");
            }
            else
            {
                Console.WriteLine(_currentPlayer.Name + " is not getting out of the penalty box");
                _isGettingOutOfPenaltyBox = false;
            }

            return !_isGettingOutOfPenaltyBox;
        }

        private void HandleRoll(int roll)
        {
            _currentPlayer.Place = _currentPlayer.Place + roll;
            if (_currentPlayer.Place > 11) _currentPlayer.Place = _currentPlayer.Place -12;

            Console.WriteLine(_currentPlayer.Name
                    + "'s new location is "
                    + _currentPlayer.Place);
            Console.WriteLine("The category is " + CurrentCategory());
            AskQuestion();
        }

        private void AskQuestion()
        {
            var categorie = CurrentCategory();
            Console.WriteLine(categorie.Questions.First());
            categorie.Questions.RemoveFirst();
        }

        private Categorie CurrentCategory()
            => Categorie.FromValue(_currentPlayer.Place);

        public bool WasCorrectlyAnswered()
        {
            bool canPlay = (_currentPlayer.InPenaltyBox && _isGettingOutOfPenaltyBox)
                || _currentPlayer.InPenaltyBox is false;
            bool notAWinner = true;

            if (canPlay)
            {
                PlayerWonTheTurn();
                notAWinner = DidPlayerWin();
            }

            IncreasePlayer();
            return notAWinner;
        }

        private void IncreasePlayer()
        {
            var currentIndex = _players.IndexOf(_currentPlayer);
            currentIndex++;

            if (currentIndex <= _players.Count - 1)
                _currentPlayer = _players[currentIndex];
            else 
                 _currentPlayer = _players.First();
        }

        private void PlayerWonTheTurn()
        {
            Console.WriteLine("Answer was correct!!!!");
            _currentPlayer.Purses++;
            Console.WriteLine(_currentPlayer.Name
                    + " now has "
                    + _currentPlayer.Purses
                    + " Gold Coins.");
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_currentPlayer.Name + " was sent to the penalty box");
            _currentPlayer.InPenaltyBox = true;

            IncreasePlayer();
            return true;
        }

        private bool DidPlayerWin() => !(_currentPlayer.Purses == 6);
    }

}
