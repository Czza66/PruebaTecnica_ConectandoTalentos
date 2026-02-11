using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;

namespace ConectandoTalentosSolucion.Utilidades
{
    public class ProtectorUtils
    {
        private readonly IDataProtectionProvider _dataProtection;

        public ProtectorUtils(IDataProtectionProvider dataProtection)
        {
            _dataProtection = dataProtection;
        }
        public string EncriptarInt(int id, string purpose)
        {
            var protector = _dataProtection.CreateProtector(purpose);
            return protector.Protect(id.ToString());
        }
        public int DesencriptarInt(string valorEncriptado, string purpose)
        {
            try
            {
                var protector = _dataProtection.CreateProtector(purpose);
                var desencriptado = protector.Unprotect(valorEncriptado);

                if (!int.TryParse(desencriptado, out int id))
                    throw new Exception();

                return id;
            }
            catch
            {
                throw new Exception("ID inválido o manipulado.");
            }
        }
    }
}
