// Book "Дизайн патерни - просто, як двері" by Andriy Buday is licensed under a
// Creative Commons Attribution-NonCommercial 3.0 Unported License ( http://creativecommons.org/licenses/by-nc/3.0/ ). 
// Те саме стосується і вихідного коду.
// Можете міняти код на свій лад, проте не видавайте ідеї прикладів до дизайн патернів за свої власні.
using System;
using System.Collections.Generic;

namespace DesignPatterns.Behavioural
{

    public class Caretaker
    {
        private readonly GameOriginator _game = new GameOriginator();
        private readonly Stack<GameMemento> _quickSaves = new Stack<GameMemento>();

        public void ShootThatDumbAss()
        {
            _game.Play();
        }

        public void F5()
        {
            _quickSaves.Push(_game.GameSave());
        }

        public void F9()
        {
            _game.LoadGame(_quickSaves.Peek());
            _quickSaves.Peek().GetState().Health = 8;
        }
    }

    public class GameOriginator
    {
        private GameState _state = new GameState(100, 0);//Health & Killed Monsters

        public void Play()
        {
            //During this Play method game's state is continuously changed
            Console.WriteLine(_state.ToString());
            _state = new GameState((int)(_state.Health * 0.9), _state.KilledMonsters + 2);
        }

        public GameMemento GameSave()
        {
            return new GameMemento(_state);
        }

        public void LoadGame(GameMemento memento)
        {
            _state = memento.GetState();
        }
    }

    public class GameMemento
    {
        private readonly GameState _state;

        public GameMemento(GameState state)
        {
            _state = state;
        }

        protected internal GameState GetState()
        {
            return _state;
        }
    }

    public class GameState
    {
        public GameState(double health, int killedMonsters)
        {
            Health = health;
            KilledMonsters = killedMonsters;
        }

        public double Health { get; set; }
        public int KilledMonsters { get; set; }

        public override string ToString()
        {
            return string.Format("Health: {0}{1}Killed Monsters: {2}", Health, Environment.NewLine, KilledMonsters);
        }
    }

    class MementoDemo
    {
        public static void Run()
        {
            var caretaker = new Caretaker();
            caretaker.F5();
            caretaker.ShootThatDumbAss();
            caretaker.F5();
            caretaker.ShootThatDumbAss();
            caretaker.ShootThatDumbAss();
            caretaker.ShootThatDumbAss();
            caretaker.ShootThatDumbAss();
            caretaker.F9();
            caretaker.ShootThatDumbAss();
        }
    }
}