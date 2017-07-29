using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartShop.Admin
{
    public static class AppConstant
    {
        public enum Action
        {
            list,
            create,
            saveTemp,
            save,
            approve,
            reject,
            delete,
            print,
            import,
            export,
            reload
        }

        public static string layGiaTri(this Action action)
        {
            switch (action)
            {
                case Action.list: return "list";
                case Action.create: return "create";
                case Action.save: return "save";
                case Action.approve: return "approve";
                case Action.reject: return "reject";
                case Action.delete: return "delete";
                case Action.print: return "print";
                case Action.import: return "import";
                case Action.export: return "export";
                case Action.reload: return "reload";
                default: return "";
            }
        }

        public static Action layAction(string action)
        {
            switch (action)
            {
                case "list": return Action.list;
                case "create": return Action.create;
                case "save": return Action.save;
                case "approve": return Action.approve;
                case "reject": return Action.reject;
                case "delete": return Action.delete;
                case "print": return Action.print;
                case "import": return Action.import;
                case "export": return Action.export;
                case "reload": return Action.reload;
                default: return Action.list;
            }
        }

        public static List<ActionAttribute> ListAction(string currentControler)
        {
            var listAction = new List<ActionAttribute>
                                 {
                                     new ActionAttribute
                                         {
                                             Key = Action.list.layGiaTri(),
                                             Name = "Danh sách",
                                             Icon = "fa fa-bars",
                                             ExtAction = currentControler + "/DanhSach"
                                         },
                                     new ActionAttribute
                                         {
                                             Key = Action.create.layGiaTri(),
                                             Name = "Thêm",
                                             Icon = "fa fa-file-text",
                                             ExtAction =
                                                 currentControler + "/Get" + currentControler.Split('/').Last() + "Detail"
                                         },
                                     new ActionAttribute
                                         {Key = Action.save.layGiaTri(), Name = "Lưu", Icon = "fa fa-save"},
                                     new ActionAttribute
                                         {
                                             Key = Action.approve.layGiaTri(),
                                             Name = "Duyệt",
                                             Icon = "fa fa-check-circle-o",
                                             ExtAction = currentControler + "/Approve" + currentControler.Split('/').Last()
                                         },
                                     new ActionAttribute
                                         {
                                             Key = Action.reject.layGiaTri(),
                                             Name = "Hủy Duyệt",
                                             Icon = "fa fa-check-circle-o",
                                             ExtAction = currentControler + "/Reject" + currentControler.Split('/').Last()
                                         },
                                     new ActionAttribute
                                         {
                                             Key = Action.delete.layGiaTri(),
                                             Name = "Xóa",
                                             Icon = "fa fa-trash-o",
                                             ExtAction = currentControler + "/Delete" + currentControler.Split('/').Last()
                                         },
                                     new ActionAttribute
                                         {
                                             Key = Action.print.layGiaTri(),
                                             Name = "In",
                                             Icon = "fa fa-print",
                                             ExtAction = currentControler + "/Print" + currentControler.Split('/').Last()
                                         },
                                     new ActionAttribute
                                         {
                                             Key = Action.import.layGiaTri(),
                                             Name = "Nhập File",
                                             Icon = "fa fa-sign-in",
                                             ExtAction = currentControler + "/Import" + currentControler.Split('/').Last()
                                         },
                                     new ActionAttribute
                                         {
                                             Key = Action.export.layGiaTri(),
                                             Name = "Xuất File",
                                             Icon = "fa fa-sign-out",
                                             ExtAction = currentControler + "/Export" + currentControler.Split('/').Last()
                                         },
                                     new ActionAttribute
                                         {
                                             Key = Action.reload.layGiaTri(),
                                             Name = "Tải lại",
                                             Icon = "fa fa-refresh",
                                             ExtAction = currentControler + "/Reload" + currentControler.Split('/').Last()
                                         }
                                 };
            return listAction;
        }
    }

    public class ActionAttribute
    {
        string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string icon;

        public string Icon
        {
            get { return icon; }
            set { icon = value; }
        }
        string extAction;

        public string ExtAction
        {
            get { return extAction; }
            set { extAction = value; }
        }
    }
}