using System.Collections.Generic;

namespace Code.Abstractions.Interfaces
{
    public interface IRaitingService
    {
        void AddRecord(int scores);

        Dictionary<string, int> GetRaitingTable { get; }
    }
}