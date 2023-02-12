using System;
using System.Collections.Generic;
using System.Text;

namespace Einstein.Domain.Entities
{
    public class PAYMENT
    {
        public long ID { get; set; }
        public DateTime DATE { get; set; }
        public string TERMINALID { get; set; }
        
        //OrderId String Номер заказа в системе Продавца
        public long ORDERID { get; set; }
        //Success bool Успешность операции
        public bool SUCCESS { get; set; }
        //Status String Статус транзакции
        public string STATUS { get; set; }
        //PaymentId String Уникальный идентификатор транзакции в системе Банка
        public string PAYMENTID { get; set; }
        //ErrorCode String Код ошибки, «0» - если успешно
        public int ERRORCODE { get; set; }
        //Amount Number Текущая сумма транзакции в копейках
        public decimal AMOUNT { get; set; }
        //CardId Number Если разрешена автоматическая привязка карт Покупателей к терминалу, и
        //при вызове метода Init был передан параметр CustomerKey - в этот
        //параметр будет передан идентификатор привязанной карты
        public long CARDID { get; set; }
        //Pan String Замаскированный номер карты/Замаскированный номер телефона
        public string PAN { get; set; }
        //ExpDate String Срок действия карты
        public string EXPDATE { get; set; }
    }
}
