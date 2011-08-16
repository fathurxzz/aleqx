using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Exchange.Models.Factories;
using Exchange.Models.Helpers;

namespace TendersTransfer
{
    class Program
    {
        private static int GetOfficeIdByPodrid(IDbConnection conn, string podrid)
        {
            string query = "select office_id from tx_office where podrid=? and office_level='1' and (date_close is null or date_close>getdate())";
            return conn.ExecuteScalar<int>(query, podrid);
        }

        static void Main(string[] args)
        {
            using (var conn = DbConnector.GetCustomConnection("morgan"))
            {

                /*
                var offices = TendersTransfer.Office.GetOffices(conn).ToList();

                using (var conn1 = DbConnector.GetCustomConnection("ProjectPrim"))
                {
                    foreach (var office in offices)
                    {
                        Office.Update(office, conn1);
                    }
                }
                */

                
                var users = TendersTransfer.User.GetUsers(conn).ToList();


                User.UpdateOffice(conn, users);
                using (var conn1 = DbConnector.GetCustomConnection("ProjectPrim"))
                {
                    foreach (var user in users.Where(u => u.Id == 1196))
                    {
                        try
                        {
                            User.CreateUser(conn1, user.Name, user.TabNum, user.UserCode, user.OfficeId,user.Id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                








                //var tenders = Tender.GetTenders(conn, DateTime.Now.Date, DateTime.Now.Date);
                //Console.WriteLine(tenders.Count());


                /*
                using (var conn1 = DbConnector.GetCustomConnection("ProjectPrim"))
                {
                    var tenderFactory = new TenderFactory(conn1);

                    foreach (Exchange.Models.Tender tender in tenders)
                    {
                        tender.Rest = tender.DocSum;
                        tender.OfficeId = tender.ParentOfficeId = GetOfficeIdByPodrid(conn, tender.Podrid);
                        try
                        {
                            tenderFactory.Save(tender);
                            Console.WriteLine("saved id="+tender.Id);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("error id=" + tender.Id);
                            Console.WriteLine(ex.Message);
                        }
                    }
                }*/


            }
        }
    }
}
