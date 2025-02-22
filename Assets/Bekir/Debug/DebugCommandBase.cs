using System;

public class DebugCommandBase
{
    public string commandId { get; }
    public string commandDescription { get; }
    public string commandFormat { get; }

    public DebugCommandBase(string id, string CommandDescription, string Format)
    {
        commandId = id;
        commandDescription = CommandDescription;
        commandFormat = Format;
    }
}

public class DebugCommand : DebugCommandBase
{
    public Action command;
    
    public DebugCommand(string id, string CommandDescription, string Format, Action command) : base(id, CommandDescription, Format)
    {
        this.command = command;
    }

    public void Invoke()
    {
        command.Invoke();
    }
}

public class DebugCommand<T> : DebugCommandBase
{
    public Action<T> command;
    
    public DebugCommand(string id, string CommandDescription, string Format, Action<T> command) : base(id, CommandDescription, Format)
    {
        this.command = command;
    }

    public void Invoke(T type)
    {
        command.Invoke(type);
    }
}
<<<<<<< Updated upstream
=======
public class DebugCommand<T, T1> : DebugCommandBase
{
    public Action<T, T1> command;
    
    public DebugCommand(string id, string CommandDescription, string Format, Action<T, T1> command) : base(id, CommandDescription, Format)
    {
        this.command = command;
    }

    public void Invoke(T type, T1 type2)
    {
        command.Invoke(type, type2);
    }
}
>>>>>>> Stashed changes
