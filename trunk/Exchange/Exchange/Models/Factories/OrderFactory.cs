using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Exchange.Models.Enum;
using Exchange.Models.Helpers;

namespace Exchange.Models.Factories
{
    public class OrderFactory
    {
        private readonly IDbConnection _conn;
        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
        }

        public OrderFactory(IDbConnection connection)
        {
            _conn = connection;
        }

        public int GetMaxOrderNumber(ExchangeMarket exchangeMarket, DateTime date)
        {
            string query = "select isnull(max(number),'101') as number from tx_order where datediff(day,create_date,?)=0 and ex_code=?";
            string number = _conn.ExecuteScalar<string>(query, date, (int) exchangeMarket);
            return Convert.ToInt32(number.Substring(1, 2));
        }

        public Order GetOrder(DateTime date, string number, int curId, int operSign, int operId, bool loadDeals = true)
        {
            string query = "exec tx_getOrder ?,?,?,?,?";
            var order = _conn.ReadAsList(Order.InitOrder, query, date, number, curId, operSign, operId).FirstOrDefault();
            if (order != null && loadDeals)
            {
                LoadDeals(order);
            }
            return order;
        }

        public Order GetOrder(int id, bool loadDeals = true)
        {
            string query = "exec tx_getOrderById ?";
            var order = _conn.ReadAsList(Order.InitOrder, query, id).FirstOrDefault();

            if (order != null && loadDeals)
            {
                LoadDeals(order);
            }
            return order;
        }

        public bool Delete(Order order)
        {
            try
            {
                string query = "delete from tx_order where id=?";
                _conn.ExecuteNonQuery(query, order.Id);
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = string.Format("{0}: {1}", Helper.GetResourceString("AnErrorWhileDeletingOrder"), ex.Message);
            }
            return false;
        }

        private void LoadDeals(Order order)
        {
            string query = "exec tx_getDealsByOrderId ?";
            var deals = _conn.ReadAsList(r=>new Deal(r), query, order.Id);

            foreach (var deal in deals)
            {
                order.Deals.Add(deal);
            }
        }

        public IEnumerable<Order> GetOrders(DateTime date, bool loadDeals = false)
        {
            string query = "exec tx_getOrders ?";
            var orderList = _conn.ReadAsList(Order.InitOrder, query, date);

            if (loadDeals)
            {
                foreach (var order in orderList)
                {
                    LoadDeals(order);
                }
            }

            return orderList;
        }



        public bool Save(IEnumerable<Order> orders)
        {
            //foreach (var order in orders)
            //{
            //    if (!Save(order, updateDeals))
            //        return false;
            //}
            //return true;

            return orders.All(Save);
        }


