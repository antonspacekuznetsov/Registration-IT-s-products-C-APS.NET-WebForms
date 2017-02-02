/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace RegITProducts.administator.pages
{
    public class TANWorker:ITANLogic
    {
        private int _length;
        private static Random random = new Random();

        public TANWorker()
        {
            int.TryParse(ConfigurationManager.AppSettings["tan"], out _length);
        }

        public void GenerateTAN()
        {
            var item = TANManager.GetObject();
            try
            {
                using (var sql = ((IServerRepository<TAN>)SQLTANRepository.GetObject()))
                {
                    int t;
                    string RandTAn = RandomString();
                    int.TryParse(item.getDropDownList2.SelectedItem.Value, out t);
                    sql.Create(new TAN { TanCode = RandTAn, ProductId = t });
                    sql.Save();
                    item.GetLabelStatus.Text = "ТаН: " + RandTAn + " сгенерирован!";
                }

            }
            catch (Exception e)
            {
                item.GetLabelStatus.Text = "Не возможно сгенерировать ТаН по следующей причине: " + e.Message;
            }
        }

        public void ShowTANList()
        {
            var item = TANManager.GetObject();
            try
            {
                using (var sql = ((IServerRepository<TAN>)SQLTANRepository.GetObject()))
                {
                    item.GetLabelListOfTANs.Text = "";
                    foreach (TAN t in sql.GetAll())
                    {
                        
                        item.GetLabelListOfTANs.Text += "TaNCode: " + t.TanCode + " ProductId: " + t.ProductId + "<br />";
                    }
                }

            }
            catch (Exception e)
            {
                item.GetLabelStatus.Text = "Не возможно загрузить список ТаН'ов по следуещей причине: " + e.Message;
            }

        }

        private string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, _length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}*/