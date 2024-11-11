using ScteThreeFiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestScteThreeFiver
{
    [TestClass]
    public class UnitTestCrc
    {
        [TestMethod]
        public void TestCrc1()
        {
            // https://crccalc.com/?crc=123456789&method=CRC-32/MPEG-2&datatype=0&outtype=0
            //byte[] paylaod = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x00, 0x00, 0x00, 0x00 };
            byte[] paylaod = [ 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 ];
            // 3890640387 << wrong
            UInt32 calculated = CrcMpeg.CalculateChecksumComcast(paylaod);
            UInt32 expected = 0x0376E6E7;
            Assert.AreEqual(expected, calculated);
        }
    }
}
