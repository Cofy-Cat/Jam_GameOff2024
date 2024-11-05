using System.Collections.Generic;

public abstract class CommandPattern
{
    public abstract CommandType commandType { get; }
    public abstract bool IsMatch(ActionCommand newCommand, IReadOnlyList<ActionCommand> commandQueue);
}