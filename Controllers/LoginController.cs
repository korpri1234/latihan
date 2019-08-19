using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using FastDDHL.Models;

namespace FastDDHL.Controllers
{
    public class LoginController : Controller
    {
        AuthenticateModel auth;

        public LoginController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult proceed([Bind("PurePassword")] AuthenticateModel authenticate)
        {
            string hashPassword = "okSihTKVh9qpBkqeUm9VsLwEvLxlrn/PYmarzE3+Fkw=";

            auth = authenticate;
            if (!string.IsNullOrEmpty(auth.PurePassword))
            {
                byte[] salt = new byte[128 / 8];

                //var gnr = RandomNumberGenerator.Create();
                //gnr.GetBytes(salt);

                string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: auth.PurePassword,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8
                    ));

                auth.HashPassword = hash;

                if(hashPassword == auth.HashPassword)
                {
                    auth.PurePassword = "Cocok";
                }
            }

            return View("Index", auth);
        }
    }
}