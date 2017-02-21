namespace TLA.Models.System
{
    public interface IMonitor
    {
        bool TryEnter(object obj, int millisecondsTimeout);
        void Exit(object obj);
    }
}