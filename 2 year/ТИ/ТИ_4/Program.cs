using System;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

namespace ТИ_4
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {

            var p = new BigInteger(1125899839733759);
            var q = new BigInteger(18014398241046527);
            var r = p * q;
            var phi = (p-1)*(q-1);
            var d = new BigInteger(4398042316799);
            BigInteger x, y;
            RSA.XGCD(phi, d, out x, out y);
            x += x < 0 ? phi : 0;
            var result = (x*d)%phi == 1;
            var e = x;

            var plainBytes = Encoding.ASCII.GetBytes("TEST STRING BSUIR!!!@!@!");
            var cipherBytes = RSA.Encrypt(plainBytes, e, r);
            var newPlainBytes = RSA.Decrypt(cipherBytes, d, r);
            var newString = Encoding.ASCII.GetString(newPlainBytes);

            var digest = StudentHashFunction.Hash(Encoding.ASCII.GetBytes("BSUIR"), r);
            var eds = BigInteger.ModPow(digest, d, r);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EdsForm());
        }
    }
}
