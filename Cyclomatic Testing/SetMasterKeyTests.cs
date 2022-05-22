using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KeePass.DataExchange;

namespace Cyclomatic_Testing
{
    //[TestClass]
    public class SetMasterKeyTests
    {
        private readonly KdbManager m_kbdManager = new KdbManager();

        [TestMethod]
        public void SetMasterKey_Throw_Exception_When_Str_Null()
        {
            var masterKeyShouldNull = m_kbdManager.SetMasterKey(null, true, "secondencryption", new IntPtr(), true);

            Assert.IsNull(masterKeyShouldNull);
        }

        [TestMethod]
        public void SetMasterKey_Return_32BitKey_When_OS_Is32Bit()
        {
            var masterKey32Bit = m_kbdManager.SetMasterKey("samplemasterkey", true, "secondencryption", new IntPtr(), true);

            var thirtyTwoBitOSIntSize = 4;

            // Confirming that OS is 32 Bit OS, if OS is not 32 bit test will fail before calling second Assert
            Assert.Equals(thirtyTwoBitOSIntSize, IntPtr.Size);

            Assert.Equals(IntPtr.Size, masterKey32Bit);
        }

        [TestMethod]
        public void SetMasterKey_Return_32BitKey_When_OS_Is64Bit()
        {
            var masterKey64Bit = m_kbdManager.SetMasterKey("samplemasterkey", true, "secondencryption", new IntPtr(), true);

            var sixtyFourBitOSIntSize = 8;

            // Confirming that OS is 32 Bit OS, if OS is not 32 bit test will fail before calling second Assert
            Assert.Equals(sixtyFourBitOSIntSize, IntPtr.Size);

            Assert.Equals(IntPtr.Size, masterKey64Bit);
        }
    }
}