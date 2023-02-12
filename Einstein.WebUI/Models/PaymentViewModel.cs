using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Einstein.WebUI.Models
{
    public class PaymentFormViewModel
    {
        public ValuePaymentViewModel terminalkey { get; set; }
        public ValuePaymentViewModel frame { get; set; }
        public ValuePaymentViewModel language { get; set; }
        public ValuePaymentViewModel  email { get; set;  }

        public ValuePaymentViewModel phone { get; set; }

       public ValuePaymentViewModel  amount { get; set; }
       public ValuePaymentViewModel  order { get; set; }

       public ValuePaymentViewModel description { get; set; }
    }

    public class ValuePaymentViewModel 
    {
       public string value { get; set; }
    }

    public class PaymentNotificationViewModel
    {
        //TerminalKey String Идентификатор терминала, выдается Продавцу Банком
        public string TerminalKey { get; set; }
        //OrderId String Номер заказа в системе Продавца
        public string OrderId { get; set; }
        //Success bool Успешность операции
        public bool Success { get; set; }
        //Status String Статус транзакции
        public string Status{ get; set; }
        //PaymentId String Уникальный идентификатор транзакции в системе Банка
        public string PaymentId { get; set; }
        //ErrorCode String Код ошибки, «0» - если успешно
        public string ErrorCode{ get; set; }
        //Amount Number Текущая сумма транзакции в копейках
        public long  Amount  { get; set; }
        //CardId Number Если разрешена автоматическая привязка карт Покупателей к терминалу, и
        //при вызове метода Init был передан параметр CustomerKey - в этот
        //параметр будет передан идентификатор привязанной карты
        public long CardId { get; set; }
        //Pan String Замаскированный номер карты/Замаскированный номер телефона
        public string Pan { get; set; }
        //ExpDate String Срок действия карты
        public string ExpDate { get; set; }

        public PAYMENT ToEntity(PAYMENT payment)
        {

            payment.DATE = DateTime.Now;
            payment.TERMINALID = TerminalKey;
            payment.SUCCESS = Success;
            payment.ERRORCODE = int.Parse(ErrorCode);
            payment.ORDERID = long.Parse(OrderId);
            payment.AMOUNT = Amount / 100;
            payment.CARDID = CardId;
            payment.STATUS = Status;
            payment.PAN = Pan;
            payment.PAYMENTID = PaymentId;
            payment.EXPDATE = ExpDate;

            return payment;
        }

    }

}