using SCG = System.Collections.Generic;
namespace NText {
   public class DLocalization: ILocalization {
      private readonly SCG.IDictionary<string, ICatalog> catalogs;
      public DLocalization() {
         catalogs = new SCG.Dictionary<string, ICatalog>();
      }

      public void LoadFromDirectory(string basePathAbsolute) {
         if (System.IO.Directory.Exists(basePathAbsolute)) {
            foreach (string filename in System.IO.Directory.GetFiles(basePathAbsolute, "*.lang")) {
               string locale = GetLocaleFromFileName(filename);
               var catalog = new DCatalog(locale, basePathAbsolute);
               catalogs[locale] = catalog;
            }
         }
      }

      private string GetLocaleFromFileName(string fn) {
         return System.IO.Path.GetFileNameWithoutExtension(fn);
      }

      public string GetText(string msgID, string locale) {
         return this.GetCatalog(locale).GetText(msgID);
      }

      public ICatalog GetCatalog(string languageCode) {
         if (catalogs.ContainsKey(languageCode)) {
            return catalogs[languageCode];
         } else {
            return new NullCatalog();
         }
      }

      public SCG.IEnumerable<ICatalog> Catalogs {
         get { return catalogs.Values; }
      }
   }
}
