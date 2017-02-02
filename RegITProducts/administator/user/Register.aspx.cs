using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RegITProducts.administator.pages;
namespace RegITProducts.administator.user
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                return;

            if (!string.IsNullOrEmpty(Request.QueryString["tan"]))
            {
                using (IController<TAN> sql = new Controller<TAN>())
                {
                    int id = 0;
                    foreach (TAN r in sql.GetAll())
                    {
                        if (r.TanCode == Request.QueryString["tan"]) 
                        id = r.ProductId;
                    }
                    using (IController<Product> sql2 = new Controller<Product>())
                    using (IController<Category> sql3 = new Controller<Category>())
                    {
                        LabelInfoProduct.Text = "ТАН: " + Request.QueryString["tan"] + " сгенерирован для продукта: " + sql2.GetById(id).ProductName + " из категории: " + sql3.GetById(sql2.GetById(id).CategoryID).CategoryName;
                    }
                }

            }
            else
            {
                Response.Redirect("Default.aspx");
            }

            TextBoxRegisterDate.Text = String.Format("{0:d/MM/yyyy}", DateTime.Today); 

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!CheckIsEmpty())//проверка на пустые поля
                return;
            if(!CheckCorrectSymbol())//проверка на ввод запрещенных символов
                return;
            if (!CheckCorrectDate())//провера на корректную Дату
                return;
            int productID = 0;
            int TANId = 0;
            try
            {
                using (IController<TAN> sql = new Controller<TAN>())
                {
                    foreach(TAN r in sql.GetAll())
                    {
                        if (r.TanCode == Request.QueryString["tan"])
                        {
                            productID = r.ProductId;
                            TANId = r.id;
                            break;
                        }
                    }
                }
                using (IController<RegisterData> sql = new Controller<RegisterData>())
                {
                    DateTime birth,buydate,registerdate;
                    DateTime.TryParse(TextBoxBirthDay.Text.Trim(), out birth);
                    DateTime.TryParse(TextBoxBuyDate.Text.Trim(), out buydate);
                    DateTime.TryParse(TextBoxRegisterDate.Text.Trim(), out registerdate);
                    sql.Create(new RegisterData { name = TextBoxName.Text.Trim(), surname = TextBoxSurname.Text.Trim(), birth =birth, adress=TextBoxAdress.Text.Trim(), productInfo=TextBoxInfoAboutProduct.Text, buydate=buydate, registerdate=registerdate, ProductId=productID, TANId=TANId  });
                }
                Response.Redirect("RegSuccessful.html");
            }
            catch (Exception ex)
            {
                LabelError.Text = "Не возможно произвести регистрацию по следующей причине: " + ex.Message;
            }
        }


        private bool CheckIsEmpty()
        {
            LabelError.Text = "";
            int t = 0;
            string errorStroka;
            if (TextBoxName.Text == "")
            {
               LabelError.Text += "Имя, ";
                t = 1;
            }
            if (TextBoxSurname.Text == "")
            {
               LabelError.Text += "Фамилия, ";
                t = 1;
            }
            if (TextBoxBirthDay.Text == "")
            {
               LabelError.Text += "Дата рождения, ";
                t = 1;
            }
            if (TextBoxAdress.Text == "")
            {
               LabelError.Text += "Адрес, ";
                t = 1;
            }
            if (TextBoxInfoAboutProduct.Text == "")
            {
               LabelError.Text += "Информация о продукте, ";
                t = 1;
            }
            if (TextBoxBuyDate.Text == "")
            {
               LabelError.Text += "Дата покупки, ";
                t = 1;
            }
            if (t == 1)
            {
                errorStroka = "Следующие поля обязательны к заполнению: " +LabelError.Text;
               LabelError.Text = errorStroka;
                return false;
            }
            return true;
        }

        private bool CheckCorrectSymbol()
        {
            LabelError.Text = "";
            string errorStroka="";
            int t = 0;
            if(!RegExRequester.Check(TextBoxName.Text.Trim(), "^[а-яА-ЯёЁa-zA-Z]+$"))
            {
                errorStroka += "Поле \"Имя\" может содержать только символы кириллицы или латиницы!<br />";
                t = 1;
            }
            if (!RegExRequester.Check(TextBoxSurname.Text.Trim(), "^[а-яА-ЯёЁa-zA-Z]+$"))
            {
                errorStroka += "Поле \"Фамилия\" может содержать только символы кириллицы или латиницы!<br />";
                t = 1;
            }
            if (!RegExRequester.Check(TextBoxBirthDay.Text.Trim(), "^[0-9.:]+$"))
            {
                errorStroka += "Поле \"Дата рождения\" может содержать только цифры!<br />";
                t = 1;
            }
            if (!RegExRequester.Check(TextBoxAdress.Text.Trim(), "^[а-яА-ЯёЁa-zA-Z0-9.]+$"))
            {
                errorStroka += "Поле \"Адрес\" может содержать только цифры и сиволы!<br />";
                t = 1;
            }
            if (!RegExRequester.Check(TextBoxInfoAboutProduct.Text.Trim(), "^[а-яА-ЯёЁa-zA-Z0-9,.]+$"))
            {
                errorStroka += "Поле \"Информация о продукте\" может содержать только символы кириллицы или латиницы и цифры и следующие знаки ',' '.'<br />";
                t = 1;
            }
            if (!RegExRequester.Check(TextBoxBuyDate.Text.Trim(), "^[0-9.:]+$"))
            {
                errorStroka += "Поле \"Дата покупки\" может содержать только цифры!<br />";
                t = 1;
            }

            if (!RegExRequester.Check(TextBoxRegisterDate.Text.Trim(), "^[0-9.:]+$"))
            {
                errorStroka += "Поле \"Дата регистрации\" может содержать только цифры!<br />";
                t = 1;
            }
            if (t == 1)
            {
                LabelError.Text = errorStroka;
                return false;
            }
            return true;
        }

        private bool CheckCorrectDate()
        {
            LabelError.Text = "";
            DateTime myDate;
            int t = 0;
            string errorStroka = "";
            if (!DateTime.TryParse(TextBoxBirthDay.Text.Trim(), out myDate))
            {
                errorStroka += "Формат даты в поле \" Дата рождения\" не коректный (День.Месяц.Год)<br />";
                t = 1;
            }
            if (!DateTime.TryParse(TextBoxBuyDate.Text.Trim(), out myDate))
            {
                errorStroka += "Формат даты в поле \" Дата покупки\" не коректный (День.Месяц.Год)<br />";
                t = 1;
            }
            if (!DateTime.TryParse(TextBoxRegisterDate.Text.Trim(), out myDate))
            {
                errorStroka += "Формат даты в поле \" Дата регистрации\" не коректный (День.Месяц.Год)<br />";
                t = 1;
            }

            if (t == 1)
            {
                LabelError.Text = errorStroka;
                return false;
            }
            return true;
        }
    }
}