namespace Code.Abstractions.Interfaces
{
    public interface IChangeLevelService
    {
        int CurrentLevel { get; }
        void ChangeLevel();
    }
}