using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevelopmentUtilitiesV2RESTful
{
     public class JwtTokenBuilder
     {
          public JwtToken Build()
          {
               EnsureArguments();

               var claims = new List<Claim>
       {
         new Claim(JwtRegisteredClaimNames.Sub, this.subject),
         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
       }
               .Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

               var token = new JwtSecurityToken(
                                 issuer: this.issuer,
                                 audience: this.audience,
                                 claims: claims,
                                 expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                                 signingCredentials: new SigningCredentials(
                                                this.securityKey,
                                                SecurityAlgorithms.HmacSha256));

               return new JwtToken(token);
          }
     }
}
