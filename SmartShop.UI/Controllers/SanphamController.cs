using SmartShop.DAL;
using System;
using System.Data;
using System.Web.Mvc;
using SmartShop.Utilities;

namespace SmartShop.UI.Controllers
{
    public class SanphamController : Controller
    {
        //
        // GET: /Sanpham/
        static int idSanPham = 0;
        static string pathAnhChinh = "";

        public ActionResult Index()
        {
            return View();
        }

        void layThongTin()
        {
            int totalRecord = 0;
            DataSet ds = ProductDAL.GetSanPhamTheoDieuKien("", "ID", "GIAM", 10, 1, ref totalRecord, idSanPham);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    image(dt.Rows[0]);
                    thongTinChung(dt.Rows[0]);
                    thongTinChiTiet(dt.Rows[0]);
                }
            }
        }

        void image(DataRow r)
        {
            pathAnhChinh = HeThong.duongdan_anhSP(Convert.ToInt32(r["ID"]), r["YTE_ANH_CHINH"].ToString());
            string pathAnhPhu1 = HeThong.duongdan_anhSP(Convert.ToInt32(r["ID"]), r["YTE_ANH_PHU_1"].ToString());
            string pathAnhPhu2 = HeThong.duongdan_anhSP(Convert.ToInt32(r["ID"]), r["YTE_ANH_PHU_2"].ToString());
            string pathAnhPhu3 = HeThong.duongdan_anhSP(Convert.ToInt32(r["ID"]), r["YTE_ANH_PHU_3"].ToString());
            string pathAnhPhu4 = HeThong.duongdan_anhSP(Convert.ToInt32(r["ID"]), r["YTE_ANH_PHU_4"].ToString());
            string strImg = "";
            strImg += "<div class=\"productdetail_image\">";
            strImg += "    <div id=\"left-chitiet\">";
            strImg += "        <div class=\"productdetai-wrapper\">";
            strImg += "            <div id=\"anh-to\">";
            strImg += "                <a class=\"anh-chinha highslide\" alt=\"\" rel=\"lightbox[roadtrip]\" href=\"javascript:ShowZoomDialog(true);\">";
            //strImg += "                    <span id=\"zoom\"></span>";
            strImg += "                <img class=\"anh-chinh cssimageproductone\" style=\"width:300px;\" alt=\"\" src=\"" + pathAnhChinh + "\">";
            strImg += "                </a>";
            strImg += "            </div>";
            strImg += "            <script>";
            strImg += "                $(window).load(function () {";
            strImg += "                    $(\".thumb_click\").click(function () {";
            strImg += "                        $('.anh-chinh').attr('src', $(this).attr('alt'));";
            //strImg += "                        $('.anh-chinha').attr('href', $(this).attr('alt'));";
            strImg += "                    });";
            strImg += "                });";
            strImg += "            </script>";
            strImg += "            <ul>";

            string strListImgZoom = "";
            //ltrZoomImg.Text = "<img id=\"ZoomImage\" src=\"" + pathAnhChinh + "\" height=\"440px\" style=\"width: auto\" onclick=\"\" />";
            strListImgZoom += "<div style=\"float: left; width: 85px; height: 85px; background: #FFF; margin-right: 2px; margin-top: 20px; margin-bottom: 10px;\">";
            strListImgZoom += "<a href=\"javascript:changeImage('" + pathAnhChinh + "');\"><img src=\"" + pathAnhChinh + "\" style=\"float: left; width: 85px; height: 85px\" />";
            strListImgZoom += "</div>";

            if (!pathAnhPhu1.Contains("no-image"))
            {
                strImg += "                <li class=\"active1\">";
                strImg += "                    <a href=\"" + pathAnhPhu1 + "\" rel=\"\"></a>";
                strImg += "                    <img id=\"anhconcon\" class=\"thumb_img thumb_click\" alt=\"" + pathAnhPhu1 + "\" src=\"" + pathAnhPhu1 + "\" style=\"width: 60px; height: 50px\">";
                strImg += "                </li>";

                strListImgZoom += "<div style=\"float: left; width: 85px; height: 85px; background: #FFF; margin-right: 2px\">";
                strListImgZoom += "<a href=\"javascript:changeImage('" + pathAnhPhu1 + "');\"><img src=\"" + pathAnhPhu1 + "\" style=\"float: left; width: 85px; height: 85px\" />";
                strListImgZoom += "</div>";
            }
            if (!pathAnhPhu2.Contains("no-image"))
            {
                strImg += "                <li class=\"active2\">";
                strImg += "                    <a href=\"" + pathAnhPhu2 + "\" rel=\"\"></a>";
                strImg += "                    <img id=\"Img1\" class=\"thumb_img thumb_click\" alt=\"" + pathAnhPhu2 + "\" src=\"" + pathAnhPhu2 + "\" style=\"width: 60px; height: 50px\">";
                strImg += "                </li>";

                strListImgZoom += "<div style=\"float: left; width: 85px; height: 85px; background: #FFF; margin-right: 2px\">";
                strListImgZoom += "<a href=\"javascript:changeImage('" + pathAnhPhu2 + "');\"><img src=\"" + pathAnhPhu2 + "\" style=\"float: left; width: 85px; height: 85px\" />";
                strListImgZoom += "</div>";
            }
            if (!pathAnhPhu3.Contains("no-image"))
            {
                strImg += "                <li class=\"active3\">";
                strImg += "                    <a href=\"" + pathAnhPhu3 + "\" rel=\"\"></a>";
                strImg += "                    <img id=\"Img2\" class=\"thumb_img thumb_click\" alt=\"" + pathAnhPhu3 + "\" src=\"" + pathAnhPhu3 + "\" style=\"width: 60px; height: 50px\">";
                strImg += "                </li>";

                strListImgZoom += "<div style=\"float: left; width: 85px; height: 85px; background: #FFF; margin-right: 2px\">";
                strListImgZoom += "<a href=\"javascript:changeImage('" + pathAnhPhu3 + "');\"><img src=\"" + pathAnhPhu3 + "\" style=\"float: left; width: 85px; height: 85px\" />";
                strListImgZoom += "</div>";
            }
            if (!pathAnhPhu4.Contains("no-image"))
            {
                strImg += "                <li class=\"active4\">";
                strImg += "                    <a href=\"" + pathAnhPhu4 + "\" rel=\"\"></a>";
                strImg += "                    <img id=\"Img3\" class=\"thumb_img thumb_click\" alt=\"" + pathAnhPhu4 + "\" src=\"" + pathAnhPhu4 + "\" style=\"width: 60px; height: 50px\">";
                strImg += "                </li>";

                strListImgZoom += "<div style=\"float: left; width: 85px; height: 85px; background: #FFF; margin-right: 2px\">";
                strListImgZoom += "<a href=\"javascript:changeImage('" + pathAnhPhu4 + "');\"><img src=\"" + pathAnhPhu4 + "\" style=\"float: left; width: 85px; height: 85px\" />";
                strListImgZoom += "</div>";
            }

            strListImgZoom += "</div>";
            strImg += "            </ul>";
            strImg += "        </div>";
            strImg += "    </div>";
            strImg += "</div>";
        }

        void thongTinChung(DataRow r)
        {
            string strThongTinChung = "";
            strThongTinChung += "<div class=\"productdetail_shortcontent\">";
            strThongTinChung += "    <div class=\"shortcontent-info\">";
            strThongTinChung += "        <ul>";
            strThongTinChung += "            <li>";
            strThongTinChung += "                <div class=\"productdetail_title\">";
            strThongTinChung += "                    <h1>" + r["TEN"].ToString() + "</h1>";
            strThongTinChung += "                </div>";
            strThongTinChung += "            </li>";
            strThongTinChung += "            <li>";
            strThongTinChung += "                <div class=\"pdetail-caption\">Mã sản phẩm</div>";
            strThongTinChung += "                <div class=\"pdetail-info\"><table style=\"margin:0;\"><tr>";
            strThongTinChung += "                    <td><label style=\"display: block; margin-top: -5px; font-weight: bold;\">" + r["MA"].ToString() + "</label>";
            strThongTinChung += "                    <td><img alt=\"\" src=\"" + AppEnv.ApplicationPath + "/Portal/imgs/icon/Cute-Ball-Go-icon.png\" style=\"margin-top:-5px; width: 32px; height: 32px\" >";
            strThongTinChung += "                    <td><label style=\"display: block; margin-top: -5px; font-weight: bold;\">Đã mua: " + r["XEM"].ToString() + "</label></td></tr></table>";
            strThongTinChung += "                </div>";
            strThongTinChung += "            </li>";
            strThongTinChung += "            <li>";
            strThongTinChung += "                <div class=\"pdetail-caption\">Thương hiệu</div>";
            strThongTinChung += "                <div class=\"pdetail-info\">" + r["YTE_THUONG_HIEU"].ToString() + "</div>";
            strThongTinChung += "            </li>";
            strThongTinChung += "            <li>";
            strThongTinChung += "                <div class=\"pdetail-caption\">Xuất xứ</div>";
            strThongTinChung += "                <div class=\"pdetail-info\">" + r["YTE_XUAT_XU"].ToString() + "</div>";
            strThongTinChung += "            </li>";
            strThongTinChung += "            <li>";
            strThongTinChung += "                <div class=\"pdetail-caption\">Bảo hành</div>";
            strThongTinChung += "                <div class=\"pdetail-info\">" + r["YTE_BAO_HANH"].ToString() + "</div>";
            strThongTinChung += "            </li>";
            strThongTinChung += "            <li>";
            strThongTinChung += "                <div class=\"pdetail-caption\">Đánh giá:</div>";
            strThongTinChung += "                <div class=\"pdetail-info\">";
            strThongTinChung += "                    <div id=\"rating-product-sum_0\" class=\"star-rating rater-0 star-rating-applied star-rating-readonly star-rating-on\">";
            strThongTinChung += "                        <a title=\"1\">1</a>";
            strThongTinChung += "                    </div>";
            strThongTinChung += "                    <div id=\"rating-product-sum_1\" class=\"star-rating rater-0 star-rating-applied star-rating-readonly star-rating-on\">";
            strThongTinChung += "                        <a title=\"2\">2</a>";
            strThongTinChung += "                    </div>";
            strThongTinChung += "                    <div id=\"rating-product-sum_2\" class=\"star-rating rater-0 star-rating-applied star-rating-readonly star-rating-on\">";
            strThongTinChung += "                        <a title=\"3\">3</a>";
            strThongTinChung += "                    </div>";
            strThongTinChung += "                    <div id=\"rating-product-sum_3\" class=\"star-rating rater-0 star-rating-applied star-rating-readonly\">";
            strThongTinChung += "                        <a title=\"4\">4</a>";
            strThongTinChung += "                    </div>";
            strThongTinChung += "                    <div id=\"rating-product-sum_4\" class=\"star-rating rater-0 star-rating-applied star-rating-readonly\">";
            strThongTinChung += "                        <a title=\"5\">5</a>";
            strThongTinChung += "                    </div>";
            strThongTinChung += "                </div>";
            strThongTinChung += "                <iframe src=\"//www.facebook.com/plugins/like.php?href=https%3A%2F%2Fwww.facebook.com%2Fbcare.vn&amp;width&amp;layout=button_count&amp;action=like&amp;show_faces=true&amp;share=true&amp;height=21\" scrolling=\"no\" frameborder=\"0\" style=\"border:none; overflow:hidden; height:21px;\" allowTransparency=\"true\"></iframe>";
            strThongTinChung += "            </li>";
            //lbPrice.InnerText = String.Format("{0:0,0;(0:0,0);Liên hệ}", decimal.Parse(r["YTE_GIA_BAN"].ToString()));
            strThongTinChung = "";
            strThongTinChung += "            <li>";
            strThongTinChung += "            " + r["YTE_SO_TA"].ToString();
            strThongTinChung += "            </li>";
            strThongTinChung += "        </ul>";
            strThongTinChung += "    </div>";
            strThongTinChung += "</div>";
        }

        void thongTinChiTiet(DataRow r)
        {
            string strChiTiet = "";
            strChiTiet += "<div class=\"camket\">";
            strChiTiet += "<p>Bcare cam kết</p>";
            strChiTiet += "<p id=\"vat\">Tất cả hàng hóa bán tại Bcare đều được đảm bảo đã tính 10% VAT đầy đủ.</p>";
            strChiTiet += "<ul>";
            strChiTiet += "<li id=\"doitra\">";
            strChiTiet += "</li>";
            strChiTiet += "<li id=\"giaohang\">";
            strChiTiet += "</li>";
            strChiTiet += "<li id=\"tratien\">";
            strChiTiet += "</li>";
            strChiTiet += "</ul>";
            strChiTiet += "</div>";
            strChiTiet += "<div id=\"tabsholder\">";
            strChiTiet += "    <ul class=\"tabs\">";
            strChiTiet += "        <li id=\"tab1\">Tính năng</li>";
            strChiTiet += "        <li id=\"tab2\">Thông số kỹ thuật</li>";
            strChiTiet += "    </ul>";
            strChiTiet += "    <div class=\"contents marginbot\">";
            strChiTiet += "        <div id=\"content1\" class=\"tabscontent\">";
            strChiTiet += "        " + r["YTE_TINH_NANG"].ToString();
            strChiTiet += "        </div>";
            strChiTiet += "        <div id=\"content2\" class=\"tabscontent\">";
            strChiTiet += "        " + r["YTE_THONG_SO"].ToString();
            strChiTiet += "        </div>";
            strChiTiet += "    </div>";
            strChiTiet += "</div>";
            //ltrDetail.Text = strChiTiet;
        }

    }
}
