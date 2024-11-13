namespace VoteWave.Shared.Abstractions.Commands;

public interface ICommand
{
}

public interface ICommand<TResult> : ICommand
{
}