using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<string> _players = new();

        private readonly int[] _places = new int[6];
        private readonly int[] _purses = new int[6];

        private readonly bool[] _inPenaltyBox = new bool[6];

        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

        public bool IsPlayable() => (HowManyPlayers() >= 2);

        public Game()
        {
            Categorie.InitQuestions();
        }

        public void Add(string playerName)
        {
            _players.Add(playerName);
            _places[HowManyPlayers()] = 0;
            _purses[HowManyPlayers()] = 0;
            _inPenaltyBox[HowManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine($"They are {_players.Count} players");
        }

        public int HowManyPlayers() => _players.Count;

        public void Roll(int roll)
        {
            Console.WriteLine(_players[_currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
            
            bool canRoll = !_inPenaltyBox[_currentPlayer] || !IsInPenalty(roll);

            if (canRoll)
                HandleRoll(roll);
        }

        private bool IsInPenalty(int roll)
        {
            bool isGettingOutOfPenalty = _inPenaltyBox[_currentPlayer] && roll % 2 != 0;

            if (isGettingOutOfPenalty)
            {
                _isGettingOutOfPenaltyBox = true;
                Console.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");
            }
            else
            {
                Console.WriteLine(_players[_currentPlayer] + " is not getting out of the penalty box");
                _isGettingOutOfPenaltyBox = false;
            }

            return !_isGettingOutOfPenaltyBox;
        }

        private void HandleRoll(int roll)
        {
            _places[_currentPlayer] = _places[_currentPlayer] + roll;
            if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

            Console.WriteLine(_players[_currentPlayer]
                    + "'s new location is "
                    + _places[_currentPlayer]);
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
            => Categorie.FromValue(_places[_currentPlayer]);

        public bool WasCorrectlyAnswered()
        {
            bool canPlay = (_inPenaltyBox[_currentPlayer] && _isGettingOutOfPenaltyBox)
                || _inPenaltyBox[_currentPlayer] is false;
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
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
        }

        private void PlayerWonTheTurn()
        {
            Console.WriteLine("Answer was correct!!!!");
            _purses[_currentPlayer]++;
            Console.WriteLine(_players[_currentPlayer]
                    + " now has "
                    + _purses[_currentPlayer]
                    + " Gold Coins.");
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            IncreasePlayer();
            return true;
        }


        private bool DidPlayerWin() => !(_purses[_currentPlayer] == 6);
    }

}
