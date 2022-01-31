using System.Collections.Generic;
using System.Linq;
using Code.Abstractions.Interfaces;
using UnityEngine;

namespace Code.Raiting
{
    public class RaitingService:IRaitingService
    {
        private const string First = nameof(First);
        private const string Second = nameof(Second);
        private const string Third = nameof(Third);
        public Dictionary<string, int> GetRaitingTable => _records;
        private Dictionary<string, int> _records;

        public RaitingService()
        {
            _records = new Dictionary<string, int>()
            {
                {First, PlayerPrefs.GetInt(First, 2)},
                {Second, PlayerPrefs.GetInt(Second, 1)},
                {Third, PlayerPrefs.GetInt(Third, 0)}
            };
        }

        public void AddRecord(int scores)
        {
            var key = _records
                .Where(v => v.Value < scores)
                .OrderByDescending(v => v.Value)
                .FirstOrDefault()
                .Key;
            if (key is null) return;
            _records[key] = scores;
            Save();
        }

        private void Save()
        {
            foreach (var record in _records)
            {
                PlayerPrefs.SetInt(record.Key, record.Value);
            }
        }
    }
}