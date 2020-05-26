using System.Threading.Tasks;

namespace pimp.ECS
{
    public class AbstractSystem
    {
        public bool Busy;
        public virtual void Update() { }
        public void UpdateAsync()
        {
            if (!Busy)
                Task.Run(Update);
        }
    }
}