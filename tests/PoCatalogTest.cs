using PetaTest;
using SCG = System.Collections.Generic;
namespace NText {
   /**
      USO:
      $rake test TF='-run:PoTest.PoTest'
   */
   [TestFixture]
   public class PoTest {
      private const string ES_PO = "./tests/locale/es/es.po";
      [Test]
      public void GetText_should_work() {
         var loc = new PoCatalog("es_ES");
         loc.LoadFromFile(ES_PO);
         Assert.AreEqual(loc.GetText("none"), "none");
         Assert.AreEqual(loc.GetText("cul"), "cul");
         Assert.AreEqual(loc.GetText("My name is {0}"), "Mi nombre es {0}");
         Assert.AreEqual(loc.GetText("tette"), "mamelonas");
         Assert.AreEqual(loc.GetText(""), "");
         Assert.AreEqual(loc.GetText("&Exit"), "&Salida");
         Assert.AreEqual(loc.GetText("Passo"), "Paso");
         Assert.AreEqual(loc.GetText("Passo 1"), "Paso 1");
         Assert.AreEqual(loc.GetText("Passo 2"), "Passo 2");

         Assert.IsNull(loc.GetText(null));
         Assert.IsEmpty(loc.GetText(string.Empty));
      }

      [Test]
      public void Long_string_should_work() {
         var loc = new PoCatalog("es_ES");
         loc.LoadFromFile(ES_PO);
         Assert.AreEqual(loc.GetText("Very long,long,long string"), "muy longa,longa,longa estriga");
      }

      [Test]
      public void Not_initialized_catalog_should_return_untraslated_message() {
         var loc = new PoCatalog("es_ES");
         Assert.AreEqual(loc.GetText("Very long,long,long string"), "Very long,long,long string");
         Assert.AreEqual(loc.GetText("none"), "none");
         Assert.AreEqual(loc.GetText("cul"), "cul");
         Assert.AreEqual(loc.GetText("My name is {0}"), "My name is {0}");
         Assert.IsEmpty(loc.GetText(""));
         Assert.IsNull(loc.GetText(null));
      }

      [Test]
      public void plural() {
         var loc = new PoCatalog("es_ES");
         loc.LoadFromFile(ES_PO);
         System.Console.WriteLine(loc.GetText("found %d fatal error"));
         System.Console.WriteLine(loc.GetText("found 0 fatal error"));

         System.Console.WriteLine(loc.GetText("found %d fatal errors"));
         System.Console.WriteLine(loc.GetText("found 1 fatal errors"));
      }
   }
}
