using Microsoft.IdentityModel.Tokens;
using SoftExpressTask.Server.Database.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SoftExpressTask.Server.Utils
{
    public class AppHasher
    {


        private static SHA256 SHA256 = SHA256.Create();

        private static byte[] bytes => SHA256.ComputeHash(Encoding.UTF8.GetBytes("very-hard-to-find-secret"));

        private static SymmetricSecurityKey key => new SymmetricSecurityKey(bytes);

        private static SigningCredentials credentials => new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


        public static string hashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


        public static bool verifyPassword(string password, User user)
        {

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }


        public static Guid decryptToken(string token)
        {
            // Create a JwtSecurityTokenHandler instance to read the token
            var tokenHandler = new JwtSecurityTokenHandler();

            // Define validation parameters for the token
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // You can change this based on your needs
                ValidateAudience = false, // You can change this based on your needs
                ValidateLifetime = true,
                IssuerSigningKey = key // Use the same key you used to sign the token
            };

            try
            {
                // Validate and decode the token, if valid
                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                // Ensure that the validatedToken is actually a JwtSecurityToken
                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    // Extract the user ID (Guid) from the 'sub' claim directly from the JWT token
                    var userIdClaim = jwtToken.Subject; // The 'sub' claim is typically stored in the Subject property

                    if (!string.IsNullOrEmpty(userIdClaim))
                    {
                        return Guid.Parse(userIdClaim); // Return the Guid of the user
                    }
                    else
                    {
                        throw new Exception("User ID claim not found.");
                    }
                }
                else
                {
                    throw new Exception("Invalid token format.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions such as token validation failure
                throw new Exception("Invalid token or decryption failed.", ex);
            }
        }


        public static string GenerateJwtToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "c#-react",
                audience: "users",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




    }
}
