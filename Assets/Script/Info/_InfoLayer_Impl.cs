using cfEngine.Info;
using cfEngine.Meta;

namespace cfEngine.Core.Layer
{
    public partial class InfoLayer
    {
        public static readonly InfoManager[] infos =
        {
            new InventoryInfoManager()
        };
    }
}