namespace LarkSSO.Models
{
    public class TokenEntity
    {
        /// <summary>
        /// token
        /// </summary>
        public string accessToken { get; set; }
        /// <summary>
        /// 过期时差
        /// </summary>
        public int expiresIn { get; set; }
    }
}
