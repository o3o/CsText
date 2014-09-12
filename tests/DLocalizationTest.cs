using PetaTest;
using SCG = System.Collections.Generic;
namespace NText.Test {
   [TestFixture]
   public class DLocalizationTest {
      private const string LOC = "./tests/locale";
      [Test]
      public void Valid_locale_should_return_valid_catalog() {
         var loc = new DLocalization();
         loc.LoadFromDirectory(LOC);
         var catalog = loc.GetCatalog("xx");
         Assert.IsNotNull(catalog);
         Assert.IsInstanceOf<DCatalog>(catalog);
      }

      [Test]
      public void Invalid_locale_should_return_null_catalog() {
         var loc = new DLocalization();
         loc.LoadFromDirectory(LOC);
         var catalog = loc.GetCatalog("null cat");
         Assert.IsNotNull(catalog);
         Assert.IsInstanceOf<NullCatalog>(catalog);
      }


      [Test]
      public void GetText_should_work_it() {
         var loc = new DLocalization();
         loc.LoadFromDirectory(LOC);
         Assert.AreEqual(loc.GetText("none", "it"), "none");
         Assert.AreEqual(loc.GetText("My name is {0}", "it"), "Mi chiamo {0}");
         Assert.AreEqual(loc.GetText("tette", "it"), "poppe");
         Assert.AreEqual(loc.GetText("", "it"), "");
         Assert.AreEqual(loc.GetText("&Exit", "it"), "&Esci");
         Assert.AreEqual(loc.GetText("Passo", "it"), "culo");
         Assert.AreEqual(loc.GetText("Passo 1", "it"), "Paso 1");
         Assert.AreEqual(loc.GetText("Passo 2", "it"), "Passo 2");

         Assert.IsNull(loc.GetText(null , "it"));
         Assert.IsEmpty(loc.GetText(string.Empty , "it"));
      }

   }
}
