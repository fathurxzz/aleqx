using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models.Helpers
{
    public class Certificate
    {
        /// <summary>
        /// Мфо
        /// </summary>
        public string Podrid { get; private set; }

        /// <summary>
        /// Табельний номер
        /// </summary>
        public string TabNum { get; private set; }

        /// <summary>
        /// Користувач
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; private set; }


        public Certificate(HttpRequest request)
        {
            if (request.ClientCertificate == null)
                throw new Exception("Невозможно получить информацию из клиентского сертификата");
            string certContent = request.ClientCertificate["SUBJECT0.9.2342.19200300.100.1.1"];
            string[] x = certContent.Split(new[] { '-' }, StringSplitOptions.None);
            if (x.Length < 3)
            {
                throw new Exception("Невозможно получить информацию из клиентского сертификата: (поле 0.9.2342.19200300.100.1.1)" + certContent);
            }
            Podrid = x[0];
            TabNum = x[2];
            UserName = request.ClientCertificate["SUBJECTCN"];
            Email = request.ClientCertificate["SUBJECTE"];
        }
    }
}