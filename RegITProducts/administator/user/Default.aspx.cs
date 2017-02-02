using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RegITProducts.administator.pages;
namespace RegITProducts.administator.user
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonCheckTAN_Click(object sender, EventArgs e)
        {
            if (this.TextBoxTAN.Text.Trim() == "")
            {
                LabelInfo.Text = "Введите ТАН";
                return;
            }
            if(!RegExRequester.Check(this.TextBoxTAN.Text.Trim(), "^[а-яА-ЯёЁa-zA-Z0-9]+$"))
            {
                LabelInfo.Text = "Вы ввели запрещенные символы! Попробуйте снова.";
                this.TextBoxTAN.Text = "";
                return;
            }
            try
                        {

                            using (IController<TAN> sql = new Controller<TAN>())
                            {
                                LabelInfo.Text = "";
                                int status = 0;
                                foreach (TAN r in sql.GetAll())
                                {
                                    if (r.TanCode == this.TextBoxTAN.Text.Trim())
                                    {
                                        status = 1;
                                    }
                                }
                                if (status == 0)
                                {
                                    LabelInfo.Text = "ТАН " + this.TextBoxTAN.Text.Trim() + " в базе данных не существует!";
                                    this.TextBoxTAN.Text = "";
                                    return;
                                }

                                Response.Redirect("Register.aspx?tan=" + this.TextBoxTAN.Text.Trim());
                            }
                        }

                        catch (Exception ex)
                        {
                        
                                LabelInfo.Text = "Не возможно загрузить список категорий по следующей причине: " + ex.Message;
                        }
                        
        }
    }
}