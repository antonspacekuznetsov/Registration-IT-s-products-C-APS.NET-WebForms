using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
namespace RegITProducts.administator.pages
{
    public partial class TANManager : System.Web.UI.Page
    {
        private int _length;
        private static Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            int.TryParse(ConfigurationManager.AppSettings["tan"], out _length);
        }

        protected void ButtonGenerateTAN_Click(object sender, EventArgs e)
        {

            try
            {
                using (IController<TAN> sql = new Controller<TAN>())
                {
                    int t;
                    string RandTAn = RandomString();
                    int.TryParse(DropDownList2.SelectedItem.Value, out t);
                    sql.Create(new TAN { TanCode = RandTAn, ProductId = t });
                    LabelStatus.Text = "ТаН: " + RandTAn + " сгенерирован!";
                }

            }
            catch (Exception ex)
            {
                LabelStatus.Text = "Не возможно сгенерировать ТаН по следующей причине: " + ex.Message;
            }
        }

        protected void ButtonShowTANs_Click(object sender, EventArgs e)
        {
            try
            {
                using (IController<TAN> sql = new Controller<TAN>())
                {
                    LabelListOfTANs.Text = "";
                    foreach (TAN t in sql.GetAll())
                    {

                        LabelListOfTANs.Text += "TaNCode: " + t.TanCode + " ProductId: " + t.ProductId + "<br />";
                    }
                }

            }
            catch (Exception ex)
            {
                LabelStatus.Text = "Не возможно загрузить список ТаН'ов по следуещей причине: " + ex.Message;
            }
        }

        private string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, _length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
     
    }
}