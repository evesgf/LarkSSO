using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class TokenProviderOptions
    {
        /// <summary>
        /// 发行方
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 订阅者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 过期间隔
        /// </summary>
        public TimeSpan Expiration { get; set; }=TimeSpan.FromSeconds(30);

        /// <summary>
        /// 签名证书
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
