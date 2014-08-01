using System.IO;
using PetaTest;
namespace NText {
   [TestFixture]
   public class DCatalogTest {
      [Test()]
      public void Read_IT_test_should_work() {
         var cs = new DCatalog("it", "./tests/locale");

         Assert.AreEqual(cs.GetText("does not exist"), "does not exist");
         Assert.AreEqual(cs.GetText("Hello"), "Ciao");
         // trailing spaces with quote
         Assert.AreEqual(cs.GetText("apple "), "mela ");
         //
         // trailing spaces without quote
         Assert.AreEqual(cs.GetText("cat"), "gatto");
         Assert.AreEqual(cs.GetText("null"), "");
         // UTF-8 chars
         Assert.AreEqual(cs.GetText("accento"), "Da Viá");
         Assert.IsNull(cs.GetText(null));
         Assert.IsEmpty(cs.GetText(string.Empty));
      }

      [Test()]
      public void Read_empty_file_should_work() {
         var cs = new DCatalog("xx", "./tests/locale");
         Assert.AreEqual(cs.GetText("does not exist"), "does not exist");
         Assert.AreEqual(cs.GetText("Hello"), "Hello");
         Assert.AreEqual(cs.GetText("Hello  "), "Hello  ");
      }

      [Test()]
      public void Read_invalid_file_should_work() {
         var cs = new DCatalog("does_not_exist_language", "./tests/locale");
         Assert.AreEqual(cs.GetText("does not exist"), "does not exist");
         Assert.AreEqual(cs.GetText("Hello"), "Hello");
         Assert.AreEqual(cs.GetText("Hello  "), "Hello  ");
         Assert.IsEmpty(cs.GetText(string.Empty));
      }

      [Test()] public void Write_empty_file_should_work() {
         var cs = new DCatalog("xx", "./tests/locale");
         Assert.AreEqual(cs.GetText("does not exist"), "does not exist");
         Assert.AreEqual(cs.GetText("Hello"), "Hello");
         Assert.AreEqual(cs.GetText("Hello  "), "Hello  ");
         cs.Save();
         string[] expected = {
            "\"does not exist\"=\"\"",
            "\"Hello\"=\"\"",
            "\"Hello  \"=\"\""};
         int i = 0;
         using (StreamReader sr = new StreamReader("./tests/locale/xx.lang")) {
            string line = sr.ReadLine();
            if (!string.IsNullOrEmpty(line)){
               Assert.AreEqual(line, expected[i++]);
            }
         }
      }


      [Test()] public void WriteTest() {
         var cs = new DCatalog("it", "./tests/locale");
         Assert.AreEqual(cs.GetText("cul"), "cul");
         cs.Save();
      }
   }
}


