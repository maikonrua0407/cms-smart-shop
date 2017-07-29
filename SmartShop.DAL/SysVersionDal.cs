using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.DAL
{
    public static class SysVersionDal
    {
        public static List<SYS_VERSION> GetAllVersion()
        {
            SmartShopEntities entities = new SmartShopEntities();
            return entities.SYS_VERSION.ToList();
        }
        
        public  static SYS_VERSION GetLastestVersion(string loaiVersion)
        {
            SmartShopEntities entities = new SmartShopEntities();
            return entities.SYS_VERSION.First(e=>e.LOAI.Equals(loaiVersion));
        }
    }
}