        public bool Save(Order order)
        {

            bool updateDeals = order.Deals.Count > 0;


            if (updateDeals)
            {
                int curId;
                EOperation oper;
                EOperSign operSign;
                decimal course;

                if (!order.ValidateOrder(out curId, out oper, out operSign, out course, out _errorMessage))
                    return false;
            
                foreach (var deal in order.Deals)
                {
                    deal.Equivalent = Helper.Round(deal.DocSum*deal.Course, 2);
                    if (deal.DocSum == 0)
                        deal.Deleted = true;
                }
                order.DocSum = order.Deals.Where(d => !d.Deleted).Sum(d => d.DocSum);
                order.Course = course;
                order.Equivalent = order.Deals.Where(d => !d.Deleted).Sum(d => d.Equivalent);
            }


            using (var tran = _conn.BeginTransaction())
            {

                if (order.Id == 0)
                {
                    if (!Insert(order))
                    {
                        tran.Rollback();
                        return false;
                    }
                }
                else
                {
                    if (!Update(order))
                    {
                        tran.Rollback();
                        return false;
                    }
                }

                if (updateDeals)
                {
                    try
                    {
                        foreach (var deal in order.Deals)
                        {
                            deal.OrderId = order.Id;
                            SaveDeal(deal);
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        _errorMessage = ex.Message;
                        return false;
                    }
                    order.Deals.RemoveAll(d => d.Deleted);
                    if (order.Deals.Count == 0)
                    {
                        Delete(order);
                    }
                }
                tran.Commit();
            }
            return true;
        }


        private void SaveDeal(Deal deal)
        {
            if (deal.Id == 0)
            {
                InsertDeal(deal);
            }
            else
            {
                if (deal.Deleted)
                    DeleteDeal(deal);
                else
                    UpdateDeal(deal);
            }
        }

        private void InsertDeal(Deal d)
        {
            int id = Helper.GetId(_conn, "tx_deal");
            string query = "insert into tx_deal(id,tender_id,order_id,status,doc_sum,create_date,course,equiv,ex_code,user_id)values(?,?,?,?,?,?,?,?,?,?)";
            _conn.ExecuteNonQuery(query, id, d.TenderId, d.OrderId, (int)d.Status, d.DocSum, d.CreateDate, d.Course, d.Equivalent, (int)d.ExCode, d.UserId);
            query = "update tx_tender set rest=rest-? where id=?";
            _conn.ExecuteNonQuery(query, d.DocSum, d.TenderId);

        }

        private void DeleteDeal(Deal deal)
        {
            decimal docSum = deal.DocSum;
            string query = "delete from tx_deal where id=?";
            _conn.ExecuteNonQuery(query, deal.Id);
            query = "update tx_tender set rest=rest+? where id=?";
            _conn.ExecuteNonQuery(query, docSum, deal.TenderId);
        }

        private void UpdateDeal(Deal deal)
        {
            string query =
                "update tx_deal set order_id=?, status=?, doc_sum=?, create_date=?, course=?, course_client=?, equiv=?, ex_code=?, user_id=? where id=?";
            _conn.ExecuteNonQuery(query, deal.OrderId, (int)deal.Status, deal.DocSum, DateTime.Now, deal.Course, deal.CourseClient,
                                  deal.Equivalent, (int)deal.ExCode, deal.UserId, deal.Id);
            query = "update tx_tender set rest=rest-? where id=?";
            _conn.ExecuteNonQuery(query, deal.DeltaSum, deal.TenderId);
        }

        private bool Insert(Order o)
        {
            try
            {
                o.Id = Helper.GetId(_conn, "tx_order");
                string query = "Insert into tx_order(id,status,cur_id,oper_id,doc_sum,course,equiv,oper_sign,number,ex_code,create_date,deal_with_nbu)values(?,?,?,?,?,?,?,?,?,?,?,?)";
                _conn.ExecuteNonQuery(
                    query,
                    o.Id,
                    (int)o.Status,
                    o.CurId,
                    (int)o.Operation,
                    o.DocSum,
                    o.Course,
                    o.Equivalent,
                    (int)o.OperSign,
                    o.Number,
                    (int)o.ExchangeMarket,
                    DateTime.Now,
                    false);
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = string.Format("{0}: {1}", Helper.GetResourceString("AnErrorWhileInsertingOrder"), ex.Message);

            }
            return false;
        }

        private bool Update(Order o)
        {
            try
            {
                string query = "update tx_order set status=?,cur_id=?,oper_id=?,doc_sum=?,course=?,equiv=?,oper_sign=?,number=?,ex_code=?,create_date=?,deal_with_nbu=?,send_date=?,info=?,info_for_branch=?,info_for_pay=? where id=?";
                _conn.ExecuteNonQuery(
                    query,
                    (int) o.Status,
                    o.CurId,
                    (int) o.Operation,
                    o.DocSum,
                    o.Course,
                    o.Equivalent,
                    (int) o.OperSign,
                    o.Number,
                    (int) o.ExchangeMarket,
                    DateTime.Now,
                    o.DealWithNBU,
                    o.SendDate.With(DbType.DateTime),
                    o.Info,
                    o.InfoForBranch,
                    o.InfoForPay,
                    o.Id);

                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = string.Format("{0}: {1}", Helper.GetResourceString("AnErrorWhileUpdatingOrder"), ex.Message);
            }
            return false;
        }
    }
}