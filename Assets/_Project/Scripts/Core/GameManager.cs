using System.Collections.Generic;
using UnityEngine;

namespace TinySurvivors.Core
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private List<PlayerDatabase> database;
        public IReadOnlyList<PlayerDatabase> Database => database;
        
        protected override void Awake()
        {
            base.Awake();
            
            DontDestroyOnLoad(gameObject);
        }
        
        public int SelectedPlayerIndex { get; private set; }

        public void SelectPlayer(int index)
        {
            SelectedPlayerIndex = index;
        }
    }
}