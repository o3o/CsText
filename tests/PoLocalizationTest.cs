using PetaTest;
using SCG = System.Collections.Generic;
namespace NText.Test {
   [TestFixture]
   public class PoLocalizationTest {
      private const string LOC = "./tests/locale";
      [Test]
      public void GetText_should_work() {
         var loc = new PoLocalization();
         loc.LoadFromDirectory(LOC);
         Assert.AreEqual(loc.GetText("none", "es"), "none");
         Assert.AreEqual(loc.GetText("cul", "es"), "cul");
         Assert.AreEqual(loc.GetText("My name is {0}", "es"), "Mi nombre es {0}");
         Assert.AreEqual(loc.GetText("tette", "es"), "mamelonas");
         Assert.AreEqual(loc.GetText("", "es"), "");
         Assert.AreEqual(loc.GetText("&Exit", "es"), "&Salida");
         Assert.AreEqual(loc.GetText("Passo", "es"), "Paso");
         Assert.AreEqual(loc.GetText("Passo 1", "es"), "Paso 1");
         Assert.AreEqual(loc.GetText("Passo 2", "es"), "Passo 2");

         Assert.IsNull(loc.GetText(null , "es"));
         Assert.IsEmpty(loc.GetText(string.Empty , "es"));
      }

      [Test]
      public void GetText_should_work_it() {
         var loc = new PoLocalization();
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
