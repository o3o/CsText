namespace NText {
   public class NullCatalog: ICatalog {
      public string GetText(string s) {
         return s;
      }

      public string Locale {
         get { return "null"; }
      }

      public void Save() {}
   }
}
