using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Basket
/// </summary>
public class Basket
{
    DataTable dt;
    public Basket()
    {
        dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("Name");
        dt.Columns.Add("Quantity");
        dt.Columns.Add("Price");
        dt.Columns.Add("Total");
    }

    public DataTable setTable()
    {
        return this.dt;
    }

    public DataTable Insert(DataTable tb, int id, string name, int quantity, double? price)
    {
        if (tb.Rows.Count != 0)
        {
            bool f = false;
            foreach (DataRow dr in tb.Rows)
            {
                if (Convert.ToInt32(dr[0]) == id)
                {
                    dr[2] = Convert.ToInt32(dr[2]) + 1;
                    dr[4] = Convert.ToInt32(dr[2]) * price;
                    f = true;
                }
            }
            if (!f)
            {
                DataRow dr = tb.NewRow();
                dr[0] = id;
                dr[1] = name;
                dr[2] = quantity;
                dr[3] = price;
                dr[4] = price * quantity;
                tb.Rows.Add(dr);
            }
        }
        else
        {
            DataRow dr = tb.NewRow();
            dr[0] = id;
            dr[1] = name;
            dr[2] = quantity;
            dr[3] = price;
            dr[4] = price * quantity;
            tb.Rows.Add(dr);
        }
        return tb;
    }

    public DataTable Delete(DataTable tb, int id)
    {
        foreach (DataRow dr in tb.Rows)
        {
            if (Convert.ToInt32(dr[0]) == id)
            {
                tb.Rows.Remove(dr);
                break;
            }
        }
        return tb;
    }

    public DataTable Update(DataTable tb, int id, int quantity)
    {
        DataTable td = tb;
        foreach (DataRow dr in td.Rows)
        {
            if (Convert.ToInt32(dr[0]) == id)
            {
                dr[2] = quantity;
                dr[4] = quantity * Convert.ToDouble(dr[3]);
            }
        }
        return td;
    }

}