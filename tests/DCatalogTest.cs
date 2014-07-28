using PetaTest;
namespace NText {
   [TestFixture]
   public class DCatalogTest {
      [Test()]
      public void ReadTest() {
         var cs = new DCatalog("it", "./tests/locale");

         Assert.AreEqual(cs.GetText("does not exist"), "does not exist");
         Assert.AreEqual(cs.GetText("Hello"), "Ciao");
         Assert.AreEqual(cs.GetText("apple "), "mela ");
         Assert.AreEqual(cs.GetText("cat"), "gatto");
         Assert.AreEqual(cs.GetText("null"), "");
         Assert.AreEqual(cs.GetText("accento"), "Da Viá");
         Assert.IsNull(cs.GetText(null));
         Assert.IsEmpty(cs.GetText(string.Empty));
      }

      [Test()]
      public void WriteTest() {
         var cs = new DCatalog("it", "./tests/locale");
         Assert.AreEqual(cs.GetText("cul"), "cul");
         cs.SaveFuzzyFile();
      }
   }
}


