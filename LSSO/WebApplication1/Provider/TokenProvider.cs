
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication1.Models;

namespace WebApplication1.Provider
{
    public class TokenProvider
    {
        private readonly TokenProviderOptions _options;

        public TokenProvider(TokenProviderOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// 生成令牌
        /// </summary>
        /// <param name="context">http上下文</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <param name="role">角色</param>
        /// <returns></returns>
        public async Task<TokenEntity> GenerateToken(HttpContext context, string userName, string passWord, string role)
        {
            var identity = await GetIdentity(userName);

            if (identity == null) return null;

            var now = DateTime.UtcNow;
            //声明
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,ToUnixEpochDate(now).ToString(),ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Role,role),
                new Claim(ClaimTypes.Name,userName)
            };
            //Jwt安全令牌
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);
            //生成令牌字符串
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new TokenEntity
            {
                access_token = encodedJwt,
                expires_in = (int)_options.Expiration.TotalSeconds
            };
            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }

        /// <summary>
        /// 查看令牌是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private Task<ClaimsIdentity> GetIdentity(string userName)
        {
            return Task.FromResult(new ClaimsIdentity(new GenericIdentity(userName, "Token"), new Claim[]
            {
                new Claim(ClaimTypes.Name, userName),
            }));
        }
    }
}
