using PetaTest;
namespace CsText {
   [TestFixture]
   public class TextTest {
      [Test()]
      public void ReadTest() {
         var cs = new Text("it", "locale");

         Assert.AreEqual(cs.GetText("cul"), "cul");
         Assert.AreEqual(cs.GetText("Hello"), "ciao");
         Assert.AreEqual(cs.GetText("apple "), "mela ");
         Assert.AreEqual(cs.GetText("apple"), "apple");
         Assert.AreEqual(cs.GetText("null"), "");
         Assert.AreEqual(cs.GetText("accento"), "Da Viá");
      }

      [Test()]
      public void WriteTest() {
         var cs = new Text("it", "locale");
         Assert.AreEqual(cs.GetText("cul"), "cul");
         cs.SaveFuzzyFile();
      }
   }
}


