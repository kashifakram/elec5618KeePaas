using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KeePass.DataExchange;
using KeePass.DataExchange.Formats;
using KeePass.UI;
using KeePassLib;
using KeePassLib.Utility;
using System.Windows.Forms;
using System.Drawing;

namespace BottomUp_Testing
{
    /// <summary>
    /// This test class tests all the constructors, private fields and private and public methods
    /// </summary>
    [TestClass]
    public class CsvStreamReaderIntegrationTests
    {
        // Local class instance initialized as null
        private CsvStreamReader m_csvStreamReader = null!;

        // Local DataVaultCsv47 class instance initialized as null  to test integration testing as Import method of this classes uses CsvStreamReader class's ReadLine method
        private DataVaultCsv47 m_DataVaultCsv47 = null!;

        // Local PwsPlusCsv1007 class instance initialized as null to test integration testing as Import method of this classes uses CsvStreamReader class's ReadLine method
        private PwsPlusCsv1007 m_PwsPlusCsv1007 = null!;

        // test data with quotes to be tested with class methods
        private readonly string m_testDataWithoutQuotes = "Test data WITHOUT quotes.";

        // test data with quotes to be tested with class methods
        private readonly string m_testDataWithQuotes = "Test data WITH " + "\"" + " quotes." + "\"";

        /// <summary>
        /// This test verifies default constructor which does not allow quotes by default
        /// </summary>
        [TestMethod]
        public void CSVReader_Try_CreateReader_With_Quoted_Defaults()
        {
            // creating class instance with passing data with quotes to default constructor
            // the constructor will assign passed data to class' private m_sChars character stream private variable
            // the constructor will assign m_bAllowUnquoted to false as default value
            m_csvStreamReader = new CsvStreamReader(m_testDataWithoutQuotes);

            // calling ReadLine on class instance which will read the default quote value in class and run the ReadLineUnquoted method   
            var strArray = m_csvStreamReader.ReadLine();
            
            // verifying if the returned value contains quotes
            var hasQuotes = strArray.Contains("\"");

            //combining returned value and returned quotes result which must be false as we are testing with unquoted string
            var result = strArray.Any() && !hasQuotes;

            // asserting the created reader is not null
            Assert.IsNotNull(m_csvStreamReader);
            
            // asserting the result should not be true as QUOTES are not allowed in default constructor which we used to create class instance
            Assert.IsTrue(result);
        }
        
        /// <summary>
        /// This test verifies explicit constructor which ALLOWS quotes by passing boolean value TRUE to constructor
        /// </summary>
        [TestMethod]
        public void CSVReader_Try_CreateReader_With_Unquoted_Explicitly()
        {
            // creating class instance with passing data explicit constructor
            // the constructor will assign passed data to class' private m_sChars character stream private variable and boolean value to m_bAllowUnquoted
            // m_testDataWithoutQuotes contains string without quotes so result must NOT contains the QUOTES

            m_csvStreamReader = new CsvStreamReader(m_testDataWithoutQuotes, true);

            // calling ReadLine on class instance which will read the passed quote value in class and run the ReadLineUnQuoted method   
            var strArray = m_csvStreamReader.ReadLine();

            // verifying if the returned value contains quotes
            var hasQuotes = strArray.Contains("\"");

            //combining returned value and returned quotes result, hasQuotes must be false here which is being tested by comparing inverse of hasQuotes
            var result = strArray.Any() && !hasQuotes;

            // asserting the created reader is not null
            Assert.IsNotNull(m_csvStreamReader);

            // asserting the result as QUOTES are not allowed in explicit constructor where we passed the allowUnquoted true which we used to create class instance
            Assert.IsTrue(result);
        }

