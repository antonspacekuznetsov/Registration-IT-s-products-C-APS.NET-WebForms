using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RegITProducts.administator.Data;
using System.Drawing;
namespace RegITProducts.administator.pages
{
    public partial class ItemManager : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Iinitialization();
        }

        protected void ButtonGetCategory_Click(object sender, EventArgs e)
        {
           switch(ItemSelected)
            {
                case 0:
                    {
                        try
                        {

                            using (IController<Category> sql = new Controller<Category>())
                            {
                                LabelInfo.Text = "";
                                foreach (Category r in sql.GetAll())
                                {
                                    LabelInfo.Text += r.CategoryName + "<br />";
                                }

                                LabelStatus.Text = "Список категорий загружен!";
                            }
                        }

                        catch (Exception ex)
                        {
                        
                                LabelStatus.Text = "Не возможно загрузить список категорий по следующей причине: " + ex.Message;
                        }
                        break;
                    }
                case 1:
                    {
                        try
                        {
                            using (IController<Product> sql = new Controller<Product>())
                            {
                                LabelInfo.Text = "";
                                int id = 0;
                                int.TryParse(DropDownListCategory.SelectedValue, out id);
                                foreach (Product r in sql.GetAll())
                                {
                                    if (r.CategoryID == id)
                                        LabelInfo.Text += r.ProductName + "<br />";
                                }
                                LabelStatus.Text = "Список продуктов из категории " + DropDownListCategory.SelectedItem.Text + " загружен!";
                            }
                        }
                        catch (Exception ex)
                        {

                            LabelStatus.Text = "Не возможно загрузить список продуктов по следующей причине: " + ex.Message;
                        }
                        break;
                    }
        }
            
            
        }

        protected void ButtonAddCategory_Click(object sender, EventArgs e)
        {
            switch (ItemSelected)
            {
                case 0:
                    {
                        if (TextBoxAddCategory.Text.Length == 0)
                        {
                            LabelStatus.Text = "Следует ввести новую категорию!";
                            TextBoxAddCategory.BorderColor = Color.Red;
                            return;
                        }

                        TextBoxAddCategory.BorderColor = Color.LightGray;

                        if (!RegExRequester.Check(TextBoxAddCategory.Text, "^[а-яА-ЯёЁa-zA-Z0-9]+$"))
                        {
                            LabelStatus.Text = "Вы ввели запрещенные символы, название категории может состоять только из символов и цифр!";
                            return;
                        }

                        try
                        {
                            string categoryString = TextBoxAddCategory.Text;
                            using (IController<Category> sql = new Controller<Category>())
                            {
                                foreach (Category r in sql.GetAll())
                                {
                                    if (categoryString.ToLower() == r.CategoryName.ToLower())
                                    {
                                        LabelStatus.Text = "Такая категория:" + categoryString + " уже существует!";
                                        return;
                                    }
                                }

                                sql.Create(new Category { CategoryName = categoryString });


                                LabelStatus.Text = "Запись:" + categoryString + " успешно добавлена!";
                                TextBoxAddCategory.Text = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            LabelStatus.Text = "Не возможно добавить категорию по следующей причине: " + ex.Message;
                            TextBoxAddCategory.Text = "";
                        }
                        break;
                    }
                case 1:
                    {
                        if (TextBoxAddCategory.Text.Length == 0)
                        {
                            LabelStatus.Text = "Следует ввести название продукта!";
                            return;
                        }
                        try
                        {
                            using (IController<Product> sql = new Controller<Product>())
                            {
                                int id = 0;
                                int.TryParse(DropDownListCategory.SelectedValue, out id);
                                foreach (Product r in sql.GetAll())
                                {
                                    if (TextBoxAddCategory.Text.ToLower() == r.ProductName.ToLower() && id == r.CategoryID)
                                    {
                                        LabelStatus.Text = "Такой продукт " + TextBoxAddCategory.Text + " в категории " + DropDownListCategory.SelectedItem.Text + " уже существует!";
                                        return;
                                    }
                                }
                                sql.Create(new Product { ProductName = TextBoxAddCategory.Text, CategoryID = id });
                                LabelStatus.Text = "Запись:" + TextBoxAddCategory.Text + " успешно добавлена!";
                                TextBoxAddCategory.Text = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            LabelStatus.Text = "Не возможно загрузить список продуктов по следующей причине: " + ex.Message;
                        }
                        break;
                    }
            }
        }

        protected void ButtonDeleteCategory_Click(object sender, EventArgs e)
        {
            switch(ItemSelected)
            {
                case 0:
                    {
                        if (TextBoxDeleteCategory.Text.Length == 0)
                        {
                            LabelStatus.Text = "Следует ввести категорию которую нужно удалить!";
                            TextBoxDeleteCategory.BorderColor = Color.Red;
                            return;
                        }
                        TextBoxDeleteCategory.BorderColor = Color.LightGray;

                        try
                        {
                            using(IController<Category> sql = new Controller<Category>())
                            {
                                int count = 0;
                                int id = 0;
                                string categoryString = TextBoxDeleteCategory.Text;
                                foreach (Category r in sql.GetAll())
                                {
                                    if (categoryString.ToLower() == r.CategoryName.ToLower())
                                    {
                                        count = 1;
                                        id = r.Id;

                                        break;
                                    }
                                }

                                if (count == 0)
                                {
                                    LabelStatus.Text = "Введенная вами категория:" + TextBoxDeleteCategory.Text + " в базе данных не существует!";
                                    TextBoxDeleteCategory.Text = "";
                                    return;
                                }

                                sql.Delete(id);
                                LabelStatus.Text = "Категория:" + TextBoxDeleteCategory.Text + " и продукты содержащиеся в этой категории удалены!";
                                TextBoxDeleteCategory.Text = "";
                            }

                        }

                        catch (Exception ex)
                        {
                            LabelStatus.Text = "Не возможно удалить категорию по следующей причине: " + ex.Message;
                            TextBoxDeleteCategory.Text = "";
                        }
                        break;
                    }
                case 1:
                    {
                        if (TextBoxDeleteCategory.Text.Length == 0)
                        {
                            LabelStatus.Text = "Следует ввести продукт который нужно удалить из категории" + DropDownListCategory.SelectedItem.Text;
                            TextBoxDeleteCategory.BorderColor = Color.Red;
                            return;
                        }
                        TextBoxDeleteCategory.BorderColor = Color.LightGray;

                        try
                        {
                            using (IController<Product> sql = new Controller<Product>())
                            {
                                int count = 0;
                                int id = 0;
                                int.TryParse(DropDownListCategory.SelectedValue, out id);
                                foreach (Product r in sql.GetAll())
                                {
                                    if (TextBoxDeleteCategory.Text.ToLower() == r.ProductName.ToLower() && id == r.CategoryID)
                                    {
                                        count = 1;
                                        sql.Delete(r.Id);
                                        break;
                                    }
                                }
                                if (count == 0)
                                {
                                    LabelStatus.Text = "Введенный вами продукт:" + TextBoxDeleteCategory.Text + " из категории " + DropDownListCategory.SelectedItem.Text + " в базе данных не существует!";
                                    TextBoxDeleteCategory.Text = "";
                                    return;
                                }

                                LabelStatus.Text = "Продукт: " + TextBoxDeleteCategory.Text + " из категории " + DropDownListCategory.SelectedItem.Text + " удален!";
                                TextBoxDeleteCategory.Text = "";

                            }
                        }
                        catch (Exception ex)
                        {
                            LabelStatus.Text = "Не возможно удалить продукт: " + TextBoxDeleteCategory.Text + " по следующей причине: " + ex.Message;
                        }
                        break; 
                    }
        }
        }

        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            switch(ItemSelected)
            {
                case 0:
                    {
                        if (TextBoxEditInboxVal.Text.Length == 0)
                        {
                            LabelStatus.Text = "Следует ввести категорию которую нужно отредактировать!";
                            TextBoxEditInboxVal.BorderColor = Color.Red;
                            return;
                        }

                        if (TextBoxEditOutboxVal.Text.Length == 0)
                        {
                            LabelStatus.Text = "Следует ввести новую категорию!";
                            TextBoxEditOutboxVal.BorderColor = Color.Red;
                            return;
                        }
                        if (!RegExRequester.Check(TextBoxEditOutboxVal.Text, "^[а-яА-ЯёЁa-zA-Z0-9]+$"))
                        {
                            LabelStatus.Text = "Вы ввели запрещенные символы, название категории может состоять только из символов и цифр!";
                            return;
                        }

                        TextBoxEditInboxVal.BorderColor = Color.LightGray;
                        TextBoxEditOutboxVal.BorderColor = Color.LightGray;

                        try
                        {
                            using (IController<Category> sql = new Controller<Category>())
                            {
                                int count = 0;
                                foreach (Category r in sql.GetAll())
                                {
                                    if (TextBoxEditInboxVal.Text.ToLower() == r.CategoryName.ToLower())
                                    {
                                        r.CategoryName = TextBoxEditOutboxVal.Text;
                                        count = 1;
                                        sql.Update(r);
                                        break;
                                    }
                                }

                                if (count == 0)
                                {
                                    LabelStatus.Text = "Введенная вами категория:" + TextBoxEditInboxVal.Text + " в базе данных не существует!";
                                    TextBoxDeleteCategory.Text = "";
                                    return;
                                }
                                LabelStatus.Text = "Категория успешно отредактирована";
                                TextBoxEditInboxVal.Text = "";
                                TextBoxEditOutboxVal.Text = "";

                            }
                        }
                        catch (Exception ex)
                        {
                            LabelStatus.Text = "Не возможно отредактировать категорию по следующей причине: " + ex.Message;
                            TextBoxEditInboxVal.Text = "";
                            TextBoxEditOutboxVal.Text = "";
                        }
                        break;
                    }
                case 1:
                    {
                        if (TextBoxEditInboxVal.Text.Length == 0)
                        {
                            LabelStatus.Text = "Следует ввести категорию которую нужно отредактировать!";
                            TextBoxEditInboxVal.BorderColor = Color.Red;
                            return;
                        }

                        if (TextBoxEditOutboxVal.Text.Length == 0)
                        {
                            LabelStatus.Text = "Следует ввести новую категорию!";
                            TextBoxEditOutboxVal.BorderColor = Color.Red;
                            return;
                        }

                        TextBoxEditInboxVal.BorderColor = Color.LightGray;
                        TextBoxEditOutboxVal.BorderColor = Color.LightGray;

                        try
                        {
                            using (IController<Product> sql = new Controller<Product>())
                            {
                                int count = 0;
                                int id = 0;
                                int.TryParse(DropDownListCategory.SelectedValue, out id);
                                foreach (Product r in sql.GetAll())
                                {
                                    if (TextBoxEditOutboxVal.Text.ToLower() == r.ProductName.ToLower() && id == r.CategoryID)
                                    {
                                        LabelStatus.Text = "Такой продукт " + TextBoxEditOutboxVal.Text + " в категории " + DropDownListCategory.SelectedItem.Text + " уже существует!";
                                        return;
                                    }
                                }
                                foreach (Product r in sql.GetAll())
                                {
                                    if (TextBoxEditInboxVal.Text.ToLower() == r.ProductName.ToLower() && id == r.CategoryID)
                                    {
                                        count = 1;
                                        r.ProductName = TextBoxEditOutboxVal.Text;
                                        sql.Update(r);
                                        break;
                                    }
                                }
                                if (count == 0)
                                {
                                    LabelStatus.Text = "Введенный вами продукт:" + TextBoxDeleteCategory.Text + " из категории " + DropDownListCategory.SelectedItem.Text + " в базе данных не существует!";
                                    TextBoxDeleteCategory.Text = "";
                                    return;
                                }

                                LabelStatus.Text = "Продукт успешно отредактирован!";
                                TextBoxEditInboxVal.Text = "";
                                TextBoxEditOutboxVal.Text = "";

                            }
                        }
                        catch (Exception ex)
                        {
                            LabelStatus.Text = "Не возможно отредактировать категорию по следующей причине: " + ex.Message;
                            TextBoxEditInboxVal.Text = "";
                            TextBoxEditOutboxVal.Text = "";
                        }
                    }

                    break;
        }
        }

        protected void TextBoxDeleteCategory_TextChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged();
        }

        private int ItemSelected;
        private string buttonget = "Загрузить/обновить список (";

        //Метод инициализирующий базавые настройки web forms на странице
        private void Iinitialization()
        {
            int.TryParse(DropDownListMain.SelectedValue, out ItemSelected);
            ButtonGetDataList.Text = buttonget + DropDownListMain.SelectedItem + ")";
            if (ItemSelected == 0)
            {
                LabelFromCategory.Visible = false;
                LabelFromCategory.Enabled = false;

                DropDownListCategory.Visible = false;
                DropDownListCategory.Enabled = false;
            }
        }

        //Метод изменения настроек, вызывается при сменен значения в DropDownListMain события DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        private void SelectedIndexChanged()
        {
            int.TryParse(DropDownListMain.SelectedValue, out ItemSelected);
            LabelInfo.Text = "";
            LabelStatus.Text = "";
            ButtonGetDataList.Text = buttonget + DropDownListMain.SelectedItem + ")";

            switch (ItemSelected)
            {
                case 0:
                    LabelFromCategory.Visible = false;
                    LabelFromCategory.Enabled = false;

                    DropDownListCategory.Visible = false;
                    DropDownListCategory.Enabled = false;
                    break;
                case 1:
                    LabelFromCategory.Visible = true;
                    LabelFromCategory.Enabled = true;

                    DropDownListCategory.Visible = true;
                    DropDownListCategory.Enabled = true;
                    DropDownListCategory.DataBind();
                    break;
            }
        }  
    }
}