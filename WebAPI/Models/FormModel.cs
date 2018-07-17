using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class FormModel
    {
        #region variáveis

        private int id;
        private string side;
        private string symbol;
        private int quantity;
        private double price;

        #endregion variáveis

        #region Propriedades

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Side
        {
            get
            {
                return side;
            }

            set
            {
                side = value;
            }
        }

        public string Symbol
        {
            get
            {
                return symbol;
            }

            set
            {
                symbol = value;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }

        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        #endregion Propriedades

        public FormModel(int id, string side, string symbol, int quantity, double price)
        {
            this.Id = id;
            this.Side = side;
            this.Symbol = symbol;
            this.Quantity = quantity;
            this.Price = price;
        }
    }
}