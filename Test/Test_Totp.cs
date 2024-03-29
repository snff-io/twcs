using Xunit;
using library.worldcomputer.info;
using OtpNet;

namespace test.worldcomputer.info
{
    public class Test_Totp
    {
        [Fact]
        public void Generates_Useable_Key()
        {
            var sk = KeyGeneration.GenerateRandomKey(OtpHashMode.Sha1);

            var se = System.Text.Encoding.UTF8.GetString(sk);

            var be = System.Text.Encoding.UTF8.GetBytes(se);


            var totp = new Totp(be);
            var computed = totp.ComputeTotp();
            long timeStepMatched = 0; 
            var valid = totp.VerifyTotp(computed, out timeStepMatched);

            Assert.True(valid);
        }
    }
}