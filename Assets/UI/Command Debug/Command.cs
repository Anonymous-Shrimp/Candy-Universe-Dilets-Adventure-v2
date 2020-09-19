using System;

[Serializable]
public class Command
{
    public string name;
    public string callCommand;
    public DebugLog.varibleType[] varibleType;
    public string helpText;
}
