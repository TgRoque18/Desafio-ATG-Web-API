using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace WebAPI.Models
{
    //[Serializable, XmlRoot("FormModel"), DataContract(Name = "FormModel")]
    //[DataContract]
    //[Serializable]

    [DataContract]
    public class FormModelResult
    {
        #region variáveis

        private int id;
        private string side;
        private string symbol;
        private int quantity;
        private double price;
        private string status;
        private string msgs;

        #endregion variáveis

        #region Propriedades

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
        public string Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        [DataMember]
        public string Msgs
        {
            get
            {
                return msgs;
            }

            set
            {
                msgs = value;
            }
        }

        #endregion Propriedades

    }
}