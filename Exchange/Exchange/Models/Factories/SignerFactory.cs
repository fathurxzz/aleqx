using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Helpers;

namespace Exchange.Models.Factories
{
    public class SignerFactory
    {

        private readonly IDbConnection _conn;

        public SignerFactory(IDbConnection connection)
        {
            _conn = connection;
        }

        public Signer GetSigner(int id)
        {
            string query = "SELECT * FROM tx_signer WHERE id=?";
            using (IDataReader dr = _conn.ExecuteReader(query, id))
            {
                while (dr.Read())
                {
                    return InitSigner(new Signer(), dr);
                }
            }
            return null;
        }

        public void SaveChanges(Signer signer)
        {
            if (signer.Id == 0)
            {
                InsertItem(signer);
                return;
            }
            string query = "UPDATE tx_signer SET post=?, name=? WHERE id=?";
            _conn.ExecuteNonQuery(query, signer.Post, signer.Name, signer.Id);
        }

        public void Delete(Signer signer)
        {
            string query = "DELETE FROM tx_signer WHERE id=?";
            _conn.ExecuteNonQuery(query, signer.Id);
        }


        private void InsertItem(Signer signer)
        {
            using (IDbTransaction tran = _conn.BeginTransaction())
            {
                signer.Id = Helper.GetId(_conn, "tx_signer");
                string query = @"INSERT INTO tx_signer(id, office_id, post, name)                   
		                                values(?,?,?,?)";
                _conn.ExecuteNonQuery(query, signer.Id, signer.OfficeId, signer.Post, signer.Name);
                tran.Commit();
            }
        }



        public static Signer InitSigner(Signer s, IDataRecord dr)
        {
            s.Name = dr.GetValue<string>("name");
            s.Post = dr.GetValue<string>("post");
            s.OfficeId = dr.GetValue<int>("office_id");
            s.Id = dr.GetValue<int>("id");
            return s;
        }

        public IEnumerable<Signer> GetOfficialsByOfficeId(int officeId)
        {
            string query = "SELECT * FROM tx_signer WHERE office_id=?";
            return _conn.ReadAsList(r => InitSigner(new Signer(), r), query, officeId);
        }

    }
}