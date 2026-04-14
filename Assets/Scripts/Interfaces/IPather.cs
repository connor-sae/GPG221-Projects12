using System;

namespace Westhouse.GPG221.AI.Navigation
{
    public interface IPather
    {
        public void SetPath(NavPath path, Action onPathCompleteCallback);
        public NavPath GetPath();
    }
}
