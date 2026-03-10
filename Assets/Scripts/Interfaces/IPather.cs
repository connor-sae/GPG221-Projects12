using System;
using GPG221.AI;
public interface IPather
{
    public void SetPath(NavPath path, Action onPathCompleteCallback);
    public NavPath GetPath();
}
