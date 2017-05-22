using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class TokenEntity
    {
        /// <summary>
        /// token
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 过期时差
        /// </summary>
        public int expires_in { get; set; }
    }
}
