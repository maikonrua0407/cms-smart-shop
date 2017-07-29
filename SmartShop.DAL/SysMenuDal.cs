using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShop.DAL
{
    public static class SysMenuDal
    {
        public static List<SYS_MENU> GetAllMenu()
        {
            SmartShopEntities entities=new SmartShopEntities();
            return entities.SYS_MENU.ToList();
        }
    }
}