        /// <summary>
        /// This test verifies explicit constructor which DOES NOT ALLOW quotes by passing boolean value FALSE to constructor AND uses ReadLineQuoted method
        /// </summary>
        [TestMethod]
        public void CSVReader_Try_CreateReader_With_Quoted_Explicitly()
        {
            // creating class instance with passing data explicit constructor
            // the constructor will assign passed data to class' private m_sChars character stream private variable and boolean value to m_bAllowUnquoted
            // m_testDataWithQuotes contains string WITH quotes so result MUST contains the QUOTES
            m_csvStreamReader = new CsvStreamReader(m_testDataWithQuotes, false);

            // calling ReadLine on class instance which will read the passed quote value in class and run the ReadLineUnQuoted method   
            var strArray = m_csvStreamReader.ReadLineQuoted();

            // verifying if the returned value contains quotes
            var hasQuotes = !strArray.Contains("\"");

            //combining returned value and returned quotes result
            var result = strArray.Any() && hasQuotes;

            // asserting the created reader is not null
            Assert.IsNotNull(m_csvStreamReader);

            // asserting the result should be true as QUOTES are allowed in explicit constructor where we passed the AllowUnquoted false which we used to create class instance
            Assert.IsTrue(result);
        }

        /// <summary>
        /// This test verifies explicit constructor which DOES NOT ALLOW quotes by passing boolean value FALSE to constructor AND uses ReadLineUnquoted method
        /// </summary>
        [TestMethod]
        public void CSVReader_Try_CreateReader_With_UnQuoted_Explicitly()
        {
            // creating class instance with passing data explicit constructor
            // the constructor will assign passed data to class' private m_sChars character stream private variable and boolean value to m_bAllowUnquoted
            // m_testDataWithQuotes contains string WITH quotes so result MUST NOT contains the QUOTES
            m_csvStreamReader = new CsvStreamReader(m_testDataWithQuotes, true);

            // calling ReadLine on class instance which will read the passed quote value in class and run the ReadLineUnQuoted method   
            var strArray = m_csvStreamReader.ReadLineUnquoted();

            // verifying if the returned value contains quotes
            var hasQuotes = strArray.Contains("\"");

            //combining returned value and returned quotes result
            var result = strArray.Any() && hasQuotes;

            // asserting the created reader is not null
            Assert.IsNotNull(m_csvStreamReader);

            // asserting the result should be FALSE as QUOTES are NOT allowed in explicit constructor where we passed the AllowUnquoted TRUE which we used to create class instance
            Assert.IsFalse(result);
        }

        //DataVaultCsv47
        /// <summary>
        /// This test verifies explicit constructor which DOES NOT ALLOW quotes by passing boolean value FALSE to constructor AND uses ReadLineUnquoted method
        /// </summary>
        [TestMethod]
        public void DataVaultCsv47_Try_Import_Which_Uses_CsvStreamReader()
        {
            // creating class instance with passing data explicit constructor
            // the constructor will assign passed data to class' private m_sChars character stream private variable and boolean value to m_bAllowUnquoted
            // m_testDataWithQuotes contains string WITH quotes so result MUST NOT contains the QUOTES
            m_DataVaultCsv47 = new DataVaultCsv47();

            Assert.IsNotNull(m_DataVaultCsv47);

            Assert.IsNull(Assert.ThrowsException<FileNotFoundException>(new Action(() => m_DataVaultCsv47.Import(new PwDatabase(), null, new StatusBarLogger()))));
            
        }

        //DataVaultCsv47
        /// <summary>
        /// This test verifies explicit constructor which DOES NOT ALLOW quotes by passing boolean value FALSE to constructor AND uses ReadLineUnquoted method
        /// </summary>
        [TestMethod]
        public void PwsPlusCsv1007_Try_Import_Which_Uses_CsvStreamReader()
        {
            // creating class instance with passing data explicit constructor
            // the constructor will assign passed data to class' private m_sChars character stream private variable and boolean value to m_bAllowUnquoted
            // m_testDataWithQuotes contains string WITH quotes so result MUST NOT contains the QUOTES
            m_PwsPlusCsv1007 = new PwsPlusCsv1007();

            Assert.IsNotNull(m_PwsPlusCsv1007);

            Assert.IsNull(Assert.ThrowsException<FileNotFoundException>(new Action(() => m_PwsPlusCsv1007.Import(new PwDatabase(), null, new StatusBarLogger()))));
        }
    }
}